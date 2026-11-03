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
        ReadScore();
        ReadUnits();
        AddPaths();
        List<string> commands = [];
        ReadTriangles(commands);
        ReadLinks();
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

    void ReadTriangles(List<string> commands) {
        int triangles = int.Parse(Debug.ReadLine()!);
        for (var _ = 0; _ < triangles; _++) {
            string[] data = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(data[0]);
            int house2 = int.Parse(data[1]);
            int house3 = int.Parse(data[2]);
            int[] triangle = [house1, house2, house3];
            List<int> required = [.. triangle];
            int owner = int.Parse(data[3]);
            bool meCanCapture = data[4] != "0";
            bool opponentCanCapture = data[5] != "0";
            for (int i = required.Count - 1; i >= 0; i--) {
                int house = required[i];
                if (houses[house].MyUnits > 0) {
                    required.RemoveAt(i);
                }
            }
            if (required.Count != 3) {
                Debug.Log($"triangle {house1}-{house2}-{house3} needs: {required.Count}");
            }
            if (required.Count != 1 || commands.Count > 0) {
                continue;
            }
            int target = required.First();
            var source = houses.FirstOrDefault(it => it.MyUnits > 0 && !triangle.Contains(it.Id));
            var path = Pathfinding.Shortest(source!.Id, target, id => houses[id].Neighbors)!;
            for (int i = path.Count - 1; i >= 1; i--) {
                commands.Add(Commands.Move(path[i - 1], path[i], 1));
            }
            Debug.Log($"path {string.Join(',', path)}");
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
    /// Units can be moved from a house to one of the direct neighbors each turn.
    /// If there is no direct connection between the houses,
    /// the units will move closer along the shortest path if possible or stay if there is no path.
    public static string Move(int from, int to, int units) => $"MOVE {from} {to} {units}";

    /// Spawn a unit on <paramref name="house1"/> using the given triangle.
    /// Change the house order to affect the spawning location
    public static string Spawn(int house1, int house2, int house3) => $"SPAWN {house1} {house2} {house3}";
}

static class Pathfinding {
    /// BFS to end
    public static List<T>? Shortest<T>(T start, T end, Func<T, IEnumerable<T>> getNext) where T : IEquatable<T> {
        Dictionary<T, T> parents = [];
        Queue<T> queue = [];
        HashSet<T> visited = [];
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (current.Equals(end)) {
                return Reconstruct(parents, start, current);
            }
            foreach (var next in getNext(current)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next] = current;
            }
        }
        return null;
    }

    static List<T> Reconstruct<T>(Dictionary<T, T> parents, T start, T end) where T : IEquatable<T> {
        var path = new List<T>();
        var current = end;
        while (!current.Equals(start)) {
            path.Add(current);
            current = parents[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
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
