using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

/// Visual <br/>
/// https://www.codingame.com/training/hard/the-fall-episode-2
static class TheFall2 {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
            Console.WriteLine(game.NextCommand);
        }
    }
}

class Game : IGame<GameState> {
    readonly StateSearch<GameState> stateSearch;
    int width, height;
    Dictionary<Point, Room> rooms = null!;
    Point exit;
    Queue<Command>? commands;
    readonly HashSet<Point> blocked = [];
    public string NextCommand { get; private set; } = null!;

    public Game() {
        stateSearch = new StateSearch<GameState>(this, 35);
    }

    public void Init() {
        string[] inputs = Debug.ReadLine().Split(' ');
        width = Math.Clamp(int.Parse(inputs[0]), 1, 20);
        height = Math.Clamp(int.Parse(inputs[1]), 1, 20);
        rooms = new Dictionary<Point, Room>();
        for (var y = 0; y < height; y++) {
            string[] types = Debug.ReadLine().Split(' ');
            for (var x = 0; x < width; x++) {
                var point = new Point(x, y);
                int roomType = int.Parse(types[x]);
                rooms[point] = new Room(point, roomType > 0) {
                    Type = (RoomType)Math.Abs(roomType)
                };
            }
        }
        int xExit = Math.Clamp(int.Parse(Debug.ReadLine()), 0, width - 1);
        exit = new Point(xExit, height - 1);
    }

    public void Loop() {
        string[] inputs = Debug.ReadLine().Split(' ');
        int x = Math.Clamp(int.Parse(inputs[0]), 0, width - 1);
        int y = Math.Clamp(int.Parse(inputs[1]), 0, height - 1);
        string enteredFrom = inputs[2];
        var current = new Position(new Point(x, y), enteredFrom);
        int rockCount = int.Parse(Debug.ReadLine());
        var rocks = new Position[rockCount];
        for (var i = 0; i < rockCount; i++) {
            inputs = Debug.ReadLine().Split(' ');
            int rockX = Math.Clamp(int.Parse(inputs[0]), 0, width - 1);
            int rockY = Math.Clamp(int.Parse(inputs[1]), 0, height - 1);
            string rockEnteredFrom = inputs[2];
            rocks[i] = new Position(new Point(rockX, rockY), rockEnteredFrom);
        }
        commands ??= BuildTunnel(current);
        if (TryBlockRock(rocks, current, out var nextCommand)) {
            NextCommand = nextCommand.ToString();
            return;
        }
        NextCommand = commands.TryDequeue(out var command) ? command.ToString() : Command.WAIT;
    }

    Queue<Command> BuildTunnel(Position start) {
        var startState = new GameState(start) {
            Commands = [],
            RotatableRooms = rooms.Where(it => it.Value.CanRotate).ToDictionary()
        };
        var best = stateSearch.FindBest(startState);
        if (best == null) {
            throw new ArgumentException($"Couldn't find path from {start} to {exit}");
        }
        foreach (var (point, room) in best.RotatableRooms) {
            rooms[point] = room;
        }
        return new Queue<Command>(best.Commands);
    }

    bool TryBlockRock(Position[] rocks, Position indy, out Command command) {
        command = null!;
        if (rocks.Length == 0) {
            return false;
        }
        if (commands!.TryPeek(out var nextCommand) && Point.ManhattanDistance(indy.Point, nextCommand.Point) == 1) {
            return false;
        }
        rooms[indy.Point].TryMove(indy.EnteredFrom, out var indyNext);
        var rock = rocks
                .Select(it => RockRooms(it, indyNext))
                .Where(it => it.roomTarget != null && it.roomTarget.Point != indy.Point)
                .OrderBy(it => Point.ManhattanDistance(it.room.Point, indy.Point))
                .FirstOrDefault();
        if (rock == default) {
            return false;
        }
        rock.roomTarget!.RotateLeft();
        command = Command.RotateLeft(rock.roomTarget.Point);
        if (!rock.roomTarget.TryMove(rock.enteredFrom, out _)) {
            blocked.Add(rock.roomTarget.Point);
        }
        return true;
    }

    (Room room, Room? roomTarget, string enteredFrom) RockRooms(Position rock, Position indyNext) {
        const int maxNextRooms = 3;
        var room = rooms[rock.Point];
        var roomCurrent = room;
        Room? roomTarget = null;
        for (var i = 0; i < maxNextRooms; i++) {
            if (!roomCurrent.TryMove(rock.EnteredFrom, out var next)) {
                break;
            }
            if (!rooms.TryGetValue(next.Point, out var roomNext)) {
                break;
            }
            if (!roomNext.CanRotate || next.Point == indyNext.Point || blocked.Contains(next.Point)) {
                roomCurrent = roomNext;
                continue;
            }
            roomTarget = roomNext;
            break;
        }
        return (room, roomTarget, enteredFrom: rock.EnteredFrom);
    }

