using System;
using System.Collections.Generic;
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
    Factory[] factories = null!;
    readonly List<Troop> troops = new();
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
        }
    }

    public void Loop() {
        ReadInput();
        var actions = new List<IAction>();
        if (remainingBombs > 0) {
            var bombTarget = factories
                    .Where(it => it.Player == Player.Opponent)
                    .Where(it => it.Production == Factory.PRODUCTION_MAX)
                    .Where(it => it.connections.Any(connection => connection.factory.Player == Player.Me))
                    .Where(it => it.CooldownTurns == 0)
                    .FirstOrDefault(it => bombs.All(bomb => bomb.TargetId != it.id));
            if (bombTarget != null) {
                actions.Add(SendBomb(bombTarget));
            }
        }
        foreach (var factory in factories) {
            if (factory.Player != Player.Me) {
                continue;
            }
            actions.AddRange(Actions(factory));
        }
        Console.WriteLine(string.Join(';', actions));
    }

    void ReadInput() {
        foreach (var factory in factories) {
            factory.ApproachingCyborgs = default;
        }
        troops.Clear();
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
        troops.Add(new Troop(id, player, sourceId, targetId, cyborgs, walkTurns));
        factories[targetId].UpdateApproaching(cyborgs, player);
    }

    SendBomb SendBomb(Factory target) {
        remainingBombs--;
        var source = target.connections
                .Where(it => it.factory.Player == Player.Me)
                .OrderBy(it => it.distanceInTurns)
                .First();
        return new SendBomb(source.factory.id, target.id);
    }

    static IEnumerable<IAction> Actions(Factory factory) {
        if (factory.CanIncreaseProduction) {
            yield return factory.IncreaseProduction();
        }
        var notMyTargets = factory.connections
                .Where(it => it.factory.Player != Player.Me)
                .OrderBy(it => it.distanceInTurns)
                .ThenByDescending(it => it.factory.Production);
        foreach (var target in notMyTargets) {
            if (factory.Cyborgs == 0) {
                yield break;
            }
            if (target.factory.NeededCyborgs > 0) {
                yield return factory.MoveTroop(target.factory);
            }
        }
        var myTargets = factory.connections
                .Where(it => it.factory is { Player: Player.Me, Production: < Factory.PRODUCTION_MAX })
                .OrderBy(it => it.distanceInTurns)
                .ThenBy(it => it.factory.Production);
        foreach (var target in myTargets) {
            if (factory.Cyborgs == 0) {
                yield break;
            }
            if (target.factory.NeededCyborgs > 0) {
                yield return factory.MoveTroop(target.factory);
            }
        }
    }
}

class Factory(int id) {
    public const int PRODUCTION_MAX = 3;
    const int PRODUCTION_INCREASE_COST = 10;

    public readonly int id = id;
    public readonly List<(Factory factory, int distanceInTurns)> connections = new();
    public Player Player { get; set; }
    public int Cyborgs { get; set; }
    public (int mine, int opponent) ApproachingCyborgs { get; set; }

    public int NeededCyborgs {
        get {
            int cyborgs = Player == Player.Me
                    ? (PRODUCTION_MAX - Production) * PRODUCTION_INCREASE_COST
                    : Cyborgs + 1;
            if (Player != Player.Me && Production == 0) {
                cyborgs += PRODUCTION_INCREASE_COST;
            }
            return cyborgs - ApproachingCyborgs.mine + ApproachingCyborgs.opponent;
        }
    }

    public int Production { get; set; }

    /// number of turns before the factory starts producing again (0 means that the factory produces normally)
    public int CooldownTurns { get; set; }

    public bool CanIncreaseProduction => Production < PRODUCTION_MAX && Cyborgs >= PRODUCTION_INCREASE_COST;

    public void UpdateApproaching(int cyborgs, Player player) {
        if (player == Player.Me) {
            ApproachingCyborgs = ApproachingCyborgs with { mine = ApproachingCyborgs.mine + cyborgs };
        } else {
            ApproachingCyborgs = ApproachingCyborgs with { opponent = ApproachingCyborgs.opponent + cyborgs };
        }
    }

    public IncreaseProduction IncreaseProduction() {
        Cyborgs -= PRODUCTION_INCREASE_COST;
        return new IncreaseProduction(id);
    }

    public MoveTroop MoveTroop(Factory target) {
        int cyborgs = Math.Min(Cyborgs, target.NeededCyborgs);
        Cyborgs -= cyborgs;
        return new MoveTroop(id, target.id, cyborgs);
    }
}

struct Troop(int id, Player player, int sourceId, int targetId, int cyborgs, int walkTurns);

/// <param name="targetId">id of the targeted factory if it's my bomb, -1 otherwise</param>
/// <param name="turns">remaining number of turns before the bomb explodes (positive integer) if that's your bomb, -1 otherwise</param>
readonly struct Bomb(int id, Player player, int sourceId, int targetId, int turns) {
    public int TargetId => targetId;
}

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
