using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DoNotPanic2;

/// https://www.codingame.com/training/hard/don't-panic-episode-2
static class DoNotPanic2 {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
        }
    }
}

class Game {
    Grid drive = null!;
    Pathfinding pathfinding = null!;
    Point end;
    int additionalElevators;
    Dictionary<Point, string>? path;
    readonly HashSet<Point> used = new();

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int floors = Math.Clamp(int.Parse(inputs[0]), 1, 15);
        int width = Math.Clamp(int.Parse(inputs[1]), 5, 100);
        drive = new Grid(width, floors);
        pathfinding = new Pathfinding(drive);
        int maxTurns = Math.Clamp(int.Parse(inputs[2]), 10, 200);
        int exitFloor = Math.Clamp(int.Parse(inputs[3]), 0, floors);
        int exitX = Math.Clamp(int.Parse(inputs[4]), 0, width);
        end = new Point(exitX, exitFloor);
        drive.Set(end, CellType.Exit);
        int clones = Math.Clamp(int.Parse(inputs[5]), 2, 50);
        additionalElevators = int.Parse(inputs[6]);
        Console.Error.WriteLine($"additionalElevators: {additionalElevators}");
        int elevatorCount = Math.Clamp(int.Parse(inputs[7]), 0, 100);
        for (var i = 0; i < elevatorCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int elevatorFloor = Math.Clamp(int.Parse(inputs[0]), 0, floors);
            int elevatorX = Math.Clamp(int.Parse(inputs[1]), 0, width);
            var elevator = new Point(elevatorX, elevatorFloor);
            drive.Set(elevator, CellType.Elevator);
        }
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int leadingCloneFloor = Math.Clamp(int.Parse(inputs[0]), -1, drive.Floors);
        int leadingCloneX = Math.Clamp(int.Parse(inputs[1]), -1, drive.Width);
        var point = new Point(leadingCloneX, leadingCloneFloor);
        var leadingCloneDirection = inputs[2] == "LEFT" ? Point.LEFT : Point.RIGHT;
        bool noLeadingClone = leadingCloneFloor < 0;
        if (noLeadingClone) {
            Console.WriteLine(Actions.WAIT);
            return;
        }
        if (path == null) {
            var start = new PathState(point, leadingCloneDirection, additionalElevators);
            path = pathfinding.Path(start, end)!.ToDictionary(it => it.point, it => it.action);
            path[end] = Actions.WAIT;
        }
        if (!used.Add(point)) {
            Console.WriteLine(Actions.WAIT);
            return;
        }
        Console.WriteLine(path[point]);
    }
}

record Grid(int Width, int Floors) {
    readonly CellType[,] cells = new CellType[Width, Floors];

    public void Set(Point point, CellType type) => cells[point.X, point.Y] = type;
    CellType Get(Point point) => cells[point.X, point.Y];

    public IEnumerable<(PathState, int turns)> Next(PathState current) {
        if (!current.Point.InBounds(Width, Floors)) {
            yield break;
        }
        if (Get(current.Point) == CellType.Elevator) {
            yield return (current with {
                Point = current.Point + Point.UP,
                Action = Actions.WAIT
            }, 1);
            yield break;
        }
        yield return (current with {
            Point = current.Point + current.Direction,
            Action = Actions.WAIT
        }, 1);
        var otherDirection = current.Direction * -1;
        yield return (current with {
            Point = current.Point + otherDirection,
            Direction = otherDirection,
            Action = Actions.BLOCK
        }, 4);
        if (current.Elevators > 0) {
            yield return (current with {
                Point = current.Point + Point.UP,
                Elevators = current.Elevators - 1,
                Action = Actions.ELEVATOR
            }, 4);
        }
    }
}

enum CellType {
    Empty,
    Elevator,
    Exit
}

static class Actions {
    public const string WAIT = "WAIT";
    public const string BLOCK = "BLOCK";
    public const string ELEVATOR = "ELEVATOR";
}

record PathState(Point Point, Point Direction, int Elevators) {
    public string? Action { get; init; }

    public virtual bool Equals(PathState? other) {
        if (other is null) {
            return false;
        }
        if (ReferenceEquals(this, other)) {
            return true;
        }
        return Point.Equals(other.Point)
                && Direction.Equals(other.Direction)
                && Elevators == other.Elevators;
    }

    public override int GetHashCode() {
        return HashCode.Combine(Point, Direction, Elevators);
    }
}

class Pathfinding(Grid grid) {
    readonly PriorityQueue<Node, int> queue = new();
    readonly Dictionary<PathState, int> visited = new();

    /// Dijkstra
    public List<(Point point, string action)>? Path(PathState start, Point end) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(start);
        queue.Enqueue(startNode, 0);
        visited.Add(start, 0);
        while (queue.TryDequeue(out var current, out int currentTurns)) {
            if (current.state.Point == end) {
                return Reconstruct(startNode, current);
            }
            foreach ((var nextState, int turns) in grid.Next(current.state)) {
                int newTurns = currentTurns + turns;
                if (newTurns >= visited.GetValueOrDefault(nextState, int.MaxValue)) {
                    continue;
                }
                var next = new Node(nextState) { parent = current };
                queue.Enqueue(next, newTurns);
                visited[nextState] = newTurns;
            }
        }
        return null;
    }

    static List<(Point, string)> Reconstruct(Node start, Node end) {
        var path = new List<(Point, string)>();
        var current = end;
        while (current != start) {
            string action = current.state.Action!;
            current = current.parent;
            path.Add((current!.state.Point, action));
        }
        path.Reverse();
        return path;
    }

    class Node(PathState state) {
        public readonly PathState state = state;
        public Node? parent;
    }
}

readonly record struct Point(int X, int Y) {
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}
