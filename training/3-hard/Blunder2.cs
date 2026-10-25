using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/hard/blunder-episode-2
int roomCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 9999);
var building = new Building(roomCount);
building.Read();
Console.WriteLine(building.MaxMoney());

readonly record struct PathState(int RoomId, int Money) : IState {
    int IState.Score => Money;

    public bool Equals(IState? other) => other is PathState s && Equals(s);
    public bool Equals(PathState other) => RoomId == other.RoomId;
    public override int GetHashCode() => RoomId;
}

class Building : IGame<PathState> {
    const int BEAM_WIDTH = 500;

    readonly BeamSearch<PathState> search;

    readonly Room[] rooms;
    readonly int exitId;

    public Building(int roomCount) {
        search = new(this);
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
        var startState = new PathState(startRoom.Id, startRoom.Money);
        return search.FindBest(startState, rooms.Length, BEAM_WIDTH).Money;
    }

    IEnumerable<PathState> IGame<PathState>.Next(PathState current) {
        var currentRoom = rooms[current.RoomId];
        return currentRoom.Doors
                .Select(id => rooms[id])
                .Select(it => new PathState(it.Id, current.Money + it.Money));
    }
}

readonly record struct Room(int Id, int Money, List<int> Doors) {
    public void AddDoor(string input, int exit) {
        if (!int.TryParse(input, out int doorId)) {
            doorId = exit;
        }
        Doors.Add(doorId);
    }
}

class BeamSearch<T>(IGame<T> game) where T : IState {
    readonly HashSet<T> visited = [];

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public T? FindBest(T start, int levels, int beamWidth = 10) {
        T? best = default;
        var currentLevel = new List<T> { start };
        for (var level = 0; level < levels; level++) {
            var nextLevel = new List<T>();
            visited.Clear();
            foreach (var current in currentLevel) {
                nextLevel.AddRange(game.Next(current));
            }
            currentLevel.Clear();
            // by score descending
            nextLevel.Sort((a, b) => b.Score.CompareTo(a.Score));
            foreach (var state in nextLevel) {
                if (!visited.Add(state)) {
                    continue;
                }
                if (currentLevel.Count >= beamWidth) {
                    break;
                }
                currentLevel.Add(state);
            }
            if (currentLevel.Count == 0) {
                break;
            }
            var bestCurrent = currentLevel.First();
            if (best == null || bestCurrent.Score > best.Score) {
                best = bestCurrent;
            }
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<TState> Next(TState current);
}

interface IState : IEquatable<IState> {
    int Score { get; }
}
