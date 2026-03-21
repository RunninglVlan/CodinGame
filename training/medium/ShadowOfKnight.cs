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
    (int xLeft, int yTop, int xRight, int yBottom) bombZone;
    (int x, int y) batman;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        // 1 ≤ dimension ≤ 10000
        string[] dimensions = Console.ReadLine()!.Split(' ');
        int width = int.Parse(dimensions[0]);
        int height = int.Parse(dimensions[1]);
        bombZone = (0, 0, width - 1, height - 1);
        // maximum number of turns before game over
        int maxTurns = int.Parse(Console.ReadLine()!);
        string[] position = Console.ReadLine()!.Split(' ');
        int x = int.Parse(position[0]);
        int y = int.Parse(position[1]);
        batman = (x, y);
    }

    /// Response time per turn ≤ 150ms
    public void Loop() {
        // direction of the bomb from batman's current location
        var bombDirection = ToDirection(Console.ReadLine()!);
        UpdateBombZone(bombDirection);
        MoveBatman();
        Console.WriteLine($"{batman.x} {batman.y}");
    }

    static Direction ToDirection(string value) {
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

    void UpdateBombZone(Direction direction) {
        switch (direction) {
            case Direction.Up:
                bombZone.xLeft = batman.x;
                bombZone.xRight = batman.x;
                bombZone.yBottom = batman.y - 1;
                break;
            case Direction.Down:
                bombZone.xLeft = batman.x;
                bombZone.xRight = batman.x;
                bombZone.yTop = batman.y + 1;
                break;
            case Direction.Right:
                bombZone.yTop = batman.y;
                bombZone.yBottom = batman.y;
                bombZone.xLeft = batman.x + 1;
                break;
            case Direction.Left:
                bombZone.yTop = batman.y;
                bombZone.yBottom = batman.y;
                bombZone.xRight = batman.x - 1;
                break;
            case Direction.UpRight:
                bombZone.xLeft = batman.x + 1;
                bombZone.yBottom = batman.y - 1;
                break;
            case Direction.DownRight:
                bombZone.xLeft = batman.x + 1;
                bombZone.yTop = batman.y + 1;
                break;
            case Direction.UpLeft:
                bombZone.xRight = batman.x - 1;
                bombZone.yBottom = batman.y - 1;
                break;
            case Direction.DownLeft:
                bombZone.xRight = batman.x - 1;
                bombZone.yTop = batman.y + 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    void MoveBatman() {
        batman.x = (bombZone.xLeft + bombZone.xRight) / 2;
        batman.y = (bombZone.yTop + bombZone.yBottom) / 2;
    }
}

enum Direction {
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
}
