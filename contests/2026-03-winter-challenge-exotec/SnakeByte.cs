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
        grid = new Grid();
        bots = new Bots(grid);
    }

    public void Init() {
        // ReSharper disable once UnusedVariable
        int myId = int.Parse(Console.ReadLine()!);
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

class Grid {
    int width;
    int height;

    public static Point[] FourDirections { get; } = [
        Point.RIGHT, Point.LEFT, Point.UP, Point.DOWN
    ];

    Cell[] cells = null!;
    public Cell Get(int x, int y) => cells[width * y + x];
    void Set(int x, int y, Cell cell) => cells[width * y + x] = cell;
    public Cell Get(Point point) => Get(point.x, point.y);

    public void Init() {
        width = int.Parse(Console.ReadLine()!);
        height = int.Parse(Console.ReadLine()!);
        cells = new Cell[width * height];
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

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public string GetActions() {
        var powerSources = grid.AllPower();
        var possibleTargets = new List<(int id, Point target, int distance)>();
        foreach ((int id, var snake) in snakes) {
            if (snake is not { isMine: true, IsDead: false }) {
                continue;
            }
            foreach (var power in powerSources) {
                int distance = pathfinding.Path(snake.Head, power)?.Count ?? int.MaxValue;
                possibleTargets.Add((id, power, distance));
            }
        }
        possibleTargets.Sort((a, b) => a.distance.CompareTo(b.distance));

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
            } catch (Exception e) {
                Console.Error.WriteLine($"{id} {e.Message}");
            }
        }
        return result.ToString();
    }

    string MoveToTarget(Point source, Point target, bool mark = false) {
        var path = pathfinding.Path(source, target);
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
    readonly Dictionary<Point, Node> nodes = new();
    readonly List<Point> path = new();
    readonly PriorityQueue<Node, int> openQueue = new();

    /// A*
    public List<Point>? Path(Point start, Point end) {
        nodes.Clear();
        openQueue.Clear();
        var startNode = new Node(start) {
            gCost = 0,
            hCost = Point.ManhattanDistance(start, end)
        };
        nodes.Add(start, startNode);
        openQueue.Enqueue(startNode, startNode.Priority);
        while (openQueue.Count > 0) {
            var current = openQueue.Dequeue();
            // ignore stale nodes
            if (current.isClosed) {
                continue;
            }
            current.isOpen = false;
            current.isClosed = true;
            if (current.point == end) {
                return Reconstruct(startNode, current);
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
                if (!nodes.TryGetValue(cell.point, out var neighbour)) {
                    neighbour = new Node(cell.point);
                    nodes.Add(cell.point, neighbour);
                }
                if (neighbour.isClosed) {
                    continue;
                }
                // we don't move diagonally
                int gCost = current.gCost + 1;
                if (gCost >= neighbour.gCost && neighbour.isOpen) {
                    continue;
                }
                neighbour.gCost = gCost;
                neighbour.hCost = Point.ManhattanDistance(neighbourPoint, end);
                neighbour.parent = current;
                openQueue.Enqueue(neighbour, neighbour.Priority);
                neighbour.isOpen = true;
            }
        }
        return null;
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

class Node(Point point) {
    public Point point = point;

    /// Distance from start
    public int gCost = int.MaxValue;

    /// Distance to end
    public int hCost = int.MaxValue;

    public Node? parent;
    public bool isOpen;
    public bool isClosed;

    int FCost => gCost + hCost;

    /// Order by FCost, then hCost
    public int Priority => FCost * 1000 + hCost;
}
