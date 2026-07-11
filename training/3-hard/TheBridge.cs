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
        } else if (speed == nextHole.length + 1) {
            Action = Actions.WAIT;
        }
    }

    void UpdateMotorbikes() {
        for (var i = 0; i < motorbikes.Length; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int x = Math.Clamp(int.Parse(inputs[0]), 0, 499);
            int y = Math.Clamp(int.Parse(inputs[1]), 0, 3);
            int active = int.Parse(inputs[2]);
            var motorbike = new Motorbike(x, y, active == Motorbike.IS_ACTIVE);
            motorbikes[i] = motorbike;
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

    bool[,] safeCells = null!;

    public void ReadInput() {
        const char safeSpace = '.';
        string lane = Console.ReadLine()!;
        Console.Error.WriteLine(lane);
        int xMax = lane.Length;
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
        if (y < 0 || y > safeCells.GetLength(1) - 1) {
            return false;
        }
        for (int x = xStart; x < safeCells.GetLength(0); x++) {
            if (!safeCells[x, y]) {
                return false;
            }
        }
        return true;
    }

    public (int distance, int length) NextHole(int x, int y) {
        var distance = 0;
        while (x + distance < safeCells.GetLength(0) && safeCells[x + distance, y]) {
            distance++;
        }
        var length = 0;
        while (x + distance + length < safeCells.GetLength(0) && !safeCells[x + distance + length, y]) {
            length++;
        }
        return (distance, length);
    }
}

record Motorbike(int X, int Y, bool IsActive) {
    public const int IS_ACTIVE = 1;

    public bool OnSafeLane { get; set; }

    public string MoveToSafeLane(Road road) {
        if (road.IsLaneSafe(X, Y - 1)) {
            return Actions.UP;
        }
        return road.IsLaneSafe(X, Y + 1)
                ? Actions.DOWN
                : throw new ArgumentException($"No safe lane around ({X}, {Y})");
    }
}
