using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

// https://www.codingame.com/training/medium/blunder-episode-1
string[] dimensions = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(dimensions[0]), 4, 100);
int width = Math.Clamp(int.Parse(dimensions[1]), 4, 100);
var map = new Map(width, height);
map.Read();
map.MoveBlunder();
if (map.BlunderMoves == null) {
    Console.WriteLine("LOOP");
    return;
}
foreach (string move in map.BlunderMoves) {
    Console.WriteLine(move);
}

class Map(int width, int height) {
    static readonly Point NORTH = new(0, -1);
    static readonly Point SOUTH = new(0, 1);
    static readonly Point WEST = new(-1, 0);
    static readonly Point EAST = new(1, 0);
    static readonly Point[] DIRECTIONS = [SOUTH, EAST, NORTH, WEST];

    readonly Dictionary<Point, Cell> map = [];
    Blunder blunder;

    readonly Dictionary<CellType, Point> modifierDirections = new() {
        { CellType.GoSouth, SOUTH },
        { CellType.GoEast, EAST },
        { CellType.GoNorth, NORTH },
        { CellType.GoWest, WEST }
    };

    public List<string>? BlunderMoves { get; private set; }

    public void Read() {
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                map[new Point(x, y)] = Cell.From(row[x]);
            }
        }
        Debug.Log(this);
    }

    public void MoveBlunder() {
        var start = map.First(it => it.Value.Type == CellType.Start).Key;
        blunder = new Blunder([start], SOUTH);
        List<string> moves = [];
        while (map[blunder.Point].Type != CellType.SuicideBooth) {
            if (modifierDirections.TryGetValue(map[blunder.Point].Type, out var direction)) {
                blunder.Direction = direction;
            }
            var next = blunder.Point + blunder.Direction;
            var nextCell = map[next].Type;
            switch (nextCell) {
                case CellType.Empty or CellType.SuicideBooth:
                    break;
                case CellType.Wall or CellType.Obstacle:
                    ChangeDirection();
                    continue;
                case CellType.GoSouth or CellType.GoEast or CellType.GoNorth or CellType.GoWest:
                    Debug.Log($"path modifier {nextCell}", 1);
                    break;
                default:
                    throw new ArgumentException($"TODO: {nextCell}");
            }
            blunder.Path.Add(next);
            string move = ToMove(blunder.Direction);
            moves.Add(move);
            Debug.Log($"moved {move}");
        }
        BlunderMoves = moves;
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    void ChangeDirection() {
        Debug.Log("changed direction", 1);
        foreach (var direction in DIRECTIONS) {
            var next = blunder.Point + direction;
            if (map[next].Type == CellType.Empty) {
                blunder.Direction = direction;
                return;
            }
        }
    }

    static string ToMove(Point direction) => direction switch {
        _ when direction == NORTH => nameof(NORTH),
        _ when direction == SOUTH => nameof(SOUTH),
        _ when direction == WEST => nameof(WEST),
        _ => nameof(EAST),
    };

    public override string ToString() {
        var rows = new StringBuilder();
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                rows.Append(map[new Point(x, y)].Symbol);
            }
            if (y < height - 1) {
                rows.AppendLine();
            }
        }
        return rows.ToString();
    }
}

record struct Blunder(List<Point> Path, Point Direction) {
    public Point Point => Path.Last();
}

readonly record struct Cell(char Symbol, CellType Type) {
    public static Cell From(char symbol) {
        var type = symbol switch {
            ' ' => CellType.Empty,
            '@' => CellType.Start,
            '$' => CellType.SuicideBooth,
            '#' => CellType.Wall,
            'X' => CellType.Obstacle,
            'S' => CellType.GoSouth,
            'E' => CellType.GoEast,
            'N' => CellType.GoNorth,
            'W' => CellType.GoWest,
            'R' => CellType.Reverse,
            'B' => CellType.Beer,
            _ => CellType.Teleporter
        };
        return new Cell(symbol, type);
    }
}

enum CellType {
    Empty,
    Start,
    SuicideBooth,
    Wall,
    Obstacle,
    GoSouth,
    GoEast,
    GoNorth,
    GoWest,
    Reverse,
    Beer,
    Teleporter
}

readonly record struct Point(int X, int Y) {
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"({X}, {Y})";
}

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; } = true;

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        if (!Enabled) {
            return;
        }
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
