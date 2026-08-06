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

class GolfCourse(int width, int height) {
    readonly bool[,] waterCells = new bool[width, height];
    readonly List<Ball> balls = [];
    readonly List<Point> holes = [];
    readonly List<List<Point>> paths = [];

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
                var end = holes[i];
                if (!TryFindPath(ball, end, out var path)) {
                    continue;
                }
                holes.RemoveAt(i);
                paths.Add(path);
                break;
            }
        }
    }

    bool TryFindPath(Ball start, Point end, out List<Point> path) {
        var existingPaths = paths.SelectMany(it => it).ToHashSet();
        var queue = new Queue<Node>();
        var startNode = new Node(start);
        queue.Enqueue(startNode);
        HashSet<Ball> visited = [start];
        while (queue.TryDequeue(out var current)) {
            if (current.ball.Point.Equals(end)) {
                path = Reconstruct(startNode, current);
                return true;
            }
            foreach (var direction in Point.Directions4) {
                if (!TryMove(current.ball.Point, direction, current.ball.Shots, out var next)) {
                    continue;
                }
                if (waterCells[next.X, next.Y]) {
                    continue;
                }
                var nextBall = new Ball(next, current.ball.Shots - 1);
                if (!visited.Add(nextBall)) {
                    continue;
                }
                queue.Enqueue(new Node(nextBall) { parent = current });
            }
        }
        path = [];
        return false;

        bool TryMove(Point current, Point direction, int moves, out Point next) {
            next = default;
            for (var move = 1; move <= moves; move++) {
                next = current + direction * move;
                if (!next.InBounds(width, height) || existingPaths.Contains(next)) {
                    return false;
                }
            }
            return true;
        }

        static List<Point> Reconstruct(Node start, Node end) {
            var path = new List<Point>();
            var current = end;
            while (current != start) {
                path.Add(current!.ball.Point);
                current = current.parent;
            }
            path.Add(start.ball.Point);
            path.Reverse();
            return path;
        }
    }

    public void PrintPaths() {
        const char empty = '.';
        var directions = new Dictionary<Point, char>();
        foreach (var path in paths) {
            for (var i = 0; i < path.Count - 1; i++) {
                var current = path[i];
                var next = path[i + 1];
                directions.Add(current, Direction(next - current));
            }
        }
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                Console.Write(directions.GetValueOrDefault(new Point(x, y), empty));
            }
            Console.WriteLine();
        }
        return;

        char Direction(Point direction) {
            return direction switch {
                _ when direction == Point.RIGHT => '>',
                _ when direction == Point.LEFT => '<',
                _ when direction == Point.UP => '^',
                _ => 'v'
            };
        }
    }

    class Node(Ball ball) {
        public readonly Ball ball = ball;
        public Node? parent;
    }
}

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
readonly record struct Point(int X, int Y) {
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
}

readonly record struct Ball(Point Point, int Shots);
