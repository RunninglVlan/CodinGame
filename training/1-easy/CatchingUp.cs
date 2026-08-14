using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// Visual
// catch up and win
// https://www.codingame.com/training/easy/catching-up
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game : IGrid<Point> {
    const int SIZE = 10;

    readonly Pathfinding<Point> pathfinding;

    public Game() {
        pathfinding = new Pathfinding<Point>(this);
    }

    readonly bool[,] floors = new bool[SIZE, SIZE];
    Point player, susMan;

    bool IsFloor(Point point) => floors[point.X, point.Y];

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public void Init() {
        // representing after how many turns the sus man will make another move
        Math.Clamp(int.Parse(Debug.ReadLine()!), 1, 3);
        for (var y = 0; y < SIZE; y++) {
            string mapRow = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                char block = mapRow[x];
                if (block == Blocks.PLAYER) {
                    player = new Point(x, y);
                } else if (block == Blocks.SUS_MAN) {
                    susMan = new Point(x, y);
                }
                floors[x, y] = block != Blocks.WALL;
            }
        }
    }

    public void Loop() {
        string[] susCoordinates = Console.ReadLine()!.Split(' ');
        var path = pathfinding.Path(player, susMan);
        var direction = path!.First() - player;
        Console.WriteLine(Screen.ToChar(direction));
        player += direction;
        susMan = Point.From(susCoordinates[1], susCoordinates[0]);
    }

    IEnumerable<Point> IGrid<Point>.Next(Point current) => Screen.Directions4
            .Select(it => current + it)
            .Where(it => it.InBounds(SIZE, SIZE) && IsFloor(it))
            .OrderBy(it => Point.ManhattanDistance(it, susMan));
}

static class Blocks {
    public const char WALL = '*';
    public const char FLOOR = '-';
    public const char PLAYER = 'P';
    public const char SUS_MAN = 'E';
}

readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(Math.Clamp(int.Parse(x), 0, 9), Math.Clamp(int.Parse(y), 0, 9));
    }

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"({X}, {Y})";
}

static class Screen {
    static readonly Point UP = new(0, -1);
    static readonly Point DOWN = new(0, 1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static char ToChar(Point direction) => direction switch {
        _ when direction == UP => 'U',
        _ when direction == DOWN => 'D',
        _ when direction == LEFT => 'L',
        _ when direction == RIGHT => 'R',
        _ => throw new ArgumentException($"Unsupported direction: {direction}")
    };
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }
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
