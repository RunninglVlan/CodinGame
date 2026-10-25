using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
    public List<string>? BlunderMoves { get; private set; }

    public void Read() {
        for (var y = 0; y < height; y++) {
            string row = Debug.ReadLine()!;
            for (var x = 0; x < width; x++) {
                map[new Point(x, y)] = row[x] switch {
                    ' ' => Cell.Empty,
                    '@' => Cell.Start,
                    '$' => Cell.SuicideBooth,
                    '#' => Cell.Wall,
                    'X' => Cell.Obstacle,
                    'S' => Cell.GoSouth,
                    'E' => Cell.GoEast,
                    'N' => Cell.GoNorth,
                    'W' => Cell.GoWest,
                    'R' => Cell.Reverse,
                    'B' => Cell.Beer,
                    _ => Cell.Teleporter
                };
            }
        }
    }

    public void MoveBlunder() {
        var start = map.First(it => it.Value == Cell.Start).Key;
        blunder = new Blunder([start], SOUTH);
        List<string> moves = [];
        while (map[blunder.Point] != Cell.SuicideBooth) {
            var next = blunder.Point + blunder.Direction;
            if (map[next] is Cell.Wall or Cell.Obstacle) {
                ChangeDirection();
                continue;
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
            if (map[next] == Cell.Empty) {
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
}

record struct Blunder(List<Point> Path, Point Direction) {
    public Point Point => Path.Last();
}

enum Cell {
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
