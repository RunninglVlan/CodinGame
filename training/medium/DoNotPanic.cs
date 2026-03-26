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
    Target exit = null!;
    readonly Dictionary<int, Target> elevators = new();

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int floors = int.Parse(inputs[0]);
        int width = int.Parse(inputs[1]);
        int maxRounds = int.Parse(inputs[2]);
        exit = new(Position.From(inputs[4], inputs[3]));
        int clones = int.Parse(inputs[5]);
        int additionalElevators = int.Parse(inputs[6]);
        int elevatorCount = int.Parse(inputs[7]);
        for (var i = 0; i < elevatorCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            var elevatorPosition = Position.From(inputs[1], inputs[0]);
            elevators.Add(elevatorPosition.Floor, new Target(elevatorPosition));
        }
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var leadingClone = Position.From(inputs[1], inputs[0]);
        string leadingCloneDirection = inputs[2];
        Console.WriteLine(ChooseAction(leadingClone, leadingCloneDirection));
    }

    string ChooseAction(Position clone, string direction) {
        if (clone.Floor < 0) {
            // no leading clone
            return Action.WAIT;
        }
        if (exit.reachable) {
            return Action.WAIT;
        }
        if (exit.position.Floor != clone.Floor) {
            var elevator = elevators[clone.Floor];
            if (elevator.reachable) {
                return Action.WAIT;
            }
            elevator.reachable = true;
            return BlockIfMovingInWrongDirection(clone, direction, elevator);
        }
        exit.reachable = true;
        return BlockIfMovingInWrongDirection(clone, direction, exit);
    }

    static string BlockIfMovingInWrongDirection(Position clone, string direction, Target target) {
        if (target.position.X < clone.X) {
            return direction == Direction.RIGHT ? Action.BLOCK : Action.WAIT;
        }
        return direction == Direction.LEFT ? Action.BLOCK : Action.WAIT;
    }
}

class Target(Position position) {
    public readonly Position position = position;
    public bool reachable;
}

readonly record struct Position(int X, int Floor) {
    public static Position From(string x, string floor) {
        return new(int.Parse(x), int.Parse(floor));
    }

    public override string ToString() => $"({X}, {Floor})";
}

static class Direction {
    public const string LEFT = "LEFT";
    public const string RIGHT = "RIGHT";
}

static class Action {
    public const string WAIT = "WAIT";
    public const string BLOCK = "BLOCK";
}
