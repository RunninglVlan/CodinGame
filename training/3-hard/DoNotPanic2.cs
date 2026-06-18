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
    Cell end = null!;
    int turns;
    int additionalElevators;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int floors = Math.Clamp(int.Parse(inputs[0]), 1, 15);
        int width = Math.Clamp(int.Parse(inputs[1]), 5, 100);
        drive = new Grid(width, floors);
        pathfinding = new Pathfinding(drive);
        turns = Math.Clamp(int.Parse(inputs[2]), 10, 200);
        int exitFloor = Math.Clamp(int.Parse(inputs[3]), 0, floors);
        int exitX = Math.Clamp(int.Parse(inputs[4]), 0, width);
        end = drive.Get(exitX, exitFloor);
        end.type = CellType.Exit;
        int clones = Math.Clamp(int.Parse(inputs[5]), 2, 50);
        additionalElevators = int.Parse(inputs[6]);
        Console.Error.WriteLine($"additionalElevators: {additionalElevators}");
        int elevatorCount = Math.Clamp(int.Parse(inputs[7]), 0, 100);
        for (var i = 0; i < elevatorCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int elevatorFloor = Math.Clamp(int.Parse(inputs[0]), 0, floors);
            int elevatorX = Math.Clamp(int.Parse(inputs[1]), 0, width);
            drive.Get(elevatorX, elevatorFloor).type = CellType.Elevator;
        }
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int leadingCloneFloor = Math.Clamp(int.Parse(inputs[0]), -1, drive.Floors);
        int leadingCloneX = Math.Clamp(int.Parse(inputs[1]), -1, drive.Width);
        var leadingCloneDirection = inputs[2] == "LEFT" ? Point.LEFT : Point.RIGHT;
        Console.WriteLine(ChooseAction(leadingCloneX, leadingCloneFloor, leadingCloneDirection));
        turns--;
    }

    string ChooseAction(int x, int floor, Point direction) {
        bool noLeadingClone = floor < 0;
        if (noLeadingClone) {
            return Actions.WAIT;
        }
        var targets = drive.GetFloor(floor).Where(it => it.type is CellType.Elevator or CellType.Exit).ToArray();
        if (TryBuildElevator(targets, x, floor)) {
            return Actions.ELEVATOR;
        }
        int target = targets
                .Select(it => it.point.X)
                .MinBy(targetX => {
                    int distance = Math.Abs(targetX - x);
                    bool sameDirection = targetX < x && direction == Point.LEFT
                            || targetX > x && direction == Point.RIGHT;
                    return (distance, !sameDirection);
                });
        return BlockIfMovingInWrongDirection(x, target, direction);
    }

    bool TryBuildElevator(Cell[] targets, int x, int floor) {
        const int elevatorCost = 3;
        if (targets.Length == 0 && additionalElevators > 0) {
            additionalElevators--;
            drive.Get(x, floor).type = CellType.Elevator;
            return true;
        }
        if (additionalElevators <= 0 || drive.Get(x, floor).type != CellType.Empty) {
            return false;
        }
        var start = new Point(x, floor);
        int? without = pathfinding.Path(start, end.point);
        drive.Get(start).type = CellType.Elevator;
        int? with = pathfinding.Path(start, end.point);
        if (with < without && with + elevatorCost < turns) {
            additionalElevators--;
            return true;
        }
        drive.Get(start).type = CellType.Empty;
        return false;
    }

    static string BlockIfMovingInWrongDirection(int x, int target, Point direction) {
        if (target < x) {
            return direction == Point.RIGHT ? Actions.BLOCK : Actions.WAIT;
        }
        if (target > x) {
            return direction == Point.LEFT ? Actions.BLOCK : Actions.WAIT;
        }
        return Actions.WAIT;
    }
}

class Grid {
    public int Width { get; }
    public int Floors { get; }
    readonly Cell[,] cells;

    public Grid(int width, int floors) {
        Width = width;
        Floors = floors;
        cells = new Cell[width, floors];
        for (var floor = 0; floor < floors; floor++) {
            for (var x = 0; x < width; x++) {
                cells[x, floor] = new Cell(x, floor);
            }
        }
    }

    public Cell Get(int x, int floor) => cells[x, floor];
    public Cell Get(Point point) => Get(point.X, point.Y);

    public IEnumerable<Cell> GetFloor(int y) {
        for (var x = 0; x < Width; x++) {
            yield return cells[x, y];
        }
    }

    public IEnumerable<Cell> Neighbors(Cell cell) => Point.Directions4.Select(it => cell.point + it)
            .Where(it => it.InBounds(Width, Floors))
            .Where(it => it.Y == cell.point.Y || cell.type == CellType.Elevator && it.Y > cell.point.Y)
            .Select(Get);
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type = CellType.Empty;
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

class Pathfinding(Grid grid) {
    readonly Queue<Node> queue = new();
    readonly Dictionary<Point, Node> visited = new();

    /// BFS
    public int? Path(Point start, Point end) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(grid.Get(start));
        queue.Enqueue(startNode);
        visited.Add(start, startNode);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (current.cell.point == end) {
                return Count(startNode, current);
            }
            foreach (var cell in grid.Neighbors(current.cell)) {
                if (visited.ContainsKey(cell.point)) {
                    continue;
                }
                var neighbor = new Node(cell) { parent = current };
                queue.Enqueue(neighbor);
                visited.Add(cell.point, neighbor);
            }
        }
        return null;
    }

    static int Count(Node start, Node end) {
        // TODO: directional change costs 3 turns
        var count = 0;
        var current = end;
        for (; current != start; count++) {
            current = current!.parent;
        }
        return count;
    }

    class Node(Cell cell) {
        public readonly Cell cell = cell;
        public Node? parent;
    }
}

readonly record struct Point(int X, int Y) {
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"({X}, {Y})";
}
