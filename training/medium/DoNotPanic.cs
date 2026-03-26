using System;
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
    Position exit;
    bool exitReachable;

    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var floors = int.Parse(inputs[0]);
        var width = int.Parse(inputs[1]);
        int maxRounds = int.Parse(inputs[2]);
        exit = Position.From(inputs[4], inputs[3]);
        int clones = int.Parse(inputs[5]);
        int additionalElevators = int.Parse(inputs[6]);
        int elevators = int.Parse(inputs[7]);
        for (var i = 0; i < elevators; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            var elevator = Position.From(inputs[1], inputs[0]);
        }
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        var leadingClone = Position.From(inputs[1], inputs[0]);
        string leadingCloneDirection = inputs[2];
        Console.WriteLine(ChooseAction(leadingClone, leadingCloneDirection));
    }

    string ChooseAction(Position clone, string direction) {
        if (exitReachable || exit.Floor != clone.Floor) {
            return Action.WAIT;
        }
        exitReachable = true;
        if (exit.X < clone.X) {
            return direction == Direction.RIGHT ? Action.BLOCK : Action.WAIT;
        }
        return direction == Direction.LEFT ? Action.BLOCK : Action.WAIT;
    }
}

readonly record struct Position(int X, int Floor) {
    public static Position From(string x, string floor) {
        return new(int.Parse(x), int.Parse(floor));
    }
}

static class Direction {
    public const string LEFT = "LEFT";
    public const string RIGHT = "RIGHT";
}

static class Action {
    public const string WAIT = "WAIT";
    public const string BLOCK = "BLOCK";
}
