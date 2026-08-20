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
    Entrance nextEntrance;

    public List<PathSegment> DoTurn() {
        var visited = new List<PathSegment>();
        grid.Add(nextPoint, Tile.Read());
        while (grid.TryGetValue(nextPoint, out var tile)) {
            var (segment, exit) = tile.Visit(nextEntrance);
            visited.Add(new PathSegment(nextPoint, segment));
            Score += visited.Count;
            nextPoint = Tile.NextPoint(nextPoint, exit);
            nextEntrance = Tile.NextEntrance(exit);
        }
        return visited;
    }
}

/// Hexagonal tile with 6 segments and 12 entrances
record Tile(Segment[] Segments) {
    public static Tile Read() {
        var segments = new Segment[6];
        for (var i = 0; i < 6; i++) {
            string[] entrances = Debug.ReadLine()!.Split(' ');
            var a = Enum.Parse<Entrance>(entrances[0]);
            var b = Enum.Parse<Entrance>(entrances[1]);
            segments[i] = new Segment(a, b);
        }
        return new Tile(segments);
    }

    public static Point NextPoint(Point current, Entrance entrance) => entrance switch {
        Entrance.South0 or Entrance.South1 => current with { Y = current.Y + 1 },
        Entrance.SouthEast0 or Entrance.SouthEast1 => new Point(current.X + 1, current.Y + 1),
        Entrance.NorthEast0 or Entrance.NorthEast1 => current with { X = current.X + 1 },
        Entrance.North0 or Entrance.North1 => current with { Y = current.Y - 1 },
        Entrance.NorthWest0 or Entrance.NorthWest1 => new Point(current.X - 1, current.Y - 1),
        Entrance.SouthWest0 or Entrance.SouthWest1 => current with { X = current.X - 1 },
        _ => throw new ArgumentException($"Can't find next point based on entrance: {entrance}")
    };

    public static Entrance NextEntrance(Entrance current) => current switch {
        Entrance.South0 => Entrance.North1,
        Entrance.South1 => Entrance.North0,
        Entrance.SouthEast0 => Entrance.NorthWest1,
        Entrance.SouthEast1 => Entrance.NorthWest0,
        Entrance.NorthEast0 => Entrance.SouthWest1,
        Entrance.NorthEast1 => Entrance.SouthWest0,
        Entrance.North0 => Entrance.South1,
        Entrance.North1 => Entrance.South0,
        Entrance.NorthWest0 => Entrance.SouthEast1,
        Entrance.NorthWest1 => Entrance.SouthEast0,
        Entrance.SouthWest0 => Entrance.NorthEast1,
        Entrance.SouthWest1 => Entrance.NorthEast0,
        _ => throw new ArgumentException($"Can't find next entrance based on entrance: {current}")
    };

    public (Segment, Entrance) Visit(Entrance entrance) {
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

readonly record struct Segment(Entrance A, Entrance B);

enum Entrance {
    South0,
    South1,
    SouthEast0,
    SouthEast1,
    NorthEast0,
    NorthEast1,
    North0,
    North1,
    NorthWest0,
    NorthWest1,
    SouthWest0,
    SouthWest1
}

readonly record struct PathSegment(Point Point, Segment Segment) {
    public override string ToString() => $"{Point.X} {Point.Y} {(int)Segment.A} {(int)Segment.B}";
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
