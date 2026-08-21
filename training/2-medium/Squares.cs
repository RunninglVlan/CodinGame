using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/medium/squares
string[] inputs = Console.ReadLine()!.Split(' ');
int gridSide = Math.Clamp(int.Parse(inputs[0]), 1, 20_000);
var grid = new Rect(Point.ZERO, Point.ONE * gridSide);
int squareCount = Math.Clamp(int.Parse(inputs[1]), 1, 4);
int targetTiles = Math.Clamp(int.Parse(inputs[2]), 1, 100);
var squares = new Rect[squareCount];
for (var i = 0; i < squareCount; i++) {
    string[] squareInputs = Console.ReadLine()!.Split(' ');
    int x = Math.Clamp(int.Parse(squareInputs[0]), 1, gridSide);
    int y = Math.Clamp(int.Parse(squareInputs[1]), 1, gridSide);
    int side = Math.Clamp(int.Parse(squareInputs[2]), 1, gridSide);
    squares[i] = new Rect(new Point(x, y), Point.ONE * side);
}
for (var _ = 0; _ < targetTiles; _++) {
    string[] coordinates = Console.ReadLine()!.Split(' ');
    int x = Math.Clamp(int.Parse(coordinates[0]), 1, gridSide);
    int y = Math.Clamp(int.Parse(coordinates[1]), 1, gridSide);
    var tile = new Point(x, y);
    bool belongsToNone = squares.All(square => !square.Contains(tile));
    int regionArea = belongsToNone
            ? grid.Area() - squares.SelectMany(it => it.Points()).Distinct().Count()
            : -1;
    Console.WriteLine(regionArea);
}

readonly struct Rect(Point min, Point size) {
    readonly Point max = min + size;

    public bool Contains(Point point) {
        int x = point.X;
        int y = point.Y;
        return x >= min.X && y >= min.Y && x < max.X && y < max.Y;
    }

    public int Area() => size.X * size.Y;

    public IEnumerable<Point> Points() {
        for (int y = min.Y; y < max.Y; y++) {
            for (int x = min.X; x < max.X; x++) {
                yield return new Point(x, y);
            }
        }
    }
}

readonly record struct Point(int X, int Y) {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}