    IEnumerable<GameState> IGame<GameState>.Next(GameState current) {
        (var point, string enteredFrom) = current.Position;
        var currentRoom = current.RotatableRooms.TryGetValue(point, out var room) ? room : rooms[point];
        currentRoom.TryMove(enteredFrom, out var next);
        if (!next.Point.InBounds(width, height)) {
            yield break;
        }
        var nextRoom = current.RotatableRooms.TryGetValue(next.Point, out room) ? room : rooms[next.Point];
        if (nextRoom.TryMove(next.EnteredFrom, out _)) {
            yield return current.CopyWith(next);
        }
        if (!nextRoom.CanRotate) {
            yield break;
        }
        nextRoom = nextRoom with { };
        for (var rotations = 1; rotations <= nextRoom.MaxRotations; rotations++) {
            nextRoom.RotateLeft();
            if (!nextRoom.TryMove(next.EnteredFrom, out _)) {
                continue;
            }
            var nextState = current.CopyWith(next);
            nextState.Commands.AddRange(RotationCommands(next.Point, rotations));
            nextState.RotatableRooms[next.Point] = nextRoom with { };
            yield return nextState;
        }
    }

    static IEnumerable<Command> RotationCommands(Point point, int rotations) {
        if (rotations == 3) {
            yield return Command.RotateRight(point);
            yield break;
        }
        for (var i = 0; i < rotations; i++) {
            yield return Command.RotateLeft(point);
        }
    }

    bool IGame<GameState>.HasReachedEnd(GameState state) {
        return state.Position.Point == exit;
    }
}

record GameState(Position Position) : IState<GameState> {
    public List<Command> Commands { get; init; } = null!;
    public Dictionary<Point, Room> RotatableRooms { get; init; } = null!;

    bool IState<GameState>.IsBetterThan(GameState other) {
        return Commands.Count < other.Commands.Count;
    }

    public GameState CopyWith(Position position) => new(position) {
        Commands = [.. Commands],
        RotatableRooms = RotatableRooms.ToDictionary(it => it.Key, it => it.Value with { })
    };
}

record Room(Point Point, bool CanRotate) {
    public RoomType Type { get; set; }
    public int MaxRotations => maxRotations[Type];

    readonly Dictionary<RoomType, int> maxRotations = new() {
        { RoomType.Wall, 0 },
        { RoomType.Cross, 0 },
        { RoomType.Horizontal, 1 },
        { RoomType.Vertical, 1 },
        { RoomType.ArcsLeft, 1 },
        { RoomType.ArcsRight, 1 },
        { RoomType.ThreeWayTop, 3 },
        { RoomType.ThreeWayRight, 3 },
        { RoomType.ThreeWayBottom, 3 },
        { RoomType.ThreeWayLeft, 3 },
        { RoomType.ArcTopLeft, 3 },
        { RoomType.ArcTopRight, 3 },
        { RoomType.ArcRightBottom, 3 },
        { RoomType.ArcLeftBottom, 3 }
    };

    public bool TryMove(string from, out Position next) {
        next = default;
        var toLeft = new Position(Point with { X = Point.X - 1 }, Sides.RIGHT);
        var toBottom = new Position(Point with { Y = Point.Y + 1 }, Sides.TOP);
        var toRight = new Position(Point with { X = Point.X + 1 }, Sides.LEFT);
        Position? result = (Type, from) switch {
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

    [SuppressMessage("ReSharper", "SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault")]
    public void RotateLeft() {
        Type = Type switch {
            RoomType.Horizontal => RoomType.Vertical,
            RoomType.Vertical => RoomType.Horizontal,
            RoomType.ArcsLeft => RoomType.ArcsRight,
            RoomType.ArcsRight => RoomType.ArcsLeft,
            RoomType.ThreeWayTop => RoomType.ThreeWayLeft,
            RoomType.ThreeWayRight => RoomType.ThreeWayTop,
            RoomType.ThreeWayBottom => RoomType.ThreeWayRight,
            RoomType.ThreeWayLeft => RoomType.ThreeWayBottom,
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
    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public override string ToString() => $"{X} {Y}";
}

readonly record struct Position(Point Point, string EnteredFrom);

record Command(Point Point, string Value) {
    public const string WAIT = "WAIT";

    public static Command RotateLeft(Point point) => new(point, "LEFT");
    public static Command RotateRight(Point point) => new(point, "RIGHT");

    public override string ToString() => $"{Point} {Value}";
}

class StateSearch<T>(IGame<T> game, int depthMax) where T : IState<T> {
    /// DFS/Backtracking
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

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string ReadLine() {
        string line = Console.ReadLine()!;
        Console.Error.WriteLine(line);
        return line;
    }
}
