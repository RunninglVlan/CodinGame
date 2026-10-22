using System;
using System.Linq;
using System.Text;

Debug.Enabled = true;
// https://www.codingame.com/contribute/view/1508259d38553ca4273153ae30ab92e5b04982
string[] inputs = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(inputs[0]), 1, 50);
int width = Math.Clamp(int.Parse(inputs[1]), 1, 50);
Math.Clamp(int.Parse(Console.ReadLine()!), 1, 150); // logLength
var transformations = Debug.ReadLine()!.Split("  ")
        .Select(it => it.Split(' '))
        .Select(it => {
            int radius = int.Parse(it[3]);
            int x = Math.Clamp(int.Parse(it[1]), radius, width - radius - 1);
            int y = Math.Clamp(int.Parse(it[0]), radius, height - radius - 1);
            int turns = Math.Clamp(int.Parse(it[2]), -10_000, 10_000);
            return new Transformation(new Point(x, y), turns, radius);
        })
        .ToArray();
var image = new Image(width, height);
image.Read();
Array.Reverse(transformations);
image.Reconstruct(transformations);
Console.WriteLine(image);

/// Transformation square
/// <param name="Turns">90° rotations, positive - CCW, negative - CW</param>
readonly record struct Transformation(Point Center, int Turns, int Radius);

class Image(int width, int height) {
    readonly char[,] cells = new char[width, height];

    public void Read() {
        for (var y = 0; y < height; y++) {
            string line = Debug.ReadLine()!;
            for (var x = 0; x < width; x++) {
                cells[x, y] = line[x];
            }
        }
    }

    public void Reconstruct(Transformation[] transformations) {
        foreach (var transformation in transformations) {
            var min = transformation.Center - transformation.Radius;
            var max = transformation.Center + transformation.Radius;
            int squareWidth = max.X - min.X + 1;
            int squareHeight = max.Y - min.Y + 1;
            // copy
            var square = new char[squareWidth, squareHeight];
            for (int y = min.Y; y <= max.Y; y++) {
                for (int x = min.X; x <= max.X; x++) {
                    square[x - min.X, y - min.Y] = cells[x, y];
                }
            }
            Debug.Log("original", 1);
            DrawSquare(square, squareWidth, squareHeight);
            int turns = transformation.Turns < 0 ? -transformation.Turns % 4 : 4 - transformation.Turns % 4;
            for (var i = 0; i < turns; i++) {
                square = TurnSquare(square, squareWidth, squareHeight, transformation.Radius);
                Debug.Log($"turned {i + 1} CW", 1);
                DrawSquare(square, squareWidth, squareHeight);
            }
            // paste
            for (int y = min.Y; y <= max.Y; y++) {
                for (int x = min.X; x <= max.X; x++) {
                    cells[x, y] = square[x - min.X, y - min.Y];
                }
            }
        }
    }

    static void DrawSquare(char[,] square, int squareWidth, int squareHeight) {
        for (var y = 0; y < squareHeight; y++) {
            var line = "";
            for (var x = 0; x < squareWidth; x++) {
                line += square[x, y];
            }
            Debug.Log(line);
        }
    }

    /// Turns square CW
    static char[,] TurnSquare(char[,] source, int squareWidth, int squareHeight, int radius) {
        var target = new char[squareWidth, squareHeight];
        target[radius, radius] = source[radius, radius];
        for (var r = 1; r <= radius; r++) {
            int side = 2 * r + 1;
            Debug.Log($"r: {r}");
            Debug.Log("x: 0, y: 0..side-1");
            for (int x = 0, y = 0; x < side - 1; x++) {
                char value = SourceAt(x, y, r);
                SetTargetAt(side - 1, x, r, value);
                Log(r, x, y, side - 1, x);
            }
            Debug.Log("x: side-1, y: 0..side-1");
            for (int x = side - 1, y = 0; y < side - 1; y++) {
                char value = SourceAt(x, y, r);
                SetTargetAt(side - 1 - y, x, r, value);
                Log(r, x, y, side - 1 - y, x);
            }
            Debug.Log("x: side-1, y: side-1..0");
            for (int x = side - 1, y = side - 1; x > 0; x--) {
                char value = SourceAt(x, y, r);
                SetTargetAt(0, x, r, value);
                Log(r, x, y, 0, x);
            }
            Debug.Log("x: 0, y: side-1..0");
            for (int x = 0, y = side - 1; y > 0; y--) {
                char value = SourceAt(x, y, r);
                SetTargetAt(side - 1 - y, 0, r, value);
                Log(r, x, y, side - 1 - y, 0);
            }
        }
        return target;

        char SourceAt(int x, int y, int r) => source[x + radius - r, y + radius - r];
        void SetTargetAt(int x, int y, int r, char value) => target[x + radius - r, y + radius - r] = value;

        void Log(int r, int sx, int sy, int tx, int ty) {
            Debug.Log($"source: {sx}, {sy} ({sx + radius - r}, {sy + radius - r}), target: {tx}, {ty} ({tx + radius - r}, {ty + radius - r})", 1);
        }
    }

    public override string ToString() {
        var lines = new StringBuilder();
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                lines.Append(cells[x, y]);
            }
            if (y < height - 1) {
                lines.AppendLine();
            }
        }
        return lines.ToString();
    }
}

readonly record struct Point(int X, int Y) {
    public static Point operator +(Point a, int value) => new(a.X + value, a.Y + value);
    public static Point operator -(Point a, int value) => new(a.X - value, a.Y - value);
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
