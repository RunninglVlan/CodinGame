using System;

namespace MadPodRacing;

/// https://www.codingame.com/multiplayer/bot-programming/mad-pod-racing
static class MadPodRacing {
    static void Main() {
        var game = new Game();
        while (true) {
            game.Loop();
        }
        // ReSharper disable once FunctionNeverReturns
    }
}

class Game {
    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var player = Point.From(inputs[0], inputs[1]);
        var nextCheckpoint = Point.From(inputs[2], inputs[3]);
        int nextCheckpointDistance = int.Parse(inputs[4]);
        // angle between pod orientation and direction of next checkpoint
        int nextCheckpointAngle = int.Parse(inputs[5]);
        inputs = Console.ReadLine()!.Split(' ');
        var opponent = Point.From(inputs[0], inputs[1]);

        Console.WriteLine(new Control(nextCheckpoint, 80));
    }
}

readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }
}

/// 0 &lt;= thrust power &lt;= 100
readonly struct Control(Point target, int power) {
    public override string ToString() => $"{target.X} {target.Y} {power}";
}
