using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SnakeByte;

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

public class Grid {
    int width;
    int height;

    public static Point[] FourDirections { get; } = [
        Point.RIGHT, Point.LEFT, Point.UP, Point.DOWN
    ];

    public static Point[] EighthDirections { get; } = [
        Point.RIGHT, Point.LEFT, Point.UP, Point.DOWN,
        new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)
    ];

    Cell[] cells = null!;
    readonly Dictionary<Point, Cell> outsideCells = new();

    [SuppressMessage("ReSharper", "InvertIf")]
    public Cell Get(int x, int y) {
        if (!IsOutside(x, y)) {
            return cells[width * y + x];
        }
        var point = new Point(x, y);
        if (!outsideCells.TryGetValue(point, out var cell)) {
            cell = new Cell(x, y);
            if (x >= -1 && x <= width && y >= -1 && y <= height) {
                cell.type = x >= 0 && x <= width - 1 ? CellType.Empty : CellType.EmptySide;
            } else {
                cell.type = CellType.OutOfBounds;
            }
            outsideCells[point] = cell;
        }
        return cell;
    }

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

    /// For local tests
    public void Init(string input, out Point source) {
        string[] rows = input.Split(Environment.NewLine);
        width = rows[0].Length;
        height = rows.Length;
        cells = new Cell[width * height];
        source = default;
        for (var y = 0; y < height; y++) {
            string row = rows[y];
            for (var x = 0; x < row.Length; x++) {
                char c = row[x];
                var cell = new Cell(x, y);
                Set(x, y, cell);
                cell.type = c switch {
                    '#' => CellType.Platform,
                    'T' => CellType.Power,
                    _ => CellType.Empty
                };
                if (c == 'S') {
                    source = cell.point;
                }
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
        outsideCells.Clear();
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

    bool IsOutside(int x, int y) => x < 0 || x >= width || y < 0 || y >= height;

    /// For copying challenge parameters and using them in local tests
    public override string ToString() {
        var result = new StringBuilder();
        for (var y = 0; y < height; y++) {
            var row = string.Empty;
            for (var x = 0; x < width; x++) {
                var cell = Get(x, y);
                char symbol = cell.type switch {
                    CellType.Platform => '#',
                    CellType.Power => 'T',
                    _ => '.'
                };
                row += symbol;
            }
            result.AppendLine(row);
        }
        return result.ToString();
    }
}

public class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
    public bool IsEmpty => type is CellType.Empty or CellType.EmptySide;
}

public enum CellType {
    Platform,
    Empty,
    EmptySide,
    Power,
    MySnake,
    EnemySnake,
    MySnakeHead,
    EnemySnakeHead,
    MySnakeTail,
    EnemySnakeTail,
    OutOfBounds
}

class Bots(Grid grid) {
    readonly Pathfinding pathfinding = new(grid);

    /// Performance optimization
    const int TARGETS_PER_SNAKE = 10;

    // Limit by the rules
    const int MARK_COUNT = 4;

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
                bool isHead = index == 0;
                bool isTail = index == parts.Length - 1;
                grid.Get(x, y).type = (snake.isMine, isHead, isTail) switch {
                    (true, true, _) => CellType.MySnakeHead,
                    (true, _, true) => CellType.MySnakeTail,
                    (true, _, _) => CellType.MySnake,
                    (false, true, _) => CellType.EnemySnakeHead,
                    (false, _, true) => CellType.EnemySnakeTail,
                    _ => CellType.EnemySnake
                };
            }
        }
    }

    public string GetActions() {
        var powerSources = grid.AllPower();
        var filteredTargets = snakes
            .Where(it => it.Value is { isMine: true, IsDead: false })
            .SelectMany(it => powerSources.Select(target => (id: it.Key, snake: it.Value, target)))
            .GroupBy(it => it.id)
            .SelectMany(snakeGroup => snakeGroup
                .OrderBy(it => Point.ManhattanDistance(it.snake.Head, it.target))
                .Take(TARGETS_PER_SNAKE)
            );

        var possibleTargets = filteredTargets
            .Select(it => {
                if (it.snake.IsIgnored(it.target)) {
                    return (it.id, it.target, cost: int.MaxValue);
                }
                (var path, int cost) = pathfinding.Path(it.snake.Head, it.target);
                if (path == null) {
                    return (it.id, it.target, cost);
                }
                var step = path.First();
                if (ValidateNextTurn(it.snake, step)) {
                    return (it.id, it.target, cost);
                }
                it.snake.Ignore(it.target);
                return (it.id, it.target, cost: cost + 100_000);
            })
            .OrderBy(it => it.cost)
            .ToList();

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

    bool ValidateNextTurn(Snake snake, Point newHead) {
        // getting power source is good
        if (grid.Get(newHead).type == CellType.Power) {
            return true;
        }
        // staying on solid cell is good
        if (IsOnSolidCell(newHead)) {
            return true;
        }
        for (var i = 0; i < snake.parts.Count - 1; i++) {
            var part = snake.parts[i];
            if (IsOnSolidCell(part)) {
                return true;
            }
        }
        // fall one cell down and check if the position changed
        if (snake.Head != newHead - Point.DOWN) {
            return true;
        }
        for (var i = 0; i < snake.parts.Count - 1; i++) {
            if (snake.parts[i] - Point.DOWN != snake.parts[i + 1]) {
                return true;
            }
        }
        return false;

        bool IsOnSolidCell(Point point) {
            var cellBelow = grid.Get(point - Point.DOWN);
            return cellBelow.type is CellType.Platform or CellType.Power;
        }
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
        return result.Length == 0 ? Action.WAIT : result.ToString();
    }

    string MoveToTarget(Point source, Point target, int markCount = 0) {
        var (path, _) = pathfinding.Path(source, target);
        if (path == null) {
            return MoveToEmpty(source);
        }
        var step = path.First();
        grid.Get(step).type = CellType.MySnake;
        string move = step.Direction(source).ToAction;
        if (markCount == 0) {
            return move;
        }
        int count = Math.Min(MARK_COUNT, markCount);
        move += ';' + string.Join(';', path.Take(count).Select(it => $"MARK {it.x} {it.y}"));
        return move;
    }

    string MoveToEmpty(Point source) {
        (Cell cell, Point direction)? fallback = null;
        foreach (var direction in Grid.FourDirections) {
            var neighbour = source + direction;
            var cell = grid.Get(neighbour);
            // prioritize tail
            if (cell.type is CellType.MySnakeTail or CellType.EnemySnakeTail) {
                cell.type = CellType.MySnake;
                return direction.ToAction;
            }
            if (!cell.IsEmpty) {
                continue;
            }
            if (fallback == null || (fallback.Value.cell.type == CellType.EmptySide && cell.type == CellType.Empty)) {
                fallback = (cell, direction);
            }
        }
        if (fallback == null) {
            throw new InvalidOperationException("No empty neighbour found");
        }
        fallback.Value.cell.type = CellType.MySnake;
        return fallback.Value.direction.ToAction;
    }
}

