using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

// todo: improve non-state search solution, keep state search, but hide it with regions
// todo: use pointFrequencies in disrupt logic
// todo: use pointFrequencies in path sorting too
// todo: make pathfinding prefer safe regions with towns, like for example to safely go through region to reach target
// todo: add regions to state
/*
// field
public Dictionary<int, Region> Regions { get; init; } = null!;
// clone
foreach ((int id, var region) in Regions) {
    regions[id] = region with { };
}
foreach (var (point, cell) in Cells) {
    cells[point] = cell with { Region = regions[cell.Region.Id] };
}
// equals
foreach ((int id, var region) in Regions) {
    if (region.Instability != that.Regions[id].Instability) {
        return false;
    }
}
// hash
var regionsHash = 0;
foreach ((int id, var region) in Regions) {
    regionsHash ^= HashCode.Combine(id, region.Instability);
}
return HashCode.Combine(cellsHash, regionsHash);
 */
// todo: add disruption to beam search
// todo: simulate opponent actions - try just using my non-beam search actions
// todo: remove bot versions after at least reaching gold

// Visual
// Connect towns with your train tracks and disrupt the opponent's
// https://www.codingame.com/contests/summer-challenge-2026-back-track-king
var game = new Game();
game.Init();
game.FindPaths();
while (true) {
    game.Loop();
}

class GameState : IState {
    public int Score => PlayerScore * 100_000 + heuristic;
    public int PlayerScore { get; set; }
    int heuristic;
    public Dictionary<Point, Cell> Cells { get; init; } = null!;
    public List<Path> Paths { get; init; } = null!;
    public List<HashSet<string>> Actions { get; init; } = null!;

    public GameState Clone() {
        return new GameState {
            Cells = Cells.ToDictionary(it => it.Key, it => it.Value with { }),
            Paths = [.. Paths.Select(it => it with { Value = [.. it.Value] })],
            Actions = [.. Actions, []]
        };
    }

    public void PlaceTrack(Point point, Owner player) {
        Actions.Last().Add(GameActions.PlaceTrack(point));
        Cells[point].SetTrackOwner(player);
        foreach (var path in Paths) {
            path.TryPlaceTrack(point);
        }
    }

    public void UpdateScore(Owner player) {
        heuristic = 0;
        foreach (var path in Paths) {
            if (!path.IsCompleted) {
                heuristic += (int)(path.GainPoints / (float)(path.GainTurns + 1) * 100);
                continue;
            }
            foreach ((var point, int _) in path.Value) {
                if (Cells[point].TracksOwner == player) {
                    PlayerScore++;
                }
            }
        }
    }

