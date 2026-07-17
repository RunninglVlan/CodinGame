using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TheBridge;

/// https://www.codingame.com/training/hard/the-bridge-episode-2
static class TheBridge {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
            Console.WriteLine(game.Action);
        }
    }
}

class Game : IGame<GameState> {
    readonly Pathfinding<GameState> pathfinding;
    Motorbike[] motorbikes = null!;

    /// the minimum amount of motorbikes that must survive
    int required;

    Road road = null!;

    readonly ActionOption[] actionOptions = [
        new SpeedOption(),
        new SlowOption(),
        new JumpOption(),
        new WaitOption(),
        new UpOption(),
        new DownOption()
    ];

    public string Action { get; private set; } = null!;

    public Game() {
        pathfinding = new Pathfinding<GameState>(this, 5);
    }

    public void Init() {
        int motorbikeCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 4);
        motorbikes = new Motorbike[motorbikeCount];
        for (var i = 0; i < motorbikeCount; i++) {
            motorbikes[i] = new Motorbike();
        }
        required = Math.Clamp(int.Parse(Console.ReadLine()!), 1, motorbikeCount);
        road = new Road();
        road.ReadInput();
    }

    public void Loop() {
        int speed = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 49);
        foreach (var motorbike in motorbikes) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int x = Math.Clamp(int.Parse(inputs[0]), 0, 499);
            int y = Math.Clamp(int.Parse(inputs[1]), 0, 3);
            string active = inputs[2];
            motorbike.X = x;
            motorbike.Y = y;
            motorbike.IsActive = active == Motorbike.IS_ACTIVE;
        }
        Action = pathfinding.FindBest(new GameState(motorbikes, speed))?.action ?? string.Empty;
    }

    IEnumerable<(GameState, string action)> IGame<GameState>.Next(GameState current) {
        foreach (var option in actionOptions) {
            if (!option.TryNext(current, out var next)) {
                continue;
            }
            SimulateTurn(next);
            if (next.Active < required || next.Active == 0) {
                continue;
            }
            yield return (next, option.Action);
        }
    }

    void SimulateTurn(GameState state) {
        foreach (var motorbike in state.Motorbikes) {
            int xStart = motorbike.X + 1;
            motorbike.X += state.Speed;
            if (!motorbike.IsActive) {
                continue;
            }
            if (!road.IsSafe(motorbike.X, motorbike.X, motorbike.Y)) {
                motorbike.IsActive = false;
                continue;
            }
            if (state.IsJumping) {
                continue;
            }
            if (!road.IsSafe(xStart, motorbike.X - 1, motorbike.Y)) {
                motorbike.IsActive = false;
                continue;
            }
            if (motorbike.YPrevious != motorbike.Y) {
                motorbike.IsActive = road.IsSafe(xStart, motorbike.X - 1, motorbike.YPrevious);
            }
        }
    }

    bool IGame<GameState>.HasReachedEnd(GameState state) => state.X > road.XMax;
}

record GameState(Motorbike[] Motorbikes, int Speed, bool IsJumping = false) : IState<GameState> {
    public int X => Motorbikes.First(it => it.IsActive).X;
    public int YMin => Motorbikes.First(it => it.IsActive).Y;
    public int YMax => Motorbikes.Last(it => it.IsActive).Y;
    public int Active => Motorbikes.Count(it => it.IsActive);

    bool IState<GameState>.IsBetterThan(GameState other) {
        return Active > other.Active;
    }

    public GameState CopyWith(int? speed = null, bool jumping = false) {
        return new GameState(Motorbikes.Select(it => it.Copy()).ToArray(), speed ?? Speed, jumping);
    }
}

abstract record ActionOption(string Action) {
    public abstract bool TryNext(GameState current, out GameState next);
}

record SpeedOption() : ActionOption("SPEED") {
    public override bool TryNext(GameState current, out GameState next) {
        next = current.CopyWith(current.Speed + 1);
        return true;
    }
}

record SlowOption() : ActionOption("SLOW") {
    public override bool TryNext(GameState current, out GameState next) {
        next = null!;
        if (current.Speed <= 1) {
            return false;
        }
        next = current.CopyWith(current.Speed - 1);
        return true;
    }
}

record JumpOption() : ActionOption("JUMP") {
    public override bool TryNext(GameState current, out GameState next) {
        next = current.CopyWith(jumping: true);
        return true;
    }
}

record WaitOption() : ActionOption("WAIT") {
    public override bool TryNext(GameState current, out GameState next) {
        next = current.CopyWith();
        return true;
    }
}

record UpOption() : ActionOption("UP") {
    public override bool TryNext(GameState current, out GameState next) {
        next = null!;
        if (current.YMin == 0) {
            return false;
        }
        next = current.CopyWith();
        foreach (var motorbike in next.Motorbikes) {
            motorbike.Y--;
        }
        return true;
    }
}

record DownOption() : ActionOption("DOWN") {
    public override bool TryNext(GameState current, out GameState next) {
        next = null!;
        if (current.YMax == Road.MAX_LANES - 1) {
            return false;
        }
        next = current.CopyWith();
        foreach (var motorbike in next.Motorbikes) {
            motorbike.Y++;
        }
        return true;
    }
}

class Road {
    public const int MAX_LANES = 4;

    public int XMax { get; private set; }
    bool[,] safeCells = null!;

    public void ReadInput() {
        const char safeSpace = '.';
        string lane = Console.ReadLine()!;
        Console.Error.WriteLine(lane);
        XMax = lane.Length;
        safeCells = new bool[XMax, MAX_LANES];
        for (var y = 0; y < MAX_LANES; y++) {
            if (y != 0) {
                lane = Console.ReadLine()!;
                Console.Error.WriteLine(lane);
            }
            for (var x = 0; x < XMax; x++) {
                safeCells[x, y] = lane[x] == safeSpace;
            }
        }
    }

    public bool IsSafe(int xStart, int xEnd, int y) {
        for (int x = xStart; x <= xEnd; x++) {
            if (x >= XMax) {
                continue;
            }
            if (!safeCells[x, y]) {
                return false;
            }
        }
        return true;
    }
}

class Motorbike {
    public const string IS_ACTIVE = "1";

    public int X { get; set; }
    public int Y { get; set; }
    public int YPrevious { get; private init; }
    public bool IsActive { get; set; }

    public Motorbike Copy() => new() { X = X, Y = Y, YPrevious = Y, IsActive = IsActive };
}

class Pathfinding<T>(IGame<T> game, int depthMax) where T : IState<T> {
    /// DFS/Backtracking
    public (T state, string action)? FindBest(T current, int depth = 0) {
        if (game.HasReachedEnd(current) || depth > depthMax) {
            return (current, string.Empty);
        }
        (T state, string action)? best = null;
        foreach ((var next, string action) in game.Next(current)) {
            var nextBest = FindBest(next, depth + 1);
            if (nextBest == null) {
                continue;
            }
            var (state, _) = nextBest.Value;
            if (best == null || state.IsBetterThan(best.Value.state)) {
                best = (state, action);
            }
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<(TState, string action)> Next(TState current);
    bool HasReachedEnd(TState state);
}

interface IState<in T> where T : IState<T> {
    bool IsBetterThan(T other);
}
