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
if (!map.TryMoveBlunder(out var moves)) {
    Console.WriteLine("LOOP");
    return;
}
foreach (string move in moves) {
    Console.WriteLine(move);
}

class Map(int width, int height) {
    const int MAX_ITERATIONS = 200;
    static readonly Point NORTH = new(0, -1);
    static readonly Point SOUTH = new(0, 1);
    static readonly Point WEST = new(-1, 0);
    static readonly Point EAST = new(1, 0);

    readonly Dictionary<Point, Cell> map = [];
    Blunder blunder;
    static readonly Point[] directionPriorities = [SOUTH, EAST, NORTH, WEST];

    readonly Dictionary<CellType, Point> pathModifiers = new() {
        { CellType.GoSouth, SOUTH },
        { CellType.GoEast, EAST },
        { CellType.GoNorth, NORTH },
        { CellType.GoWest, WEST }
    };

    public void Read() {
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                map[new Point(x, y)] = Cell.From(row[x]);
            }
        }
        Debug.Log(this);
    }

    public bool TryMoveBlunder(out List<string> moves) {
        var start = map.First(it => it.Value.Type == CellType.Start).Key;
        blunder = new Blunder(start, SOUTH);
        moves = [];
        var i = 0;
        for (var current = map[blunder.Point]; current.Type != CellType.SuicideBooth; current = map[blunder.Point]) {
            if (++i > MAX_ITERATIONS) {
                return false;
            }
            if (pathModifiers.TryGetValue(current.Type, out var direction)) {
                blunder.Direction = direction;
                Debug.Log($"path modifier: {current.Type}", 1);
            } else if (current.Type == CellType.Teleporter) {
                var target = map.First(it => it.Value.Type == CellType.Teleporter && it.Key != blunder.Point);
                blunder.Point = target.Key;
                Debug.Log($"teleported to {target.Key}", 1);
            }
            var next = blunder.Point + blunder.Direction;
            if (!TryMove(next, out var action)) {
                action();
                continue;
            }
            action();
            blunder.Point = next;
            string move = ToMove(blunder.Direction);
            moves.Add(move);
            Debug.Log($"moved {move}");
        }
        return true;
    }

    bool TryMove(Point next, out Action action) {
        action = () => { };
        var nextCell = map[next].Type;
        switch (nextCell) {
            case CellType.Empty or CellType.SuicideBooth or CellType.Teleporter
                    or CellType.GoSouth or CellType.GoEast or CellType.GoNorth or CellType.GoWest:
                break;
            case CellType.Wall:
                action = ChangeDirection;
                return false;
            case CellType.Obstacle:
                if (!blunder.Breaker) {
                    action = ChangeDirection;
                    return false;
                }
                action = () => {
                    map[next] = Cell.Empty;
                    Debug.Log($"destroyed {nextCell} at {next}", 1);
                    Debug.Log(this);
                };
                break;
            case CellType.Beer:
                action = () => {
                    blunder.Breaker = !blunder.Breaker;
                    Debug.Log($"breaker mode: {blunder.Breaker}", 1);
                };
                break;
            case CellType.CircuitInverter:
                action = () => Array.Reverse(directionPriorities);
                Debug.Log("reversed direction priorities", 1);
                break;
            default:
                throw new ArgumentException($"TODO: {nextCell}");
        }
        return true;
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    void ChangeDirection() {
        Debug.Log("changed direction", 1);
        foreach (var direction in directionPriorities) {
            var next = blunder.Point + direction;
            if (map[next].Type is not (CellType.Wall or CellType.Obstacle)) {
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

record struct Blunder(Point Point, Point Direction, bool Breaker = false);

readonly record struct Cell(char Symbol, CellType Type) {
    const char EMPTY = ' ';
    public static Cell Empty { get; } = From(EMPTY);

    public static Cell From(char symbol) {
        var type = symbol switch {
            EMPTY => CellType.Empty,
            '@' => CellType.Start,
            '$' => CellType.SuicideBooth,
            '#' => CellType.Wall,
            'X' => CellType.Obstacle,
            'S' => CellType.GoSouth,
            'E' => CellType.GoEast,
            'N' => CellType.GoNorth,
            'W' => CellType.GoWest,
            'I' => CellType.CircuitInverter,
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
    CircuitInverter,
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
