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
        course.FindUnique(out var paths);
        course.Print(paths);
    }
}

class GolfCourse : IGrid<CourseState>, IGrid<BallPath> {
    readonly Pathfinding<CourseState> stateFinding;
    readonly Pathfinding<BallPath> pathFinding;

    readonly int width, height;
    readonly bool[,] waterCells;
    readonly List<Ball> balls = [];
    readonly List<Point> holes = [];

    public GolfCourse(int width, int height) {
        stateFinding = new Pathfinding<CourseState>(this);
        pathFinding = new Pathfinding<BallPath>(this);
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

    public void FindUnique(out Dictionary<Point, string> paths) {
        var start = new CourseState(0, holes, new Dictionary<Point, string>());
        stateFinding.TryFind(start, it => it.BallIndex >= balls.Count || it.Holes.Count == 0, out var result);
        paths = result.Paths;
    }

    public void Print(Dictionary<Point, string> paths) {
        const string empty = ".";
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                Console.Write(paths.GetValueOrDefault(new Point(x, y), empty));
            }
            Console.WriteLine();
        }
    }

    IEnumerable<CourseState> IGrid<CourseState>.Next(CourseState current) {
        var ball = balls[current.BallIndex];
        for (int i = current.Holes.Count - 1; i >= 0; i--) {
            var start = new BallPath([ball.Point], ball.Shots, current.Paths);
            var end = current.Holes[i];
            if (!pathFinding.TryFind(start, it => it.HasReached(end), out var path)) {
                continue;
            }
            var next = new CourseState(current.BallIndex + 1, [.. current.Holes], current.Paths.ToDictionary());
            next.Holes.RemoveAt(i);
            next.AddArrowPath(path.Points);
            yield return next;
        }
    }

    IEnumerable<BallPath> IGrid<BallPath>.Next(BallPath current) {
        if (current.Shots <= 0) {
            yield break;
        }
        foreach (var direction in Screen.Directions4) {
            if (!TryMove(direction, out var path)) {
                continue;
            }
            var last = path.Last();
            if (waterCells[last.X, last.Y]) {
                continue;
            }
            yield return current with { Points = [.. current.Points, .. path], Shots = current.Shots - 1 };
        }
        yield break;

        bool TryMove(Point direction, out List<Point> path) {
            var last = current.Points.Last();
            path = [];
            for (var move = 1; move <= current.Shots; move++) {
                var next = last + direction * move;
                if (!next.InBounds(width, height)
                        || current.Paths.ContainsKey(next)
                        || current.Points.Contains(next)
                        || balls.Select(it => it.Point).Contains(next)) {
                    return false;
                }
                path.Add(next);
            }
            return true;
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

readonly record struct BallPath(List<Point> Points, int Shots, Dictionary<Point, string> Paths) {
    public bool HasReached(Point end) => Points.Last() == end;

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

readonly record struct CourseState(int BallIndex, List<Point> Holes, Dictionary<Point, string> Paths) {
    public void AddArrowPath(List<Point> path) {
        for (var i = 0; i < path.Count - 1; i++) {
            var current = path[i];
            var next = path[i + 1];
            var direction = next - current;
            Paths.Add(current, Screen.ToString(direction));
        }
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