class Snake(bool mine) {
    public readonly bool isMine = mine;

    const int STUCK_TURNS = 3;

    public bool IsDead => parts.Count == 0;
    public readonly List<Point> parts = new();
    public Point Head => parts[0];
    readonly Dictionary<Point, int> ignored = new();

    public bool IsIgnored(Point target) {
        if (!ignored.TryGetValue(target, out int turns)) {
            return false;
        }
        ignored[target] = --turns;
        if (turns == 0) {
            ignored.Remove(target);
        }
        return true;
    }

    public void Ignore(Point target) => ignored.Add(target, STUCK_TURNS);
}

public readonly record struct Point(int x, int y) {
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

    public override string ToString() => $"({x}, {y})";
}

static class Action {
    public const string UP = "UP";
    public const string RIGHT = "RIGHT";
    public const string DOWN = "DOWN";
    public const string LEFT = "LEFT";
    public const string WAIT = "WAIT";
}

public class Pathfinding(Grid grid) {
    readonly Dictionary<Point, Node> nodes = new();
    readonly List<Point> path = new();
    readonly PriorityQueue<Node, int> openQueue = new();

    /// A*
    public (List<Point>?, int cost) Path(Point start, Point end) {
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
                return (Reconstruct(startNode, current), current.gCost);
            }

            foreach (var direction in Grid.FourDirections) {
                var neighbourPoint = current.point + direction;
                var cell = grid.Get(neighbourPoint);
                if (!cell.IsEmpty && (cell.type != CellType.Power || cell.point != end)) {
                    continue;
                }
                if (!nodes.TryGetValue(cell.point, out var neighbour)) {
                    neighbour = new Node(cell.point);
                    nodes.Add(cell.point, neighbour);
                }
                if (neighbour.isClosed) {
                    continue;
                }
                int gCost = current.gCost + NewGCost(cell);
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
        return (null, int.MaxValue);
    }

    int NewGCost(Cell cell) {
        if (cell.type == CellType.Power) {
            return 0;
        }
        var cost = 10;
        for (var distance = 1; distance <= 3; distance++) {
            var pointBelow = cell.point + new Point(0, distance);
            // prefer ledges
            if (grid.Get(pointBelow).type != CellType.Platform) {
                continue;
            }
            cost = distance;
            break;
        }
        if (IsOpponentNearby(cell.point)) {
            cost += 20;
        }
        if (cell.type == CellType.EmptySide) {
            cost += 20;
        }
        return cost;
    }

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    bool IsOpponentNearby(Point point) {
        foreach (var direction in Grid.EighthDirections) {
            var neighbourPoint = point + direction;
            if (grid.Get(neighbourPoint).type == CellType.EnemySnakeHead) {
                return true;
            }
        }
        return false;
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
