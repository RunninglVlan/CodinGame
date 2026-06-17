using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DetectivePikaptcha4;

/// https://www.codingame.com/training/hard/detective-pikaptcha-ep4
static class DetectivePikaptcha4 {
    static void Main() {
        var game = new Game();
        game.Init();
        var sideToFollow = Console.ReadLine()! == "R" ? Side.Right : Side.Left;
        game.MoveThroughMaze(sideToFollow);
        game.PrintMaze();
    }
}

class Game {
    readonly Cube maze = new();
    Player pikaptcha = null!;

    public void Init() => maze.Init(out pikaptcha);

    [SuppressMessage("ReSharper", "InvertIf")]
    public void MoveThroughMaze(Side sideToFollow) {
        var start = pikaptcha.Cell;
        Console.Error.WriteLine($"Starting at {pikaptcha}, following {sideToFollow} side");
        while (pikaptcha.Cell != start || pikaptcha.Cell.Stepped <= 0) {
            if (!IsWallOn(sideToFollow)) {
                pikaptcha.Turn(sideToFollow);
            }
            Cell next;
            var originalDirection = pikaptcha.Direction;
            while (!TryMoveForward(out next)) {
                var oppositeSide = sideToFollow == Side.Right ? Side.Left : Side.Right;
                pikaptcha.Turn(oppositeSide);
                if (pikaptcha.Direction == originalDirection) {
                    Console.Error.WriteLine("Trapped - turned all the way around and found nowhere to go!");
                    return;
                }
            }
            pikaptcha.Move(next, maze);
        }
    }

    bool IsWallOn(Side side) {
        var wallDirection = Screen.Turn(pikaptcha.Direction, side);
        var wallToCheck = pikaptcha.Cell.Point + wallDirection;
        return maze.Get(pikaptcha.Cell.Face, wallToCheck).Type == CellType.Wall;
    }

    bool TryMoveForward(out Cell nextCell) {
        var nextPoint = pikaptcha.Cell.Point + pikaptcha.Direction;
        nextCell = maze.Get(pikaptcha.Cell.Face, nextPoint);
        return nextCell.Type != CellType.Wall;
    }

    public void PrintMaze() => maze.Print();
}

class Cube {
    const int FACES = 6;

    readonly Dictionary<(int, Point), (int face, TargetAxis x, TargetAxis y, Point direction)> topology = new();
    int size;
    Cell[][,] faces = null!;

    public Cube() {
        topology[(0, Screen.LEFT)] = (1, TargetAxis.Y, TargetAxis.Min, Screen.DOWN);
        topology[(0, Screen.RIGHT)] = (3, TargetAxis.YInverted, TargetAxis.Min, Screen.DOWN);
        topology[(0, Screen.UP)] = (5, TargetAxis.X, TargetAxis.Max, Screen.UP);
        topology[(0, Screen.DOWN)] = (2, TargetAxis.X, TargetAxis.Min, Screen.DOWN);

        topology[(1, Screen.LEFT)] = (5, TargetAxis.Min, TargetAxis.YInverted, Screen.RIGHT);
        topology[(1, Screen.RIGHT)] = (2, TargetAxis.Min, TargetAxis.Y, Screen.RIGHT);
        topology[(1, Screen.UP)] = (0, TargetAxis.Min, TargetAxis.X, Screen.RIGHT);
        topology[(1, Screen.DOWN)] = (4, TargetAxis.Min, TargetAxis.XInverted, Screen.RIGHT);

        topology[(2, Screen.LEFT)] = (1, TargetAxis.Max, TargetAxis.Y, Screen.LEFT);
        topology[(2, Screen.RIGHT)] = (3, TargetAxis.Min, TargetAxis.Y, Screen.RIGHT);
        topology[(2, Screen.UP)] = (0, TargetAxis.X, TargetAxis.Max, Screen.UP);
        topology[(2, Screen.DOWN)] = (4, TargetAxis.X, TargetAxis.Min, Screen.DOWN);

        topology[(3, Screen.LEFT)] = (2, TargetAxis.Max, TargetAxis.Y, Screen.LEFT);
        topology[(3, Screen.RIGHT)] = (5, TargetAxis.Max, TargetAxis.YInverted, Screen.LEFT);
        topology[(3, Screen.UP)] = (0, TargetAxis.Max, TargetAxis.XInverted, Screen.LEFT);
        topology[(3, Screen.DOWN)] = (4, TargetAxis.Max, TargetAxis.X, Screen.LEFT);

        topology[(4, Screen.LEFT)] = (1, TargetAxis.YInverted, TargetAxis.Max, Screen.UP);
        topology[(4, Screen.RIGHT)] = (3, TargetAxis.Y, TargetAxis.Max, Screen.UP);
        topology[(4, Screen.UP)] = (2, TargetAxis.X, TargetAxis.Max, Screen.UP);
        topology[(4, Screen.DOWN)] = (5, TargetAxis.X, TargetAxis.Min, Screen.DOWN);

        topology[(5, Screen.LEFT)] = (1, TargetAxis.Min, TargetAxis.YInverted, Screen.RIGHT);
        topology[(5, Screen.RIGHT)] = (3, TargetAxis.Max, TargetAxis.YInverted, Screen.LEFT);
        topology[(5, Screen.UP)] = (4, TargetAxis.X, TargetAxis.Max, Screen.UP);
        topology[(5, Screen.DOWN)] = (0, TargetAxis.X, TargetAxis.Min, Screen.DOWN);
    }

