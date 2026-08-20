using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/medium/entanglement---episode-1
var game = new Game();
while (true) {
    var visited = game.DoTurn();
    Console.WriteLine(game.Score);
    Console.WriteLine(string.Join("; ", visited));
}

class Game {
    public int Score { get; private set; }
    Point nextPoint = new(0, -1);
    int nextEntrance;

    public List<PathSegment> DoTurn() {
        var visited = new List<PathSegment>();
        var tile = HexTile.Read();
        var segment = tile.Segments.First(it => it.A == nextEntrance);
        Score++;
        visited.Add(new PathSegment(nextPoint, segment));
        nextPoint = segment.B switch {
            0 or 1 => nextPoint with { Y = nextPoint.Y + 1 },
            2 or 3 => new Point(nextPoint.X + 1, nextPoint.Y + 1),
            4 or 5 => nextPoint with { X = nextPoint.X + 1 },
            6 or 7 => nextPoint with { Y = nextPoint.Y - 1 },
            8 or 9 => new Point(nextPoint.X - 1, nextPoint.Y - 1),
            10 or 11 => nextPoint with { X = nextPoint.X - 1 },
            _ => throw new ArgumentException($"Can't find next point based on entrance B: {segment.B}")
        };
        nextEntrance = segment.B switch {
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
            _ => throw new ArgumentException($"Can't find next entrance based on entrance B: {segment.B}")
        };
        return visited;
    }
}

/// Has 12 entrances: <br/>
/// - south: 0, 1 <br/>
/// - south-east: 2, 3 <br/>
/// - north-east: 4, 5 <br/>
/// - north: 6, 7 <br/>
/// - north-west: 8, 9 <br/>
/// - south-west: 10, 11
record HexTile(Segment[] Segments) {
    public static HexTile Read() {
        var segments = new Segment[6];
        for (var i = 0; i < 6; i++) {
            string[] entrances = Debug.ReadLine()!.Split(' ');
            int a = int.Parse(entrances[0]);
            int b = int.Parse(entrances[1]);
            segments[i] = new Segment(a, b);
        }
        return new HexTile(segments);
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
