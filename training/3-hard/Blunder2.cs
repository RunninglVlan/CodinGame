using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/hard/blunder-episode-2
int roomCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 9999);
var building = new Building(roomCount);
building.Read();
Console.WriteLine(building.MaxMoney());

readonly record struct PathState(int RoomId, int Money) : IState<PathState> {
    bool IState<PathState>.IsBetterThan(PathState other) => Money > other.Money;
}

class Building : IGame<PathState> {
    readonly StateSearch<PathState> search;

    readonly Room[] rooms;
    readonly int exitId;

    public Building(int roomCount) {
        search = new(this, roomCount);
        rooms = new Room[roomCount + 1];
        exitId = roomCount;
    }

    public void Read() {
        for (var id = 0; id < rooms.Length - 1; id++) {
            string[] roomData = Console.ReadLine()!.Split(' ');
            var room = new Room(id, int.Parse(roomData[1]), []);
            room.AddDoor(roomData[2], exitId);
            room.AddDoor(roomData[3], exitId);
            rooms[id] = room;
        }
        rooms[exitId] = new Room(exitId, 0, []);
    }

    public int MaxMoney() {
        var startRoom = rooms[0];
        return search.FindBest(new PathState(startRoom.Id, startRoom.Money)).Money;
    }

    IEnumerable<PathState> IGame<PathState>.Next(PathState current) {
        var currentRoom = rooms[current.RoomId];
        return currentRoom.Doors
                .Select(id => rooms[id])
                .Select(it => new PathState(it.Id, current.Money + it.Money));
    }

    bool IGame<PathState>.HasReachedEnd(PathState state) => state.RoomId == exitId;
}

readonly record struct Room(int Id, int Money, List<int> Doors) {
    public void AddDoor(string input, int exit) {
        if (!int.TryParse(input, out int doorId)) {
            doorId = exit;
        }
        Doors.Add(doorId);
    }
}

class StateSearch<T>(IGame<T> game, int depthMax) where T : IState<T>, IEquatable<T> {
    readonly HashSet<T> visited = [];

    /// DFS/Backtracking
    public T? FindBest(T start) {
        visited.Clear();
        visited.Add(start);
        return FindBest(start, 0);
    }

    T? FindBest(T current, int depth) {
        if (game.HasReachedEnd(current)) {
            return current;
        }
        if (depth > depthMax) {
            return default;
        }
        T? best = default;
        foreach (var next in game.Next(current)) {
            if (!visited.Add(next)) {
                continue;
            }
            var nextBest = FindBest(next, depth + 1);
            if (nextBest == null) {
                continue;
            }
            if (best == null || nextBest.IsBetterThan(best)) {
                best = nextBest;
            }
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<TState> Next(TState current);
    bool HasReachedEnd(TState state);
}

interface IState<in T> where T : IState<T> {
    bool IsBetterThan(T other);
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
