using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

const string testCase = """
                        50 50
                        (255, 255, 0)
                        (255, 0, 255)
                        (128, 255, 0)
                        (255, 255, 0)
                        (128, 191, 0)
                        (128, 255, 0)
                        (255, 128, 0)
                        (64, 191, 191)
                        (255, 0, 255)
                        (128, 255, 0)
                        (128, 255, 0)
                        (255, 0, 255)
                        (85, 170, 170)
                        (128, 128, 128)
                        (128, 255, 0)
                        (128, 255, 0)
                        (191, 128, 128)
                        (128, 0, 128)
                        (85, 85, 85)
                        (64, 128, 191)
                        (0, 128, 128)
                        (128, 255, 0)
                        (128, 191, 0)
                        (255, 0, 255)
                        (255, 0, 255)
                        (255, 0, 255)
                        (170, 170, 85)
                        (128, 0, 128)
                        (170, 85, 85)
                        (51, 153, 204)
                        (0, 128, 128)
                        (0, 255, 0)
                        (170, 85, 170)
                        (255, 0, 255)
                        (255, 255, 0)
                        (128, 191, 128)
                        (255, 255, 0)
                        (128, 255, 0)
                        (128, 128, 128)
                        (102, 102, 204)
                        (128, 128, 128)
                        (255, 255, 0)
                        (85, 85, 170)
                        (255, 0, 255)
                        (128, 128, 255)
                        (0, 255, 255)
                        (153, 153, 102)
                        (255, 0, 0)
                        (255, 0, 255)
                        (128, 128, 128)
                        CIRCLE 733 830 266 255 0 0
                        SQUARE 494 228 33 0 255 255
                        CIRCLE 938 272 3 0 0 255
                        SQUARE 201 570 7 0 255 0
                        SQUARE 260 936 24 255 0 0
                        CIRCLE 177 120 206 0 0 255
                        SQUARE 412 725 114 0 255 0
                        SQUARE 40 559 208 255 0 0
                        CIRCLE 810 84 98 255 0 0
                        CIRCLE 867 613 6 255 255 0
                        CIRCLE 845 531 146 255 0 0
                        SQUARE 975 325 172 0 0 255
                        SQUARE 489 485 69 255 0 0
                        SQUARE 421 758 12 255 0 0
                        CIRCLE 877 315 217 255 255 0
                        SQUARE 740 941 216 255 0 0
                        CIRCLE 531 794 286 0 255 255
                        SQUARE 280 783 76 0 255 255
                        CIRCLE 780 89 42 255 0 255
                        SQUARE 98 100 112 0 255 0
                        CIRCLE 91 189 128 255 0 0
                        CIRCLE 219 216 42 255 255 0
                        CIRCLE 440 784 83 255 0 255
                        SQUARE 276 203 199 0 255 0
                        SQUARE 688 278 198 0 255 0
                        CIRCLE 363 820 25 255 0 0
                        SQUARE 342 153 35 255 255 0
                        CIRCLE 384 734 35 255 0 0
                        SQUARE 608 938 5 255 0 0
                        SQUARE 558 97 57 255 255 0
                        SQUARE 627 793 200 0 0 255
                        SQUARE 489 501 205 0 255 255
                        CIRCLE 910 407 299 0 255 0
                        CIRCLE 764 914 282 0 255 255
                        SQUARE 843 358 10 255 0 255
                        CIRCLE 402 229 210 255 0 255
                        SQUARE 373 802 205 255 255 0
                        CIRCLE 452 601 249 255 0 0
                        CIRCLE 185 736 230 255 255 0
                        SQUARE 860 897 236 0 255 255
                        SQUARE 875 432 57 0 255 255
                        SQUARE 140 998 162 0 0 255
                        SQUARE 897 383 154 255 0 0
                        SQUARE 162 1 168 0 255 0
                        SQUARE 748 876 63 0 255 255
                        SQUARE 255 959 180 255 0 255
                        CIRCLE 443 541 159 0 255 255
                        CIRCLE 116 17 13 0 0 255
                        SQUARE 973 559 262 255 0 0
                        CIRCLE 644 786 121 255 0 255
                        32 618
                        427 62
                        783 207
                        227 835
                        769 464
                        971 283
                        198 763
                        551 636
                        480 46
                        977 124
                        925 119
                        528 275
                        884 922
                        402 442
                        687 398
                        906 201
                        463 847
                        119 290
                        137 156
                        713 978
                        207 4
                        770 270
                        762 455
                        464 78
                        419 139
                        576 188
                        324 704
                        107 82
                        424 382
                        780 931
                        226 13
                        952 647
                        349 176
                        569 227
                        139 943
                        562 991
                        85 801
                        947 171
                        944 948
                        641 810
                        834 871
                        585 105
                        267 141
                        580 328
                        368 963
                        335 948
                        434 831
                        894 87
                        319 997
                        650 639
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
    if (expected != actual) {
        canvas.Log();
    }
    Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ expected {expected}, but found {actual}");
}

class Canvas {
    readonly List<(Shape, Color)> shapeColors = [];
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
            shapeColors.Add((shape, color));
        }
    }

    public string? ExpectedAt(int i) => expected[i];

    public static Point ParsePoint(string x, string y) {
        return new Point(Math.Clamp(int.Parse(x), 0, 1000), Math.Clamp(int.Parse(y), 0, 1000));
    }

    public Color MixColorsAt(Point point) {
        log.Clear();
        List<Color> colors = [];
        foreach (var (shape, color) in shapeColors) {
            log.AppendLine($"  {shape} with {color}");
            if (!shape.Contains(point, out bool onBorder)) {
                continue;
            }
            if (onBorder) {
                log.AppendLine("    has point on the border");
                return Color.BLACK;
            }
            log.AppendLine("    contains inside");
            colors.Add(color);
        }
        if (colors.Count == 0) {
            log.AppendLine("  is outside all shapes");
        }
        return colors.Count == 0 ? Color.WHITE : new Color(Mix(it => it.R), Mix(it => it.G), Mix(it => it.B));

        int Mix(Func<Color, int> value) => (int)Math.Round(colors.Average(value), MidpointRounding.AwayFromZero);
    }

    readonly StringBuilder log = new();

    public void Log() => Debug.Log(log);
}

abstract record Shape {
    public abstract bool Contains(Point point, out bool onBorder);
}

record Square(Point Min, Point Max) : Shape {
    public static Square From(Point min, int side) {
        var max = min + Point.ONE * side;
        return new Square(min, max);
    }

    public override bool Contains(Point point, out bool onBorder) {
        onBorder = false;
        (int x, int y) = point;
        if (x < Min.X || x > Max.X || y < Min.Y || y > Max.Y) {
            return false;
        }
        if (x == Min.X || x == Max.X || y == Min.Y || y == Max.Y) {
            onBorder = true;
        }
        return true;
    }
}

record Circle(Point Center, int Radius) : Shape {
    public override bool Contains(Point point, out bool onBorder) {
        onBorder = false;
        if (point == new Point(551, 636)) {
            Debug.Log();
        }
        double r = Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2));
        if (r < Radius) {
            return true;
        }
        if (!Mathf.Approximately(r, Radius)) {
            return false;
        }
        onBorder = true;
        return true;
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

    public override string ToString() => $"({R}, {G}, {B})";
}

static class Mathf {
    public static bool Approximately(double a, double b) => Math.Abs(a - b) < 0.001;
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
