using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/medium/squares
var regions = new Regions();
regions.ReadSquares(out int targetTiles);
for (var _ = 0; _ < targetTiles; _++) {
    var tile = regions.ReadTile();
    Console.WriteLine(regions.Area(tile).Count);
}

class Regions {
    Rect grid;
    Rect[] squares = null!;
    readonly HashSet<Area> areas = [];
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

    public Area Area(Point tile) {
        var area = areas.Where(it => it.Contains(tile)).Cast<Area?>().FirstOrDefault();
        if (area != null) {
            return area.Value;
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
    Area Area(Point start, Rect included, Rect[] excluded) {
        queue.Clear();
        visited.Clear();
        var startRect = GrowFromStart();
        AddStart();
        while (queue.TryDequeue(out var current)) {
            foreach (var direction in Point.Directions4) {
                var next = current + direction;
                if (!included.Contains(next) || startRect.Contains(next) ||  BelongsToAny(next, excluded)) {
                    continue;
                }
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
            }
        }
        var area = new Area(startRect, [.. visited]);
        areas.Add(area);
        return area;

        Rect GrowFromStart() {
            var rect = Rect.Square(start, 1);
            while (true) {
                var previous = rect;
                foreach (var direction in Point.Directions4) {
                    var bigger = GrowInDirection(rect, direction);
                    if (!included.Contains(bigger) || IntersectsAny(bigger, excluded)) {
                        continue;
                    }
                    rect = bigger;
                }
                if (rect == previous) {
                    break;
                }
            }
            return rect.Size.X < 3 || rect.Size.Y < 3
                    ? Rect.Square(start, 0)
                    : new Rect(rect.Min + Point.ONE, rect.Max - Point.ONE);
        }

        Rect GrowInDirection(Rect rect, Point direction) {
            var min = rect.Min + Point.Min(direction, Point.ZERO);
            var max = rect.Max + Point.Max(direction, Point.ZERO);
            return new Rect(min, max);
        }

        bool BelongsToAny(Point point, Rect[] others) {
            foreach (var other in others) {
                if (other.Contains(point)) {
                    return true;
                }
            }
            return false;
        }

        bool IntersectsAny(Rect rect, Rect[] others) {
            foreach (var other in others) {
                if (rect.Intersects(other)) {
                    return true;
                }
            }
            return false;
        }

        void AddStart() {
            if (startRect.Size == Point.ZERO) {
                queue.Enqueue(start);
                visited.Add(start);
                return;
            }
            for (int y = startRect.Min.Y - 1; y <= startRect.Max.Y; y++) {
                for (int x = startRect.Min.X - 1; x <= startRect.Max.X; x++) {
                    bool edge = x == startRect.Min.X - 1
                            || y == startRect.Min.Y - 1
                            || x == startRect.Max.X
                            || y == startRect.Max.Y;
                    if (!edge) {
                        continue;
                    }
                    var next = new Point(x, y);
                    queue.Enqueue(next);
                    visited.Add(next);
                    break;
                }
            }
        }
    }
}

readonly record struct Area(Rect Rect, HashSet<Point> Points) {
    public int Count => Rect.Size.X * Rect.Size.Y + Points.Count;

    public bool Contains(Point tile) => Rect.Contains(tile) || Points.Contains(tile);
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

    public override string ToString() => $"{Min} {Size}";
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
