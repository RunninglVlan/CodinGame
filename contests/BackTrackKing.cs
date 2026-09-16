using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// Visual
// Connect towns with your train tracks and disrupt the opponent's
// https://www.codingame.com/contests/summer-challenge-2026-back-track-king
var game = new Game();
game.Init();
game.FindPaths();
while (true) {
    game.Loop();
}

class Game : IGraph<Point> {
    const string NO_CONNECTIONS = "x";
    const int TURN_POINTS = 3;

    readonly Pathfinding<Point> pathfinding;

    Owner player, foe;
    int width, height;
    readonly Dictionary<Point, Cell> cells = [];
    readonly Dictionary<int, Region> regions = [];
    readonly Dictionary<int, Town> towns = [];

    readonly Dictionary<CellType, int> paintPoints = new() {
        { CellType.Plains, 1 },
        { CellType.River, 2 },
        { CellType.Mountain, 3 }
    };

    bool builtPaths;

    public Game() {
        pathfinding = new Pathfinding<Point>(this);
        Path.GetCost = Cost;
        Path.GetRegion = point => cells[point].Region;
    }

    public void Init() {
        player = Enum.Parse<Owner>(Console.ReadLine()!);
        foe = player == Owner.Player0 ? Owner.Player1 : Owner.Player0;
        width = Math.Clamp(int.Parse(Console.ReadLine()!), 21, 30);
        height = Math.Clamp(int.Parse(Console.ReadLine()!), 14, 20);
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' ');
                int regionId = int.Parse(inputs[0]);
                if (!regions.TryGetValue(regionId, out var region)) {
                    regions[regionId] = region = new Region(regionId);
                }
                var type = Enum.Parse<CellType>(inputs[1]);
                cells[new Point(x, y)] = new Cell(region, type);
            }
        }
        int townCount = Math.Clamp(int.Parse(Console.ReadLine()!), 4, 12);
        for (var i = 0; i < townCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int townId = int.Parse(inputs[0]);
            var townPoint = new Point(int.Parse(inputs[1]), int.Parse(inputs[2]));
            string desiredConnections = inputs[3];
            HashSet<int> desiredConnectionIds = desiredConnections == NO_CONNECTIONS
                    ? []
                    : [.. desiredConnections.Split(',').Select(int.Parse)];
            towns[townId] = new Town(townPoint, desiredConnectionIds);
            cells[townPoint].Region.Town = townId;
        }
    }

    public void Loop() {
        ReadState();
        HashSet<string> actions = [];
        var paths = GetPaths();
        var completeThisTurn = paths.Where(it => it is { IsCompleted: false, Cost: <= TURN_POINTS }).ToList();
        int points = TURN_POINTS;
        PlaceTracks(completeThisTurn, ref points, actions, "trying to complete tracks this turn");
        PlaceTracks(paths, ref points, actions, "trying to build tracks on different regions", differentRegions: true);
        PlaceTracks(paths, ref points, actions, "trying to build remaining tracks");
        if (TryFindRegionToDisrupt(out var region)) {
            actions.Add(Actions.Disrupt(region.Id));
        }
        if (actions.Count == 0) {
            actions.Add(Actions.Wait());
        }
        Console.WriteLine(string.Join(';', actions));
    }

    void ReadState() {
        int _ = int.Parse(Console.ReadLine()!); // playerScore
        int __ = int.Parse(Console.ReadLine()!); // foeScore
        foreach (var region in regions.Values) {
            region.Clear();
        }
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' ');
                var tracksOwner = Enum.Parse<Owner>(inputs[0]);
                var cell = cells[new Point(x, y)];
                cell.TracksOwner = tracksOwner;
                int tracks = cell.Region.Tracks.GetValueOrDefault(tracksOwner);
                cell.Region.Tracks[tracksOwner] = tracks + 1;
                cell.Region.Instability = int.Parse(inputs[1]);
                cell.Region.Inked = inputs[2] != "0";
                string partOfActiveConnections = inputs[3];
                if (partOfActiveConnections == NO_CONNECTIONS) {
                    continue;
                }
                foreach (string pair in partOfActiveConnections.Split(',')) {
                    foreach (int id in pair.Split('-').Select(int.Parse)) {
                        cell.Region.TownConnections.Add(id);
                    }
                }
            }
        }
    }

    public void FindPaths() {
        foreach (var town in towns.Values) {
            foreach (var path in town.DesiredConnections
                    .Select(it => towns[it])
                    .Select(targetTown => pathfinding.Path(town.Point, targetTown.Point))
                    .OfType<List<Point>>()) {
                town.Paths.Add(Path.From(path));
            }
        }
        builtPaths = true;
    }

    List<Path> GetPaths() {
        List<Path> paths = [];
        foreach (var town in towns.Values) {
            for (var i = 0; i < town.Paths.Count; i++) {
                if (town.Paths[i] is not { } path) {
                    continue;
                }
                path.Update();
                if (NeedToRebuild(path)) {
                    Debug.Log($"rebuilding path from {town.Point} to {path.Value.Last().cell} as it's unstable");
                    var newPath = pathfinding.Path(town.Point, path.Value.Last().cell);
                    if (newPath == null) {
                        town.Paths[i] = null;
                        continue;
                    }
                    town.Paths[i] = path = Path.From(newPath, path);
                }
                paths.Add(path);
            }
        }
        return paths;

        bool NeedToRebuild(Path path) {
            if (path.Instability == 0) {
                return false;
            }
            if (path.IsCompleted || path.Instability > Region.MAX_SAFE_INSTABILITY) {
                return true;
            }
            return path.Cost > (Region.MAX_SAFE_INSTABILITY + 1 - path.Instability) * TURN_POINTS;
        }
    }

    void PlaceTracks(List<Path> paths, ref int points, HashSet<string> placed,
            string log, bool differentRegions = false) {
        if (points == 0) {
            return;
        }
        Debug.Log(log);
        int lastRegion = -1;
        foreach (var path in paths.OrderByDescending(it => it.OldInstability).ThenBy(it => it.Cost)) {
            foreach ((var point, int cost) in path.Value.Where(it => it.cost > 0).OrderBy(it => it.cost)) {
                if (points - cost < 0) {
                    continue;
                }
                var cell = cells[point];
                if (differentRegions && cell.Region.Id == lastRegion) {
                    continue;
                }
                if (placed.Add(Actions.PlaceTrack(point))) {
                    Debug.Log($"placed track at {point}, cost: {cost}", 1);
                    points -= cost;
                    lastRegion = cell.Region.Id;
                }
                if (points == 0) {
                    return;
                }
            }
        }
    }

    bool TryFindRegionToDisrupt(out Region region) {
        region = null!;
        var foeRegions = cells.Values
                .Where(it => it.TracksOwner == foe)
                .GroupBy(it => it.Region)
                .Select(it => it.Key)
                .Where(it => it.Town == null && it.Tracks[foe] > it.Tracks.GetValueOrDefault(player))
                .OrderByDescending(it => it.Tracks[foe] / (float)it.Tracks.Values.Sum())
                .ToList();
        if (foeRegions.Count == 0) {
            return false;
        }
        var safeToInk = foeRegions
                .Where(it => it.Instability == Region.MAX_SAFE_INSTABILITY)
                .FirstOrDefault(it => it.TownConnections.Count >= 2);
        if (safeToInk != null) {
            Debug.Log($"destroying region connecting 2 or more towns: {safeToInk}");
            region = safeToInk;
            return true;
        }
        var safeToDisrupt = foeRegions.FirstOrDefault(it => it.Instability < Region.MAX_SAFE_INSTABILITY);
        if (safeToDisrupt == null) {
            Debug.Log("found regions to disrupt, but nothing connecting 2 or more towns");
            foeRegions.ForEach(it => Debug.Log(it, 1));
            return false;
        }
        Debug.Log($"found safe to disrupt region: {safeToDisrupt}");
        region = safeToDisrupt;
        return true;
    }

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    IEnumerable<(Point, int cost)> IGraph<Point>.Next(Point current) {
        foreach (var direction in Screen.Directions4) {
            var point = current + direction;
            if (!point.InBounds(width, height)) {
                continue;
            }
            var cell = cells[point];
            if (builtPaths && cell.Region.Instability > 0 || cell.Region.Inked) {
                continue;
            }
            yield return (point, Cost(point));
        }
    }

    int Cost(Point point) {
        var cell = cells[point];
        int? town = cell.Region.Town;
        if (cell.TracksOwner != Owner.NoTrack || town.HasValue && towns[town.Value].Point == point) {
            return 0;
        }
        return paintPoints[cell.Type];
    }

    int IGraph<Point>.Tiebreak(Point from, Point to) {
        var regionFrom = cells[from].Region;
        var regionTo = cells[to].Region;
        if (regionTo.IsSafe && !regionFrom.IsSafe) {
            return 0;
        }
        var difference = to - from;
        return Array.IndexOf(Screen.Directions4, difference) + 1;
    }
}

