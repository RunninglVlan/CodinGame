using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DoNotPanic;

/// https://www.codingame.com/training/medium/don't-panic-episode-1
static class DoNotPanic {
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
    readonly Dictionary<int, Target> targets = new();

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int floors = int.Parse(inputs[0]);
        int width = int.Parse(inputs[1]);
        int maxRounds = int.Parse(inputs[2]);
        int exitFloor = int.Parse(inputs[3]);
        int exitX = int.Parse(inputs[4]);
        targets.Add(exitFloor, new Target(exitX));
        int clones = int.Parse(inputs[5]);
        int additionalElevators = int.Parse(inputs[6]);
        int elevatorCount = int.Parse(inputs[7]);
        for (var i = 0; i < elevatorCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int elevatorFloor = int.Parse(inputs[0]);
            int elevatorX = int.Parse(inputs[1]);
            targets.Add(elevatorFloor, new Target(elevatorX));
        }
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int leadingCloneFloor = int.Parse(inputs[0]);
        int leadingCloneX = int.Parse(inputs[1]);
        string leadingCloneDirection = inputs[2];
        Console.WriteLine(ChooseAction(leadingCloneX, leadingCloneFloor, leadingCloneDirection));
    }

    string ChooseAction(int cloneX, int cloneFloor, string direction) {
        if (cloneFloor < 0) {
            // no leading clone
            return Action.WAIT;
        }
        var target = targets[cloneFloor];
        if (target.reachable) {
            return Action.WAIT;
        }
        target.reachable = true;
        return BlockIfMovingInWrongDirection(cloneX, direction, target);
    }

    static string BlockIfMovingInWrongDirection(int cloneX, string direction, Target target) {
        if (target.x < cloneX) {
            return direction == Direction.RIGHT ? Action.BLOCK : Action.WAIT;
        }
        return direction == Direction.LEFT ? Action.BLOCK : Action.WAIT;
    }
}

class Target(int x) {
    public readonly int x = x;
    public bool reachable;
}

static class Direction {
    public const string LEFT = "LEFT";
    public const string RIGHT = "RIGHT";
}

static class Action {
    public const string WAIT = "WAIT";
    public const string BLOCK = "BLOCK";
}
