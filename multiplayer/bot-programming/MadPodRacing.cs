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
    Checkpoint next;
    Point previousCheckpoint = new(-1, -1);
    readonly Dictionary<Point, int> checkpointDistances = new();
    Point? boostCheckpoint;
    bool hasBoost = true;

    public void Loop() {
        ReadInput();
        if (hasBoost && next.IsOptimalAngle && next.Point == boostCheckpoint) {
            Console.WriteLine($"{next.Point} BOOST");
            hasBoost = false;
        } else {
            int power = next.IsSafeAngle ? 100 : 0;
            Console.WriteLine(new Control(next.Point, power));
        }
    }

    void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var player = Point.From(inputs[0], inputs[1]);
        var checkpoint = Point.From(inputs[2], inputs[3]);
        next = Checkpoint.From(checkpoint, inputs[4], inputs[5]);
        inputs = Console.ReadLine()!.Split(' ');
        var opponent = Point.From(inputs[0], inputs[1]);
        if (previousCheckpoint == checkpoint) {
            return;
        }
        previousCheckpoint = checkpoint;
        if (checkpointDistances.ContainsKey(checkpoint)) {
            // found all checkpoints, use boost on longest straight
            boostCheckpoint = checkpointDistances
                    .OrderByDescending(it => it.Value)
                    .Select(it => it.Key)
                    .First();
        }
        checkpointDistances[checkpoint] = next.Distance;
    }
}

// 0 ≤ x < 16000, 0 ≤ y < 9000
readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }

    public override string ToString() => $"{X} {Y}";
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

// 0 <= thrust power <= 100
readonly struct Control(Point target, int power) {
    public override string ToString() => $"{target} {power}";
}
