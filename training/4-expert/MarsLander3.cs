using System;
using System.Diagnostics.CodeAnalysis;

namespace MarsLander3;

/// https://www.codingame.com/training/expert/mars-lander-episode-3
static class MarsLander3 {
    static void Main() {
        var game = new Game();
        while (true) {
            game.Loop();
        }
        // ReSharper disable once FunctionNeverReturns
    }
}

class Game {
    const float GRAVITY = 3.711f;

    readonly Landing landing = null!;

    public Game() {
        int numberOfSurfacePoints = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 30 - 1);
        Point? previousPoint = null;
        for (var i = 0; i < numberOfSurfacePoints; i++) {
            string[] position = Console.ReadLine()!.Split(' ');
            var point = Point.From(position[0], position[1]);
            if (previousPoint == null || point.Y != previousPoint.Value.Y) {
                previousPoint = point;
                continue;
            }
            landing = new Landing(previousPoint.Value, point);
            Console.Error.WriteLine($"Landing center: {landing.center}");
        }
    }

    public void Loop() {
        var (point, speed, _, currentControl) = ReadInput();
        landing.SetReached(point);
        Console.Error.WriteLine(!landing.reached ? "Moving to landing" : "Reached landing");
        Vector p = point;
        Vector v = speed;
        var (nextControl, _) = FindBest(p, v, currentControl, landing.center, 5);
        if (landing.reached && speed.IsSafe) {
            // make sure we're landing with 0 angle
            nextControl.angle = 0;
            // prevents hovering over landing
            nextControl.power = 3;
        }
        Console.WriteLine($"{nextControl.angle} {nextControl.power}");
    }

    [SuppressMessage("ReSharper", "UnusedTupleComponentInReturnValue")]
    static (Point, Speed, int, Control) ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var point = Point.From(inputs[0], inputs[1]);
        var speed = Speed.From(inputs[2], inputs[3]);
        int fuel = Math.Clamp(int.Parse(inputs[4]), 0, 2000);
        // negative is right, positive is left
        int angle = Math.Clamp(int.Parse(inputs[5]), -90, 90);
        int power = Math.Clamp(int.Parse(inputs[6]), 0, 4);
        return (point, speed, fuel, new Control(angle, power));
    }

    /// DFS
    static (Control control, double score) FindBest(Vector p, Vector v, Control current, Vector target, int depth) {
        if (depth == 0) {
            double distanceX = Math.Abs(p.X - target.X);
            double distanceY = Math.Abs(p.Y - target.Y);
            double speedPenalty = 0;
            if (!Speed.IsSafeX(v.X)) {
                // being faster is worse
                speedPenalty += 10_000 + Math.Abs(v.X) * 100;
            }
            if (!Speed.IsSafeY(v.Y)) {
                // unsafe vertical speed is penalized more
                speedPenalty += 20_000 + Math.Abs(v.Y) * 100;
            }
            // vertical distance is less important
            return (default, distanceX + distanceY * 0.5 + speedPenalty);
        }
        (Control control, double score) best = (default, double.MaxValue);
        for (int angle = -15; angle <= 15; angle += 15) {
            int nextAngle = Math.Clamp(current.angle + angle, -90, 90);
            for (int power = -1; power <= 1; power++) {
                int nextPower = Math.Clamp(current.power + power, 0, 4);
                var (pNew, vNew) = SimulateNext(p, v, nextAngle, nextPower, 5);
                (_, double score) = FindBest(pNew, vNew, new Control(nextAngle, nextPower), target, depth - 1);
                if (score >= best.score) {
                    continue;
                }
                best.score = score;
                best.control = new Control(nextAngle, nextPower);
            }
        }
        return best;
    }

    /// vNew = v + a * t,
    /// pNew = p + v * t + 0.5 * a * t^2,
    /// pNew = p + vNew * t - 0.5 * a * t^2.<br/>
    /// t = 1, so
    /// vNew = v + a,
    /// pNew = p + v + 0.5 * a,
    /// pNew = p + vNew - 0.5 * a.<br/>
    /// <see href="https://en.wikipedia.org/wiki/Equations_of_motion#Constant_translational_acceleration_in_a_straight_line" />
    static (Vector, Vector) SimulateNext(Vector p, Vector v, int nextAngle, int nextPower, int iterations) {
        double radians = nextAngle * Math.PI / 180;
        var thrust = new Vector(-nextPower * Math.Sin(radians), nextPower * Math.Cos(radians));
        var a = thrust with { Y = thrust.Y - GRAVITY };
        for (var i = 0; i < iterations; i++) {
            v = new Vector(v.X + a.X, v.Y + a.Y);
            p = new Vector(p.X + v.X - 0.5 * a.X, p.Y + v.Y - 0.5 * a.Y);
        }
        return (p, v);
    }
}

readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(
                Math.Clamp(int.Parse(x), 0, 7000 - 1),
                Math.Clamp(int.Parse(y), 0, 3000 - 1));
    }

    public static implicit operator Vector(Point value) => new(value.X, value.Y);

    public override string ToString() => $"({X}, {Y})";
}

class Landing(Point start, Point end) {
    public Vector center = ((Vector)start + end) / 2.0;
    public bool reached;

    public void SetReached(Point point) {
        reached = point.X > start.X && point.X < end.X;
    }
}

readonly record struct Vector(double X, double Y) {
    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector operator /(Vector a, double value) => new(a.X / value, a.Y / value);

    public override string ToString() => $"({X:F1}, {Y:F1})";
}

/// Speed in m/s
/// <param name="X">horizontal</param>
/// <param name="Y">vertical</param>
readonly record struct Speed(int X, int Y) {
    /// actual safe horizontal speed is less than or equals 20
    const int SAFE_X = 15;

    /// actual safe vertical speed is less than or equals 40
    const int SAFE_Y = 35;

    public bool IsSafe => Math.Abs(X) < SAFE_X && Math.Abs(Y) < SAFE_Y;
    public static bool IsSafeX(double value) => Math.Abs(value) < SAFE_X;
    public static bool IsSafeY(double value) => Math.Abs(value) < SAFE_Y;

    public static Speed From(string x, string y) {
        return new Speed(
                Math.Clamp(int.Parse(x), -500 + 1, 500 - 1),
                Math.Clamp(int.Parse(y), -500 + 1, 500 - 1));
    }

    public static implicit operator Vector(Speed value) => new(value.X, value.Y);
}

struct Control(int angle, int power) {
    public int angle = angle;
    public int power = power;
}
