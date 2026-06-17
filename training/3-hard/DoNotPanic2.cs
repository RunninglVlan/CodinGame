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
    Grid drive = null!;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int floors = int.Parse(inputs[0]);
        int width = int.Parse(inputs[1]);
        drive = new Grid(width, floors);
        int maxRounds = int.Parse(inputs[2]);
        int exitFloor = int.Parse(inputs[3]);
        int exitX = int.Parse(inputs[4]);
        drive.Get(exitX, exitFloor).type = CellType.Exit;
        int clones = int.Parse(inputs[5]);
        int additionalElevators = int.Parse(inputs[6]);
        Console.Error.WriteLine($"additionalElevators: {additionalElevators}");
        int elevatorCount = int.Parse(inputs[7]);
        for (var i = 0; i < elevatorCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int elevatorFloor = int.Parse(inputs[0]);
            int elevatorX = int.Parse(inputs[1]);
            drive.Get(elevatorX, elevatorFloor).type = CellType.Elevator;
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
        var targets = drive.GetFloor(floor).Where(it => it.type is CellType.Elevator or CellType.Exit).ToArray();
        if (targets.Length == 0) {
            drive.Get(x, floor).type = CellType.Elevator;
            return Actions.ELEVATOR;
        }
        int target = targets
                .Select(it => it.point.X)
                .MinBy(targetX => {
                    int distance = Math.Abs(targetX - x);
                    bool sameDirection = targetX < x && direction == Directions.LEFT
                            || targetX > x && direction == Directions.RIGHT;
                    return (distance, !sameDirection);
                });
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

class Grid {
    readonly int width;
    readonly Cell[,] cells;

    public Grid(int width, int floors) {
        this.width = width;
        cells = new Cell[width, floors];
        for (var floor = 0; floor < floors; floor++) {
            for (var x = 0; x < width; x++) {
                cells[x, floor] = new Cell(x, floor);
            }
        }
    }

    public Cell Get(int x, int floor) => cells[x, floor];

    public IEnumerable<Cell> GetFloor(int y) {
        for (var x = 0; x < width; x++) {
            yield return cells[x, y];
        }
    }
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type = CellType.Empty;
}

enum CellType {
    Empty,
    Elevator,
    Exit
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

readonly record struct Point(int X, int Y);
