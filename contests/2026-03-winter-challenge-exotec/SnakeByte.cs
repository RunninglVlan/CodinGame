using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

/// https://www.codingame.com/contests/winter-challenge-2026-exotec
static class SnakeByte {
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
        }
        // ReSharper disable once FunctionNeverReturns
    }
}

class Game {
    readonly Grid grid;
    readonly Bots bots;

    public Game() {
        // ReSharper disable once UnusedVariable
        int myId = int.Parse(Console.ReadLine()!);
        int width = int.Parse(Console.ReadLine()!);
        int height = int.Parse(Console.ReadLine()!);
        grid = new Grid(width, height);
        bots = new Bots(grid);
    }

    public void Init() {
        grid.Init();
        bots.Init();
    }

    public void Loop() {
        grid.ClearOldData();
        bots.ClearOldData();
        grid.ReadNewData();
        bots.ReadNewData();
        Console.WriteLine(bots.GetActions());
    }
}

class Grid(int width, int height) {
    readonly Cell[] cells = new Cell[width * height];

    public static Point[] FourDirections { get; } = [
        Point.RIGHT, Point.LEFT, Point.UP, Point.DOWN
    ];

    public int Width => width;
    public int Height => height;
    public Cell Get(int x, int y) => cells[width * y + x];
    void Set(int x, int y, Cell cell) => cells[width * y + x] = cell;
    public Cell Get(Point point) => Get(point.x, point.y);

    public void Init() {
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < row.Length; x++) {
                char c = row[x];
                var cell = new Cell(x, y);
                Set(x, y, cell);
                cell.type = c == '#' ? CellType.Platform : CellType.Empty;
            }
        }
    }

    /// Clears power sources and snakes
    public void ClearOldData() {
        foreach (var cell in cells) {
            if (cell.type is CellType.Platform or CellType.Empty) {
                continue;
            }
            cell.type = CellType.Empty;
        }
    }

    /// Updates power sources
    public void ReadNewData() {
        int powerSourceCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < powerSourceCount; i++) {
            string[] position = Console.ReadLine()!.Split(' ');
            int x = int.Parse(position[0]);
            int y = int.Parse(position[1]);
            Get(x, y).type = CellType.Power;
        }
    }

    public List<Point> AllPower() {
        return cells.Where(it => it.type == CellType.Power).Select(it => it.point).ToList();
    }

    public bool IsOutside(int x, int y) {
        return x < 0 || x >= width || y < 0 || y >= height;
    }

    public bool IsOutside(Point point) => IsOutside(point.x, point.y);
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
}

enum CellType {
    Platform,
    Empty,
    Power,
    MySnake,
    EnemySnake,
    MySnakeTail,
    EnemySnakeTail
}

class Bots(Grid grid) {
    readonly Pathfinding pathfinding = new(grid);
    readonly Dictionary<int, Snake> snakes = new();

