using System;
using System.Collections.Generic;
using System.Linq;

// Visual
// Connect towns with your train tracks and disrupt the opponent's
// https://www.codingame.com/contests/summer-challenge-2026-back-track-king
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game {
    const string NO_CONNECTIONS = "x";

    int player, foe;
    int width, height;
    readonly Dictionary<Point, Cell> cells = [];
    readonly Dictionary<int, Town> towns = [];

    public void Init() {
        player = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1);
        foe = player == 0 ? 1 : 0;
        width = Math.Clamp(int.Parse(Console.ReadLine()!), 21, 30);
        height = Math.Clamp(int.Parse(Console.ReadLine()!), 14, 20);
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' ');
                int regionId = int.Parse(inputs[0]);
                var type = Enum.Parse<CellType>(inputs[1]);
                cells[new Point(x, y)] = new Cell(regionId, type);
            }
        }
        int townCount = Math.Clamp(int.Parse(Console.ReadLine()!), 4, 12);
        for (var i = 0; i < townCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int townId = int.Parse(inputs[0]);
            int townX = int.Parse(inputs[1]);
            int townY = int.Parse(inputs[2]);
            var point = new Point(townX, townY);
            string desiredConnections = inputs[3];
            int[] desiredConnectionIds = desiredConnections == NO_CONNECTIONS
                    ? []
                    : [.. desiredConnections.Split(',').Select(int.Parse)];
            towns[townId] = new Town(point, desiredConnectionIds);
        }
    }

    public void Loop() {
        ReadState();
        List<string> actions = [Actions.Autoplace(towns[0], towns[1])];
        var foeTracks = cells.Values.Where(it => it.TracksOwner == foe).ToArray();
        if (foeTracks.Length > 1) {
            actions.Add(Actions.Disrupt(foeTracks.First().RegionId));
        }
        Console.WriteLine(string.Join(';', actions));
    }

    void ReadState() {
        int playerScore = int.Parse(Console.ReadLine()!);
        int foeScore = int.Parse(Console.ReadLine()!);
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' ');
                int tracksOwner = int.Parse(inputs[0]);
                var cell = cells[new Point(x, y)];
                cell.TracksOwner = tracksOwner;
                // region inked (destroyed) when this >= 3
                int instability = int.Parse(inputs[1]);
                // true if region is destroyed
                bool inked = inputs[2] != "0";
                // if this cell is part of one or more railway connections
                // this will be town ids (separated by -) in a list separated by commas
                // e.g. 0-1,1-2,1-3. "x" otherwise
                string partOfActiveConnections = inputs[3];
            }
        }
    }
}

class Cell(int regionId, CellType type) {
    public int RegionId { get; } = regionId;
    public CellType Type { get; } = type;
    public int TracksOwner { get; set; }
}

enum CellType {
    Plains,
    River,
    Mountain,
    PlaceOfInterest
}

record struct Town(Point Point, int[] DesiredConnections);

// PLACE_TRACKS x y | MESSAGE text
static class Actions {
    public static string Autoplace(Town from, Town to) {
        return $"AUTOPLACE {from.Point.X} {from.Point.Y} {to.Point.X} {to.Point.Y}";
    }

    public static string Disrupt(int regionId) {
        return $"DISRUPT {regionId}";
    }
}

readonly record struct Point(int X, int Y);
