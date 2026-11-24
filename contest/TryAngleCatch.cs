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
    const int PLAYER = 0;

    House[] houses = null!;
    Triangle[] triangles = null!;

    public void Init() {
        int houseCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
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
        ReadTriangles();
        ReadLinks();
        Console.WriteLine(string.Join(';', NextCommands()));
    }

    static void ReadScore() {
        string[] scores = Console.ReadLine()!.Split(' ');
        int myScore = int.Parse(scores[0]);
        int enemyScore = int.Parse(scores[1]);
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
        int triangleCount = int.Parse(Console.ReadLine()!);
        triangles = new Triangle[triangleCount];
        for (var i = 0; i < triangleCount; i++) {
            var triangle = ReadTriangle();
            triangles[i] = triangle;
            for (var j = 0; j < i; j++) {
                var existing = triangles[j];
                if (!existing.IsNeighbor(triangle)) {
                    continue;
                }
                existing.Neighbors.Add(triangle);
                triangle.Neighbors.Add(existing);
            }
        }
    }

    Triangle ReadTriangle() {
        string[] data = Console.ReadLine()!.Split(' ');
        int house1 = int.Parse(data[0]);
        int house2 = int.Parse(data[1]);
        int house3 = int.Parse(data[2]);
        int[] cornerHouses = [house1, house2, house3];
        int owner = int.Parse(data[3]);
        bool meCanCapture = data[4] != "0";
        bool enemyCanCapture = data[5] != "0";
        List<int> required = [.. cornerHouses];
        for (int i = required.Count - 1; i >= 0; i--) {
            int house = required[i];
            if (houses[house].MyUnits > 0) {
                required.RemoveAt(i);
            }
        }
        return new Triangle(cornerHouses, owner, required, meCanCapture);
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

    List<string> NextCommands() {
        List<string> commands = [];
        var myTriangles = triangles.Where(it => it.Owner == PLAYER).ToArray();
        if (houses.Sum(it => it.MyUnits) < 3) {
            if (myTriangles.Length == 0) {
                return commands;
            }
            commands.Add(myTriangles.First().Spawn());
            return commands;
        }
        if (TryCapture(commands)) {
            return commands;
        }
        MoveToTriangles(commands);
        return commands;
    }

    bool TryCapture(List<string> commands) {
        var captureThisTurn = triangles
                .Where(it => it.Owner != PLAYER && it.Required.Count is > 0 and < 3 && it.MeCanCapture)
                .OrderBy(it => it.Required.Count);
        foreach (var triangle in captureThisTurn) {
            Debug.Log($"triangle {triangle} needs: {triangle.Required.Count}");
            if (commands.Count > 0) {
                continue;
            }
            switch (triangle.Required.Count) {
                case 1:
                    ReachOne(triangle, commands);
                    break;
                case 2:
                    ReachTwo(triangle, commands);
                    break;
            }
        }
        return commands.Count > 0;
    }

    void ReachOne(Triangle triangle, List<string> commands) {
        int target = triangle.Required.First();
        var source = houses.FirstOrDefault(it => it.MyUnits > 0 && !triangle.Houses.Contains(it.Id));
        if (source == null) {
            // no source outside of triangle, which means one house has 2 units
            source = houses.FirstOrDefault(it => it.MyUnits > 1);
            commands.Add(Commands.Move(source!.Id, target, 1));
            return;
        }
        FollowPath(source.Id, target, commands);
    }

    void ReachTwo(Triangle triangle, List<string> commands) {
        var sources = houses.Where(it => it.MyUnits > 0 && !triangle.Houses.Contains(it.Id)).ToList();
        // first reach closest
        for (int i = sources.Count - 1; i >= 0; i--) {
            var source = sources[i];
            for (int j = triangle.Required.Count - 1; j >= 0; j--) {
                int target = triangle.Required[j];
                if (!houses[target].Neighbors.Contains(source.Id)) {
                    continue;
                }
                commands.Add(Commands.Move(source.Id, target, 1));
                sources.RemoveAt(i);
                triangle.Required.RemoveAt(j);
            }
        }
        if (triangle.Required.Count == 0) {
            return;
        }
        FollowPath(sources.First().Id, triangle.Required.First(), commands);
    }

    void FollowPath(int source, int target, List<string> commands) {
        var path = Pathfinding.Shortest(source, target, Next)!;
        Debug.Log($"path {string.Join("->", path)}", 1);
        if (path.Count != 3) {
            Debug.Log("too far", 2);
            commands.Add(Commands.Move(source, target, 1));
            return;
        }
        // 0->1->2, target is 2, 0 moves to 1, and 1 to 2
        for (int i = path.Count - 1; i >= 1; i--) {
            commands.Add(Commands.Move(path[i - 1], path[i], 1));
        }
        return;

        IEnumerable<int> Next(int current) => houses[current].Neighbors;
    }

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    void MoveToTriangles(List<string> commands) {
        var neighbors = triangles
                .Where(it => it.Owner == PLAYER)
                .SelectMany(owner => owner.Neighbors.Where(it => it.Owner != PLAYER && it.Required.Count > 0));
        foreach (var neighbor in neighbors) {
            Debug.Log($"neighbor {neighbor}");
            if (commands.Count > 0) {
                return;
            }
            MoveTo(neighbor);
        }
        foreach (var triangle in triangles.Where(it => it.Owner != PLAYER && it.Required.Count == 3)) {
            Debug.Log($"distant triangle {triangle}");
            if (commands.Count > 0) {
                return;
            }
            MoveTo(triangle);
        }
        return;

        void MoveTo(Triangle triangle) {
            int target = triangle.Required.First();
            foreach (var source in houses.Where(it => it.MyUnits > 0)) {
                commands.Add(Commands.Move(source.Id, target, 1));
            }
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

record Triangle(int[] Houses, int Owner, List<int> Required, bool MeCanCapture) {
    public HashSet<Triangle> Neighbors { get; } = [];

    public bool IsNeighbor(Triangle triangle) {
        return Houses.Intersect(triangle.Houses).Any();
    }

    public string Spawn() => Commands.Spawn(Houses[0], Houses[1], Houses[2]);

    public override string ToString() => string.Join('-', Houses);
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
