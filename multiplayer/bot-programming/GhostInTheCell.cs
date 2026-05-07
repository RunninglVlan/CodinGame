using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GhostInTheCell;

/// https://www.codingame.com/multiplayer/bot-programming/ghost-in-the-cell
static class GhostInTheCell {
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
        }
        // ReSharper disable once FunctionNeverReturns
    }
}

class Game {
    readonly Pathfinding<Factory> pathfinding = new();
    Factory[] factories = null!;
    readonly List<Bomb> bombs = new();
    int remainingBombs = 2;

    public void Init() {
        int factoryCount = Math.Clamp(int.Parse(Console.ReadLine()!), 7, 15);
        factories = new Factory[factoryCount];
        for (var i = 0; i < factoryCount; i++) {
            factories[i] = new Factory(i);
        }
        int linkCount = Math.Clamp(int.Parse(Console.ReadLine()!), 21, 105);
        for (var i = 0; i < linkCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int factoryId1 = int.Parse(inputs[0]);
            int factoryId2 = int.Parse(inputs[1]);
            int distanceInTurns = Math.Clamp(int.Parse(inputs[2]), 1, 20);
            var factory1 = factories[factoryId1];
            var factory2 = factories[factoryId2];
            factory1.connections.Add((factory2, distanceInTurns));
            factory2.connections.Add((factory1, distanceInTurns));
            Console.Error.WriteLine($"Link: {factoryId1} <-> {factoryId2} ({distanceInTurns} turns)");
        }
    }

    public void Loop() {
        ReadInput();
        var actions = new List<IAction>();
        if (remainingBombs > 0 && TryFindBombTarget(out var target)) {
            actions.Add(SendBomb(target));
        }
        foreach (var factory in factories) {
            if (factory.Player != Player.Me) {
                continue;
            }
            actions.AddRange(factory.Actions(pathfinding));
        }
        actions.Add(new Message($"Factories: my {factories.Count(it => it.Player == Player.Me)} "
                + $"vs {factories.Count(it => it.Player == Player.Opponent)}             "
                + $"Production: my {factories.Where(it => it.Player == Player.Me).Sum(it => it.Production)} "
                + $"vs {factories.Where(it => it.Player == Player.Opponent).Sum(it => it.Production)}"));
        Console.WriteLine(string.Join(';', actions));
    }

    void ReadInput() {
        foreach (var factory in factories) {
            factory.ClearOldData();
        }
        bombs.Clear();
        int entityCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < entityCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int entityId = int.Parse(inputs[0]);
            string entityType = inputs[1];
            var player = (Player)int.Parse(inputs[2]);
            int arg2 = int.Parse(inputs[3]);
            int arg3 = int.Parse(inputs[4]);
            int arg4 = int.Parse(inputs[5]);
            int arg5 = int.Parse(inputs[6]);
            switch (entityType) {
                case EntityType.FACTORY:
                    UpdateFactory(entityId, player, arg2, arg3, arg4);
                    break;
                case EntityType.TROOP:
                    ProcessTroop(entityId, player, arg2, arg3, arg4, arg5);
                    break;
                case EntityType.BOMB:
                    bombs.Add(new Bomb(entityId, player, arg2, arg3, arg4));
                    break;
            }
        }
    }

    void UpdateFactory(int id, Player player, int cyborgs, int production, int cooldownTurns) {
        var factory = factories[id];
        factory.Player = player;
        factory.Cyborgs = cyborgs;
        factory.Production = production;
        factory.CooldownTurns = cooldownTurns;
    }

    void ProcessTroop(int id, Player player, int sourceId, int targetId, int cyborgs, int walkTurns) {
        var troop = new Troop(id, player, sourceId, targetId, cyborgs, walkTurns);
        factories[targetId].ApproachingTroops.Add(troop);
    }

    bool TryFindBombTarget(out Factory bombTarget) {
        var target = factories
                .Where(it => it.Player == Player.Opponent)
                .Where(it => it.Production == Factory.PRODUCTION_MAX)
                .Where(it => it.connections.Any(connection => connection.factory.Player == Player.Me))
                .Where(it => it.CooldownTurns == 0)
                .FirstOrDefault(it => bombs.All(bomb => bomb.TargetId != it.Id));
        bombTarget = target!;
        return target != null;
    }

    SendBomb SendBomb(Factory target) {
        remainingBombs--;
        var source = target.connections
                .Where(it => it.factory.Player == Player.Me)
                .OrderBy(it => it.distanceInTurns)
                .First();
        return new SendBomb(source.factory.Id, target.Id);
    }
}

