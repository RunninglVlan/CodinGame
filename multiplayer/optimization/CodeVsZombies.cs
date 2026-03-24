using System;
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

    public void Loop() {
        string[] position = Console.ReadLine()!.Split(' ');
        ash = Point.From(position[0], position[1]);
        ReadHumans();
        ReadZombies(out var closestZombie);
        Console.WriteLine(closestZombie);
    }

    void ReadHumans() {
        int humanCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < humanCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int humanId = int.Parse(inputs[0]);
            var human = Point.From(inputs[1], inputs[2]);
        }
    }

    void ReadZombies(out Point closestZombie) {
        (Point zombie, int distance) closest = (default, int.MaxValue);
        int zombieCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < zombieCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int zombieId = int.Parse(inputs[0]);
            var zombie = Point.From(inputs[1], inputs[2]);
            var zombieNext = Point.From(inputs[3], inputs[4]);
            int distance = Point.ManhattanDistance(ash, zombie);
            if (distance < closest.distance) {
                closest = (zombie, distance);
            }
        }
        closestZombie = closest.zombie;
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