    public bool Equals(IState? other) {
        if (other is not GameState that) {
            return false;
        }
        if (ReferenceEquals(this, that)) {
            return true;
        }
        foreach (var (point, cell) in Cells) {
            if (cell.TracksOwner != that.Cells[point].TracksOwner) {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object? other) => Equals(other as IState);

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public override int GetHashCode() {
        var cellsHash = 0;
        foreach (var cell in Cells.Values) {
            cellsHash ^= cell.TracksOwner.GetHashCode();
        }
        return cellsHash;
    }
}

class Game : IGraph<Point>, IGame<GameState> {
    const string NO_CONNECTIONS = "x";
    public const int TURN_POINTS = 3;

    readonly Pathfinding<Point> pathfinding;
    readonly BeamSearch<GameState> beamSearch;

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

    readonly StringBuilder message = new();
    bool findShortest;
    readonly Dictionary<Point, int> pointFrequencies = [];

    public Game() {
        pathfinding = new Pathfinding<Point>(this);
        beamSearch = new BeamSearch<GameState>(this);
        Path.GetCost = Cost;
        Path.GetCell = point => cells[point];
    }

    public void Init() {
        player = Enum.Parse<Owner>(Console.ReadLine()!);
        Path.Player = player;
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
        var startState = ReadState();
        message.Clear();
        var actions = new HashSet<string>();
        if (false) {
            actions = beamSearch.FindBest(startState, 4, 4)?.Actions.First() ?? [];
        } else {
            int points = TURN_POINTS;
            var paths = GetPaths().OrderBy(it => it.GainTurns).ThenByDescending(it => it.GainPoints).ToList();
            UpdateFrequencies(paths);
            var completeThisTurn = paths.Where(it => it is { IsCompleted: false, Cost.value: <= TURN_POINTS }).ToList();
            PlaceTracks(completeThisTurn, ref points, actions, "trying to complete tracks this turn");
            var activePathsInDanger = paths
                    .Where(it => it.Cost.old == 0 && it.Instability.old == Region.MAX_SAFE_INSTABILITY)
                    .ToList();
            PlaceTracks(activePathsInDanger, ref points, actions, "trying to save paths in danger");
            PlaceTracks(paths, ref points, actions, "trying to build tracks on different regions", differentRegions: true);
            PlaceTracks(paths, ref points, actions, "trying to build remaining tracks");
        }
        if (TryFindRegionToDisrupt(out var region)) {
            actions.Add(GameActions.Disrupt(region.Id));
            message.Append($"DR - {region.Id}");
        }
        if (message.Length > 0) {
            actions.Add(GameActions.Message(message.ToString()));
        }
        if (actions.Count == 0) {
            actions.Add(GameActions.Wait());
        }
        Console.WriteLine(string.Join(';', actions));
    }

    GameState ReadState() {
        int playerScore = int.Parse(Console.ReadLine()!);
        int __ = int.Parse(Console.ReadLine()!); // foeScore
        foreach (var region in regions.Values) {
            region.InitTurn();
        }
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                string[] inputs = Console.ReadLine()!.Split(' '); // inputs[2] != "0" means region is inked/destroyed
                var tracksOwner = Enum.Parse<Owner>(inputs[0]);
                var cell = cells[new Point(x, y)];
                cell.SetTrackOwner(tracksOwner);
                cell.Region.Instability = int.Parse(inputs[1]);
                string partOfActiveConnections = inputs[3];
                if (partOfActiveConnections == NO_CONNECTIONS) {
                    continue;
                }
                foreach (string pair in partOfActiveConnections.Split(',')) {
                    foreach (int id in pair.Split('-').Select(int.Parse)) {
                        cell.Region.TownConnections.value.Add(id);
                    }
                }
            }
        }
        return new GameState {
            PlayerScore = playerScore,
            Cells = cells,
            Paths = GetPaths(),
            Actions = []
        };
    }

    public void FindPaths() {
        foreach ((int id, var town) in towns) {
            foreach (int targetId in town.DesiredConnections) {
                var targetTown = towns[targetId];
                var path = pathfinding.Path(town.Point, targetTown.Point);
                if (path == null) {
                    continue;
                }
                town.Paths.Add(Path.From(path));
                var lastPath = town.Paths.Last()!;
                // Debug.Log($"found path from {id} to {targetId}, Cost: {lastPath.Cost}");
                foreach (var valueTuple in lastPath.Value) {
                    // Debug.Log(valueTuple, 1);
                }
            }
        }
    }

    void UpdateFrequencies(List<Path> paths) {
        pointFrequencies.Clear();
        foreach (var path in paths) {
            foreach ((var point, int cost) in path.Value) {
                if (cost == 0) {
                    continue;
                }
                int count = pointFrequencies.GetValueOrDefault(point);
                pointFrequencies[point] = count + 1;
            }
        }
    }

    List<Path> GetPaths() {
        List<Path> paths = [];
        foreach ((int id, var town) in towns) {
            for (var i = 0; i < town.Paths.Count; i++) {
                if (town.Paths[i] is not { } path) {
                    continue;
                }
                path.Update();
                bool needToRefreshUnstable = path.NeedToRefreshUnstable();
                var targetRegion = cells[path.Value.Last().point].Region;
                bool connected = targetRegion.BecameConnected(id);
                if (needToRefreshUnstable || connected) {
                    town.Paths[i] = path = RefreshPath(town.Point, path, !needToRefreshUnstable && connected);
                    if (path == null) {
                        continue;
                    }
                    // Debug.Log($"refreshed path from {id} to {path.Value.Last().point} as {(needToRefreshUnstable ? "it's unstable" : "it was connected")}, Cost: {path.Cost}");
                    foreach (var valueTuple in path.Value) {
                        // Debug.Log(valueTuple, 1);
                    }
                }
                paths.Add(path);
            }
        }
        return paths;
    }

