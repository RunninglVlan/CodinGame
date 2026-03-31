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
            if (previous == null || point.y != previous.Value.y) {
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
        var (nextControl, _) = FindBest(p, v, currentControl, landing.center, 5);
        if (landing.reached && speed.IsSafe) {
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

    static (Control control, double score) FindBest(Vector p, Vector v, Control current, Vector target, int depth) {
        if (depth == 0) {
            double distanceX = Math.Abs(p.x - target.x);
            double distanceY = Math.Abs(p.y - target.y);
            double speedPenalty = 0;
            if (!Speed.IsSafeX(v.x)) {
                speedPenalty += 10_000 + Math.Abs(v.x) * 100;
            }
            if (!Speed.IsSafeY(v.y)) {
                speedPenalty += 20_000 + Math.Abs(v.y) * 100;
            }
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
        var a = new Vector(thrust.x, thrust.y - GRAVITY);
        for (var i = 0; i < iterations; i++) {
            v = new Vector(v.x + a.x, v.y + a.y);
            p = new Vector(p.x + v.x - 0.5 * a.x, p.y + v.y - 0.5 * a.y);
        }
        return (p, v);
    }
}

// 0 ≤ x < 7000, 0 ≤ y < 3000
readonly struct Point(int x, int y) {
    public readonly int x = x;
    public readonly int y = y;

    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }

    public static implicit operator Vector(Point value) => new(value.x, value.y);

    public override string ToString() => $"({x}, {y})";
}

class Landing(Point start, Point end) {
    public Vector center = ((Vector)start + end) / 2.0;
    public bool reached;

    public void SetReached(Point point) {
        reached = point.x > start.x && point.x < end.x;
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
    /// actual safe horizontal speed is less than or equals 20
    const int SAFE_X = 15;

    /// actual safe vertical speed is less than or equals 40
    const int SAFE_Y = 35;

    /// horizontal
    readonly int x = x;

    /// vertical
    readonly int y = y;

    public bool IsSafe => Math.Abs(x) < SAFE_X && Math.Abs(y) < SAFE_Y;
    public static bool IsSafeX(double value) => Math.Abs(value) < SAFE_X;
    public static bool IsSafeY(double value) => Math.Abs(value) < SAFE_Y;

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
