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

    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeEnumCasesNoDefault")]
    public void Loop() {
        string[] point = Console.ReadLine()!.Split(' ');
        rick = Point.From(point[1], point[0]);
        switch (maze.Get(rick).type) {
            case CellType.Empty:
                explored.Add(rick);
                break;
            case CellType.Control:
                visitedControlRoom = true;
                break;
        }
        maze.ReadNewData();
        Console.WriteLine(NextMove());
    }

    string NextMove() {
        if (explored.Count < maze.emptyCells.Count && TryExploreMaze(out var direction)) {
            return direction.ToAction;
        }
        return !visitedControlRoom ? GoToControlRoom() : ReturnToStart();
    }

    bool TryExploreMaze(out Point resultDirection) {
        resultDirection = default;
        foreach (var direction in Maze.FourDirections) {
            var neighbour = rick + direction;
            if (maze.IsOutside(neighbour)) {
                continue;
            }
            var cell = maze.Get(neighbour);
            if (cell.type != CellType.Empty) {
                continue;
            }
            if (explored.Contains(cell.point)) {
                continue;
            }
            resultDirection = direction;
            return true;
        }
        foreach (var cell in maze.emptyCells.Where(it => !explored.Contains(it))) {
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
    public readonly List<Point> emptyCells = new();

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

    bool IsOutside(int x, int y) => x < 0 || x >= width || y < 0 || y >= height;
    public bool IsOutside(Point point) => IsOutside(point.x, point.y);

    public void ReadNewData() {
        start = default;
        controlRoom = null;
        emptyCells.Clear();
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char c = row[x];
                var cell = Get(x, y);
                cell.type = c switch {
                    '#' => CellType.Wall,
                    'T' => CellType.Start,
                    'C' => CellType.Control,
                    '?' => CellType.Unknown,
                    _ => CellType.Empty
                };
                switch (c) {
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
                        emptyCells.Add(cell.point);
                        break;
                }
            }
        }
    }
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
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
        _ => Action.LEFT
    };

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
}

class Pathfinding(Maze maze) {
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

            foreach (var direction in Maze.FourDirections) {
                var neighbourPoint = current.point + direction;
                var cell = maze.Get(neighbourPoint);
                if (cell.type != CellType.Empty && cell.point != end) {
                    continue;
                }
                if (!nodes.TryGetValue(cell.point, out var neighbour)) {
                    neighbour = new Node(cell.point);
                    nodes.Add(cell.point, neighbour);
                }
                if (neighbour.isClosed) {
                    continue;
                }
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