    Path? RefreshPath(Point town, Path currentPath, bool justConnected) {
        var newPath = pathfinding.Path(town, currentPath.Value.Last().point);
        if (newPath == null) {
            return null;
        }
        if (justConnected) {
            findShortest = true;
            var shortestPath = pathfinding.Path(town, currentPath.Value.Last().point);
            if (shortestPath != null && Path.Gain(shortestPath) > Path.Gain(newPath)) {
                newPath = shortestPath;
            }
        }
        findShortest = false;
        var oldPath = justConnected ? currentPath.Old : currentPath.Value;
        return Path.From(newPath, oldPath);
    }

    void PlaceTracks(List<Path> paths, ref int points, HashSet<string> placed,
            string log, bool differentRegions = false) {
        if (points == 0 || paths.Count == 0) {
            return;
        }
        Debug.Log(log);
        int lastRegion = -1;
        Debug.Log("all paths", 1);
        foreach (var path in paths) {
            Debug.Log(path, 2);
            if (false && path.Value.First().point == new Point(11, 2) && path.Value.Last().point == new Point(15, 5)) {
                foreach (var valueTuple in path.Value) {
                    Debug.Log($"{valueTuple}: owner: {cells[valueTuple.point].TracksOwner}", 3);
                }
            }
        }
        foreach (var path in paths) {
            Debug.Log($"processing path {path.Route()}", 1);
            var pathCells = path.Value
                    .Where(it => it.cost > 0)
                    .OrderBy(it => it.cost)
                    .ThenByDescending(it => pointFrequencies[it.point]);
            foreach ((var point, int cost) in pathCells) {
                if (points - cost < 0) {
                    continue;
                }
                var cell = cells[point];
                if (differentRegions && cell.Region.Id == lastRegion) {
                    continue;
                }
                if (placed.Add(GameActions.PlaceTrack(point))) {
                    cell.SetTrackOwner(player);
                    Debug.Log($"placed track at {point}, cost: {cost} {path.Route()}", 2);
                    points -= cost;
                    lastRegion = cell.Region.Id;
                    message.Append($"PT - {point}: {cost}, ");
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
                .FirstOrDefault(it => it.TownConnections.value.Count >= 2);
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
            if (!point.InBounds(width, height) || cells[point].Region.Instability > 0) {
                continue;
            }
            yield return (point, Cost(point));
        }
    }

    int Cost(Point point) {
        var cell = cells[point];
        int? town = cell.Region.Town;
        if (!findShortest && cell.TracksOwner != Owner.NoTrack || town.HasValue && towns[town.Value].Point == point) {
            return 0;
        }
        return paintPoints[cell.Type];
    }

    int IGraph<Point>.Tiebreak(Point from, Point to) {
        var regionFrom = cells[from].Region;
        var regionTo = cells[to].Region;
        if (!findShortest && regionTo.IsSafe && !regionFrom.IsSafe) {
            return 0;
        }
        var difference = to - from;
        return Array.IndexOf(Screen.Directions4, difference) + 1;
    }

    IEnumerable<GameState> IGame<GameState>.Next(GameState current) {
        foreach (var next in CompletedPathStates(current)) {
            yield return next;
        }
        foreach (var next in RegionTrackStates(current)) {
            yield return next;
        }
        foreach (var next in PointTrackStates(current)) {
            yield return next;
        }
    }

    IEnumerable<GameState> CompletedPathStates(GameState state) {
        foreach (var path in state.Paths.Where(it => it is { IsCompleted: false, Cost.value: <= TURN_POINTS })) {
            var next = state.Clone();
            foreach (var (point, _) in path.Value.Where(it => it.cost != 0)) {
                next.PlaceTrack(point, player);
            }
            next.UpdateScore(player);
            yield return next;
        }
    }

    IEnumerable<GameState> RegionTrackStates(GameState state) {
        const int nextPerPoints = 10;
        int lastRegion = -1;
        int budget = TURN_POINTS;
        var next = state.Clone();
        var nextCount = 0;
        foreach (var path in state.Paths.OrderBy(it => it.GainTurns).ThenByDescending(it => it.GainPoints)) {
            foreach ((var point, int cost) in path.Value.Where(it => it.cost != 0)) {
                if (budget - cost < 0) {
                    continue;
                }
                var cell = cells[point];
                if (cell.Region.Id == lastRegion) {
                    continue;
                }
                lastRegion = cell.Region.Id;
                budget -= cost;
                next.PlaceTrack(point, player);
                if (budget != 0) {
                    continue;
                }
                next.UpdateScore(player);
                yield return next;
                if (++nextCount == nextPerPoints) {
                    break;
                }
                budget = TURN_POINTS;
                next = state.Clone();
            }
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    IEnumerable<GameState> PointTrackStates(GameState state) {
        const int nextPerPoints = 5;
        for (var points = 1; points <= 3; points++) {
            int budget = TURN_POINTS;
            var next = state.Clone();
            var nextCount = 0;
            foreach ((var point, int cost) in PointTracks(state, points)) {
                budget -= cost;
                next.PlaceTrack(point, player);
                if (points >= 2 || budget == 0) {
                    next.UpdateScore(player);
                    yield return next;
                    if (++nextCount == nextPerPoints) {
                        break;
                    }
                    budget = TURN_POINTS;
                    next = state.Clone();
                }
            }
            if (points == 1 && budget < 2) {
                next.UpdateScore(player);
                yield return next;
            }
        }
    }

    static IEnumerable<(Point point, int cost)> PointTracks(GameState state, int points) {
        foreach (var path in state.Paths.OrderBy(it => it.GainTurns).ThenByDescending(it => it.GainPoints)) {
            foreach ((var point, int cost) in path.Value.Where(it => it.cost != 0)) {
                if (cost != points) {
                    continue;
                }
                yield return (point, cost);
            }
        }
    }
}

record Path(List<(Point point, int cost)> Value, List<(Point, int)>? Old = null) {
    public (int value, int old) Cost { get; private set; }
    public int GainTurns => Cost.value / Game.TURN_POINTS;
    public int GainPoints { get; private set; }
    public bool IsCompleted => Cost.value == 0;
    public (int value, int old) Instability { get; private set; }

    public static Owner Player { private get; set; }
    public static Func<Point, int> GetCost { private get; set; } = null!;
    public static Func<Point, Cell> GetCell { private get; set; } = null!;

    public static Path From(List<Point> value, List<(Point, int)>? old = null) {
        var path = new Path([.. value.Select(it => (it, 0))], old);
        path.Update();
        return path;
    }

    public void Update() {
        var totalCost = 0;
        GainPoints = 0;
        for (var i = 0; i < Value.Count; i++) {
            (var point, int _) = Value[i];
            int cost = GetCost(point);
            Value[i] = (point, cost);
            totalCost += cost;
            GainPoints += CanGivePoints(GetCell(point)) ? 1 : 0;
            Instability = Instability with { value = Math.Max(Instability.value, GetCell(point).Region.Instability) };
        }
        Cost = Cost with { value = totalCost };
        if (Old == null) {
            return;
        }
        totalCost = 0;
        for (var i = 0; i < Old.Count; i++) {
            (var point, int _) = Old[i];
            int cost = GetCost(point);
            Old[i] = (point, cost);
            totalCost += cost;
            Instability = Instability with { old = Math.Max(Instability.old, GetCell(point).Region.Instability) };
        }
        Cost = Cost with { old = totalCost };
    }

    public void TryPlaceTrack(Point point) {
        for (var i = 0; i < Value.Count; i++) {
            (var pathPoint, int cost) = Value[i];
            if (pathPoint != point) {
                continue;
            }
            Value[i] = (pathPoint, 0);
            Cost = Cost with { value = Cost.value - cost };
            GainPoints++;
            break;
        }
    }

    public bool NeedToRefreshUnstable() {
        if (Instability.value == 0) {
            return false;
        }
        if (IsCompleted || Instability.value > Region.MAX_SAFE_INSTABILITY) {
            return true;
        }
        return Cost.value > (Region.MAX_SAFE_INSTABILITY + 1 - Instability.value) * Game.TURN_POINTS;
    }

    public static int Gain(List<Point> value) => value.Select(it => GetCell(it)).Count(CanGivePoints);

    static bool CanGivePoints(Cell cell) {
        return cell.TracksOwner == Player || cell.TracksOwner == Owner.NoTrack;
    }

    public string Route() => $"from {Value.First().point} to {Value.Last().point}";

    public override string ToString() {
        return $"{Route()}, Cost: {Cost}, Gain: {GainPoints} in turns: {GainTurns}, OldInstability: {Instability.old}";
    }
}

record Region(int Id) {
    public const int MAX_SAFE_INSTABILITY = 3;

    public int? Town { get; set; }
    public bool IsSafe => Town.HasValue;
    public Dictionary<Owner, int> Tracks { get; } = [];
    public (HashSet<int> value, HashSet<int> old) TownConnections { get; private set; } = ([], null!);

    /// region inked (destroyed) when this >= 4
    public int Instability { get; set; }

    public void InitTurn() {
        Tracks.Clear();
        TownConnections = TownConnections with { old = [.. TownConnections.value] };
        TownConnections.value.Clear();
    }

    public bool BecameConnected(int id) {
        return !TownConnections.old.Contains(id) && TownConnections.value.Contains(id);
    }

    public virtual bool Equals(Region? other) => Id == other?.Id;
    public override int GetHashCode() => Id;

    public override string ToString() {
        return $"""
                {Id}
                  instability: {Instability}, connected towns: {string.Join(", ", TownConnections.value)}
                  tracks: {string.Join(", ", Tracks.Select(it => (it.Key, it.Value)))}
                """;
    }
}

record Cell(Region Region, CellType Type) {
    public Owner TracksOwner { get; private set; } = Owner.NoTrack;

    public void SetTrackOwner(Owner owner) {
        TracksOwner = owner;
        int tracks = Region.Tracks.GetValueOrDefault(owner);
        Region.Tracks[owner] = tracks + 1;
    }
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

static class GameActions {
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

class BeamSearch<T>(IGame<T> game) where T : IState {
    readonly HashSet<T> visited = [];

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public T? FindBest(T start, int levels, int beamWidth = 10) {
        T? best = default;
        var currentLevel = new List<T> { start };
        for (var level = 0; level < levels; level++) {
            var nextLevel = new List<T>();
            visited.Clear();
            foreach (var current in currentLevel) {
                var stopwatch = Stopwatch.StartNew();
                nextLevel.AddRange(game.Next(current));
                Debug.Log($"Next took {stopwatch.Elapsed.TotalMilliseconds}ms", level);
            }
            Debug.Log($"Next states: {nextLevel.Count}", level);
            currentLevel.Clear();
            // by score descending
            nextLevel.Sort((a, b) => b.Score.CompareTo(a.Score));
            foreach (var state in nextLevel) {
                if (!visited.Add(state)) {
                    continue;
                }
                if (currentLevel.Count >= beamWidth) {
                    break;
                }
                currentLevel.Add(state);
            }
            if (currentLevel.Count == 0) {
                break;
            }
            var bestCurrent = currentLevel.First();
            if (best == null || bestCurrent.Score > best.Score) {
                best = bestCurrent;
            }
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<TState> Next(TState current);
}

interface IState : IEquatable<IState> {
    int Score { get; }
}

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly PriorityQueue<T, (int cost, int tiebreak)> queue = new();
    readonly Dictionary<T, T> parents = new();
    readonly Dictionary<T, int> visited = new();
    readonly StringBuilder log = new();

    /// Dijkstra
    public List<T>? Path(T start, T end) {
        queue.Clear();
        parents.Clear();
        visited.Clear();
        queue.Enqueue(start, (0, 0));
        log.Clear();
        log.AppendLine($"Put {start}, {(0, 0)}");
        visited.Add(start, 0);
        var closest = 0;
        var endPoint = new Point();
        if (start is Point startPoint && end is Point point) {
            endPoint = point;
            closest = Point.ManhattanDistance(startPoint, endPoint);
        }
        while (queue.TryDequeue(out var current, out var priority)) {
            log.Append($"  Pop {current}");
            if (current is Point currentPoint) {
                int distance = Point.ManhattanDistance(currentPoint, endPoint);
                if (distance < closest) {
                    closest = distance;
                    log.Append($": {string.Join(", ", Reconstruct(start, current))}");
                }
            }
            log.AppendLine();
            if (current.Equals(end)) {
                if (start.Equals(new Point(7, 6)) && end.Equals(new Point(14, 10))) {
                    Debug.Log(log);
                }
                return Reconstruct(start, current);
            }
            foreach ((var next, int cost) in graph.Next(current)) {
                int newCost = priority.cost + cost;
                if (newCost >= visited.GetValueOrDefault(next, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(next, (newCost, priority.tiebreak + graph.Tiebreak(current, next)));
                log.AppendLine($"Put {next}, {(newCost, priority.tiebreak + graph.Tiebreak(current, next))}");
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
        // return;
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