record Path(List<(Point cell, int cost)> Value, List<(Point, int)>? Old = null) {
    public int Cost => Value.Sum(it => it.cost);
    public bool IsCompleted => Cost == 0;
    public int Instability { get; private set; }
    public int OldInstability { get; private set; }
    public static Func<Point, int> GetCost { private get; set; } = null!;
    public static Func<Point, Region> GetRegion { private get; set; } = null!;

    public static Path From(List<Point> value, Path? old = null) {
        var path = new Path([.. value.Select(it => (it, 0))], old?.Value);
        path.Update();
        return path;
    }

    public void Update() {
        for (var i = 0; i < Value.Count; i++) {
            (var cell, int _) = Value[i];
            Value[i] = (cell, GetCost(cell));
            Instability = Math.Max(Instability, GetRegion(cell).Instability);
        }
        if (Old == null) {
            return;
        }
        foreach ((var cell, int _) in Old) {
            OldInstability = Math.Max(OldInstability, GetRegion(cell).Instability);
        }
    }
}

record Region(int Id) {
    public const int MAX_SAFE_INSTABILITY = 3;

    public int? Town { get; set; }
    public bool IsSafe => Town.HasValue;
    public Dictionary<Owner, int> Tracks { get; } = [];
    public HashSet<int> TownConnections { get; } = [];

