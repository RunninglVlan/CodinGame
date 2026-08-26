using System;
using System.IO;

const string testCase = """
                        3 6
                        SQUARE 0 5 6 255 0 0
                        SQUARE 4 2 3 0 255 0
                        CIRCLE 4 6 3 0 0 255
                        0 0
                        (255, 255, 255)
                        4 5
                        (0, 0, 0)
                        2 6
                        (128, 0, 128)
                        5 3
                        (0, 255, 0)
                        5 4
                        (0, 128, 128)
                        3 4
                        (0, 0, 255)
                        """;
Console.SetIn(new StringReader(testCase));

// https://www.codingame.com/training/easy/blending-colors
string[] inputs = Console.ReadLine()!.Split(' ');
int shapeCount = Math.Clamp(int.Parse(inputs[0]), 1, 50);
int points = Math.Clamp(int.Parse(inputs[1]), 1, 50);
var shapes = new Shape[shapeCount];
for (var i = 0; i < shapeCount; i++) {
    string[] line = Debug.ReadLine()!.Split(' ');
    string name = line[0];
    var point = ParsePoint(line[1], line[2]);
    int length = Math.Clamp(int.Parse(line[3]), 1, 500);
    var color = Color.Parse(line[4], line[5], line[6]);
    shapes[i] = name == "SQUARE" ? Square.From(point, length, color) : new Circle(point, length, color);
}
for (var _ = 0; _ < points; _++) {
    string[] coordinates = Debug.ReadLine()!.Split(' ');
    var point = ParsePoint(coordinates[0], coordinates[1]);
    Color? color = null;
    foreach (var shape in shapes) {
        if (shape.Contains(point)) {
            Debug.Log($"  belongs to {shape}");
            color ??= Color.BLACK;
            color = color.Value.Mix(shape.Color);
        }
    }
    if (color == null) {
        Debug.Log("  is outside all shapes");
    }
    color ??= Color.WHITE;
    string? expected = Console.ReadLine();
    var actual = color.ToString();
    Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ expected {expected}, but found {actual}");
}
return;

Point ParsePoint(string x, string y) => new(Math.Clamp(int.Parse(x), 0, 1000), Math.Clamp(int.Parse(y), 0, 1000));

abstract record Shape(Color Color) {
    public abstract bool Contains(Point point);
}

record Square(Point Min, Point Max, Color Color) : Shape(Color) {
    public static Square From(Point min, int side, Color color) {
        var max = min + Point.ONE * side;
        return new Square(min, max, color);
    }

    public override bool Contains(Point point) {
        (int x, int y) = point;
        return x >= Min.X && y >= Min.Y && x < Max.X && y < Max.Y;
    }
}

record Circle(Point Center, int Radius, Color Color) : Shape(Color) {
    public override bool Contains(Point point) {
        int r2 = Radius * Radius;
        for (int y = Center.Y - Radius; y < Center.Y + Radius; y++) {
            if (point.Y != y) {
                continue;
            }
            var x = (int)Math.Sqrt(r2 - y * y);
            if (point.X >= Center.X - x && point.X < Center.X + x) {
                return true;
            }
        }
        return false;
    }
}

readonly record struct Point(int X, int Y) {
    public static readonly Point ONE = new(1, 1);

    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"({X}, {Y})";
}

readonly record struct Color(int R, int G, int B) {
    public static readonly Color BLACK = new(0, 0, 0);
    public static readonly Color WHITE = new(255, 255, 255);

    public static Color Parse(string r, string g, string b) => new(
            Math.Clamp(int.Parse(r), 0, 255),
            Math.Clamp(int.Parse(g), 0, 255),
            Math.Clamp(int.Parse(b), 0, 255)
    );

    public Color Mix(Color other) {
        // todo: improve
        return new Color((R + other.R) / 2, (G + other.G) / 2, (B + other.B) / 2);
    }

    public override string ToString() => $"({R}, {G}, {B})";
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(line);
        Console.ResetColor();
    }
}
