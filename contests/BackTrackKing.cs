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

    public Game() {
        pathfinding = new Pathfinding<Point>(this);
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
        var paths = FindPaths();
        var completeThisTurn = paths.Where(it => it.Cost <= TURN_POINTS).ToList();
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
                cell.TownConnections.Clear();
                if (partOfActiveConnections == NO_CONNECTIONS) {
                    continue;
                }
                foreach (string pair in partOfActiveConnections.Split(',')) {
                    foreach (int id in pair.Split('-').Select(int.Parse)) {
                        cell.TownConnections.Add(id);
                        cell.Region.TownConnections.Add(id);
                    }
                }
            }
        }
    }

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    List<PathResult<Point>> FindPaths() {
        List<PathResult<Point>> paths = [];
        foreach (var town in towns.Values) {
            var cell = cells[town.Point];
            foreach (int connection in town.DesiredConnections) {
                if (cell.TownConnections.Contains(connection)) {
                    continue;
                }
                var targetTown = towns[connection];
                var path = pathfinding.Path(town.Point, targetTown.Point);
                if (path == null) {
                    continue;
                }
                paths.Add(path.Value);
            }
        }
        return paths;
    }

    void PlaceTracks(List<PathResult<Point>> paths, ref int points, HashSet<string> placed,
            string log, bool differentRegions = false) {
        if (points == 0) {
            return;
        }
        Debug.Log(log);
        int lastRegion = -1;
        foreach (var path in paths.OrderBy(it => it.Cost).Select(it => it.Path)) {
            foreach ((var point, int cost) in path.Where(it => it.cost > 0).OrderBy(it => it.cost)) {
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

    IEnumerable<(Point, int cost)> IGraph<Point>.Next(Point current) {
        foreach (var direction in Screen.Directions4) {
            var point = current + direction;
            if (!point.InBounds(width, height)) {
                continue;
            }
            var cell = cells[point];
            if (cell.Region.Inked) {
                continue;
            }
            int? town = cell.Region.Town;
            if (cell.TracksOwner != Owner.NoTrack || town.HasValue && towns[town.Value].Point == point) {
                yield return (point, 0);
                continue;
            }
            int cost = paintPoints[cell.Type];
            yield return (point, cost);
        }
    }

    int IGraph<Point>.Heuristic(Point start, Point end) {
        return Point.ManhattanDistance(start, end);
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
    public Owner TracksOwner { get; set; }
    public HashSet<int> TownConnections { get; } = [];
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

record struct Town(Point Point, HashSet<int> DesiredConnections);

// MESSAGE text
static class Actions {
    public static string Autoplace(Town from, Town to) {
        return $"AUTOPLACE {from.Point} {to.Point}";
    }

    public static string PlaceTrack(Point point) {
        return $"PLACE_TRACKS {point}";
    }

    public static string Disrupt(int region) {
        return $"DISRUPT {region}";
    }

    public static string Wait() => "WAIT";
}

readonly record struct Point(int X, int Y) {
    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
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
    readonly Dictionary<T, (T parent, int cost)> data = new();
    readonly Dictionary<T, int> visited = new();

    /// Dijkstra
    public PathResult<T>? Path(T start, T end) {
        queue.Clear();
        data.Clear();
        visited.Clear();
        queue.Enqueue(start, (0, 0));
        visited.Add(start, 0);
        while (queue.TryDequeue(out var current, out var priority)) {
            if (current.Equals(end)) {
                return new PathResult<T>(Reconstruct(start, current), visited[current]);
            }
            foreach ((var node, int cost) in graph.Next(current)) {
                int newCost = priority.cost + cost;
                if (newCost >= visited.GetValueOrDefault(node, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(node, (newCost, graph.Tiebreak(current, node)));
                visited[node] = newCost;
                data[node] = (current, cost);
            }
        }
        return null;
    }

    List<(T, int)> Reconstruct(T start, T end) {
        var path = new List<(T, int)>();
        var node = end;
        while (!node.Equals(start)) {
            (var parent, int cost) = data[node];
            path.Add((node, cost));
            node = parent;
        }
        path.Reverse();
        return path;
    }
}

class APathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly Dictionary<T, Node> nodes = new();
    readonly PriorityQueue<Node, int> queue = new();

    /// A*
    public (List<T>, int cost)? Path(T start, T end) {
        nodes.Clear();
        queue.Clear();
        var startNode = new Node(start) {
            gCost = 0,
            hCost = graph.Heuristic(start, end)
        };
        nodes.Add(start, startNode);
        queue.Enqueue(startNode, startNode.Priority);
        while (queue.TryDequeue(out var current, out _)) {
            // ignore stale nodes
            if (current.isClosed) {
                continue;
            }
            current.isOpen = false;
            current.isClosed = true;
            if (current.value.Equals(end)) {
                return (Reconstruct(startNode, current), current.gCost);
            }
            foreach ((var node, int cost) in graph.Next(current.value)) {
                if (!nodes.TryGetValue(node, out var neighbor)) {
                    nodes.Add(node, neighbor = new Node(node));
                }
                if (neighbor.isClosed) {
                    continue;
                }
                int gCost = current.gCost + cost;
                if (gCost >= neighbor.gCost && neighbor.isOpen) {
                    continue;
                }
                neighbor.gCost = gCost;
                neighbor.hCost = graph.Heuristic(node, end);
                neighbor.parent = current;
                queue.Enqueue(neighbor, neighbor.Priority);
                neighbor.isOpen = true;
            }
        }
        return null;
    }

    static List<T> Reconstruct(Node start, Node end) {
        var path = new List<T>();
        var current = end;
        while (current != start) {
            path.Add(current!.value);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    class Node(T value) {
        public readonly T value = value;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public Node? parent;
        public bool isOpen;
        public bool isClosed;
        int FCost => gCost + hCost;

        /// Order by FCost, then hCost
        public int Priority => FCost * 1000 + hCost;
    }
}

record struct PathResult<T>(List<(T point, int cost)> Path, int Cost);

interface IGraph<T> {
    IEnumerable<(T, int cost)> Next(T current);
    int Heuristic(T start, T end);
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
