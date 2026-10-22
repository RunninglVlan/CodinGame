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
            DrawSquare(square, squareWidth, squareHeight);
            square = TurnSquare(square, squareWidth, squareHeight, transformation);
            DrawSquare(square, squareWidth, squareHeight);
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

    static char[,] TurnSquare(char[,] square, int squareWidth, int squareHeight, Transformation transformation) {
        var turned = new char[squareWidth, squareHeight];
        var center = new Point(transformation.Radius, transformation.Radius);
        turned[center.X, center.Y] = square[center.X, center.Y];
        for (var radius = 1; radius <= transformation.Radius; radius++) {
            int side = 2 * radius + 1;
            for (int x = 0, y = 0; x < side; x++) {
                char value = SquareAt(x, y, radius);
                SetTurnedAt(side - 1, x, radius, value);
            }
            for (int x = side - 1, y = 0; y < side; y++) {
                char value = SquareAt(x, y, radius);
                SetTurnedAt(side - 1 - y, x, radius, value);
            }
            for (int x = side - 1, y = side - 1; x > 0; x--) {
                char value = SquareAt(x, y, radius);
                SetTurnedAt(0, side - 1 - x, radius, value);
            }
            for (int x = 0, y = side - 1; y > 0; y--) {
                char value = SquareAt(x, y, radius);
                SetTurnedAt(side - 1 - y, 0, radius, value);
            }
        }
        return turned;

        char SquareAt(int x, int y, int r) => square[x + center.X - r, y + center.Y - r];
        void SetTurnedAt(int x, int y, int r, char value) => turned[x + center.X - r, y + center.Y - r] = value;
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