    public void Init(out Player pikaptcha) {
        pikaptcha = null!;
        size = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
        faces = new Cell[FACES][,];
        for (var f = 0; f < FACES; f++) {
            Console.Error.WriteLine(f);
            faces[f] = new Cell[size, size];
            for (var y = 0; y < size; y++) {
                string characters = Console.ReadLine()!;
                Console.Error.WriteLine(characters);
                for (var x = 0; x < size; x++) {
                    char c = characters[x];
                    var type = c == '#' ? CellType.Wall : CellType.Passage;
                    var cell = new Cell(f, new Point(x, y), type);
                    faces[f][x, y] = cell;
                    Point? direction = c switch {
                        '^' => Screen.UP,
                        'v' => Screen.DOWN,
                        '<' => Screen.LEFT,
                        '>' => Screen.RIGHT,
                        '0' or '#' => null,
                        _ => throw new ArgumentException($"Unexpected cell type: {c}")
                    };
                    if (direction != null) {
                        pikaptcha = new(cell, direction.Value);
                    }
                }
            }
        }
    }

    public Cell Get(int f, Point point) {
        if (point.InBounds(size, size)) {
            return faces[f][point.X, point.Y];
        }
        var direction = point switch {
            { X: < 0 } => Screen.LEFT,
            { X: var x } when x >= size => Screen.RIGHT,
            { Y: < 0 } => Screen.UP,
            { Y: var y } when y >= size => Screen.DOWN,
            _ => throw new ArgumentException($"Unexpected point: {point}")
        };
        if (!topology.TryGetValue((f, direction), out var next)) {
            throw new ArgumentException($"No topology mapping for face {f} and direction {Screen.ToString(direction)}");
        }
        var pointNext = (next.x, next.y) switch {
            (TargetAxis.X, TargetAxis.Min) => point with { Y = 0 },
            (TargetAxis.X, TargetAxis.Max) => point with { Y = size - 1 },
            (TargetAxis.Y, TargetAxis.Min) => new Point(point.Y, 0),
            (TargetAxis.Y, TargetAxis.Max) => new Point(point.Y, size - 1),
            (TargetAxis.YInverted, TargetAxis.Min) => new Point(size - 1 - point.Y, 0),
            (TargetAxis.YInverted, TargetAxis.Max) => new Point(size - 1 - point.Y, size - 1),
            (TargetAxis.Min, TargetAxis.X) => new Point(0, point.X),
            (TargetAxis.Min, TargetAxis.XInverted) => new Point(0, size - 1 - point.X),
            (TargetAxis.Min, TargetAxis.Y) => point with { X = 0 },
            (TargetAxis.Min, TargetAxis.YInverted) => new Point(0, size - 1 - point.Y),
            (TargetAxis.Max, TargetAxis.Y) => point with { X = size - 1 },
            (TargetAxis.Max, TargetAxis.YInverted) => new Point(size - 1, size - 1 - point.Y),
            (TargetAxis.Max, TargetAxis.X) => new Point(size - 1, point.X),
            (TargetAxis.Max, TargetAxis.XInverted) => new Point(size - 1, size - 1 - point.X),
            _ => throw new ArgumentException($"Unexpected target axes: {next.x}, {next.y}")
        };
        return faces[next.face][pointNext.X, pointNext.Y];
    }

    public void Print() {
        for (var f = 0; f < FACES; f++) {
            var face = faces[f];
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    var cell = face[x, y];
                    Console.Write(cell.Type == CellType.Wall ? "#" : cell.Stepped);
                }
                Console.WriteLine();
            }
        }
    }

    public Point NextFaceDirection(int source, Point direction) {
        return topology.TryGetValue((source, direction), out var next)
                ? next.direction
                : throw new ArgumentException(
                        $"No topology mapping for face {source} and direction {Screen.ToString(direction)}");
    }
}

enum TargetAxis {
    X,
    Y,
    XInverted,
    YInverted,
    Min,
    Max
}

record Cell(int Face, Point Point, CellType Type) {
    public int Stepped { get; set; }

    public override string ToString() => $"[{Face}]{Point}";
}

enum CellType {
    Passage,
    Wall
}

class Player(Cell cell, Point direction) {
    public Cell Cell { get; private set; } = cell;
    public Point Direction { get; private set; } = direction;

    public void Turn(Side side) => Direction = Screen.Turn(Direction, side);

    public void Move(Cell next, Cube maze) {
        if (next.Face != Cell.Face) {
            Direction = maze.NextFaceDirection(Cell.Face, Direction);
        }
        Cell = next;
        Cell.Stepped++;
    }

    public override string ToString() => $"{Cell} moving {Screen.ToString(Direction)}";
}

enum Side {
    Right,
    Left
}

readonly record struct Point(int X, int Y) {
    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"({X}, {Y})";
}

static class Screen {
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point Turn(Point direction, Side side) {
        return side == Side.Right
                ? new Point(-direction.Y, direction.X)
                : new Point(direction.Y, -direction.X);
    }

    public static string ToString(Point direction) => direction switch {
        (0, -1) => nameof(UP),
        (0, 1) => nameof(DOWN),
        (-1, 0) => nameof(LEFT),
        (1, 0) => nameof(RIGHT),
        _ => direction.ToString()
    };
}
