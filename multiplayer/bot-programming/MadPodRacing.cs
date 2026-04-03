using System;
using System.Collections.Generic;
using System.Linq;

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
    static readonly (int none, int max) THRUST = (0, 100);

    Checkpoint next;
    Point previousCheckpoint = new(-1, -1);
    readonly Dictionary<Point, Checkpoint> checkpoints = new();
    Point? boostCheckpoint;
    bool hasBoost = true;

    public void Loop() {
        ReadInput();
        FindBoostCheckpoint();
        var control = $"{next.Point.X} {next.Point.Y} ";
        if (hasBoost && next.Point == boostCheckpoint && next.IsOptimalAngle) {
            control += Ability.BOOST;
            hasBoost = false;
        } else {
            int thrust = next.IsSafeAngle ? THRUST.max : THRUST.none;
            control += thrust;
        }
        Console.WriteLine(control);
    }

    void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var player = Point.From(inputs[0], inputs[1]);
        var checkpoint = Point.From(inputs[2], inputs[3]);
        next = Checkpoint.From(checkpoint, inputs[4], inputs[5]);
        inputs = Console.ReadLine()!.Split(' ');
        var opponent = Point.From(inputs[0], inputs[1]);
    }

    void FindBoostCheckpoint() {
        if (boostCheckpoint != null || previousCheckpoint == next.Point) {
            return;
        }
        previousCheckpoint = next.Point;
        if (checkpoints.ContainsKey(next.Point)) {
            // found all checkpoints, use boost on longest straight
            boostCheckpoint = checkpoints
                    .OrderByDescending(it => it.Value.Distance)
                    .Select(it => it.Key)
                    .First();
        }
        checkpoints[next.Point] = next;
    }
}

// 0 ≤ x < 16000, 0 ≤ y < 9000
readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }
}

// Has radius of 600
// -180 <= angle <= 180 between pod orientation and direction of next checkpoint
readonly struct Checkpoint(Point point, int distance, int angle) {
    public Point Point => point;
    public int Distance => distance;
    public bool IsSafeAngle => angle is < 90 and > -90;
    public bool IsOptimalAngle => angle is < 5 and > -5;

    public static Checkpoint From(Point point, string distance, string angle) {
        return new Checkpoint(point, int.Parse(distance), int.Parse(angle));
    }
}

static class Ability {
    public const string BOOST = "BOOST";
}
