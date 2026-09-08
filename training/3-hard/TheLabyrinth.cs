using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

// Visual
// https://www.codingame.com/training/hard/the-labyrinth
var game = new Game();
while (true) {
    game.Loop();
}

class Game {
    readonly Grid maze;
    readonly Pathfinding<Cell> pathfinding;

    /// number of rounds between the time the alarm countdown is activated and the time the alarm goes off
    readonly int alarmRounds;

    Cell rick = null!;
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
        rick = maze.Get(Point.From(point[1], point[0]));
        maze.ReadNewData();
        if (rick.type == CellType.Control) {
            visitedControlRoom = true;
        }
        Console.WriteLine(NextMove());
    }

    string NextMove() {
        if (maze.controlRoom == null) {
            return ExploreMaze();
        }
        var controlToStart = pathfinding.Path(maze.controlRoom, maze.start);
        if (controlToStart == null || controlToStart.Count > alarmRounds) {
            return ExploreMaze();
        }
        return !visitedControlRoom ? GoToControlRoom() : ReturnToStart();
    }

    string ExploreMaze() {
        var pathToUnknown = pathfinding.Path(rick, cell => cell.type == CellType.Unknown);
        return pathToUnknown == null
                ? throw new ArgumentException("Can't find path to unknown cell")
                : Action.From(pathToUnknown.First().point.Direction(rick.point));
    }

    string GoToControlRoom() {
        if (maze.controlRoom == null) {
            throw new ArgumentException("Explored maze, but couldn't find control room");
        }
        var path = pathfinding.Path(rick, maze.controlRoom);
        return path == null
                ? throw new ArgumentException("Can't find path to control room")
                : Action.From(path.First().point.Direction(rick.point));
    }

    string ReturnToStart() {
        var path = pathfinding.Path(rick, maze.start);
        return path == null
                ? throw new ArgumentException("Can't find path back to start")
                : Action.From(path.First().point.Direction(rick.point));
    }
}

class Grid : IGraph<Cell> {
    static Point[] FourDirections { get; } = [
        Point.RIGHT, Point.LEFT, Point.UP, Point.DOWN
    ];

    readonly int width;
    readonly int height;
    readonly Cell[] cells;
    public Cell start = null!;
    public Cell? controlRoom;

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
    public Cell Get(Point point) => Get(point.X, point.Y);

    bool TryGet(Point point, out Cell cell) {
        cell = null!;
        if (point.X < 0 || point.X >= width || point.Y < 0 || point.Y >= height) {
            return false;
        }
        cell = Get(point);
        return true;
    }

    public void ReadNewData() {
        start = null!;
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
                        start = cell;
                        break;
                    case 'C':
                        cell.type = CellType.Control;
                        controlRoom = cell;
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

    IEnumerable<Cell> IGraph<Cell>.Next(Cell cell) {
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

class Cell(int x, int y) : IEquatable<Cell> {
    public Point point = new(x, y);
    public CellType type;
    public bool IsWalkable => type is CellType.Empty or CellType.Start or CellType.Control or CellType.Unknown;

    public bool Equals(Cell? other) => ReferenceEquals(this, other);
    public override bool Equals(object? obj) => Equals(obj as Cell);
    public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);
}

enum CellType {
    Wall,
    Empty,
    Start,
    Control,
    Unknown
}

readonly record struct Point(int X, int Y) {
    public static readonly Point UP = new(0, -1);
    public static readonly Point RIGHT = new(1, 0);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);

    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }

    public static Point operator +(Point a, Point b) {
        return new Point(a.X + b.X, a.Y + b.Y);
    }

    public static Point operator -(Point a, Point b) {
        return new Point(a.X - b.X, a.Y - b.Y);
    }

    public Point Direction(Point from) {
        return this - from;
    }

    public override string ToString() => $"({X}, {Y})";
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

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly Dictionary<T, T> parents = new();

    /// BFS to end
    public List<T>? Path(T start, T end) {
        return Path(start, it => it.Equals(end));
    }

    /// BFS to endPredicate
    public List<T>? Path(T start, Predicate<T> endPredicate) {
        Queue<T> queue = [];
        HashSet<T> visited = [];
        parents.Clear();
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                return Reconstruct(start, current);
            }
            foreach (var next in graph.Next(current)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        var path = new List<T>();
        var current = end;
        while (!current.Equals(start)) {
            path.Add(current);
            current = parents[current];
        }
        path.Reverse();
        return path;
    }
}

interface IGraph<T> {
    IEnumerable<T> Next(T current);
}
