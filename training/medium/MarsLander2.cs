using System;

/// https://www.codingame.com/training/medium/mars-lander-episode-2
static class MarsLander2 {
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
    (Point start, Point end) landing;

    bool ReachedLanding(Point point) {
        // "reaching landing" a bit quicker
        const float landingShift = 250;
        return point.x > landing.start.x - landingShift && point.x < landing.end.x + landingShift;
    }

    public void Init() {
        // 2 ≤ number < 30
        int numberOfSurfacePoints = int.Parse(Console.ReadLine()!);
        Point? previousPoint = null;
        for (var i = 0; i < numberOfSurfacePoints; i++) {
            string[] position = Console.ReadLine()!.Split(' ');
            var point = Point.From(position[0], position[1]);
            if (previousPoint == null || point.y != previousPoint.Value.y) {
                previousPoint = point;
                continue;
            }
            landing = (previousPoint.Value, point);
            Console.Error.WriteLine($"Landing: {landing}");
        }
    }

    public void Loop() {
        var (point, speed, _, _, _) = ReadInput();
        var result = new Result { power = 3 };
        if (!ReachedLanding(point)) {
            MoveToLanding(point, speed, ref result);
        } else {
            Land(speed, ref result);
        }
        if (Math.Abs(speed.vertical) > 30) {
            result.power = 4;
            result.angle = Math.Abs(speed.horizontal) < 20 ? 0 : Math.Clamp(speed.horizontal, -30, 30);
        }
        Console.WriteLine($"{result.angle} {result.power}");
    }

    static (Point, Speed, int, int, int) ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var point = Point.From(inputs[0], inputs[1]);
        var speed = Speed.From(inputs[2], inputs[3]);
        // 0 ≤ fuel ≤ 2000
        int fuel = int.Parse(inputs[4]);
        // -90 (right) ≤ angle ≤ 90 (left)
        int angle = int.Parse(inputs[5]);
        // 0 ≤ power ≤ 4
        int power = int.Parse(inputs[6]);
        return (point, speed, fuel, angle, power);
    }

    void MoveToLanding(Point point, Speed speed, ref Result result) {
        // x is from 0 to 7000
        const float maxDistance = 3500;
        int landingCenterX = (landing.start.x + landing.end.x) / 2;
        int distance = point.x - landingCenterX;
        float distancePercent = distance / maxDistance;
        result.angle = (int)(distancePercent * 40);
        // if became too fast, reverse angle to slow down
        if (Math.Abs(speed.horizontal) > 40) {
            result.angle = Math.Clamp(speed.horizontal, -65, 65);
        }
        Console.Error.WriteLine("Moving to landing");
    }

    static void Land(Speed speed, ref Result result) {
        if (!speed.IsSafe) {
            Console.Error.WriteLine("Reached landing, normalizing speed");
            result.angle = Math.Clamp(speed.horizontal, -90, 90);
        } else {
            Console.Error.WriteLine("Normalized speed, straightened");
            result.angle = 0;
        }
    }
}

// 0 ≤ x < 7000, 0 ≤ y < 3000
readonly struct Point(int x, int y) {
    public readonly int x = x;
    public readonly int y = y;

    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }

    public override string ToString() => $"({x}, {y})";
}

// -500 < horizontal, vertical < 500, in m/s
readonly struct Speed(int horizontal, int vertical) {
    public readonly int horizontal = horizontal;
    public readonly int vertical = vertical;

    public bool IsSafe => Math.Abs(horizontal) < 20 && Math.Abs(vertical) < 40;

    public static Speed From(string horizontal, string vertical) {
        return new Speed(int.Parse(horizontal), int.Parse(vertical));
    }
}

struct Result {
    public int angle;
    public int power;
}
