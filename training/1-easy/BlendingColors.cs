using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/blending-colors
var canvas = new Canvas();
canvas.ReadShapes(out int points);
for (var _ = 0; _ < points; _++) {
    string[] coordinates = Debug.ReadLine()!.Split(' ');
    var point = Canvas.ParsePoint(coordinates[0], coordinates[1]);
    var color = canvas.MixColorsAt(point);
    Console.WriteLine(color);
}

class Canvas {
    readonly List<(Shape, Color)> shapeColors = [];

    public void ReadShapes(out int points) {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int shapeCount = Math.Clamp(int.Parse(inputs[0]), 1, 50);
        points = Math.Clamp(int.Parse(inputs[1]), 1, 50);
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

    public static Point ParsePoint(string x, string y) {
        return new Point(Math.Clamp(int.Parse(x), 0, 1000), Math.Clamp(int.Parse(y), 0, 1000));
    }

    public Color MixColorsAt(Point point) {
        List<Color> colors = [];
        foreach (var (shape, color) in shapeColors) {
            if (!shape.Contains(point, out bool onBorder)) {
                continue;
            }
            if (onBorder) {
                return Color.BLACK;
            }
            colors.Add(color);
        }
        return colors.Count == 0 ? Color.WHITE : new Color(Mix(it => it.R), Mix(it => it.G), Mix(it => it.B));

        int Mix(Func<Color, int> value) => (int)Math.Round(colors.Average(value), MidpointRounding.AwayFromZero);
    }
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
        int yBottom = Center.Y - Radius, yTop = Center.Y + Radius;
        if (point.Y < yBottom || point.Y > yTop) {
            return false;
        }
        double x = Math.Sqrt(Math.Pow(Radius, 2) - Math.Pow(point.Y - Center.Y, 2));
        double xLeft = Center.X - x, xRight = Center.X + x;
        if (point.X < xLeft || point.X > xRight) {
            return false;
        }
        if (Mathf.Approximately(point.X, xLeft) || Mathf.Approximately(point.X, xRight)) {
            onBorder = true;
        }
        return true;
    }
}

readonly record struct Point(int X, int Y) {
    public static readonly Point ONE = new(1, 1);

    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
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
    public static bool Approximately(double a, double b) => Math.Abs(a - b) < 0.01;
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }
}
