using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TheBridge;

/// https://www.codingame.com/training/hard/the-bridge-episode-2
static class TheBridge {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
            Console.WriteLine(game.Action);
        }
    }
}

class Game {
    Motorbike[] motorbikes = null!;

    /// the minimum amount of motorbikes that must survive
    int required;

    Road road = null!;
    public string Action { get; private set; } = null!;

    public void Init() {
        int motorbikeCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 4);
        motorbikes = new Motorbike[motorbikeCount];
        for (var i = 0; i < motorbikeCount; i++) {
            motorbikes[i] = new Motorbike();
        }
        required = Math.Clamp(int.Parse(Console.ReadLine()!), 1, motorbikeCount);
        road = new Road();
        road.ReadInput();
    }

    public void Loop() {
        int speed = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 49);
        Action = Actions.SPEED;
        UpdateMotorbikes();
        int safeMotorbikes = motorbikes.Count(it => it is { IsActive: true, OnSafeLane: true });
        if (safeMotorbikes >= required) {
            return;
        }
        if (motorbikes.Count(it => it.IsActive) < Road.MAX_LANES) {
            Action = MoveToSafeLane();
            return;
        }
        int x = motorbikes.First().X;
        var nextHole = road.NextHole(x, 0);
        if (nextHole.distance == 1) {
            Action = Actions.JUMP;
        } else {
            Action = speed > nextHole.length + 1 ? Actions.SLOW :
                    speed < nextHole.length + 1 ? Actions.SPEED : Actions.WAIT;
        }
    }

    void UpdateMotorbikes() {
        foreach (var motorbike in motorbikes) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int x = Math.Clamp(int.Parse(inputs[0]), 0, 499);
            int y = Math.Clamp(int.Parse(inputs[1]), 0, 3);
            string active = inputs[2];
            motorbike.Update(x, y, active);
            if (motorbike.IsActive) {
                motorbike.OnSafeLane = road.IsLaneSafe(x, y);
            }
        }
    }

    string MoveToSafeLane() {
        if (motorbikes.Count(it => it.IsActive) > 1) {
            throw new ArgumentException("TODO: More than 1 motorbike");
        }
        var motorbike = motorbikes.First(it => it.IsActive);
        return motorbike.MoveToSafeLane(road);
    }
}

static class Actions {
    public const string SPEED = "SPEED";
    public const string SLOW = "SLOW";
    public const string JUMP = "JUMP";
    public const string WAIT = "WAIT";
    public const string UP = "UP";
    public const string DOWN = "DOWN";
}

class Road {
    public const int MAX_LANES = 4;

    int xMax;
    bool[,] safeCells = null!;

    public void ReadInput() {
        const char safeSpace = '.';
        string lane = Console.ReadLine()!;
        Console.Error.WriteLine(lane);
        xMax = lane.Length;
        safeCells = new bool[xMax, MAX_LANES];
        for (var y = 0; y < MAX_LANES; y++) {
            if (y != 0) {
                lane = Console.ReadLine()!;
                Console.Error.WriteLine(lane);
            }
            for (var x = 0; x < xMax; x++) {
                safeCells[x, y] = lane[x] == safeSpace;
            }
        }
    }

    public bool IsLaneSafe(int xStart, int y) {
        if (y is < 0 or > MAX_LANES - 1) {
            return false;
        }
        for (int x = xStart; x < xMax; x++) {
            if (!safeCells[x, y]) {
                return false;
            }
        }
        return true;
    }

    public (int distance, int length) NextHole(int x, int y) {
        var distance = 0;
        while (x + distance < xMax && safeCells[x + distance, y]) {
            distance++;
        }
        var length = 0;
        while (x + distance + length < xMax && !safeCells[x + distance + length, y]) {
            length++;
        }
        return (distance, length);
    }
}

class Motorbike {
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool IsActive { get; private set; }
    public bool OnSafeLane { get; set; }

    public void Update(int x, int y, string active) {
        X = x;
        Y = y;
        IsActive = active == "1";
    }

    public string MoveToSafeLane(Road road) {
        if (road.IsLaneSafe(X, Y - 1)) {
            return Actions.UP;
        }
        return road.IsLaneSafe(X, Y + 1)
                ? Actions.DOWN
                : throw new ArgumentException($"No safe lane around ({X}, {Y})");
    }
}
