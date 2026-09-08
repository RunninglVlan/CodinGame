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
    int player, foe;
    int playerScore, foeScore;
    int width, height;
    Cell[,] cells = null!;
    Town[] towns = null!;

    public void Init() {
        player = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1);
        foe = player == 0 ? 1 : 0;
        width = Math.Clamp(int.Parse(Console.ReadLine()!), 21, 30);
        height = Math.Clamp(int.Parse(Console.ReadLine()!), 14, 20);
        cells = new Cell[width, height];
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' ');
                int regionId = int.Parse(inputs[0]);
                var type = Enum.Parse<CellType>(inputs[1]);
                cells[x, y] = new Cell(regionId, type);
            }
        }
        int townCount = Math.Clamp(int.Parse(Console.ReadLine()!), 4, 12);
        towns = new Town[townCount];
        for (var i = 0; i < townCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int townId = int.Parse(inputs[0]);
            int townX = int.Parse(inputs[1]);
            int townY = int.Parse(inputs[2]);
            string desiredConnections = inputs[3];
            int[] desiredConnectionIds = desiredConnections == Town.NO_DESIRED_CONNECTIONS
                    ? []
                    : [.. desiredConnections.Split(',').Select(int.Parse)];
            towns[i] = new Town(townId, townX, townY, desiredConnectionIds);
        }
    }

    public void Loop() {
        playerScore = int.Parse(Console.ReadLine()!);
        foeScore = int.Parse(Console.ReadLine()!);
        List<Cell> foeTracks = [];
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' ');
                int tracksOwner = int.Parse(inputs[0]);
                var cell = cells[x, y];
                cell.TracksOwner = tracksOwner;
                if (tracksOwner == foe) {
                    foeTracks.Add(cell);
                }
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

        List<string> actions = [Actions.Autoplace(towns[0], towns[1])];
        if (foeTracks.Count > 0) {
            actions.Add(Actions.Disrupt(foeTracks.First().RegionId));
        }
        Console.WriteLine(string.Join(';', actions));
    }
}

class Cell(int regionId, CellType type) {
    public int RegionId { get; init; } = regionId;
    public CellType Type { get; init; } = type;
    public int TracksOwner { get; set; }
}

enum CellType {
    Plains,
    River,
    Mountain,
    PlaceOfInterest
}

record struct Town(int Id, int X, int Y, int[] DesiredConnections) {
    public const string NO_DESIRED_CONNECTIONS = "x";
}

// PLACE_TRACKS x y | MESSAGE text
static class Actions {
    public static string Autoplace(Town from, Town to) {
        return $"AUTOPLACE {from.X} {from.Y} {to.X} {to.Y}";
    }

    public static string Disrupt(int regionId) {
        return $"DISRUPT {regionId}";
    }
}
