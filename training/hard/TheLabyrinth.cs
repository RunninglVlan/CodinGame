using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
    readonly Maze maze;
    readonly Pathfinding pathfinding;

    /// number of rounds between the time the alarm countdown is activated and the time the alarm goes off
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    int alarmRounds;

    Point rick;
    readonly HashSet<Point> explored = new();
    bool visitedControlRoom;

    /// 10 ≤ height ≤ 100,
    /// 20 ≤ width ≤ 200,
    /// 1 ≤ alarmRounds ≤ 100
    public Game() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int height = int.Parse(inputs[0]);
        int width = int.Parse(inputs[1]);
        maze = new Maze(width, height);
        pathfinding = new(maze);
        alarmRounds = int.Parse(inputs[2]);
    }

    public void Loop() {
        string[] point = Console.ReadLine()!.Split(' ');
        rick = Point.From(point[1], point[0]);
        maze.ReadNewData();
        var cell = maze.Get(rick);
        if (cell.IsWalkable) {
            explored.Add(rick);
        } else if (cell.type == CellType.Control) {
            visitedControlRoom = true;
        }
        Console.WriteLine(NextMove());
    }

    string NextMove() {
        if (explored.Count < maze.walkableCells.Count && TryExploreMaze(out var direction)) {
            return direction.ToAction;
        }
        return !visitedControlRoom ? GoToControlRoom() : ReturnToStart();
    }

    bool TryExploreMaze(out Point resultDirection) {
        resultDirection = default;
        foreach (var direction in Maze.FourDirections) {
            var neighbour = rick + direction;
            if (!maze.TryGet(neighbour, out var cell)) {
                continue;
            }
            if (!cell.IsWalkable || explored.Contains(cell.point)) {
                continue;
            }
            resultDirection = direction;
            return true;
        }
        foreach (var cell in maze.walkableCells.Where(it => !explored.Contains(it))) {
            var path = pathfinding.Path(rick, cell);
            if (path == null) {
                continue;
            }
            resultDirection = path.First().Direction(rick);
            return true;
        }
        return false;
    }

    string GoToControlRoom() {
        if (maze.controlRoom == null) {
            throw new ArgumentException("Explored maze, but couldn't find control room");
        }
        var path = pathfinding.Path(rick, maze.controlRoom.Value);
        return path == null
            ? throw new ArgumentException("Can't find path to control room")
            : path.First().Direction(rick).ToAction;
    }

    string ReturnToStart() {
        var path = pathfinding.Path(rick, maze.start);
        return path == null
            ? throw new ArgumentException("Can't find path back to start")
            : path.First().Direction(rick).ToAction;
    }
}

class Maze {
    public static Point[] FourDirections { get; } = [
        Point.RIGHT, Point.LEFT, Point.UP, Point.DOWN
    ];

    readonly int width;
    readonly int height;
    readonly Cell[] cells;
    public Point start;
    public Point? controlRoom;
    public readonly List<Point> walkableCells = new();

    public Maze(int width, int height) {
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

    public bool TryGet(Point point, out Cell cell) {
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
        walkableCells.Clear();
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
                if (cell.IsWalkable) {
                    walkableCells.Add(cell.point);
                }
            }
        }
    }
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
    public bool IsWalkable => type is CellType.Empty or CellType.Start;
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

    /// Direction to Action
    public string ToAction => this switch {
        (0, -1) => Action.UP,
        (1, 0) => Action.RIGHT,
        (0, 1) => Action.DOWN,
        (-1, 0) => Action.LEFT,
        _ => throw new ArgumentOutOfRangeException($"Unsupported direction: {this}")
    };

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
}

class Pathfinding(Maze maze) {
    readonly List<Point> path = new();
    readonly Queue<Node> queue = new();
    readonly Dictionary<Point, Node> visited = new();

    /// BFS
    public List<Point>? Path(Point start, Point end) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(start);
        queue.Enqueue(startNode);
        visited.Add(start, startNode);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (current.point == end) {
                return Reconstruct(startNode, current);
            }
            foreach (var direction in Maze.FourDirections) {
                var neighbourPoint = current.point + direction;
                if (!maze.TryGet(neighbourPoint, out var cell)) {
                    continue;
                }
                if (!cell.IsWalkable && cell.point != end) {
                    continue;
                }
                if (visited.ContainsKey(cell.point)) {
                    continue;
                }
                var neighbour = new Node(cell.point) { parent = current };
                queue.Enqueue(neighbour);
                visited.Add(cell.point, neighbour);
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
    public Node? parent;
}
