using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// Save humans, destroy zombies!<br/>
/// https://www.codingame.com/multiplayer/optimization/code-vs-zombies
static class CodeVsZombies {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        while (true) {
            game.Loop();
        }
    }
}

class Game {
    const int ASH_MOVE = 1000;
    const int ASH_RADIUS = 2000;
    const int ZOMBIE_MOVE = 400;

    Point ash;
    readonly Dictionary<int, Point> humans = new();
    readonly Dictionary<int, Point> zombies = new();

    public void Loop() {
        string[] position = Console.ReadLine()!.Split(' ');
        ash = Point.From(position[0], position[1]);
        ReadHumans();
        ReadZombies();
        Console.WriteLine(ChooseTarget());
    }

    void ReadHumans() {
        humans.Clear();
        int humanCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < humanCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int humanId = int.Parse(inputs[0]);
            var human = Point.From(inputs[1], inputs[2]);
            humans.Add(humanId, human);
        }
    }

    void ReadZombies() {
        zombies.Clear();
        int zombieCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < zombieCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int zombieId = int.Parse(inputs[0]);
            var zombie = Point.From(inputs[1], inputs[2]);
            var zombieNext = Point.From(inputs[3], inputs[4]);
            zombies.Add(zombieId, zombie);
        }
    }

    Point ChooseTarget() {
        (Point? human, int distance) closest = (null, int.MaxValue);
        foreach (var human in humans.Values) {
            var distance = (int)Point.Distance(human, ash);
            if (distance >= closest.distance) {
                continue;
            }
            if (IsZombieCloser(human)) {
                continue;
            }
            closest = (human, distance);
        }
        return closest.human ?? throw new ArgumentException("No target found");
    }

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    bool IsZombieCloser(Point human) {
        int ashDistance = (int)Point.Distance(human, ash) - ASH_RADIUS;
        int ashTurns = ashDistance / ASH_MOVE;
        foreach (var zombie in zombies.Values) {
            var distance = (int)Point.Distance(zombie, human);
            int turns = distance / ZOMBIE_MOVE;
            if (turns < ashTurns) {
                return true;
            }
        }
        return false;
    }
}

readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new(int.Parse(x), int.Parse(y));
    }

    public static float Distance(Point a, Point b) {
        float diffX = a.X - b.X;
        float diffY = a.Y - b.Y;
        return (float)Math.Sqrt(diffX * diffX + diffY * diffY);
    }

    public override string ToString() => $"{X} {Y}";
}
