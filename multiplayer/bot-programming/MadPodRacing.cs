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
    Checkpoint next;

    public void Loop() {
        ReadInput();
        int power = next.IsSafeAngle ? 100 : 0;
        Console.WriteLine(new Control(next.Point, power));
    }

    void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var player = Point.From(inputs[0], inputs[1]);
        var checkpoint = Point.From(inputs[2], inputs[3]);
        next = Checkpoint.From(checkpoint, inputs[4], inputs[5]);
        inputs = Console.ReadLine()!.Split(' ');
        var opponent = Point.From(inputs[0], inputs[1]);
    }
}

readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }
}

// -180 <= angle <= 180 between pod orientation and direction of next checkpoint
readonly struct Checkpoint(Point point, int distance, int angle) {
    public Point Point => point;
    public bool IsSafeAngle => angle is < 90 and > -90;

    public static Checkpoint From(Point point, string distance, string angle) {
        return new Checkpoint(point, int.Parse(distance), int.Parse(angle));
    }
}

// 0 <= thrust power <= 100
readonly struct Control(Point target, int power) {
    public override string ToString() => $"{target.X} {target.Y} {power}";
}
