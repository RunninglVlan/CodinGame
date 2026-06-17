using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DoNotPanic2;

/// https://www.codingame.com/training/hard/don't-panic-episode-2
static class DoNotPanic2 {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
        }
    }
}

class Game {
    List<int>[] targets = null!;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int floors = int.Parse(inputs[0]);
        targets = Enumerable.Range(0, floors)
                .Select(_ => new List<int>())
                .ToArray();
        int width = int.Parse(inputs[1]);
        int maxRounds = int.Parse(inputs[2]);
        int exitFloor = int.Parse(inputs[3]);
        int exitX = int.Parse(inputs[4]);
        targets[exitFloor].Add(exitX);
        int clones = int.Parse(inputs[5]);
        int additionalElevators = int.Parse(inputs[6]);
        Console.Error.WriteLine($"additionalElevators: {additionalElevators}");
        int elevatorCount = int.Parse(inputs[7]);
        for (var i = 0; i < elevatorCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int elevatorFloor = int.Parse(inputs[0]);
            int elevatorX = int.Parse(inputs[1]);
            targets[elevatorFloor].Add(elevatorX);
        }
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int leadingCloneFloor = int.Parse(inputs[0]);
        int leadingCloneX = int.Parse(inputs[1]);
        string leadingCloneDirection = inputs[2];
        Console.WriteLine(ChooseAction(leadingCloneX, leadingCloneFloor, leadingCloneDirection));
    }

    string ChooseAction(int x, int floor, string direction) {
        bool noLeadingClone = floor < 0;
        if (noLeadingClone) {
            return Actions.WAIT;
        }
        var floorTargets = targets[floor];
        if (floorTargets.Count == 0) {
            floorTargets.Add(x);
            return Actions.ELEVATOR;
        }
        int target = floorTargets.MinBy(it => Math.Abs(it - x));
        return BlockIfMovingInWrongDirection(x, target, direction);
    }

    static string BlockIfMovingInWrongDirection(int x, int target, string direction) {
        if (target < x) {
            return direction == Directions.RIGHT ? Actions.BLOCK : Actions.WAIT;
        }
        if (target > x) {
            return direction == Directions.LEFT ? Actions.BLOCK : Actions.WAIT;
        }
        return Actions.WAIT;
    }
}

static class Directions {
    public const string LEFT = "LEFT";
    public const string RIGHT = "RIGHT";
}

static class Actions {
    public const string WAIT = "WAIT";
    public const string BLOCK = "BLOCK";
    public const string ELEVATOR = "ELEVATOR";
}