    /// region inked (destroyed) when this >= 4
    public int Instability { get; set; }

    /// true if the region is destroyed
    public bool Inked { get; set; }

    public void Clear() {
        Tracks.Clear();
        TownConnections.Clear();
    }

    public virtual bool Equals(Region? other) => Id == other?.Id;
    public override int GetHashCode() => Id;

    public override string ToString() {
        return $"""
                {Id}
                  instability: {Instability}, connected towns: {string.Join(", ", TownConnections)}
                  tracks: {string.Join(", ", Tracks.Select(it => (it.Key, it.Value)))}
                """;
    }
}

record Cell(Region Region, CellType Type) {
    public Owner TracksOwner { get; set; } = Owner.NoTrack;
}

enum Owner {
    NoTrack = -1,
    Player0 = 0,
    Player1 = 1,
    Neutral = 2
}

enum CellType {
    Plains,
    River,
    Mountain
}

record Town(Point Point, HashSet<int> DesiredConnections) {
    public List<Path?> Paths { get; } = [];
}

static class Actions {
    public static string Autoplace(Town from, Town to) => $"AUTOPLACE {from.Point} {to.Point}";
    public static string PlaceTrack(Point point) => $"PLACE_TRACKS {point}";
    public static string Disrupt(int region) => $"DISRUPT {region}";
    public static string Wait() => "WAIT";
    public static string Message(string text) => $"MESSAGE {text}";
}

readonly record struct Point(int X, int Y) {
    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"{X} {Y}";
}

static class Screen {
    static readonly Point UP = new(0, -1);
    static readonly Point DOWN = new(0, 1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [UP, RIGHT, DOWN, LEFT];
}

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly PriorityQueue<T, (int cost, int tiebreak)> queue = new();
    readonly Dictionary<T, T> parents = new();
    readonly Dictionary<T, int> visited = new();

    /// Dijkstra
    public List<T>? Path(T start, T end) {
        queue.Clear();
        parents.Clear();
        visited.Clear();
        queue.Enqueue(start, (0, 0));
        visited.Add(start, 0);
        while (queue.TryDequeue(out var current, out var priority)) {
            if (current.Equals(end)) {
                return Reconstruct(start, current);
            }
            foreach ((var next, int cost) in graph.Next(current)) {
                int newCost = priority.cost + cost;
                if (newCost >= visited.GetValueOrDefault(next, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(next, (newCost, priority.tiebreak + graph.Tiebreak(current, next)));
                visited[next] = newCost;
                parents[next] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        var path = new List<T>();
        var node = end;
        while (!node.Equals(start)) {
            path.Add(node);
            node = parents[node];
        }
        path.Reverse();
        return path;
    }
}

interface IGraph<T> {
    IEnumerable<(T, int cost)> Next(T current);
    int Tiebreak(T from, T to);
}

static class Debug {
    const int TAB_LENGTH = 2;

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
