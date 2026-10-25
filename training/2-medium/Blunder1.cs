using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/blunder-episode-1
string[] dimensions = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(dimensions[0]), 4, 100);
int width = Math.Clamp(int.Parse(dimensions[1]), 4, 100);
var map = new Map(width, height);
map.Read();
var moves = map.BlunderMoves();
if (moves == null) {
    Console.WriteLine("LOOP");
    return;
}
foreach (string move in moves) {
    Console.WriteLine(move);
}

class Map(int width, int height) {
    readonly Dictionary<Point, char> map = [];

    public void Read() {
        for (var y = 0; y < height; y++) {
            string row = Debug.ReadLine()!;
            for (var x = 0; x < width; x++) {
                map[new Point(x, y)] = row[x];
            }
        }
    }

    public List<string>? BlunderMoves() {
        return null;
    }
}

static class Cells {
    public const char EMPTY = ' ';
    public const char START = '@';
    public const char SUICIDE_BOOTH = '$';
    public const char WALL = '#';
    public const char OBSTACLE = 'X';
    public const char GO_SOUTH = 'S';
    public const char GO_EAST = 'E';
    public const char GO_NORTH = 'N';
    public const char GO_WEST = 'W';
    public const char REVERSE = '|';
    public const char BEER = 'B';
    public const char TELEPORTER = 'T';
}

readonly record struct Point(int X, int Y) {
    public override string ToString() => $"({X}, {Y})";
}

static class Screen {
    public static readonly Point NORTH = new(0, -1);
    public static readonly Point SOUTH = new(0, 1);
    public static readonly Point WEST = new(-1, 0);
    public static readonly Point EAST = new(1, 0);

    public static Point[] Directions4 { get; } = [EAST, WEST, NORTH, SOUTH];

    public static string ToString(Point direction) => direction switch {
        _ when direction == NORTH => nameof(NORTH),
        _ when direction == SOUTH => nameof(SOUTH),
        _ when direction == WEST => nameof(WEST),
        _ when direction == EAST => nameof(EAST),
        _ => throw new ArgumentException($"Unsupported direction: {direction}")
    };
}

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; }

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
