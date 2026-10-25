using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/hard/blunder-episode-2
int roomCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 9999);
var rooms = new Room[roomCount + 1];
var exit = new Room(roomCount, 0, []);
for (var id = 0; id < roomCount; id++) {
    string[] roomData = Console.ReadLine()!.Split(' ');
    var room = new Room(id, int.Parse(roomData[1]), []);
    room.AddDoor(roomData[2], exit.Id);
    room.AddDoor(roomData[3], exit.Id);
    rooms[id] = room;
}
rooms[exit.Id] = exit;
int maxMoney = rooms.Select(it => it.Money).Max();
var path = Pathfinding.Cheapest(0, exit.Id, Next)!;
Console.WriteLine(path.Sum(id => rooms[id].Money));
return;

IEnumerable<(int, int cost)> Next(int current) => rooms[current].Doors
        .Select(id => rooms[id])
        .Select(it => (it.Id, maxMoney - it.Money));

readonly record struct Room(int Id, int Money, List<int> Doors) {
    public void AddDoor(string input, int exit) {
        if (!int.TryParse(input, out int doorId)) {
            doorId = exit;
        }
        Doors.Add(doorId);
    }
}

static class Pathfinding {
    /// Dijkstra
    public static List<T>? Cheapest<T>(T start, T end, Func<T, IEnumerable<(T, int cost)>> getNext)
            where T : IEquatable<T> {
        PriorityQueue<T, int> queue = new();
        Dictionary<T, T> parents = [];
        Dictionary<T, int> visited = [];
        queue.Enqueue(start, 0);
        visited.Add(start, 0);
        Debug.Log(start);
        while (queue.TryDequeue(out var current, out int currentCost)) {
            if (current.Equals(end)) {
                return Reconstruct(parents, start, current);
            }
            foreach ((var next, int cost) in getNext(current)) {
                int newCost = currentCost + cost;
                if (newCost >= visited.GetValueOrDefault(next, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(next, newCost);
                visited[next] = newCost;
                parents[next] = current;
                Debug.Log((next, newCost));
            }
        }
        return null;
    }

    static List<T> Reconstruct<T>(Dictionary<T, T> parents, T start, T end) where T : IEquatable<T> {
        var path = new List<T>();
        var node = end;
        while (!node.Equals(start)) {
            path.Add(node);
            node = parents[node];
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
