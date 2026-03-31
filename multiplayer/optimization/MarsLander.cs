using System;
using System.Diagnostics.CodeAnalysis;

namespace MarsLander;

/// Save the Planet. Use less Fossil Fuel.<br/>
/// https://www.codingame.com/multiplayer/optimization/mars-lander
static class MarsLander {
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

    Landing landing = null!;

    public void Init() {
        // 2 ≤ number < 30
        int surfacePoints = int.Parse(Console.ReadLine()!);
        Point? previous = null;
        for (var i = 0; i < surfacePoints; i++) {
            string[] position = Console.ReadLine()!.Split(' ');
            var point = Point.From(position[0], position[1]);
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
        var (nextControl, _) = FindBest(p, v, currentControl, landing.Nearest(point), 5);
        if (landing.reached && speed.IsOptimal) {
            // make sure we're landing with 0 angle
            nextControl.angle = 0;
            // prevents hovering over landing
            nextControl.power = 3;
        }
        Console.WriteLine(nextControl);
    }

    [SuppressMessage("ReSharper", "UnusedTupleComponentInReturnValue")]
    static (Point, Speed, int, Control) ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var point = Point.From(inputs[0], inputs[1]);
        var speed = Speed.From(inputs[2], inputs[3]);
        // 0 ≤ fuel ≤ 2000
        int fuel = int.Parse(inputs[4]);
        // -90 (right) ≤ angle ≤ 90 (left)
        int angle = int.Parse(inputs[5]);
        // 0 ≤ thrust power ≤ 4
        int power = int.Parse(inputs[6]);
        return (point, speed, fuel, new Control(angle, power));
    }

    static (Control control, double score) FindBest(Vector p, Vector v, Control current, Point target, int depth) {
        if (depth == 0) {
            double distanceX = Math.Abs(p.x - target.X);
            double distanceY = Math.Abs(p.y - target.Y);
            // vertical distance is less important
            double score = distanceX + distanceY * 0.5;
            if (!Speed.IsOptimalX(v.x)) {
                // being faster is worse
                score += 10_000 + Math.Abs(v.x) * 100;
            }
            if (!Speed.IsOptimalY(v.y)) {
                // unsafe vertical speed is penalized more
                score += 20_000 + Math.Abs(v.y) * 100;
            }
            return (default, score);
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
        var a = new Vector(thrust.x, thrust.y - GRAVITY);
        for (var i = 0; i < iterations; i++) {
            v = new Vector(v.x + a.x, v.y + a.y);
            p = new Vector(p.x + v.x - 0.5 * a.x, p.y + v.y - 0.5 * a.y);
        }
        return (p, v);
    }
}

// 0 ≤ x < 7000, 0 ≤ y < 3000
readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }

    public static implicit operator Vector(Point value) => new(value.X, value.Y);

    public override string ToString() => $"({X}, {Y})";
}

class Landing(Point left, Point right) {
    public bool reached;

    /// Landing is flat, so left.y == right.y
    public Point center = left with { X = (left.X + right.X) / 2 };

    public Point Nearest(Point point) {
        return point.X < left.X ? left : point.X > right.X ? right : new Point(point.X, center.Y);
    }

    public void SetReached(Point point) {
        reached = point.X > left.X && point.X < right.X;
    }
}

readonly struct Vector(double x, double y) {
    public readonly double x = x;
    public readonly double y = y;

    public static Vector operator +(Vector a, Vector b) {
        return new Vector(a.x + b.x, a.y + b.y);
    }

    public static Vector operator /(Vector a, double divisor) {
        return new Vector(a.x / divisor, a.y / divisor);
    }

    public override string ToString() => $"({x:F1}, {y:F1})";
}

// -500 < horizontal, vertical < 500, in m/s
readonly struct Speed(int x, int y) {
    static readonly (int optimal, int max) X_LIMIT = (15, 20);
    static readonly (int optimal, int max) Y_LIMIT = (35, 40);

    /// horizontal
    readonly int x = x;

    /// vertical
    readonly int y = y;

    public bool IsSafe => Math.Abs(x) <= X_LIMIT.max && Math.Abs(y) <= Y_LIMIT.max;
    public bool IsOptimal => Math.Abs(x) < X_LIMIT.optimal && Math.Abs(y) < Y_LIMIT.optimal;
    public static bool IsOptimalX(double value) => Math.Abs(value) < X_LIMIT.optimal;
    public static bool IsOptimalY(double value) => Math.Abs(value) < Y_LIMIT.optimal;

    public static Speed From(string x, string y) {
        return new Speed(int.Parse(x), int.Parse(y));
    }

    public static implicit operator Vector(Speed value) => new(value.x, value.y);
}

struct Control(int angle, int power) {
    public int angle = angle;
    public int power = power;

    public override string ToString() => $"{angle} {power}";
}