class Factory(int id) : INode<Factory> {
    public const int PRODUCTION_MAX = 3;
    const int PRODUCTION_INCREASE_COST = 10;

    public int Id { get; } = id;
    public readonly List<(Factory factory, int distanceInTurns)> connections = new();
    List<(Factory, int)> INode<Factory>.Connections => connections;
    public Player Player { get; set; }
    public int Cyborgs { get; set; }
    public List<Troop> ApproachingTroops { get; } = new();

    int NeededCyborgs {
        get {
            int cyborgs = Player == Player.Me
                    ? (PRODUCTION_MAX - Production) * PRODUCTION_INCREASE_COST
                    : Cyborgs + 1;
            if (Player != Player.Me && Production == 0) {
                cyborgs += PRODUCTION_INCREASE_COST;
            }
            return cyborgs
                    - ApproachingTroops.Where(it => it.Player == Player.Me).Sum(it => it.Cyborgs)
                    + ApproachingTroops.Where(it => it.Player == Player.Opponent).Sum(it => it.Cyborgs);
        }
    }

    public int Production { get; set; }

    /// number of turns before the factory starts producing again (0 means that the factory produces normally)
    public int CooldownTurns { get; set; }

    public void ClearOldData() => ApproachingTroops.Clear();

    public IEnumerable<IAction> Actions(Pathfinding<Factory> pathfinding) {
        if (CanIncreaseProduction()) {
            yield return IncreaseProduction();
        }
        int availableCyborgs = AvailableCyborgs();
        if (Production == 0 || availableCyborgs == 0) {
            yield break;
        }
        foreach (var target in PrioritizedTargets()) {
            if (availableCyborgs == 0) {
                yield break;
            }
            if (target.cyborgs <= 0) {
                continue;
            }
            var path = pathfinding.Path(this, target.factory);
            if (path == null) {
                continue;
            }
            int cyborgs = Math.Min(availableCyborgs, target.cyborgs);
            availableCyborgs -= cyborgs;
            yield return new MoveTroop(Id, path.First().Id, cyborgs);
        }
    }

    bool CanIncreaseProduction() {
        return Production < PRODUCTION_MAX && Cyborgs >= PRODUCTION_INCREASE_COST && IsSafeToIncreaseProduction();
    }

    bool IsSafeToIncreaseProduction() {
        return SimulateMinCyborgs(Cyborgs - PRODUCTION_INCREASE_COST, Production + 1) > 0;
    }

    int AvailableCyborgs() {
        int simulated = SimulateMinCyborgs(Cyborgs, Production);
        return Math.Clamp(simulated, 0, Cyborgs);
    }

    int SimulateMinCyborgs(int initial, int production) {
        int current = initial;
        int min = initial;
        int horizon = ApproachingTroops.Count != 0 ? ApproachingTroops.Max(t => t.WalkTurns) : 0;
        int simulateTurns = Math.Max(horizon, 3);
        for (var turn = 1; turn <= simulateTurns; turn++) {
            current += ApproachingTroops.Where(tr => tr.WalkTurns == turn)
                    .Sum(troop => troop.Player == Player.Me ? troop.Cyborgs : -troop.Cyborgs);
            if (current < min) {
                min = current;
            }
            current += production;
        }
        return min;
    }

    IncreaseProduction IncreaseProduction() {
        Cyborgs -= PRODUCTION_INCREASE_COST;
        Production++;
        return new IncreaseProduction(Id);
    }

