using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/medium/squares
string[] inputs = Console.ReadLine()!.Split(' ');
int gridSide = Math.Clamp(int.Parse(inputs[0]), 1, 20_000);
var grid = Rect.From(Point.ZERO, gridSide);
int squareCount = Math.Clamp(int.Parse(inputs[1]), 1, 4);
int targetTiles = Math.Clamp(int.Parse(inputs[2]), 1, 100);
var squares = new Rect[squareCount];
for (var i = 0; i < squareCount; i++) {
    string[] squareInputs = Console.ReadLine()!.Split(' ');
    int x = Math.Clamp(int.Parse(squareInputs[0]), 1, gridSide);
    int y = Math.Clamp(int.Parse(squareInputs[1]), 1, gridSide);
    int side = Math.Clamp(int.Parse(squareInputs[2]), 1, gridSide);
    squares[i] = Rect.From(new Point(x, y), side);
}
for (var _ = 0; _ < targetTiles; _++) {
    string[] coordinates = Console.ReadLine()!.Split(' ');
    int x = Math.Clamp(int.Parse(coordinates[0]), 1, gridSide);
    int y = Math.Clamp(int.Parse(coordinates[1]), 1, gridSide);
    var tile = new Point(x, y);
    Console.WriteLine(RegionArea(tile));
}
return;

int RegionArea(Point tile) {
    var belongsToSquares = squares.Where(it => it.Contains(tile)).ToArray();
    switch (belongsToSquares.Length) {
        case 0:
            return grid.Area() - squares.SelectMany(it => it.Points()).Distinct().Count();
        case 1: {
            var squarePoints = belongsToSquares.First().Points().ToList();
            var otherPoints = squares.Except(belongsToSquares).SelectMany(it => it.Points()).Distinct().ToHashSet();
            for (int i = squarePoints.Count - 1; i >= 0; i--) {
                var point = squarePoints[i];
                if (otherPoints.Contains(point)) {
                    squarePoints.RemoveAt(i);
                }
            }
            return squarePoints.Count;
        }
        default:
            var points = belongsToSquares.First().Points();
            for (var i = 1; i < belongsToSquares.Length; i++) {
                var square = belongsToSquares[i];
                points = points.Intersect(square.Points());
            }
            return points.Count();
    }
}

readonly record struct Rect(Point Min, Point Max) {
    Point Size => Max - Min;

    public static Rect From(Point topLeft, int size) {
        var max = topLeft + Point.ONE * size;
        return new Rect(topLeft, max);
    }

    public bool Contains(Point point) {
        int x = point.X;
        int y = point.Y;
        return x >= Min.X && y >= Min.Y && x < Max.X && y < Max.Y;
    }

    public int Area() => Size.X * Size.Y;

    public IEnumerable<Point> Points() {
        for (int y = Min.Y; y < Max.Y; y++) {
            for (int x = Min.X; x < Max.X; x++) {
                yield return new Point(x, y);
            }
        }
    }
}

readonly record struct Point(int X, int Y) {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}
