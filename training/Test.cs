using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

const string testCase = """
                        10 50
                        (153, 51, 102)
                        (64, 128, 128)
                        (0, 255, 255)
                        (191, 64, 64)
                        (85, 85, 85)
                        (255, 255, 0)
                        (0, 255, 255)
                        (170, 43, 85)
                        (85, 85, 85)
                        (153, 51, 102)
                        (128, 0, 255)
                        (64, 128, 128)
                        (85, 85, 170)
                        (85, 85, 170)
                        (191, 64, 128)
                        (0, 255, 255)
                        (255, 0, 0)
                        (170, 85, 170)
                        (191, 0, 128)
                        (170, 85, 170)
                        (0, 255, 0)
                        (128, 128, 0)
                        (153, 51, 51)
                        (0, 255, 255)
                        (255, 0, 0)
                        (153, 51, 102)
                        (170, 85, 170)
                        (128, 128, 0)
                        (85, 85, 170)
                        (191, 64, 128)
                        (85, 85, 170)
                        (0, 128, 255)
                        (85, 85, 170)
                        (255, 0, 0)
                        (85, 85, 170)
                        (128, 128, 0)
                        (85, 85, 170)
                        (153, 51, 51)
                        (191, 64, 128)
                        (255, 0, 0)
                        (0, 255, 0)
                        (0, 255, 255)
                        (128, 64, 128)
                        (85, 85, 170)
                        (0, 255, 128)
                        (0, 255, 0)
                        (0, 255, 0)
                        (85, 85, 170)
                        (170, 85, 85)
                        (255, 0, 0)
                        SQUARE 78 56 96 0 255 0
                        CIRCLE 41 5 52 255 255 0
                        CIRCLE 81 0 32 255 0 0
                        SQUARE 29 53 82 255 0 0
                        CIRCLE 72 45 74 0 0 255
                        CIRCLE 91 62 60 255 0 0
                        CIRCLE 39 53 47 255 0 255
                        SQUARE 54 89 82 255 0 0
                        SQUARE 32 75 75 0 255 0
                        SQUARE 86 1 90 0 255 255
                        40 87
                        134 70
                        141 8
                        81 18
                        50 110
                        5 6
                        159 41
                        60 94
                        44 106
                        48 80
                        8 70
                        116 77
                        109 19
                        135 23
                        72 44
                        154 37
                        67 167
                        42 27
                        41 67
                        29 18
                        53 148
                        50 119
                        69 117
                        160 16
                        135 162
                        60 79
                        0 29
                        99 127
                        110 27
                        43 42
                        116 17
                        137 10
                        101 26
                        87 165
                        122 22
                        76 136
                        127 25
                        59 99
                        42 41
                        101 156
                        143 137
                        159 46
                        95 14
                        104 30
                        157 86
                        48 140
                        165 134
                        123 29
                        83 1
                        65 165
                        """;
Console.SetIn(new StringReader(testCase));

// https://www.codingame.com/training/easy/blending-colors
var canvas = new Canvas();
canvas.ReadShapes(out int points);
for (var i = 0; i < points; i++) {
    string[] coordinates = Debug.ReadLine()!.Split(' ');
    var point = Canvas.ParsePoint(coordinates[0], coordinates[1]);
    var color = canvas.MixColorsAt(point);
    string? expected = canvas.ExpectedAt(i);
    var actual = color.ToString();
    Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ expected {expected}, but found {actual}");
}

class Canvas {
    readonly Dictionary<Shape, Color> shapeColors = [];
    readonly List<string?> expected = [];

    public void ReadShapes(out int points) {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int shapeCount = Math.Clamp(int.Parse(inputs[0]), 1, 50);
        points = Math.Clamp(int.Parse(inputs[1]), 1, 50);
        for (var _ = 0; _ < points; _++) {
            expected.Add(Console.ReadLine());
        }
        for (var _ = 0; _ < shapeCount; _++) {
            string[] line = Debug.ReadLine()!.Split(' ');
            string name = line[0];
            var point = ParsePoint(line[1], line[2]);
            int length = Math.Clamp(int.Parse(line[3]), 1, 500);
            var color = Color.Parse(line[4], line[5], line[6]);
            Shape shape = name == "SQUARE" ? Square.From(point, length) : new Circle(point, length);
            shapeColors.Add(shape, color);
        }
    }

    public string? ExpectedAt(int i) => expected[i];

    public static Point ParsePoint(string x, string y) {
        return new Point(Math.Clamp(int.Parse(x), 0, 1000), Math.Clamp(int.Parse(y), 0, 1000));
    }

    public Color MixColorsAt(Point point) {
        List<Color> colors = [];
        foreach (var (shape, color) in shapeColors) {
            (bool onBorder, bool inside) = shape.Contains(point);
            if (onBorder) {
                Debug.Log($"  on the border of {shape}");
                return Color.BLACK;
            }
            if (inside) {
                Debug.Log($"  belongs to {shape} with {color}");
                colors.Add(color);
            }
        }
        if (colors.Count == 0) {
            Debug.Log("  is outside all shapes");
        }
        return Color.Mix(colors);
    }
}

abstract record Shape {
    public abstract (bool onBorder, bool inside) Contains(Point point);
}

record Square(Point Min, Point Max) : Shape {
    public static Square From(Point min, int side) {
        var max = min + Point.ONE * side;
        return new Square(min, max);
    }

    public override (bool, bool) Contains(Point point) {
        (int x, int y) = point;
        if (x < Min.X || x > Max.X || y < Min.Y || y > Max.Y) {
            return (false, false);
        }
        if (x == Min.X || x == Max.X || y == Min.Y || y == Max.Y) {
            return (true, false);
        }
        return (false, true);
    }
}

record Circle(Point Center, int Radius) : Shape {
    public override (bool, bool) Contains(Point point) {
        // todo: handle border
        int r2 = Radius * Radius;
        for (int y = Center.Y - Radius; y <= Center.Y + Radius; y++) {
            if (point.Y != y) {
                continue;
            }
            int yPart = y - Center.Y;
            var x = (int)Math.Sqrt(r2 - yPart * yPart - Center.X);
            if (point.X >= Center.X - x && point.X <= Center.X + x) {
                return (false, true);
            }
        }
        return (false, false);
    }
}

readonly record struct Point(int X, int Y) {
    public static readonly Point ONE = new(1, 1);

    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
}

readonly record struct Color(int R, int G, int B) {
    public static readonly Color BLACK = new(0, 0, 0);
    static readonly Color WHITE = new(255, 255, 255);

    public static Color Parse(string r, string g, string b) => new(
            Math.Clamp(int.Parse(r), 0, 255),
            Math.Clamp(int.Parse(g), 0, 255),
            Math.Clamp(int.Parse(b), 0, 255)
    );

    public static Color Mix(List<Color> colors) {
        return colors.Count == 0
                ? WHITE
                : new Color(MixValues(it => it.R), MixValues(it => it.G), MixValues(it => it.B));

        int MixValues(Func<Color, int> value) {
            return (int)Math.Round(colors.Select(value).Sum() / (float)colors.Count, MidpointRounding.AwayFromZero);
        }
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
