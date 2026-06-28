using System;
using System.Diagnostics.CodeAnalysis;

namespace MarsLander3;

/// https://www.codingame.com/training/expert/mars-lander-episode-3
static class MarsLander3 {
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
    const float GRAVITY = 3.711f;

    Point[] surface = null!;
    Landing landing = null!;

    public void Init() {
        int surfacePoints = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 30 - 1);
        surface = new Point[surfacePoints];
        Point? previous = null;
        for (var i = 0; i < surfacePoints; i++) {
            string[] position = Console.ReadLine()!.Split(' ');
            var point = Point.From(position[0], position[1]);
            surface[i] = point;
            if (previous == null || point.Y != previous.Value.Y) {
                previous = point;
                continue;
            }
            landing = new Landing(previous.Value, point);
            Console.Error.WriteLine($"Landing center: {landing.center}");
        }
    }

    public void Loop() {
        var (point, speed, _, currentControl) = ReadInput();
        landing.SetReached(point);
        Console.Error.WriteLine(!landing.reached ? "Moving to landing" : "Reached landing");
        Vector p = point;
        Vector v = speed;
        var target = landing.Nearest(point);
        if (IsObstacleInWay(point, target, 100, out var obstacle)) {
            target = obstacle;
        }
        var (nextControl, _) = FindBest(p, v, currentControl, target, landing.reached, 5);
        if (landing.reached && speed.IsSafe) {
            // make sure we're landing with 0 angle
            nextControl.Angle = 0;
            // prevents hovering over landing
            nextControl.Power = 3;
        }
        Console.WriteLine(nextControl);
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

    bool IsObstacleInWay(Point current, Point target, int margin, out Point obstacle) {
        // straight line: y = mx + b
        double m = (double)(target.Y - current.Y) / (target.X - current.X);
        double b = current.Y - m * current.X;
        int left = Math.Min(current.X, target.X);
        int right = Math.Max(current.X, target.X);
        (Point? point, double violation) mostDangerous = (null, -margin);
        obstacle = default;
        foreach (var point in surface) {
            if (point.X <= left || point.X >= right) {
                continue;
            }
            double lineY = m * point.X + b;
            double violation = point.Y - lineY;
            if (violation > mostDangerous.violation) {
                mostDangerous = (point, violation);
            }
        }
        if (mostDangerous.point == null) {
            return false;
        }
        obstacle = mostDangerous.point.Value with { Y = mostDangerous.point.Value.Y + margin };
        return true;
    }

    /// DFS
    static (Control control, double score) FindBest(Vector p, Vector v, Control current,
            Vector target, bool landingReached, int depth) {
        if (depth == 0) {
            double distanceX = Math.Abs(p.X - target.X);
            double distanceY = Math.Abs(p.Y - target.Y);
            double yWeight = !landingReached ? 2 : 0.5;
            double score = distanceX + distanceY * yWeight;
            if (!Speed.IsSafeX(v.X)) {
                // being faster is worse
                score += 10_000 + Math.Abs(v.X) * 100;
            }
            if (!Speed.IsSafeY(v.Y)) {
                // unsafe vertical speed is penalized more
                score += 20_000 + Math.Abs(v.Y) * 100;
            }
            return (default, score);
        }
        (Control control, double score) best = (default, double.MaxValue);
        for (int angle = -15; angle <= 15; angle += 15) {
            int nextAngle = Math.Clamp(current.Angle + angle, -90, 90);
            for (int power = -1; power <= 1; power++) {
                int nextPower = Math.Clamp(current.Power + power, 0, 4);
                var (pNew, vNew) = SimulateNext(p, v, nextAngle, nextPower, 7);
                (_, double score) = FindBest(pNew, vNew, new Control(nextAngle, nextPower),
                        target, landingReached, depth - 1);
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

class Landing(Point left, Point right) {
    public bool reached;

    /// Landing is flat, so left.Y == right.Y
    public Point center = left with { X = (left.X + right.X) / 2 };

    public Point Nearest(Point point) {
        return point.X < left.X ? left : point.X > right.X ? right : new Point(point.X, center.Y);
    }

    public void SetReached(Point point) {
        reached = point.X > left.X && point.X < right.X;
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

record struct Control(int Angle, int Power) {
    public override string ToString() => $"{Angle} {Power}";
}
