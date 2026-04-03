using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

    int turn;
    readonly Pod player = new(), opponent = new();
    Checkpoint next;
    Point previousCheckpoint = new(-1, -1);
    readonly List<Point> checkpoints = new();
    bool checkpointsFound;
    Point? boostCheckpoint;

    public void Loop() {
        ReadInput();
        FindBoostCheckpoint();
        var control = $"{next.Point.X} {next.Point.Y} ";
        if (player.Speed > 300 && turn - player.ShieldTurn > 3 && player.HasCollisionRisk(opponent)) {
            control += Ability.SHIELD;
            player.ShieldTurn = turn;
        } else if (player.HasBoost && next.Point == boostCheckpoint && Math.Abs(next.Angle) < 5) {
            control += Ability.BOOST;
            player.HasBoost = false;
        } else {
            control += Thrust();
        }
        Console.WriteLine(control);
        turn++;
    }

    void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        player.Point = Point.From(inputs[0], inputs[1]);
        var checkpoint = Point.From(inputs[2], inputs[3]);
        next = Checkpoint.From(checkpoint, inputs[4], inputs[5]);
        inputs = Console.ReadLine()!.Split(' ');
        opponent.Point = Point.From(inputs[0], inputs[1]);
    }

    void FindBoostCheckpoint() {
        if (boostCheckpoint != null || previousCheckpoint == next.Point) {
            return;
        }
        if (!checkpoints.Contains(next.Point)) {
            checkpoints.Add(next.Point);
            previousCheckpoint = next.Point;
            return;
        }
        checkpointsFound = true;
        var maxDistance = double.MinValue;
        for (var i = 0; i < checkpoints.Count; i++) {
            var checkpoint = checkpoints[i];
            var checkpointNext = checkpoints[(i + 1) % checkpoints.Count];
            double distance = Vector.Distance(checkpoint, checkpointNext);
            if (distance <= maxDistance) {
                continue;
            }
            // use boost on longest straight
            maxDistance = distance;
            boostCheckpoint = checkpointNext;
        }
    }

    int Thrust() {
        if (Math.Abs(next.Angle) > 90) {
            return THRUST.none;
        }
        (double score, int thrust) best = (int.MaxValue, 100);
        for (var thrust = 0; thrust <= 100; thrust += 10) {
            var pNew = SimulateNext(player.Point, player.Velocity, next.Point, thrust, 4);
            double score = Score(pNew, thrust);
            if (score >= best.score) {
                continue;
            }
            best = (score, thrust);
        }
        return best.thrust;
    }

    static Vector SimulateNext(Vector p, Vector v, Vector target, int thrust, int iterations) {
        for (var i = 0; i < iterations; i++) {
            var direction = target - p;
            v += direction.Normalized() * thrust;
            p += v;
        }
        return p;
    }

    /// Prioritizing points inside checkpoint radius
    double Score(Vector pNew, int thrust) {
        double distance = Vector.Distance(pNew, next.Point);
        if (distance > Checkpoint.RADIUS) {
            return 100000 + distance;
        }
        if (!checkpointsFound) {
            return 100 - thrust;
        }
        int i = checkpoints.IndexOf(next.Point);
        int iNext = (i + 1) % checkpoints.Count;
        var nextCheckpoint = checkpoints[iNext];
        return Vector.Distance(pNew, nextCheckpoint);
    }
}

// 0 ≤ x < 16000, 0 ≤ y < 9000
readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public static implicit operator Vector(Point value) => new(value.X, value.Y);
}

readonly record struct Vector(double X, double Y) {
    public Vector Normalized() => this / Magnitude();

    public double Magnitude() => Math.Sqrt(X * X + Y * Y);

    public static double Distance(Vector a, Vector b) {
        double diffX = a.X - b.X;
        double diffY = a.Y - b.Y;
        return Math.Sqrt(diffX * diffX + diffY * diffY);
    }

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector operator *(Vector a, double value) => new(a.X * value, a.Y * value);
    public static Vector operator /(Vector a, double value) => new(a.X / value, a.Y / value);
}

// -180 <= angle <= 180 between pod orientation and direction of next checkpoint
readonly struct Checkpoint(Point point, int distance, int angle) {
    public const int RADIUS = 600;

    public Point Point => point;
    public int Angle => angle;

    public static Checkpoint From(Point point, string distance, string angle) {
        return new Checkpoint(point, int.Parse(distance), int.Parse(angle));
    }
}

[SuppressMessage("ReSharper", "ReplaceWithFieldKeyword")]
class Pod {
    const int DIAMETER = 800;

    Point? previous;
    Point current;
    public Vector Velocity { get; private set; }

    public Point Point {
        get => current;
        set {
            current = value;
            if (previous != null) {
                Velocity = current - previous.Value;
                Speed = Velocity.Magnitude();
            }
            previous = value;
        }
    }

    public double Speed { get; private set; }
    public bool HasBoost { get; set; } = true;
    public int ShieldTurn { get; set; } = -3;

    public bool HasCollisionRisk(Pod opponent) {
        var next = Point + Velocity;
        var nextOpponent = opponent.Point + opponent.Velocity;
        return Vector.Distance(next, nextOpponent) < DIAMETER;
    }
}

static class Ability {
    public const string BOOST = "BOOST";
    public const string SHIELD = "SHIELD";
}
