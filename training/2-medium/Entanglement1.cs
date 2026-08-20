using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/entanglement---episode-1
var game = new Game();
while (true) {
    var visited = game.DoTurn();
    Console.WriteLine(game.Score);
    Console.WriteLine(string.Join("; ", visited));
}

class Game {
    readonly Dictionary<Point, Tile> grid = [];
    public int Score { get; private set; }
    Point nextPoint = new(0, -1);
    int nextEntrance;

    public List<PathSegment> DoTurn() {
        var visited = new List<PathSegment>();
        grid.Add(nextPoint, Tile.Read());
        while (grid.TryGetValue(nextPoint, out var tile)) {
            (var segment, int exit) = tile.Visit(nextEntrance);
            visited.Add(new PathSegment(nextPoint, segment));
            Score += visited.Count;
            nextPoint = Tile.NextPoint(nextPoint, exit);
            nextEntrance = Tile.NextEntrance(exit);
        }
        return visited;
    }
}

/// Hexagonal tile with 6 segments and 12 entrances:
/// <list type="bullet">
/// <item>south: 0, 1</item>
/// <item>south-east: 2, 3</item>
/// <item>north-east: 4, 5</item>
/// <item>north: 6, 7</item>
/// <item>north-west: 8, 9</item>
/// <item>south-west: 10, 11</item>
/// </list>
record Tile(Segment[] Segments) {
    public static Tile Read() {
        var segments = new Segment[6];
        for (var i = 0; i < 6; i++) {
            string[] entrances = Debug.ReadLine()!.Split(' ');
            int a = int.Parse(entrances[0]);
            int b = int.Parse(entrances[1]);
            segments[i] = new Segment(a, b);
        }
        return new Tile(segments);
    }

    public static Point NextPoint(Point current, int entrance) => entrance switch {
        0 or 1 => current with { Y = current.Y + 1 },
        2 or 3 => new Point(current.X + 1, current.Y + 1),
        4 or 5 => current with { X = current.X + 1 },
        6 or 7 => current with { Y = current.Y - 1 },
        8 or 9 => new Point(current.X - 1, current.Y - 1),
        10 or 11 => current with { X = current.X - 1 },
        _ => throw new ArgumentException($"Can't find next point based on entrance: {entrance}")
    };

    public static int NextEntrance(int current) => current switch {
        0 => 7,
        1 => 6,
        2 => 9,
        3 => 8,
        4 => 11,
        5 => 10,
        6 => 1,
        7 => 0,
        8 => 3,
        9 => 2,
        10 => 5,
        11 => 4,
        _ => throw new ArgumentException($"Can't find next entrance based on entrance: {current}")
    };

    public (Segment, int) Visit(int entrance) {
        foreach (var segment in Segments) {
            if (segment.A == entrance) {
                return (segment, segment.B);
            }
            if (segment.B == entrance) {
                return (segment, segment.A);
            }
        }
        throw new ArgumentException($"Can't find segment based on entrance: {entrance}");
    }
}

/// <param name="A">entrance A</param>
/// <param name="B">entrance B</param>
readonly record struct Segment(int A, int B);

readonly record struct PathSegment(Point Point, Segment Segment) {
    public override string ToString() => $"{Point.X} {Point.Y} {Segment.A} {Segment.B}";
}

readonly record struct Point(int X, int Y);

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }
}
