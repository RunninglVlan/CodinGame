using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/hard/blunder-episode-2
int roomCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 9999);
var rooms = new Room[roomCount];
for (var id = 0; id < roomCount; id++) {
    string[] roomData = Console.ReadLine()!.Split(' ');
    var room = new Room(id, int.Parse(roomData[1]), []);
    room.AddDoor(roomData[2]);
    room.AddDoor(roomData[3]);
    rooms[id] = room;
}
Console.WriteLine(rooms.Length == 1 ? rooms.First().Money : 0);

record struct Room(int Number, int Money, List<int> Doors, bool HasExit = false) {
    public void AddDoor(string input) {
        if (int.TryParse(input, out int doorId)) {
            Doors.Add(doorId);
        } else if (!HasExit) {
            HasExit = true;
        }
    }
}
