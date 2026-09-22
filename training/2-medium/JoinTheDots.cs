using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/join-the-dots
string[] inputs = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(inputs[0]), 3, 40);
int width = Math.Clamp(int.Parse(inputs[1]), 3, 40);
var board = new Board(width, height);
board.Read();
board.JoinDots();
board.Print();

class Board(int width, int height) {
    const string DOT_CHARS = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    readonly Dictionary<int, Point> dots = [];
    readonly Dictionary<Point, char> lines = [];

    public void Read() {
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char c = row[x];
                int index = DOT_CHARS.IndexOf(c);
                if (index == -1) {
                    continue;
                }
                dots[index] = new Point(x, y);
            }
        }
    }

    public void JoinDots() {
        for (var i = 0; i < DOT_CHARS.Length; i++) {
            if (i + 1 == dots.Count) {
                break;
            }
            var current = dots[i];
            var next = dots[i + 1];
            if (current.Y == next.Y) {
                for (int x = Math.Min(current.X, next.X) + 1; x < Math.Max(current.X, next.X); x++) {
                    PlaceLine(current with { X = x }, Line.ROW);
                }
            } else if (current.X == next.X) {
                for (int y = Math.Min(current.Y, next.Y) + 1; y < Math.Max(current.Y, next.Y); y++) {
                    PlaceLine(current with { Y = y }, Line.COLUMN);
                }
            } else {
                JoinDiagonally(current, next);
            }
        }
    }

    void PlaceLine(Point point, char line) {
        if (!lines.TryGetValue(point, out char existing)) {
            lines[point] = line;
            return;
        }
        lines[point] = existing switch {
            Line.DIAGONAL_BACKWARD or Line.DIAGONAL_FORWARD => Line.CROSS_DIAGONAL,
            Line.ROW or Line.COLUMN => Line.CROSS_ORTHOGONAL,
            Line.CROSS_ORTHOGONAL or Line.CROSS_DIAGONAL => Line.CROSS_MULTIPLE,
            _ => lines[point]
        };
    }

    void JoinDiagonally(Point current, Point next) {
        var direction = Screen.DOWN + Screen.RIGHT;
        char line = Line.DIAGONAL_BACKWARD;
        if (next.X < current.X && next.Y > current.Y || next.X > current.X && next.Y < current.Y) {
            direction = Screen.DOWN + Screen.LEFT;
            line = Line.DIAGONAL_FORWARD;
        }
        bool currentIsAbove = current.Y < next.Y;
        var start = currentIsAbove ? current : next;
        var end = currentIsAbove ? next : current;
        for (var linePoint = start + direction; linePoint != end; linePoint += direction) {
            PlaceLine(linePoint, line);
        }
    }

    public void Print() {
        for (var y = 0; y < height; y++) {
            var row = string.Empty;
            for (var x = 0; x < width; x++) {
                var point = new Point(x, y);
                char c = lines.TryGetValue(point, out char line)
                        ? line
                        : dots.ContainsValue(point) ? 'o' : ' ';
                row += c;
            }
            Console.WriteLine(row.TrimEnd());
        }
    }
}

static class Line {
    public const char ROW = '-';
    public const char COLUMN = '|';
    public const char DIAGONAL_FORWARD = '/';
    public const char DIAGONAL_BACKWARD = '\\';
    public const char CROSS_ORTHOGONAL = '+';
    public const char CROSS_DIAGONAL = 'X';
    public const char CROSS_MULTIPLE = '*';
}

readonly record struct Point(int X, int Y) {
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"({X}, {Y})";
}

static class Screen {
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);
}
