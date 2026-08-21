using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/medium/squares
string[] inputs = Console.ReadLine()!.Split(' ');
int gridSide = Math.Clamp(int.Parse(inputs[0]), 1, 20_000);
var grid = Rect.Square(Point.ZERO, gridSide);
int squareCount = Math.Clamp(int.Parse(inputs[1]), 1, 4);
int targetTiles = Math.Clamp(int.Parse(inputs[2]), 1, 100);
var squares = new Rect[squareCount];
for (var i = 0; i < squareCount; i++) {
    string[] squareInputs = Console.ReadLine()!.Split(' ');
    int x = Math.Clamp(int.Parse(squareInputs[0]), 1, gridSide);
    int y = Math.Clamp(int.Parse(squareInputs[1]), 1, gridSide);
    int side = Math.Clamp(int.Parse(squareInputs[2]), 1, gridSide);
    squares[i] = Rect.Square(new Point(x, y), side);
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
            return Points(grid).Except(squares.SelectMany(Points)).Count();
        case 1: {
            var points = Points(belongsToSquares.First()).ToArray();
            var otherPoints = squares.Except(belongsToSquares).SelectMany(Points).ToHashSet();
            return points.Length - points.Where(otherPoints.Contains).Count();
        }
        default:
            var firstSquare = belongsToSquares.First();
            return belongsToSquares.Skip(1)
                    .Select(Points)
                    .Aggregate(Points(firstSquare), (current, it) => current.Intersect(it))
                    .Count();
    }
}

IEnumerable<Point> Points(Rect square) {
    for (int y = square.Min.Y; y < square.Max.Y; y++) {
        for (int x = square.Min.X; x < square.Max.X; x++) {
            yield return new Point(x, y);
        }
    }
}

readonly record struct Rect(Point Min, Point Max) {
    public static Rect Square(Point min, int size) {
        var max = min + Point.ONE * size;
        return new Rect(min, max);
    }

    public bool Contains(Point point) {
        (int x, int y) = point;
        return x >= Min.X && y >= Min.Y && x < Max.X && y < Max.Y;
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
