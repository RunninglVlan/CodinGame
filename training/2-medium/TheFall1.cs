using System;
using System.Diagnostics.CodeAnalysis;

namespace TheFall1;

/// https://www.codingame.com/training/medium/the-fall-episode-1
static class TheFall1 {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
            Console.WriteLine($"{game.Indy.x} {game.Indy.y}");
        }
    }
}

class Game {
    int width, height;
    Room[,] rooms = null!;
    public (int x, int y) Indy { get; private set; }

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(inputs[0]), 1, 20);
        height = Math.Clamp(int.Parse(inputs[1]), 1, 20);
        rooms = new Room[width, height];
        for (var y = 0; y < height; y++) {
            string line = Console.ReadLine()!;
            Console.Error.WriteLine(line);
            string[] types = line.Split(' ');
            for (var x = 0; x < width; x++) {
                rooms[x, y] = (Room)int.Parse(types[x]);
            }
        }
        int xExit = Math.Clamp(int.Parse(Console.ReadLine()!), 0, width - 1);
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int x = Math.Clamp(int.Parse(inputs[0]), 0, width - 1);
        int y = Math.Clamp(int.Parse(inputs[1]), 0, height - 1);
        string enteredFrom = inputs[2];
        var room = rooms[x, y];
        Indy = (room, enteredFrom) switch {
            (Room.Cross, _) => (x, y + 1),
            (Room.Horizontal, Sides.LEFT) => (x + 1, y),
            (Room.Horizontal, Sides.RIGHT) => (x - 1, y),
            (Room.Vertical, Sides.TOP) => (x, y + 1),
            (Room.ArcsLeft, Sides.TOP) => (x - 1, y),
            (Room.ArcsLeft, Sides.RIGHT) => (x, y + 1),
            (Room.ArcsRight, Sides.TOP) => (x + 1, y),
            (Room.ArcsRight, Sides.LEFT) => (x, y + 1),
            (Room.ThreeWayTop, Sides.LEFT) => (x + 1, y),
            (Room.ThreeWayTop, Sides.RIGHT) => (x - 1, y),
            (Room.ThreeWayRight, _) when enteredFrom != Sides.LEFT => (x, y + 1),
            (Room.ThreeWayBottom, _) when enteredFrom != Sides.TOP => (x, y + 1),
            (Room.ThreeWayLeft, _) when enteredFrom != Sides.RIGHT => (x, y + 1),
            (Room.ArcTopLeft, Sides.TOP) => (x - 1, y),
            (Room.ArcTopRight, Sides.TOP) => (x + 1, y),
            (Room.ArcRightBottom, Sides.RIGHT) => (x, y + 1),
            (Room.ArcLeftBottom, Sides.LEFT) => (x, y + 1),
            _ => throw new ArgumentException($"Unknown room: {room} and entrance: {enteredFrom}")
        };
    }
}

enum Room {
    /// Type0 - This room type is not part of the tunnel per se as Indy cannot move across it
    Wall,

    /// Type1 - Can move from LEFT/TOP/RIGHT to BOTTOM
    Cross,

    /// Type2 - Can move from LEFT to RIGHT and reversed
    Horizontal,

    /// Type3 - Can move from TOP to BOTTOM
    Vertical,

    /// Type4 - Can move from TOP to LEFT and from RIGHT to BOTTOM
    ArcsLeft,

    /// Type5 - Can move from TOP to RIGHT and from LEFT to BOTTOM
    ArcsRight,

    /// Type6 - Can move from LEFT to RIGHT and reversed
    ThreeWayTop,

    /// Type7 - Can move from TOP/RIGHT to BOTTOM
    ThreeWayRight,

    /// Type8 - Can move from LEFT/RIGHT to BOTTOM
    ThreeWayBottom,

    /// Type9 - Can move from TOP/LEFT to BOTTOM
    ThreeWayLeft,

    /// Type10 - Can move from TOP to LEFT
    ArcTopLeft,

    /// Type11 - Can move from TOP to RIGHT
    ArcTopRight,

    /// Type12 - Can move from RIGHT to BOTTOM
    ArcRightBottom,

    /// Type13 - Can move from LEFT to BOTTOM
    ArcLeftBottom
}

static class Sides {
    public const string LEFT = "LEFT";
    public const string TOP = "TOP";
    public const string RIGHT = "RIGHT";
}
