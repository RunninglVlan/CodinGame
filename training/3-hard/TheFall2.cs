using System;
using System.Diagnostics.CodeAnalysis;

namespace TheFall2;

/// https://www.codingame.com/training/hard/the-fall-episode-2
static class TheFall2 {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
            // One line containing one of three commands: 'X Y LEFT', 'X Y RIGHT' or 'WAIT'
            Console.WriteLine(game.Command);
        }
    }
}

class Game {
    int width, height;
    Room[,] rooms = null!;
    public string Command { get; private set; } = null!;

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
                // The absolute value of T specifies the type of the room. If T is negative, the room cannot be rotated.
                rooms[x, y] = (Room)Math.Abs(int.Parse(types[x]));
            }
        }
        int xExit = Math.Clamp(int.Parse(Console.ReadLine()!), 0, width - 1);
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int x = Math.Clamp(int.Parse(inputs[0]), 0, width - 1);
        int y = Math.Clamp(int.Parse(inputs[1]), 0, height - 1);
        string enteredFrom = inputs[2];
        int rocks = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < rocks; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int rockX = Math.Clamp(int.Parse(inputs[0]), 0, width - 1);
            int rockY = Math.Clamp(int.Parse(inputs[1]), 0, height - 1);
            string rockEnteredFrom = inputs[2];
        }

        var room = rooms[x, y];
        Point toLeft = new(x - 1, y), toBottom = new(x, y + 1), toRight = new(x + 1, y);
        var next = (room, enteredFrom) switch {
            (Room.Cross, _) => toBottom,
            (Room.Horizontal, Sides.LEFT) => toRight,
            (Room.Horizontal, Sides.RIGHT) => toLeft,
            (Room.Vertical, Sides.TOP) => toBottom,
            (Room.ArcsLeft, Sides.TOP) => toLeft,
            (Room.ArcsLeft, Sides.RIGHT) => toBottom,
            (Room.ArcsRight, Sides.TOP) => toRight,
            (Room.ArcsRight, Sides.LEFT) => toBottom,
            (Room.ThreeWayTop, Sides.LEFT) => toRight,
            (Room.ThreeWayTop, Sides.RIGHT) => toLeft,
            (Room.ThreeWayRight, _) when enteredFrom != Sides.LEFT => toBottom,
            (Room.ThreeWayBottom, _) when enteredFrom != Sides.TOP => toBottom,
            (Room.ThreeWayLeft, _) when enteredFrom != Sides.RIGHT => toBottom,
            (Room.ArcTopLeft, Sides.TOP) => toLeft,
            (Room.ArcTopRight, Sides.TOP) => toRight,
            (Room.ArcRightBottom, Sides.RIGHT) => toBottom,
            (Room.ArcLeftBottom, Sides.LEFT) => toBottom,
            _ => throw new ArgumentException($"Unknown room: {room} and entrance: {enteredFrom}")
        };

        Command = Commands.WAIT;
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

readonly record struct Point(int X, int Y) {
    public override string ToString() => $"{X} {Y}";
}

static class Commands {
    public const string ROTATE_LEFT = "LEFT";
    public const string ROTATE_RIGHT = "RIGHT";
    public const string WAIT = "WAIT";
}
