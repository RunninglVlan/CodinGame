using System.Diagnostics.CodeAnalysis;

const string testCase = """
                        1
                        21
                        14
                        0 0
                        0 0
                        0 0
                        1 2
                        1 2
                        1 0
                        1 2
                        2 2
                        2 2
                        2 2
                        3 1
                        3 0
                        4 0
                        4 0
                        4 0
                        5 0
                        5 0
                        5 0
                        6 0
                        6 0
                        6 0
                        0 0
                        0 2
                        0 2
                        1 2
                        1 2
                        1 0
                        1 0
                        2 0
                        2 2
                        2 2
                        3 1
                        3 1
                        3 0
                        4 1
                        4 1
                        5 1
                        5 1
                        5 0
                        6 0
                        6 0
                        6 0
                        0 0
                        7 0
                        0 2
                        8 2
                        1 2
                        8 2
                        2 0
                        2 0
                        2 0
                        3 0
                        3 0
                        3 1
                        4 1
                        4 1
                        4 0
                        12 0
                        12 1
                        5 0
                        13 0
                        6 0
                        6 0
                        7 0
                        7 0
                        7 0
                        8 2
                        8 0
                        8 2
                        9 0
                        9 0
                        10 0
                        10 0
                        10 0
                        10 0
                        11 2
                        11 0
                        11 0
                        12 0
                        12 1
                        12 1
                        13 0
                        13 1
                        13 1
                        14 0
                        7 0
                        7 0
                        8 0
                        8 0
                        9 0
                        9 0
                        9 0
                        9 0
                        10 0
                        10 0
                        11 0
                        11 2
                        18 2
                        18 0
                        12 0
                        12 0
                        12 1
                        20 1
                        13 1
                        13 0
                        14 0
                        14 0
                        14 0
                        15 0
                        15 0
                        15 0
                        15 0
                        16 0
                        16 0
                        17 0
                        17 0
                        11 2
                        11 2
                        18 2
                        19 0
                        19 0
                        19 0
                        19 0
                        20 0
                        20 0
                        20 0
                        14 0
                        14 0
                        21 0
                        15 0
                        15 0
                        15 0
                        16 0
                        16 0
                        16 0
                        16 2
                        17 0
                        17 0
                        11 2
                        18 0
                        19 0
                        26 0
                        19 0
                        19 0
                        19 0
                        20 0
                        20 0
                        14 0
                        21 0
                        21 0
                        22 0
                        22 0
                        22 0
                        22 0
                        16 0
                        23 0
                        17 2
                        17 2
                        17 2
                        18 2
                        18 0
                        18 0
                        26 0
                        26 0
                        26 0
                        26 0
                        27 0
                        20 0
                        14 0
                        21 0
                        21 0
                        21 0
                        22 0
                        22 0
                        22 0
                        23 0
                        23 0
                        24 1
                        24 1
                        24 2
                        25 0
                        25 0
                        18 0
                        25 0
                        26 0
                        26 0
                        34 0
                        27 0
                        27 0
                        28 0
                        28 0
                        21 0
                        29 0
                        29 0
                        22 0
                        30 0
                        30 1
                        23 1
                        23 1
                        24 2
                        24 0
                        25 0
                        25 0
                        25 0
                        25 0
                        33 0
                        34 0
                        34 0
                        27 0
                        27 0
                        28 0
                        28 0
                        29 0
                        29 0
                        29 0
                        29 0
                        30 0
                        30 1
                        30 2
                        23 2
                        31 0
                        31 0
                        32 0
                        32 0
                        32 0
                        32 0
                        33 0
                        33 0
                        34 0
                        34 0
                        27 0
                        28 2
                        28 2
                        28 2
                        28 2
                        29 0
                        29 0
                        36 0
                        30 1
                        30 2
                        31 2
                        31 0
                        38 0
                        38 0
                        32 0
                        32 2
                        33 0
                        33 0
                        33 0
                        34 0
                        34 0
                        27 0
                        35 0
                        35 2
                        35 0
                        35 0
                        36 0
                        36 1
                        36 1
                        37 1
                        37 2
                        31 2
                        38 2
                        38 0
                        38 0
                        39 2
                        39 2
                        39 0
                        40 0
                        40 0
                        41 0
                        41 0
                        41 0
                        35 0
                        35 0
                        35 0
                        35 0
                        36 0
                        36 1
                        36 0
                        37 0
                        37 2
                        37 2
                        38 2
                        38 0
                        39 0
                        39 2
                        39 2
                        40 0
                        40 0
                        40 0
                        41 0
                        41 0
                        41 0
                        5
                        0 12 1 3
                        1 3 4 x
                        2 7 6 0,1
                        3 17 7 1,2,4
                        4 3 10 0,1,2 
                        """;
