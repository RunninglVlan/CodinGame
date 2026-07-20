using System;
using System.Collections.Generic;
using System.Linq;

namespace MovesInMaze;

/// https://www.codingame.com/training/easy/moves-in-maze
static class MovesInMaze {
    static void Main() {
        var solution = new Solution();
        solution.ReadInput();
        solution.PrintGridWithMoves();
    }
}

class Solution : IGrid<Point> {
    const string MOVES = "0123456789ABCDEFGHIJKLMNOPQRSTXYZ";

    readonly Pathfinding<Point> pathfinding;

    int width, height;
    bool[,] walkableCells = null!;
    Point start;

    public Solution() {
        pathfinding = new Pathfinding<Point>(this);
    }

    public void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(inputs[0]), 3, 30);
        height = Math.Clamp(int.Parse(inputs[1]), 3, 30);
        walkableCells = new bool[width, height];
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char cell = row[x];
                if (cell == Cells.START) {
                    start = new Point(x, y);
                }
                walkableCells[x, y] = cell is Cells.START or Cells.EMPTY;
            }
        }
    }

    public void PrintGridWithMoves() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                if (!walkableCells[x, y]) {
                    Console.Write(Cells.WALL);
                    continue;
                }
                var path = pathfinding.Path(start, new Point(x, y))!;
                Console.Write(MOVES[path.Count]);
            }
            Console.WriteLine();
        }
    }

    IEnumerable<Point> IGrid<Point>.Next(Point current) {
        return Point.Directions4
                .Select(it => current + it)
                .Where(it => it.InBounds(width, height))
                .Where(it => walkableCells[it.X, it.Y]);
    }
}

static class Cells {
    public const char EMPTY = '.';
    public const char WALL = '#';
    public const char START = 'S';
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
}

class Pathfinding<T>(IGrid<T> grid) where T : IEquatable<T> {
    readonly Queue<Node> queue = [];
    readonly HashSet<T> visited = [];

    /// BFS to end
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