    IEnumerable<(Factory factory, int cyborgs)> PrioritizedTargets() {
        const int maxAttackingCyborgs = 3;
        var myFactories = connections.Where(it => it.factory.Player == Player.Me).ToArray();
        var helpFactoriesIMightLose = myFactories
                .Where(it => {
                    int simulated = it.factory.SimulateMinCyborgs(it.factory.Cyborgs, it.factory.Production);
                    return simulated < 0;
                })
                .OrderBy(it => it.distanceInTurns)
                .ThenByDescending(it => it.factory.Production);
        var captureOthers = connections
                .Where(it => it.factory.Player != Player.Me)
                .OrderByDescending(it => (float)it.factory.Production / it.distanceInTurns)
                .ThenBy(it => it.distanceInTurns);
        var helpInUpgrading = myFactories
                .Where(it => it.factory.Production < PRODUCTION_MAX)
                .OrderBy(it => it.distanceInTurns)
                .ThenBy(it => it.factory.Production);
        foreach (var target in helpFactoriesIMightLose.Concat(captureOthers).Concat(helpInUpgrading)) {
            yield return (target.factory, target.factory.NeededCyborgs);
        }
        var attackOpponent = connections
                .Where(it => it.factory.Player == Player.Opponent)
                .OrderBy(it => it.distanceInTurns)
                .ThenByDescending(it => it.factory.Production);
        foreach (var target in attackOpponent) {
            yield return (target.factory, maxAttackingCyborgs);
        }
    }
}

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
readonly record struct Troop(int Id, Player Player, int SourceId, int TargetId, int Cyborgs, int WalkTurns);

/// <param name="TargetId">id of the targeted factory if it's my bomb, -1 otherwise</param>
/// <param name="Turns">remaining number of turns before the bomb explodes (positive integer) if that's your bomb, -1 otherwise</param>
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
readonly record struct Bomb(int Id, Player Player, int SourceId, int TargetId, int Turns);

static class EntityType {
    public const string FACTORY = "FACTORY";
    public const string TROOP = "TROOP";
    public const string BOMB = "BOMB";
}

enum Player {
    Me = 1,
    Opponent = -1,
    Neutral = 0
}

interface IAction;

struct Wait : IAction {
    public override string ToString() => "WAIT";
}

readonly struct IncreaseProduction(int factoryId) : IAction {
    public override string ToString() => $"INC {factoryId}";
}

readonly struct MoveTroop(int sourceId, int targetId, int cyborgs) : IAction {
    public override string ToString() => $"MOVE {sourceId} {targetId} {cyborgs}";
}

readonly struct SendBomb(int sourceId, int targetId) : IAction {
    public override string ToString() => $"BOMB {sourceId} {targetId}";
}

readonly struct Message(string value) : IAction {
    public override string ToString() => $"MSG {value}";
}

class Pathfinding<T> where T : INode<T> {
    readonly List<T> path = new();
    readonly PriorityQueue<T, int> queue = new();
    readonly Dictionary<int, int> distances = new();
    readonly Dictionary<int, T> parents = new();

    /// Dijkstra
    public List<T>? Path(T start, T end) {
        queue.Clear();
        distances.Clear();
        parents.Clear();
        queue.Enqueue(start, 0);
        distances.Add(start.Id, 0);
        while (queue.TryDequeue(out var current, out int currentDistance)) {
            if (current.Id == end.Id) {
                return Reconstruct(start, current);
            }
            // skip if we've already found a better path
            if (currentDistance > distances.GetValueOrDefault(current.Id, int.MaxValue)) {
                continue;
            }
            foreach ((var node, int distance) in current.Connections) {
                int newDistance = currentDistance + distance;
                if (newDistance >= distances.GetValueOrDefault(node.Id, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(node, newDistance);
                distances[node.Id] = newDistance;
                parents[node.Id] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        path.Clear();
        var current = end;
        while (current.Id != start.Id) {
            path.Add(current);
            current = parents[current.Id];
        }
        path.Reverse();
        return path;
    }
}

interface INode<T> where T : INode<T> {
    int Id { get; }
    List<(T, int)> Connections { get; }
}
