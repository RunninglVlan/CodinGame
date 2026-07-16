using System;
using System.Collections.Generic;
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
            Console.WriteLine(game.Command);
        }
    }
}

class Game {
    int width, height;
    Room[,] rooms = null!;
    Point exit;
    Queue<string>? commands;
    public string Command { get; private set; } = null!;

    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(inputs[0]), 1, 20);
        height = Math.Clamp(int.Parse(inputs[1]), 1, 20);
        rooms = new Room[width, height];
        for (var y = 0; y < height; y++) {
            string[] types = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < width; x++) {
                int roomType = int.Parse(types[x]);
                var room = new Room(new Point(x, y)) {
                    Type = (RoomType)Math.Abs(roomType)
                };
                rooms[x, y] = room;
            }
        }
        int xExit = Math.Clamp(int.Parse(Console.ReadLine()!), 0, width - 1);
        exit = new Point(xExit, height - 1);
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int x = Math.Clamp(int.Parse(inputs[0]), 0, width - 1);
        int y = Math.Clamp(int.Parse(inputs[1]), 0, height - 1);
        var current = new Point(x, y);
        string enteredFrom = inputs[2];
        int rocks = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < rocks; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int rockX = Math.Clamp(int.Parse(inputs[0]), 0, width - 1);
            int rockY = Math.Clamp(int.Parse(inputs[1]), 0, height - 1);
            string rockEnteredFrom = inputs[2];
        }
        commands ??= new Queue<string>(BuildTunnel((current, enteredFrom)));
        Command = commands.TryDequeue(out var command) ? command : Commands.WAIT;
    }

    IEnumerable<string> BuildTunnel((Point point, string from) current) {
        while (current.point != exit) {
            var room = rooms[current.point.X, current.point.Y];
            (Point, string from) next;
            while (!room.TryMove(current.from, out next)) {
                room.RotateLeft();
                yield return $"{room.Point} {Commands.ROTATE_LEFT}";
            }
            current = next;
        }
    }
}

record Room(Point Point) {
    public RoomType Type { get; set; }

    public bool TryMove(string from, out (Point point, string from) next) {
        next = default;
        var toLeft = (Point with { X = Point.X - 1 }, Sides.RIGHT);
        var toBottom = (Point with { Y = Point.Y + 1 }, Sides.TOP);
        var toRight = (Point with { X = Point.X + 1 }, Sides.LEFT);
        (Point point, string to)? result = (Type, from) switch {
            (RoomType.Cross, _) => toBottom,
            (RoomType.Horizontal, Sides.LEFT) => toRight,
            (RoomType.Horizontal, Sides.RIGHT) => toLeft,
            (RoomType.Vertical, Sides.TOP) => toBottom,
            (RoomType.ArcsLeft, Sides.TOP) => toLeft,
            (RoomType.ArcsLeft, Sides.RIGHT) => toBottom,
            (RoomType.ArcsRight, Sides.TOP) => toRight,
            (RoomType.ArcsRight, Sides.LEFT) => toBottom,
            (RoomType.ThreeWayTop, Sides.LEFT) => toRight,
            (RoomType.ThreeWayTop, Sides.RIGHT) => toLeft,
            (RoomType.ThreeWayRight, _) when from != Sides.LEFT => toBottom,
            (RoomType.ThreeWayBottom, _) when from != Sides.TOP => toBottom,
            (RoomType.ThreeWayLeft, _) when from != Sides.RIGHT => toBottom,
            (RoomType.ArcTopLeft, Sides.TOP) => toLeft,
            (RoomType.ArcTopRight, Sides.TOP) => toRight,
            (RoomType.ArcRightBottom, Sides.RIGHT) => toBottom,
            (RoomType.ArcLeftBottom, Sides.LEFT) => toBottom,
            _ => null
        };
        if (result == null) {
            return false;
        }
        next = result.Value;
        return true;
    }

    public void RotateLeft() {
        Type = Type switch {
            RoomType.Horizontal => RoomType.Vertical,
            RoomType.Vertical => RoomType.Horizontal,
            RoomType.ArcsLeft => RoomType.ArcsRight,
            RoomType.ArcsRight => RoomType.ArcsLeft,
            _ => throw new ArgumentException($"Unknown room: {Type}")
        };
    }
}

enum RoomType {
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
