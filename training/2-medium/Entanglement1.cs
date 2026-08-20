using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/entanglement---episode-1
var game = new Game();
while (true) {
    game.ReadTile();
    game.PlaceTile();
    Console.WriteLine(game.Score);
    Console.WriteLine(game.Path);
    break;
}

class Game {
    public int Score { get; private set; }
    readonly List<Segment> path = [];
    public string Path => string.Join("; ", path);

    public void ReadTile() {
        for (var i = 0; i < 6; i++) {
            string[] entrances = Debug.ReadLine()!.Split(' ');
            int a = int.Parse(entrances[0]);
            int b = int.Parse(entrances[1]);
        }
    }

    public void PlaceTile() {
        Score++;
        path.Add(new Segment(0, -1, 0, 1));
    }
}

readonly record struct Segment(int X, int Y, int EntranceA, int EntranceB) {
    public override string ToString() => $"{X} {Y} {EntranceA} {EntranceB}";
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }
}
