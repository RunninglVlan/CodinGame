using System;
using System.Diagnostics.CodeAnalysis;

/// https://www.codingame.com/training/medium/shadows-of-the-knight-episode-1
static class ShadowOfKnight {
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
        }
        // ReSharper disable once FunctionNeverReturns
    }
}

class Game {
    BombZone bombZone = null!;
    Point batman;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        // 1 ≤ dimension ≤ 10000
        string[] dimensions = Console.ReadLine()!.Split(' ');
        int width = int.Parse(dimensions[0]);
        int height = int.Parse(dimensions[1]);
        bombZone = new(0, 0, width - 1, height - 1);
        // maximum number of turns before game over
        int maxTurns = int.Parse(Console.ReadLine()!);
        string[] position = Console.ReadLine()!.Split(' ');
        int x = int.Parse(position[0]);
        int y = int.Parse(position[1]);
        batman = new(x, y);
    }

    /// Response time per turn ≤ 150ms
    public void Loop() {
        var bombDirection = ReadDirection();
        bombZone.Shrink(bombDirection, batman);
        batman = bombZone.Center;
        Console.WriteLine(batman);
    }

    static Direction ReadDirection() {
        // direction of the bomb from batman's current location
        string value = Console.ReadLine()!;
        return value switch {
            "U" => Direction.Up,
            "UR" => Direction.UpRight,
            "R" => Direction.Right,
            "DR" => Direction.DownRight,
            "D" => Direction.Down,
            "DL" => Direction.DownLeft,
            "L" => Direction.Left,
            "UL" => Direction.UpLeft,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}

class BombZone(int xLeft, int yTop, int xRight, int yBottom) {
    int xLeft = xLeft;
    int yTop = yTop;
    int xRight = xRight;
    int yBottom = yBottom;

    public Point Center => new((xLeft + xRight) / 2, (yTop + yBottom) / 2);

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public void Shrink(Direction direction, Point batman) {
        if (direction is Direction.Up or Direction.Down) {
            xLeft = batman.X;
            xRight = batman.X;
        } else if (direction is Direction.Right or Direction.Left) {
            yTop = batman.Y;
            yBottom = batman.Y;
        }
        if (direction.HasFlag(Direction.Up)) {
            yBottom = batman.Y - 1;
        } else if (direction.HasFlag(Direction.Down)) {
            yTop = batman.Y + 1;
        }
        if (direction.HasFlag(Direction.Right)) {
            xLeft = batman.X + 1;
        } else if (direction.HasFlag(Direction.Left)) {
            xRight = batman.X - 1;
        }
    }
}

readonly record struct Point(int X, int Y) {
    public override string ToString() => $"{X} {Y}";
}

[Flags]
enum Direction {
    Up = 0b_0001,
    Right = 0b_0010,
    Down = 0b_0100,
    Left = 0b_1000,
    UpRight = Up | Right,
    DownRight = Down | Right,
    DownLeft = Down | Left,
    UpLeft = Up | Left
}
