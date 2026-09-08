using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// Visual
// Connect towns with your train tracks and disrupt the opponent's
// https://www.codingame.com/contests/summer-challenge-2026-back-track-king
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game : IGraph<Point> {
    const string NO_CONNECTIONS = "x";

    readonly Pathfinding<Point> pathfinding;

    int player, foe;
    int width, height;
    readonly Dictionary<Point, Cell> cells = [];
    readonly Dictionary<int, Town> towns = [];

    readonly Dictionary<CellType, int> paintPoints = new() {
        { CellType.Plains, 1 },
        { CellType.River, 2 },
        { CellType.Mountain, 3 },
    };

    public Game() {
        pathfinding = new Pathfinding<Point>(this);
    }

    public void Init() {
        player = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1);
        foe = player == 0 ? 1 : 0;
        width = Math.Clamp(int.Parse(Console.ReadLine()!), 21, 30);
        height = Math.Clamp(int.Parse(Console.ReadLine()!), 14, 20);
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' ');
                int regionId = int.Parse(inputs[0]);
                var type = Enum.Parse<CellType>(inputs[1]);
                cells[new Point(x, y)] = new Cell(regionId, type);
            }
        }
        int townCount = Math.Clamp(int.Parse(Console.ReadLine()!), 4, 12);
        for (var i = 0; i < townCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int townId = int.Parse(inputs[0]);
            int townX = int.Parse(inputs[1]);
            int townY = int.Parse(inputs[2]);
            var point = new Point(townX, townY);
            string desiredConnections = inputs[3];
            HashSet<int> desiredConnectionIds = desiredConnections == NO_CONNECTIONS
                    ? []
                    : [.. desiredConnections.Split(',').Select(int.Parse)];
            towns[townId] = new Town(point, desiredConnectionIds);
        }
    }

    public void Loop() {
        ReadState();
        List<string> actions = [Autoplace()];
        var foeTracks = cells.Where(it => it.Value.TracksOwner == foe).ToArray();
        if (foeTracks.Length > 1) {
            actions.Add(Actions.Disrupt(foeTracks.First().Key));
        }
        Console.WriteLine(string.Join(';', actions));
    }

    void ReadState() {
        int playerScore = int.Parse(Console.ReadLine()!);
        int foeScore = int.Parse(Console.ReadLine()!);
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' ');
                int tracksOwner = int.Parse(inputs[0]);
                var cell = cells[new Point(x, y)];
                cell.TracksOwner = tracksOwner;
                // region inked (destroyed) when this >= 3
                int instability = int.Parse(inputs[1]);
                // true if region is destroyed
                bool inked = inputs[2] != "0";
                string partOfActiveConnections = inputs[3];
                cell.TownConnections.Clear();
                if (partOfActiveConnections == NO_CONNECTIONS) {
                    continue;
                }
                foreach (string pair in partOfActiveConnections.Split(',')) {
                    foreach (int id in pair.Split('-').Select(int.Parse)) {
                        cell.TownConnections.Add(id);
                    }
                }
            }
        }
    }

    string Autoplace() {
        (int? cost, Town from, Town to) best = (null, default, default);
        foreach (var town in towns.Values) {
            var cell = cells[town.Point];
            foreach (int connection in town.DesiredConnections) {
                if (cell.TownConnections.Contains(connection)) {
                    continue;
                }
                var targetTown = towns[connection];
                int? cost = pathfinding.PathCost(town.Point, targetTown.Point);
                if (cost != null && (best.cost == null || cost < best.cost)) {
                    best = (cost.Value, town, targetTown);
                }
            }
        }
        return Actions.Autoplace(best.from, best.to);
    }

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    IEnumerable<(Point, int cost)> IGraph<Point>.Next(Point current) {
        foreach (var direction in Point.Directions4) {
            var point = current + direction;
            if (!point.InBounds(width, height)) {
                continue;
            }
            var cellType = cells[point].Type;
            int cost = paintPoints[cellType];
            yield return (point, cost);
        }
    }
}

record Cell(int RegionId, CellType Type) {
    public int TracksOwner { get; set; }
    public HashSet<int> TownConnections { get; } = [];
}

enum CellType {
    Plains,
    River,
    Mountain
}

record struct Town(Point Point, HashSet<int> DesiredConnections);

// PLACE_TRACKS x y | MESSAGE text
static class Actions {
    public static string Autoplace(Town from, Town to) {
        return $"AUTOPLACE {from.Point} {to.Point}";
    }

    public static string Disrupt(Point cellPoint) {
        return $"DISRUPT {cellPoint}";
    }
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"{X} {Y}";
}

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly PriorityQueue<T, int> queue = new();
    readonly Dictionary<T, int> visited = new();

    /// Dijkstra
    public int? PathCost(T start, T end) {
        queue.Clear();
        visited.Clear();
        queue.Enqueue(start, 0);
        visited.Add(start, 0);
        while (queue.TryDequeue(out var current, out int currentCost)) {
            if (current.Equals(end)) {
                return visited[current];
            }
            foreach ((var node, int cost) in graph.Next(current)) {
                int newCost = currentCost + cost;
                if (newCost >= visited.GetValueOrDefault(node, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(node, newCost);
                visited[node] = newCost;
            }
        }
        return null;
    }
}

interface IGraph<T> {
    IEnumerable<(T, int cost)> Next(T current);
}
