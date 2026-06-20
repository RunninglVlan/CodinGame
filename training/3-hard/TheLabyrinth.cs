using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLabyrinth;

/// https://www.codingame.com/training/hard/the-labyrinth
static class TheLabyrinth {
    static void Main() {
        var game = new Game();
        while (true) {
            game.Loop();
        }
        // ReSharper disable once FunctionNeverReturns
    }
}

class Game {
    readonly Grid maze;
    readonly Pathfinding pathfinding;

    /// number of rounds between the time the alarm countdown is activated and the time the alarm goes off
    readonly int alarmRounds;

    Point rick;
    bool visitedControlRoom;

    /// 10 ≤ height ≤ 100,
    /// 20 ≤ width ≤ 200,
    /// 1 ≤ alarmRounds ≤ 100
    public Game() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int height = int.Parse(inputs[0]);
        int width = int.Parse(inputs[1]);
        maze = new Grid(width, height);
        pathfinding = new(maze);
        alarmRounds = int.Parse(inputs[2]);
    }

    public void Loop() {
        string[] point = Console.ReadLine()!.Split(' ');
        rick = Point.From(point[1], point[0]);
        maze.ReadNewData();
        if (maze.Get(rick).type == CellType.Control) {
            visitedControlRoom = true;
        }
        Console.WriteLine(NextMove());
    }

    string NextMove() {
        if (maze.controlRoom == null) {
            return ExploreMaze();
        }
        var controlToStart = pathfinding.Path(maze.controlRoom.Value, maze.start);
        if (controlToStart == null || controlToStart.Count > alarmRounds) {
            return ExploreMaze();
        }
        return !visitedControlRoom ? GoToControlRoom() : ReturnToStart();
    }

    string ExploreMaze() {
        var pathToUnknown = pathfinding.Path(rick, cell => cell.type == CellType.Unknown);
        return pathToUnknown == null
            ? throw new ArgumentException("Can't find path to unknown cell")
            : Action.From(pathToUnknown.First().Direction(rick));
    }

    string GoToControlRoom() {
        if (maze.controlRoom == null) {
            throw new ArgumentException("Explored maze, but couldn't find control room");
        }
        var path = pathfinding.Path(rick, maze.controlRoom.Value);
        return path == null
            ? throw new ArgumentException("Can't find path to control room")
            : Action.From(path.First().Direction(rick));
    }

    string ReturnToStart() {
        var path = pathfinding.Path(rick, maze.start);
        return path == null
            ? throw new ArgumentException("Can't find path back to start")
            : Action.From(path.First().Direction(rick));
    }
}

class Grid {
    static Point[] FourDirections { get; } = [
        Point.RIGHT, Point.LEFT, Point.UP, Point.DOWN
    ];

    readonly int width;
    readonly int height;
    readonly Cell[] cells;
    public Point start;
    public Point? controlRoom;

    public Grid(int width, int height) {
        this.width = width;
        this.height = height;
        cells = new Cell[width * height];
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                Set(x, y, new Cell(x, y));
            }
        }
    }

    void Set(int x, int y, Cell cell) => cells[width * y + x] = cell;
    Cell Get(int x, int y) => cells[width * y + x];
    public Cell Get(Point point) => Get(point.x, point.y);

    bool TryGet(Point point, out Cell cell) {
        cell = null!;
        if (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height) {
            return false;
        }
        cell = Get(point);
        return true;
    }

    public void ReadNewData() {
        start = default;
        controlRoom = null;
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                var cell = Get(x, y);
                switch (row[x]) {
                    case '#':
                        cell.type = CellType.Wall;
                        break;
                    case 'T':
                        cell.type = CellType.Start;
                        start = cell.point;
                        break;
                    case 'C':
                        cell.type = CellType.Control;
                        controlRoom = cell.point;
                        break;
                    case '?':
                        cell.type = CellType.Unknown;
                        break;
                    default:
                        cell.type = CellType.Empty;
                        break;
                }
            }
        }
    }

    public IEnumerable<Cell> Next(Cell cell) {
        foreach (var direction in FourDirections) {
            if (!TryGet(cell.point + direction, out var neighbor)) {
                continue;
            }
            if (!neighbor.IsWalkable) {
                continue;
            }
            yield return neighbor;
        }
    }
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
    public bool IsWalkable => type is CellType.Empty or CellType.Start or CellType.Control or CellType.Unknown;
}

enum CellType {
    Wall,
    Empty,
    Start,
    Control,
    Unknown
}

readonly record struct Point(int x, int y) {
    public readonly int x = x;
    public readonly int y = y;

    public static readonly Point UP = new(0, -1);
    public static readonly Point RIGHT = new(1, 0);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);

    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
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
    const string UP = "UP";
    const string RIGHT = "RIGHT";
    const string DOWN = "DOWN";
    const string LEFT = "LEFT";

    public static string From(Point direction) => direction switch {
        (0, 1) => DOWN,
        (0, -1) => UP,
        (-1, 0) => LEFT,
        (1, 0) => RIGHT,
        _ => throw new ArgumentOutOfRangeException($"Unsupported direction: {direction}")
    };
}

class Pathfinding(Grid grid) {
    readonly Queue<Node> queue = new();
    readonly HashSet<Point> visited = new();

    /// BFS to end
    public List<Point>? Path(Point start, Point end) {
        return Path(start, cell => cell.point == end);
    }

    /// BFS to endPredicate
    public List<Point>? Path(Point start, Predicate<Cell> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(grid.Get(start));
        queue.Enqueue(startNode);
        visited.Add(start);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.cell)) {
                return Reconstruct(startNode, current);
            }
            foreach (var nextCell in grid.Next(current.cell)) {
                if (!visited.Add(nextCell.point)) {
                    continue;
                }
                var next = new Node(nextCell) { parent = current };
                queue.Enqueue(next);
            }
        }
        return null;
    }

    static List<Point> Reconstruct(Node start, Node end) {
        var path = new List<Point>();
        var current = end;
        while (current != start) {
            path.Add(current!.cell.point);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    class Node(Cell cell) {
        public readonly Cell cell = cell;
        public Node? parent;
    }
}
