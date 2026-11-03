using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
            string[] data = Console.ReadLine()!.Split(' ');
            int houseId = int.Parse(data[0]);
            houses[houseId] = new House(houseId);
            Math.Clamp(int.Parse(data[1]), 0, 1920 - 1); // houseX
            Math.Clamp(int.Parse(data[2]), 0, 1080 - 1); // houseY
        }
    }

    public void Loop() {
        foreach (var house in houses) {
            house.Clear();
        }
        // todo: capture triangle, spawn unit
        // todo: capture other triangle, spawn unit
        // todo: maximize captured triangles
        // todo: move and spawn - can move to neighbors each turn, but move command can have final target
        ReadScore();
        ReadUnits();
        AddPaths();
        ReadTriangles();
        ReadLinks();
        var commands = houses
                .Where(it => it.MyUnits > 0)
                .Select(it => Commands.Move(it.Id, it.Neighbors.First(), 1));
        Console.WriteLine(string.Join(';', commands));
    }

    static void ReadScore() {
        string[] scores = Console.ReadLine()!.Split(' ');
        int myScore = int.Parse(scores[0]);
        int opponentScore = int.Parse(scores[1]);
    }

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    void ReadUnits() {
        for (var _ = 0; _ < houses.Length; _++) {
            string[] data = Console.ReadLine()!.Split(' ');
            int houseId = int.Parse(data[0]);
            int myUnits = int.Parse(data[1]);
            int enemyUnits = int.Parse(data[2]);
            var house = houses[houseId];
            house.MyUnits = myUnits;
            house.EnemyUnits = enemyUnits;
        }
    }

    void AddPaths() {
        int paths = int.Parse(Console.ReadLine()!);
        for (var _ = 0; _ < paths; _++) {
            string[] data = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(data[0]);
            int house2 = int.Parse(data[1]);
            houses[house1].Neighbors.Add(house2);
            houses[house2].Neighbors.Add(house1);
        }
    }

    void ReadTriangles() {
        int triangles = int.Parse(Debug.ReadLine()!);
        for (var _ = 0; _ < triangles; _++) {
            string[] data = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(data[0]);
            int house2 = int.Parse(data[1]);
            int house3 = int.Parse(data[2]);
            int owner = int.Parse(data[3]);
            bool meCanCapture = data[4] != "0";
            bool opponentCanCapture = data[5] != "0";
            var captured = 0;
            if (houses[house1].MyUnits > 0) {
                captured++;
            }
            if (houses[house2].MyUnits > 0) {
                captured++;
            }
            if (houses[house3].MyUnits > 0) {
                captured++;
            }
            // Debug.Log($"triangle {house1}-{house2}-{house3} meCaptured: {captured / 3f}");
        }
    }

    static void ReadLinks() {
        // ignore in lower leagues
        int linkableCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < linkableCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(inputs[0]);
            int house2 = int.Parse(inputs[1]);
        }
    }
}

record House(int Id) {
    public HashSet<int> Neighbors { get; } = [];
    public int MyUnits { get; set; }
    public int EnemyUnits { get; set; }

    public void Clear() {
        Neighbors.Clear();
        MyUnits = 0;
        EnemyUnits = 0;
    }
}

static class Commands {
    public static string Move(int from, int to, int units) => $"MOVE {from} {to} {units}";
}

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
