using System;
using System.Collections.Generic;
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
    readonly Dictionary<Point, char> map = [];
    Blunder blunder;
    public List<string>? BlunderMoves { get; private set; }

    public void Read() {
        for (var y = 0; y < height; y++) {
            string row = Debug.ReadLine()!;
            for (var x = 0; x < width; x++) {
                map[new Point(x, y)] = row[x];
            }
        }
    }

    public void MoveBlunder() {
        var start = map.First(it => it.Value == Cells.START).Key;
        blunder = new Blunder([start]);
        Debug.Log(start);
        while (map[blunder.Point] != Cells.SUICIDE_BOOTH) {
            foreach (var direction in Screen.Directions4) {
                var next = blunder.Point + direction;
                Debug.Log(next);
                if (map[next] is Cells.WALL or Cells.OBSTACLE) {
                    Debug.Log("changing direction", 1);
                    continue;
                }
                blunder.Path.Add(next);
                Debug.Log($"moved {Screen.ToString(direction)}", 1);
                break;
            }
        }
        BlunderMoves = blunder.Moves();
    }
}

readonly record struct Blunder(List<Point> Path) {
    public Point Point => Path.Last();

    public List<string> Moves() {
        List<string> moves = [];
        for (var i = 1; i < Path.Count; i++) {
            var direction = Path[i] - Path[i - 1];
            moves.Add(Screen.ToString(direction));
        }
        return moves;
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
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"({X}, {Y})";
}

static class Screen {
    public static readonly Point NORTH = new(0, -1);
    public static readonly Point SOUTH = new(0, 1);
    public static readonly Point WEST = new(-1, 0);
    public static readonly Point EAST = new(1, 0);

    public static Point[] Directions4 { get; } = [SOUTH, EAST, NORTH, WEST];

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
