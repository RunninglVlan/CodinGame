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
    Point ash;
    readonly List<Point> humans = new();
    readonly List<Point> zombies = new();

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
            humans.Add(human);
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
            zombies.Add(zombie);
        }
    }

    Point ChooseTarget() {
        (Point human, int distance) closest = (default, int.MaxValue);
        foreach (var human in humans) {
            int distance = Point.ManhattanDistance(human, ash);
            if (distance < closest.distance) {
                closest = (human, distance);
            }
        }
        return closest.human;
    }
}

readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new(int.Parse(x), int.Parse(y));
    }

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public override string ToString() => $"{X} {Y}";
}
