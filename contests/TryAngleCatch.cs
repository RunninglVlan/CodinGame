using System;
using System.Collections.Generic;

// Visual
// https://www.codingame.com/contests/fall-challenge-2026
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game {
    House[] houses = null!;

    public void Init() {
        int houseCount = Math.Clamp(int.Parse(Debug.ReadLine()!), 1, 50);
        houses = new House[houseCount];
        for (var _ = 0; _ < houseCount; _++) {
            string[] data = Debug.ReadLine()!.Split(' ');
            int houseId = int.Parse(data[0]);
            houses[houseId] = new House([]);
            Math.Clamp(int.Parse(data[1]), 0, 1920 - 1); // houseX
            Math.Clamp(int.Parse(data[2]), 0, 1080 - 1); // houseY
        }
    }

    public void Loop() {
        // todo: capture triangle, spawn unit
        // todo: capture other triangle, spawn unit
        // todo: maximize captured triangles
        // todo: move and spawn - can move to neighbors each turn, but move command can have final target
        string[] inputs = Console.ReadLine()!.Split(' ');
        int myScore = int.Parse(inputs[0]);
        int opponentScore = int.Parse(inputs[1]);
        for (var i = 0; i < houses.Length; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int houseId = int.Parse(inputs[0]);
            int myUnits = int.Parse(inputs[1]);
            int opponentUnits = int.Parse(inputs[2]);
        }
        AddPaths();
        int triangleCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < triangleCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(inputs[0]);
            int house2 = int.Parse(inputs[1]);
            int house3 = int.Parse(inputs[2]);
            int owner = int.Parse(inputs[3]);
            bool meCanCapture = inputs[4] != "0";
            bool opponentCanCapture = inputs[5] != "0";
        }
        {
            // ignore in lower leagues
            int linkableCount = int.Parse(Console.ReadLine()!);
            for (var i = 0; i < linkableCount; i++) {
                inputs = Console.ReadLine()!.Split(' ');
                int house1 = int.Parse(inputs[0]);
                int house2 = int.Parse(inputs[1]);
            }
        }
        Console.WriteLine("MOVE 3 17 1");
    }

    void AddPaths() {
        foreach (var house in houses) {
            house.Neighbors.Clear();
        }
        int pathCount = int.Parse(Console.ReadLine()!);
        for (var _ = 0; _ < pathCount; _++) {
            string[] data = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(data[0]);
            int house2 = int.Parse(data[1]);
            houses[house1].Neighbors.Add(house2);
            houses[house2].Neighbors.Add(house1);
        }
    }
}

record House(HashSet<int> Neighbors);

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; } = true;

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        if (!Enabled) {
            return;
        }
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
