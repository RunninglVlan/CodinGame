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

class GolfCourse : IGrid<Ball> {
    readonly Pathfinding<Ball> pathfinding;

    readonly int width, height;
    readonly bool[,] waterCells;
    readonly List<Ball> balls = [];
    readonly List<Point> holes = [];
    readonly Dictionary<Point, string> paths = [];

    public GolfCourse(int width, int height) {
        pathfinding = new Pathfinding<Ball>(this);
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

    [SuppressMessage("ReSharper", "UseCollectionExpression")]
    public void FindUniquePaths() {
        foreach (var ball in balls) {
            for (int i = holes.Count - 1; i >= 0; i--) {
                var end = holes[i];
                var path = pathfinding.Path(ball, new Ball(end, 0));
                if (path == null) {
                    continue;
                }
                holes.RemoveAt(i);
                AddArrowPath(path.Prepend(ball).ToArray());
                break;
            }
        }
    }

    void AddArrowPath(Ball[] path) {
        for (var i = 0; i < path.Length - 1; i++) {
            var current = path[i];
            var next = path[i + 1];
            var direction = (next.Point - current.Point).Normalized();
            for (var move = 0; move < current.Shots; move++) {
                var point = current.Point + direction * move;
                paths.Add(point, Screen.ToString(direction));
            }
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

    IEnumerable<Ball> IGrid<Ball>.Next(Ball current) {
        if (current.Shots <= 0) {
            yield break;
        }
        foreach (var direction in Screen.Directions4) {
            if (!TryMove(direction, current.Shots, out var next)) {
                continue;
            }
            if (waterCells[next.X, next.Y]) {
                continue;
            }
            yield return new Ball(next, current.Shots - 1);
        }
        yield break;

        bool TryMove(Point direction, int moves, out Point next) {
            next = default;
            for (var move = 1; move <= moves; move++) {
                next = current.Point + direction * move;
                if (!next.InBounds(width, height) || paths.ContainsKey(next)) {
                    return false;
                }
            }
            return true;
        }
    }
}

readonly record struct Point(int X, int Y) {
    public Point Normalized() => new(Math.Sign(X), Math.Sign(Y));

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}

readonly record struct Ball(Point Point, int Shots);

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
    readonly Queue<Node> queue = [];
    readonly HashSet<T> visited = [];

    /// BFS
    public List<T>? Path(T start, T end) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(start);
        queue.Enqueue(startNode);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (current.cell.Equals(end)) {
                return Reconstruct(startNode, current);
            }
            foreach (var next in grid.Next(current.cell)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(new Node(next) { parent = current });
            }
        }
        return null;
    }

    static List<T> Reconstruct(Node start, Node end) {
        var path = new List<T>();
        var current = end;
        while (current != start) {
            path.Add(current!.cell);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    class Node(T cell) {
        public readonly T cell = cell;
        public Node? parent;
    }
}

interface IGrid<T> {
    IEnumerable<T> Next(T current);
}
