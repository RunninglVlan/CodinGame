using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/medium/squares
string[] inputs = Console.ReadLine()!.Split(' ');
int gridSide = Math.Clamp(int.Parse(inputs[0]), 1, 20_000);
var grid = Rect.Square(Point.ONE, gridSide);
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
var areas = new HashSet<HashSet<Point>>();
for (var _ = 0; _ < targetTiles; _++) {
    string[] coordinates = Console.ReadLine()!.Split(' ');
    int x = Math.Clamp(int.Parse(coordinates[0]), 1, gridSide);
    int y = Math.Clamp(int.Parse(coordinates[1]), 1, gridSide);
    var tile = new Point(x, y);
    var area = areas.FirstOrDefault(it => it.Contains(tile));
    area ??= RegionArea(tile);
    areas.Add(area);
    Console.WriteLine(area.Count);
}
return;

HashSet<Point> RegionArea(Point tile) {
    var belongsToSquares = squares.Where(it => it.Contains(tile)).ToArray();
    switch (belongsToSquares.Length) {
        case 0:
            return Area(tile, grid, squares);
        case 1: {
            return Area(tile, belongsToSquares.First(), [.. squares.Except(belongsToSquares)]);
        }
        default:
            var intersection = belongsToSquares.Skip(1)
                    .Aggregate(belongsToSquares.First(), (current, it) => current.Intersect(it)!.Value);
            return Area(tile, intersection, [.. squares.Except(belongsToSquares)]);
    }
}

[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
HashSet<Point> Area(Point start, Rect included, Rect[] excluded) {
    Queue<Point> queue = [];
    HashSet<Point> visited = [start];
    queue.Enqueue(start);
    while (queue.TryDequeue(out var current)) {
        foreach (var direction in Point.Directions4) {
            var next = current + direction;
            if (!included.Contains(next) || BelongsToAny(excluded, next)) {
                continue;
            }
            if (!visited.Add(next)) {
                continue;
            }
            queue.Enqueue(next);
        }
    }
    return visited;

    bool BelongsToAny(Rect[] rects, Point point) {
        foreach (var rect in rects) {
            if (rect.Contains(point)) {
                return true;
            }
        }
        return false;
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

    public Rect? Intersect(Rect other) {
        int xMin = Math.Max(Min.X, other.Min.X);
        int xMax = Math.Min(Max.X, other.Max.X);
        int yMin = Math.Max(Min.Y, other.Min.Y);
        int yMax = Math.Min(Max.Y, other.Max.Y);
        if (xMin < xMax && yMin < yMax) {
            return new Rect(new Point(xMin, yMin), new Point(xMax, yMax));
        }
        return null;
    }
}

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
readonly record struct Point(int X, int Y) {
    public static readonly Point ONE = new(1, 1);
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}
