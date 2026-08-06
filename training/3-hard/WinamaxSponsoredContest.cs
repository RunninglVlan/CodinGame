using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

/// https://www.codingame.com/training/hard/winamax-sponsored-contest
static class WinamaxSponsoredContest {
    static void Main() {
        string[] dimensions = Console.ReadLine()!.Split(' ');
        int width = Math.Clamp(int.Parse(dimensions[0]), 1, 1000);
        int height = Math.Clamp(int.Parse(dimensions[1]), 1, 1000);
        var course = new GolfCourse(width, height);
        for (var y = 0; y < height; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                course.Read(line, x, y);
            }
        }
        course.FindUniquePaths();
        course.PrintPaths();
    }
}

class GolfCourse : IGrid<BallPath> {
    readonly Pathfinding<BallPath> pathfinding;

    readonly int width, height;
    readonly bool[,] waterCells;
    readonly List<Ball> balls = [];
    readonly List<Point> holes = [];
    readonly Dictionary<Point, string> paths = [];

    public GolfCourse(int width, int height) {
        pathfinding = new Pathfinding<BallPath>(this);
        this.width = width;
        this.height = height;
        waterCells = new bool[width, height];
    }

    public void Read(string line, int x, int y) {
        char c = line[x];
        if (char.IsDigit(c)) {
            balls.Add(new Ball(new Point(x, y), (int)char.GetNumericValue(c)));
        } else if (c == 'H') {
            holes.Add(new Point(x, y));
        }
        waterCells[x, y] = c == 'W';
    }

    public void FindUniquePaths() {
        foreach (var ball in balls) {
            for (int i = holes.Count - 1; i >= 0; i--) {
                var start = new BallPath([ball.Point], ball.Shots);
                var end = holes[i];
                if (!pathfinding.TryFind(start, it => it.HasReached(end), out var path)) {
                    continue;
                }
                holes.RemoveAt(i);
                AddArrowPath(path.Points);
                break;
            }
        }
    }

    void AddArrowPath(List<Point> path) {
        for (var i = 0; i < path.Count - 1; i++) {
            var current = path[i];
            var next = path[i + 1];
            var direction = next - current;
            paths.Add(current, Screen.ToString(direction));
        }
    }

    public void PrintPaths() {
        const string empty = ".";
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                Console.Write(paths.GetValueOrDefault(new Point(x, y), empty));
            }
            Console.WriteLine();
        }
    }

    IEnumerable<BallPath> IGrid<BallPath>.Next(BallPath currentPath) {
        if (currentPath.Shots <= 0) {
            yield break;
        }
        foreach (var direction in Screen.Directions4) {
            var path = TryMove(direction).ToArray();
            if (path.Length == 0) {
                continue;
            }
            var last = path.Last();
            if (waterCells[last.X, last.Y]) {
                continue;
            }
            yield return new BallPath([.. currentPath.Points, .. path], currentPath.Shots - 1);
        }
        yield break;

        IEnumerable<Point> TryMove(Point direction) {
            var current = currentPath.Points.Last();
            for (var move = 1; move <= currentPath.Shots; move++) {
                var next = current + direction * move;
                if (next.InBounds(width, height) && !paths.ContainsKey(next) && !currentPath.Points.Contains(next)) {
                    yield return next;
                }
            }
        }
    }
}

readonly record struct Point(int X, int Y) {
    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}

readonly record struct Ball(Point Point, int Shots);

readonly record struct BallPath(List<Point> Points, int Shots) {
    public bool HasReached(Point end) => Points.Last() == end && Shots == 0;

    [SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]
    public bool Equals(BallPath other) {
        return (Points.Count > 0 && other.Points.Count > 0)
                && Points.Last().Equals(other.Points.Last())
                && Shots == other.Shots;
    }

    public override int GetHashCode() {
        return HashCode.Combine(Points.Count > 0 ? Points.Last() : default, Shots);
    }
}

static class Screen {
    static readonly Point UP = new(0, -1);
    static readonly Point DOWN = new(0, 1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static string ToString(Point direction) => direction switch {
        _ when direction == UP => "^",
        _ when direction == DOWN => "v",
        _ when direction == LEFT => "<",
        _ when direction == RIGHT => ">",
        _ => direction.ToString()
    };
}

class Pathfinding<T>(IGrid<T> grid) where T : IEquatable<T> {
    readonly Queue<T> queue = [];
    readonly HashSet<T> visited = [];

    /// BFS
    public bool TryFind(T start, Predicate<T> endPredicate, out T result) {
        queue.Clear();
        visited.Clear();
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                result = current;
                return true;
            }
            foreach (var next in grid.Next(current)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
            }
        }
        result = default!;
        return false;
    }
}

interface IGrid<T> {
    IEnumerable<T> Next(T current);
}