    public void Init() {
        pathfinding.Init();
        int snakesPerPlayer = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < snakesPerPlayer; i++) {
            int mySnakeId = int.Parse(Console.ReadLine()!);
            snakes.Add(mySnakeId, new Snake(true));
        }
        for (var i = 0; i < snakesPerPlayer; i++) {
            int enemySnakeId = int.Parse(Console.ReadLine()!);
            snakes.Add(enemySnakeId, new Snake(false));
        }
    }

    /// Clears snake parts
    public void ClearOldData() {
        foreach (var snake in snakes.Values) {
            snake.parts.Clear();
        }
    }

    /// Updates snake parts, and grid too
    public void ReadNewData() {
        int snakeCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < snakeCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int snakeId = int.Parse(inputs[0]);
            var snake = snakes[snakeId];
            string body = inputs[1];
            string[] parts = body.Split(':');
            for (var index = 0; index < parts.Length; index++) {
                string part = parts[index];
                string[] position = part.Split(',');
                int x = int.Parse(position[0]);
                int y = int.Parse(position[1]);
                snake.parts.Add(new Point(x, y));
                if (grid.IsOutside(x, y)) {
                    Console.Error.WriteLine($"Snake {snakeId} outside grid: {x}, {y}");
                    continue;
                }
                var cell = grid.Get(x, y);
                bool isTail = index == parts.Length - 1;
                if (snake.isMine) {
                    cell.type = isTail ? CellType.MySnakeTail : CellType.MySnake;
                } else {
                    cell.type = isTail ? CellType.EnemySnakeTail : CellType.EnemySnake;
                }
            }
        }
    }

    public string GetActions() {
        var powerSources = grid.AllPower();
        var possibleTargets = new List<(int id, Point target, int cost)>();
        foreach ((int id, var snake) in snakes) {
            if (snake is not { isMine: true, IsDead: false }) {
                continue;
            }
            foreach (var power in powerSources) {
                (_, int cost) = pathfinding.Path(snake.Head, power);
                if (snake.ShouldIgnore(power)) {
                    cost = int.MaxValue;
                }
                possibleTargets.Add((id, power, cost));
            }
        }
        possibleTargets.Sort((a, b) => a.cost.CompareTo(b.cost));

        var snakeTargets = new Dictionary<int, Point>();
        var selectedTargets = new HashSet<Point>();
        foreach ((int id, var target, int _) in possibleTargets) {
            if (snakeTargets.ContainsKey(id) || selectedTargets.Contains(target)) {
                continue;
            }
            snakeTargets.Add(id, target);
            selectedTargets.Add(target);
        }

        return MoveSnakes(snakeTargets);
    }

    string MoveSnakes(Dictionary<int, Point> snakeTargets) {
        var result = new StringBuilder();
        foreach ((int id, var snake) in snakes) {
            if (snake is not { isMine: true, IsDead: false }) {
                continue;
            }
            try {
                if (!snakeTargets.TryGetValue(id, out var target)) {
                    string wanderingMove = MoveToEmpty(snake.Head);
                    result.Append($"{id} {wanderingMove};");
                    continue;
                }
                string move = MoveToTarget(snake.Head, target);
                result.Append($"{id} {move};");
                snake.SaveTurnData(target);
            } catch (Exception e) {
                Console.Error.WriteLine($"{id} {e.Message}");
            }
        }
        return result.ToString();
    }

    string MoveToTarget(Point source, Point target, bool mark = false) {
        var (path, _) = pathfinding.Path(source, target);
        string move = path != null ? path.First().Direction(source).ToAction : MoveToEmpty(source);
        if (mark && path != null) {
            // only 4 marks are allowed
            move += ';' + string.Join(';', path.Take(4).Select(it => $"MARK {it.x} {it.y}"));
        }
        return move;
    }

    string MoveToEmpty(Point source) {
        Point? emptyDirection = null;
        foreach (var direction in Grid.FourDirections) {
            var neighbour = source + direction;
            if (grid.IsOutside(neighbour)) {
                continue;
            }
            var cell = grid.Get(neighbour);
            switch (cell.type) {
                // prioritize tail
                case CellType.MySnakeTail or CellType.EnemySnakeTail:
                    return direction.ToAction;
                case CellType.Empty:
                    emptyDirection = direction;
                    break;
            }
        }
        return emptyDirection?.ToAction ?? throw new InvalidOperationException("No empty neighbour found");
    }
}

class Snake(bool mine) {
    public readonly bool isMine = mine;

    public bool IsDead => parts.Count == 0;
    public readonly List<Point> parts = new();
    public Point Head => parts[0];
    readonly List<Point> previousParts = new();
    Point previousTarget;
    int waitTurns;

    public void SaveTurnData(Point target) {
        if (IsSamePosition() && target == previousTarget) {
            waitTurns = 3;
        } else if (waitTurns > 0) {
            waitTurns--;
        }
        previousParts.Clear();
        previousParts.AddRange(parts);
        previousTarget = target;
    }

    public bool ShouldIgnore(Point target) => target == previousTarget && waitTurns > 0;

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    bool IsSamePosition() {
        if (parts.Count != previousParts.Count) {
            return false;
        }
        for (var i = 0; i < parts.Count; i++) {
            if (parts[i] != previousParts[i]) {
                return false;
            }
        }
        return true;
    }
}

readonly record struct Point(int x, int y) {
    public readonly int x = x;
    public readonly int y = y;

    public static readonly Point UP = new(0, 1);
    public static readonly Point RIGHT = new(1, 0);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);

    /// Direction to Action
    public string ToAction {
        get {
            if (this == UP) {
                return Action.DOWN;
            }
            if (this == RIGHT) {
                return Action.RIGHT;
            }
            return this == DOWN ? Action.UP : Action.LEFT;
        }
    }

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    public static Point operator +(Point a, Point b) {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public static Point operator -(Point a, Point b) {
        return new Point(a.x - b.x, a.y - b.y);
    }

    public Point Direction(Point from) {
        return this - from;
    }
}

static class Action {
    public const string UP = "UP";
    public const string RIGHT = "RIGHT";
    public const string DOWN = "DOWN";
    public const string LEFT = "LEFT";
    public const string WAIT = "WAIT";
}

class Pathfinding(Grid grid) {
    readonly Node[] nodes = new Node[grid.Width * grid.Height];
    readonly List<Point> path = new();
    readonly Heap<Node> open = new(grid.Width * grid.Height);
    readonly HashSet<Node> closed = new();
    int iteration;

