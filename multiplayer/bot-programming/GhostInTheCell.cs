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
    readonly Dictionary<int, Troop> troops = new();
    readonly Dictionary<int, Bomb> bombs = new();

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
        foreach (var factory in factories) {
            if (factory.Player != Player.Me) {
                continue;
            }
            actions.AddRange(Actions(factory));
        }
        Console.WriteLine(string.Join(';', actions));
    }

    static IEnumerable<IAction> Actions(Factory factory) {
        if (factory.CanIncreaseProduction) {
            yield return factory.IncreaseProduction();
        }
        var targets = factory.connections
                .Where(it => it.factory.Player != Player.Me)
                .OrderBy(it => it.distanceInTurns)
                .Take(2)
                .ToArray();
        int cyborgs = factory.Cyborgs / targets.Length;
        foreach (var target in targets) {
            yield return new Move(factory.id, target.factory.id, cyborgs);
        }
    }

    void ReadInput() {
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
                    UpdateTroop(entityId, player, arg2, arg3, arg4, arg5);
                    break;
                case EntityType.BOMB:
                    UpdateBomb(entityId, player, arg2, arg3, arg4);
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

    void UpdateTroop(int id, Player player, int sourceId, int targetId, int cyborgs, int walkTurns) {
        if (!troops.TryGetValue(id, out var troop)) {
            troops.Add(id, troop = new Troop());
        }
        troop.Player = player;
        troop.SourceId = sourceId;
        troop.TargetId = targetId;
        troop.Cyborgs = cyborgs;
        troop.WalkTurns = walkTurns;
    }

    void UpdateBomb(int id, Player player, int sourceId, int targetId, int turns) {
        if (!bombs.TryGetValue(id, out var bomb)) {
            bombs.Add(id, bomb = new Bomb());
        }
        bomb.Player = player;
        bomb.SourceId = sourceId;
        bomb.TargetId = targetId;
        bomb.Turns = turns;
    }
}

class Factory(int id) {
    const int PRODUCTION_MAX = 3;
    const int PRODUCTION_INCREASE_COST = 10;

    public readonly int id = id;
    public readonly List<(Factory factory, int distanceInTurns)> connections = new();
    public Player Player { get; set; }
    public int Cyborgs { get; set; }
    public int Production { get; set; }

    /// number of turns before the factory starts producing again (0 means that the factory produces normally)
    public int CooldownTurns { get; set; }

    public bool CanIncreaseProduction => Production < PRODUCTION_MAX && Cyborgs >= PRODUCTION_INCREASE_COST;

    public IncreaseProduction IncreaseProduction() {
        Cyborgs -= PRODUCTION_INCREASE_COST;
        return new IncreaseProduction(id);
    }
}

class Troop {
    public Player Player { get; set; }
    public int SourceId { get; set; }
    public int TargetId { get; set; }
    public int Cyborgs { get; set; }
    public int WalkTurns { get; set; }
}

class Bomb {
    public Player Player { get; set; }
    public int SourceId { get; set; }

    /// id of the targeted factory if it's my bomb, -1 otherwise
    public int TargetId { get; set; }

    /// remaining number of turns before the bomb explodes (positive integer) if that's your bomb, -1 otherwise
    public int Turns { get; set; }
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

readonly struct Move(int sourceId, int targetId, int cyborgs) : IAction {
    public override string ToString() => $"MOVE {sourceId} {targetId} {cyborgs}";
}
