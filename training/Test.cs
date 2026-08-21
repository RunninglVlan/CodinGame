using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;


const string testCase = """
                        1000 4 10
                        1 1 700
                        501 501 500
                        499 500 3
                        10 900 2
                        2 3
                        4 5
                        6 7
                        100 200
                        400 753
                        500 500
                        999 1
                        533 599
                        1000 1000
                        501 502
                        """;
Console.SetIn(new StringReader(testCase));

// https://www.codingame.com/training/medium/squares
var regions = new Regions();
regions.ReadSquares(out int targetTiles);
for (var _ = 0; _ < targetTiles; _++) {
    var stopwatch = Stopwatch.StartNew();
    var tile = regions.ReadTile();
    Console.WriteLine(regions.Area(tile).Count);
    Console.Error.WriteLine($"  elapsed: {stopwatch.ElapsedMilliseconds}");
}

class Regions {
    Rect grid;
    Rect[] squares = null!;
    readonly HashSet<HashSet<Point>> areas = [];
    readonly Queue<Point> queue = [];
    readonly HashSet<Point> visited = [];

    public void ReadSquares(out int targetTiles) {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int gridSide = Math.Clamp(int.Parse(inputs[0]), 1, 20_000);
        grid = Rect.Square(Point.ONE, gridSide);
        int squareCount = Math.Clamp(int.Parse(inputs[1]), 1, 4);
        targetTiles = Math.Clamp(int.Parse(inputs[2]), 1, 100);
        squares = new Rect[squareCount];
        for (var i = 0; i < squareCount; i++) {
            string[] squareInputs = Console.ReadLine()!.Split(' ');
            int x = Math.Clamp(int.Parse(squareInputs[0]), 1, gridSide);
            int y = Math.Clamp(int.Parse(squareInputs[1]), 1, gridSide);
            int side = Math.Clamp(int.Parse(squareInputs[2]), 1, gridSide);
            squares[i] = Rect.Square(new Point(x, y), side);
        }
    }

    public Point ReadTile() {
        string[] coordinates = Console.ReadLine()!.Split(' ');
        int x = Math.Clamp(int.Parse(coordinates[0]), 1, grid.Size.X);
        int y = Math.Clamp(int.Parse(coordinates[1]), 1, grid.Size.X);
        return new Point(x, y);
    }

    public HashSet<Point> Area(Point tile) {
        var area = areas.FirstOrDefault(it => it.Contains(tile));
        if (area != null) {
            return area;
        }
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

    /// Flood fill
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    HashSet<Point> Area(Point start, Rect included, Rect[] excluded) {
        queue.Clear();
        visited.Clear();
        queue.Enqueue(start);
        visited.Add(start);
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
        areas.Add(visited);
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
}

readonly record struct Rect(Point Min, Point Max) {
    public Point Size => Max - Min;

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
