using System;
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

class Game {
    const int SIZE = 10;

    readonly bool[,] floors = new bool[SIZE, SIZE];
    Point player, susMan;
    Point? playerPrevious;

    bool IsFloor(Point point) => floors[point.X, point.Y];

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public void Init() {
        // representing after how many turns the sus man will make another move
        int k = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 3);
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
        var susPoint = Point.From(susCoordinates[1], susCoordinates[0]);
        var nextDirection = NextDirection(susMan);
        Console.WriteLine(Screen.ToChar(nextDirection));
        playerPrevious = player;
        player += nextDirection;
        if (player == susMan) {
            susMan = susPoint;
        }
    }

    Point NextDirection(Point susPoint) {
        (int distance, Point direction)? best = null;
        foreach (var direction in Screen.Directions4) {
            var playerNext = player + direction;
            int distance = Point.ManhattanDistance(playerNext, susPoint);
            if (!playerNext.InBounds(SIZE, SIZE) || !IsFloor(playerNext) || playerNext == playerPrevious) {
                continue;
            }
            if (best == null || distance < best.Value.distance) {
                best = (distance, direction);
            }
        }
        return best!.Value.direction;
    }
}

static class Blocks {
    public const char WALL = '*';
    public const char FLOOR = '-';
    public const char PLAYER = 'P';
    public const char SUS_MAN = 'E';
}

readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(Math.Clamp(int.Parse(x), 1, 9), Math.Clamp(int.Parse(y), 1, 9));
    }

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

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