    Node Get(int x, int y) => nodes[grid.Width * y + x];
    void Set(int x, int y, Node node) => nodes[grid.Width * y + x] = node;
    Node Get(Point point) => Get(point.x, point.y);

    public void Init() {
        for (var y = 0; y < grid.Height; y++) {
            for (var x = 0; x < grid.Width; x++) {
                var node = new Node(new(x, y));
                Set(x, y, node);
            }
        }
    }

    /// A*
    public (List<Point>?, int cost) Path(Point start, Point end) {
        iteration++;
        open.Clear();
        closed.Clear();
        var startNode = Get(start);
        startNode.Reset(iteration);
        startNode.gCost = 0;
        startNode.hCost = Point.ManhattanDistance(start, end);
        open.Add(startNode);
        while (open.Count > 0) {
            var current = open.RemoveFirst();
            closed.Add(current);
            if (current.point == end) {
                return (Reconstruct(startNode, current), current.gCost);
            }

            foreach (var direction in Grid.FourDirections) {
                var neighbourPoint = current.point + direction;
                if (grid.IsOutside(neighbourPoint)) {
                    continue;
                }
                var cell = grid.Get(neighbourPoint);
                if (cell.type != CellType.Empty && (cell.type != CellType.Power || cell.point != end)) {
                    continue;
                }
                var neighbour = Get(cell.point);
                if (neighbour.iteration != iteration) {
                    neighbour.Reset(iteration);
                }
                if (closed.Contains(neighbour)) {
                    continue;
                }
                int gCost = current.gCost + NewGCost(neighbourPoint);
                if (gCost >= neighbour.gCost && open.Contains(neighbour)) {
                    continue;
                }
                neighbour.gCost = gCost;
                neighbour.hCost = Point.ManhattanDistance(neighbourPoint, end);
                neighbour.parent = current;
                if (!open.Contains(neighbour)) {
                    open.Add(neighbour);
                } else {
                    open.UpdateItem(neighbour);
                }
            }
        }
        return (null, int.MaxValue);
    }

    int NewGCost(Point point) {
        for (var distance = 1; distance <= 3; distance++) {
            var belowPoint = point + new Point(0, distance);
            if (grid.IsOutside(belowPoint)) {
                continue;
            }
            // prefer ledges
            if (grid.Get(belowPoint).type == CellType.Platform) {
                return distance;
            }
        }
        return 10;
    }

    List<Point> Reconstruct(Node start, Node end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.point);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }
}

class Node(Point point) : IHeapItem<Node> {
    public Point point = point;

    /// Distance from start
    public int gCost;

    /// Distance to end
    public int hCost;

    int FCost => gCost + hCost;
    public int iteration;
    public Node? parent;
    int IHeapItem<Node>.HeapIndex { get; set; }

    public void Reset(int newIteration) {
        gCost = int.MaxValue;
        hCost = int.MaxValue;
        iteration = newIteration;
        parent = null;
    }

    int IComparable<Node>.CompareTo(Node? other) {
        if (other == null) {
            return 1;
        }
        int compare = FCost.CompareTo(other.FCost);
        if (compare == 0) {
            return hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}

class Heap<T>(int maxSize) where T : IHeapItem<T> {
    readonly T[] items = new T[maxSize];
    public int Count { get; private set; }

    public void Clear() => Count = 0;

    public void Add(T item) {
        item.HeapIndex = Count;
        items[Count] = item;
        SortUp(item);
        Count++;
    }

    public T RemoveFirst() {
        var firstItem = items[0];
        Count--;
        items[0] = items[Count];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    public bool Contains(T item) {
        return item.HeapIndex < Count && Equals(items[item.HeapIndex], item);
    }

    public void UpdateItem(T item) {
        SortUp(item);
    }

    void SortUp(T item) {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true) {
            var parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0) {
                Swap(item, parentItem);
            } else {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void SortDown(T item) {
        while (true) {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            if (childIndexLeft < Count) {
                int swapIndex = childIndexLeft;
                if (childIndexRight < Count) {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) {
                        swapIndex = childIndexRight;
                    }
                }
                if (item.CompareTo(items[swapIndex]) < 0) {
                    Swap(item, items[swapIndex]);
                } else {
                    break;
                }
            } else {
                break;
            }
        }
    }

    void Swap(T aItem, T bItem) {
        items[aItem.HeapIndex] = bItem;
        items[bItem.HeapIndex] = aItem;
        (aItem.HeapIndex, bItem.HeapIndex) = (bItem.HeapIndex, aItem.HeapIndex);
    }
}

interface IHeapItem<in T> : IComparable<T> {
    int HeapIndex { get; set; }
}
