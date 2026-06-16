using System;

static class DoNotPanic {
    static int[] targets = null!;

    static void Main() {
        Init();
        while (true) {
            Loop();
        }
    }

    static void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int floors = int.Parse(inputs[0]);
        targets = new int[floors];
        int exitFloor = int.Parse(inputs[3]);
        int exitX = int.Parse(inputs[4]);
        targets[exitFloor] = exitX;
        int elevatorCount = int.Parse(inputs[7]);
        for (var i = 0; i < elevatorCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int elevatorFloor = int.Parse(inputs[0]);
            int elevatorX = int.Parse(inputs[1]);
            targets[elevatorFloor] = elevatorX;
        }
    }

    static void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int leadingCloneFloor = int.Parse(inputs[0]);
        int leadingCloneX = int.Parse(inputs[1]);
        string leadingCloneDirection = inputs[2];
        Console.WriteLine(ChooseAction(leadingCloneX, leadingCloneFloor, leadingCloneDirection));
    }

    static string ChooseAction(int x, int floor, string direction) {
        bool noLeadingClone = floor < 0;
        return noLeadingClone ? Actions.WAIT : BlockIfMovingInWrongDirection(x, floor, direction);
    }

    static string BlockIfMovingInWrongDirection(int x, int floor, string direction) {
        int target = targets[floor];
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
}
