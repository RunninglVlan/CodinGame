using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

class Game : IGame<GameState> {
    readonly Pathfinding<GameState> pathfinding;
    int width, height;
    Dictionary<Point, Room> rooms = null!;
    Point exit;
    Queue<string>? commands;
    public string Command { get; private set; } = null!;

    public Game() {
        pathfinding = new Pathfinding<GameState>(this, 15);
    }

    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(inputs[0]), 1, 20);
        height = Math.Clamp(int.Parse(inputs[1]), 1, 20);
        rooms = new Dictionary<Point, Room>();
        for (var y = 0; y < height; y++) {
            string[] types = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < width; x++) {
                var point = new Point(x, y);
                int roomType = int.Parse(types[x]);
                rooms[point] = new Room(point, roomType > 0) {
                    Type = (RoomType)Math.Abs(roomType)
                };
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
        commands ??= BuildTunnel(current, enteredFrom);
        Command = commands.TryDequeue(out var command) ? command : Commands.WAIT;
    }

    Queue<string> BuildTunnel(Point start, string enteredFrom) {
        var startState = new GameState(start, enteredFrom) {
            Commands = [],
            RotatableRooms = rooms.Where(it => it.Value.CanRotate).ToDictionary()
        };
        var best = pathfinding.FindBest(startState);
        return best == null
                ? throw new ArgumentException($"Couldn't find path from {start} to {exit}")
                : new Queue<string>(best.Commands);
    }

    IEnumerable<GameState> IGame<GameState>.Next(GameState current) {
        var currentRoom = current.RotatableRooms.TryGetValue(current.Point, out var room)
                ? room
                : rooms[current.Point];
        currentRoom.TryMove(current.EnteredFrom, out var next);
        var nextRoom = rooms[next.point];
        if (nextRoom.Type == RoomType.Wall) {
            yield break;
        }
        if (nextRoom.TryMove(next.from, out _)) {
            yield return current.CopyWith(next.point, next.from);
            yield break;
        }
        nextRoom = nextRoom with { };
        for (var rotations = 1; rotations <= 3; rotations++) {
            nextRoom.RotateLeft();
            if (!nextRoom.TryMove(next.from, out _)) {
                continue;
            }
            var nextState = current.CopyWith(next.point, next.from);
            nextState.Commands.AddRange(RotationCommands(next.point, rotations));
            nextState.RotatableRooms[next.point] = nextRoom with { };
            yield return nextState;
        }
    }

    static IEnumerable<string> RotationCommands(Point point, int rotations) {
        if (rotations == 3) {
            yield return $"{point} {Commands.ROTATE_RIGHT}";
            yield break;
        }
        for (var i = 0; i < rotations; i++) {
            yield return $"{point} {Commands.ROTATE_LEFT}";
        }
    }

    bool IGame<GameState>.HasReachedEnd(GameState state) {
        return state.Point == exit;
    }
}

record GameState(Point Point, string EnteredFrom) : IState<GameState> {
    public List<string> Commands { get; init; } = null!;
    public Dictionary<Point, Room> RotatableRooms { get; init; } = null!;

    bool IState<GameState>.IsBetterThan(GameState other) {
        return Commands.Count < other.Commands.Count;
    }

    public GameState CopyWith(Point next, string enteredFrom) => new(next, enteredFrom) {
        Commands = [..Commands],
        RotatableRooms = RotatableRooms.ToDictionary(it => it.Key, it => it.Value with { })
    };
}

record Room(Point Point, bool CanRotate) {
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
            RoomType.ArcTopLeft => RoomType.ArcLeftBottom,
            RoomType.ArcTopRight => RoomType.ArcTopLeft,
            RoomType.ArcRightBottom => RoomType.ArcTopRight,
            RoomType.ArcLeftBottom => RoomType.ArcRightBottom,
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

class Pathfinding<T>(IGame<T> game, int depthMax) where T : IState<T> {
    /// DFS
    public T? FindBest(T current, int depth = 0) {
        if (game.HasReachedEnd(current) || depth > depthMax) {
            return current;
        }
        T? best = default;
        foreach (var next in game.Next(current)) {
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
