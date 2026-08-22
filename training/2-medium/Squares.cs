using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/medium/squares
var regions = new Regions();
regions.ReadSquares(out int targetTiles);
for (var _ = 0; _ < targetTiles; _++) {
    var tile = regions.ReadTile();
    var region = regions.Get(tile);
    int area = region.Select(it => it.Size.X * it.Size.Y).Sum();
    Console.WriteLine(area);
}

class Regions {
    Rect grid;
    Rect[] squares = null!;
    readonly HashSet<List<Rect>> regions = [];
    readonly Queue<Point> queue = [];

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

    public List<Rect> Get(Point tile) {
        var region = regions.FirstOrDefault(it => it.Any(rect => rect.Contains(tile)));
        if (region != null) {
            return region;
        }
        var belongsToSquares = squares.Where(it => it.Contains(tile)).ToArray();
        switch (belongsToSquares.Length) {
            case 0:
                return Region(tile, grid, squares);
            case 1: {
                return Region(tile, belongsToSquares.First(), [.. squares.Except(belongsToSquares)]);
            }
            default:
                var intersection = belongsToSquares.Skip(1)
                        .Aggregate(belongsToSquares.First(), (current, it) => current.Intersect(it)!.Value);
                return Region(tile, intersection, [.. squares.Except(belongsToSquares)]);
        }
    }

    /// Flood fill
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    List<Rect> Region(Point start, Rect included, Rect[] excluded) {
        var visited = new List<Rect>();
        queue.Clear();
        queue.Enqueue(start);
        Visit(start);
        while (queue.TryDequeue(out var current)) {
            foreach (var direction in Point.Directions4) {
                var next = current + direction;
                if (!included.Contains(next) || BelongsToAny(next, excluded)) {
                    continue;
                }
                if (visited.Any(it => it.Contains(next))) {
                    continue;
                }
                Visit(next);
            }
        }
        regions.Add(visited);
        return visited;

        void Visit(Point point) {
            var rect = Grow(point);
            visited.Add(rect);
            // add border, ignore duplicate corners, refactor?
            for (int y = rect.Min.Y; y < rect.Max.Y; y++) {
                queue.Enqueue(rect.Min with { Y = y });
                queue.Enqueue(new Point(rect.Max.X - 1, y));
            }
            for (int x = rect.Min.X; x < rect.Max.X; x++) {
                queue.Enqueue(rect.Min with { X = x });
                queue.Enqueue(new Point(x, rect.Max.Y - 1));
            }
        }

        Rect Grow(Point point) {
            var rect = Rect.Square(point, 1);
            while (true) {
                var previous = rect;
                foreach (var direction in Point.Directions4) {
                    var bigger = GrowInDirection(rect, direction);
                    if (!included.Contains(bigger)
                            || IntersectsAny(bigger, visited)
                            || IntersectsAny(bigger, excluded)) {
                        continue;
                    }
                    rect = bigger;
                }
                if (rect == previous) {
                    break;
                }
            }
            return rect;
        }

        Rect GrowInDirection(Rect rect, Point direction) {
            var min = rect.Min + Point.Min(direction, Point.ZERO);
            var max = rect.Max + Point.Max(direction, Point.ZERO);
            return new Rect(min, max);
        }

        bool IntersectsAny(Rect rect, IEnumerable<Rect> others) {
            foreach (var other in others) {
                if (rect.Intersects(other)) {
                    return true;
                }
            }
            return false;
        }

        bool BelongsToAny(Point point, Rect[] rects) {
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

    public bool Contains(Rect other) {
        // refactor
        return Contains(other.Min) && Contains(new Point(other.Max.X - 1, other.Max.Y - 1));
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

    public bool Intersects(Rect other) => Intersect(other) is not null;
}

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
readonly record struct Point(int X, int Y) {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point Min(Point a, Point b) => new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
    public static Point Max(Point a, Point b) => new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}