Console.SetIn(new StringReader(testCase));
// Visual
// Connect towns with your train tracks and disrupt the opponent's
// https://www.codingame.com/contests/summer-challenge-2026-back-track-king
var game = new Game();
game.Init();
game.Test();

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

    public void Test() {
        var town = towns[2];
        var targetTown = towns[1];
        foreach (var valueTuple in pathfinding.Path(town.Point, targetTown.Point).Value.path) {
            Console.Error.WriteLine(valueTuple);
        }
    }

    public void Loop() {
        ReadState();
        List<string> actions = [.. PlaceTracks()];
        if (TryFindRegionToDisrupt(out var region)) {
            actions.Add(Actions.Disrupt(region.Id));
        }
        if (actions.Count == 0) {
            actions.Add(Actions.Wait());
        }
        Console.WriteLine(string.Join(';', actions));
    }

    void ReadState() {
        int playerScore = int.Parse(Console.ReadLine()!);
        int foeScore = int.Parse(Console.ReadLine()!);
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

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    IEnumerable<string> PlaceTracks() {
        // todo: replace with maximum place tracks (1-3)
        // todo: so if i can finish connection right now, do it
        // todo: otherwise place single tracks at the start/end of different paths
        // todo: let's start with just "implementation" of autoplace
        (List<(Point point, int cost)> path, int? cost) best = (null!, null);
        foreach (var town in towns.Values) {
            var cell = cells[town.Point];
            foreach (int connection in town.DesiredConnections) {
                if (cell.TownConnections.Contains(connection)) {
                    continue;
                }
                var targetTown = towns[connection];
                var current = pathfinding.Path(town.Point, targetTown.Point);
                if (current != null && (best.cost == null || current.Value.cost < best.cost)) {
                    (var path, int cost) = current.Value;
                    best = ([.. path.Prepend((town.Point, 0))], cost);
                }
            }
        }
        if (best.cost == null) {
            yield break;
        }
        // i need to maximize tracks, because i get points for any track
        // i don't get more points if i spent more on it
        // so if i have 3 that cost 1, i need to place them
        // otherwise use 2 and 1
        // otherwise 3
        Console.Error.WriteLine($"path from {best.path.First().point} to {best.path.Last().point}");
        foreach ((var point, int cost) in best.path) {
            Console.Error.WriteLine($"  {point}: {cost}");
        }
        yield return Actions.Autoplace(best.path.First().point, best.path.Last().point);
        yield break;
        int points = TURN_POINTS;
        foreach ((var point, int cost) in best.path.Where(it => it.cost > 0).OrderBy(it => it.cost)) {
            if (points - cost >= 0) {
                yield return Actions.PlaceTrack(point);
                points -= cost;
            }
            if (points == 0) {
                yield break;
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
                .ToArray();
        if (foeRegions.Length == 0) {
            return false;
        }
        var safeToInk = foeRegions
                .Where(it => it.Instability == Region.MAX_SAFE_INSTABILITY)
                .FirstOrDefault(it => it.TownConnections.Count >= 2);
        if (safeToInk != null) {
            region = safeToInk;
            return true;
        }
        var safeToDisrupt = foeRegions.FirstOrDefault(it => it.Instability < Region.MAX_SAFE_INSTABILITY);
        if (safeToDisrupt == null) {
            return false;
        }
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

    int IGraph<Point>.Tiebreak(Point from, Point to) {
        var difference = to - from;
        return Array.IndexOf(Screen.Directions4, difference);
    }
}

record Region(int Id) {
    public const int MAX_SAFE_INSTABILITY = 3;

    public int? Town { get; set; }
    public Dictionary<Owner, int> Tracks { get; } = [];
    public HashSet<int> TownConnections { get; } = [];

    /// region inked (destroyed) when this >= 4
    public int Instability { get; set; }

    /// true if region is destroyed
    public bool Inked { get; set; }

    public void Clear() {
        Tracks.Clear();
        TownConnections.Clear();
    }

    public virtual bool Equals(Region? other) => Id == other?.Id;
    public override int GetHashCode() => Id;
}

record Cell(Region Region, CellType Type) {
    public Owner TracksOwner { get; set; } = Owner.NoTrack;
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

// PLACE_TRACKS x y | MESSAGE text
static class Actions {
    public static string Autoplace(Point from, Point to) {
        return $"AUTOPLACE {from} {to}";
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
    public (List<(T value, int cost)> path, int cost)? Path(T start, T end) {
        queue.Clear();
        data.Clear();
        visited.Clear();
        queue.Enqueue(start, (0, 0));
        Console.Error.WriteLine($"Enqueue {start}, {(0, 0)}");
        visited.Add(start, 0);
        while (queue.TryDequeue(out var current, out var priority)) {
            Console.Error.WriteLine($"  Dequeue {current}");
            if (current.Equals(end)) {
                return (Reconstruct(start, current), visited[current]);
            }
            foreach ((var node, int cost) in graph.Next(current)) {
                int newCost = priority.cost + cost;
                if (newCost >= visited.GetValueOrDefault(node, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(node, (newCost, graph.Tiebreak(current, node)));
                Console.Error.WriteLine($"Enqueue {node}, {(newCost, graph.Tiebreak(current, node))}");
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

interface IGraph<T> {
    IEnumerable<(T, int cost)> Next(T current);
    int Tiebreak(T from, T to);
}
