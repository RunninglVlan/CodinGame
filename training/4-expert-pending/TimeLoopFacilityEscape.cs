using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// Navigate through a facility to reach the extraction point by coordinating robot actions across multiple time loops
// https://www.codingame.com/contribute/view/1415851ba1aa5d4b0600a53ac02dd35417fd8b
string[] inputs = Console.ReadLine()!.Split(' ');
int width = Math.Clamp(int.Parse(inputs[0]), 3, 30);
int height = Math.Clamp(int.Parse(inputs[1]), 3, 30);
int maxLoops = Math.Clamp(int.Parse(inputs[2]), 1, 5);
var facility = new Facility(width, height, maxLoops);
facility.ReadGrid();
facility.BuildConnections();
facility.ReachExit();
facility.PrintActions();

record GhostState(List<List<Point>> Paths, Stack<char> Targets) : IState<GhostState> {
    bool IState<GhostState>.IsBetterThan(GhostState other) {
        var actions = Actions();
        var otherActions = other.Actions();
        Debug.Log("choosing best between this:");
        foreach (string action in actions) {
            Debug.Log(action, 1);
        }
        Debug.Log("and other:");
        foreach (string action in otherActions) {
            Debug.Log(action, 1);
        }
        int turns = Paths.First().Count.CompareTo(other.Paths.First().Count);
        if (turns != 0) {
            return turns < 0;
        }
        int loops = Paths.Count.CompareTo(other.Paths.Count);
        if (loops != 0) {
            return loops < 0;
        }
        for (var i = 0; i < actions.Count; i++) {
            if (actions[i] == otherActions[i]) {
                continue;
            }
            return string.Compare(actions[i], otherActions[i], StringComparison.Ordinal) > 0;
        }
        return false;
    }

    public List<string> Actions() => [.. Paths.Select(Actions)];

    public static string Actions(List<Point> path) {
        var actions = path.Skip(1).Zip(path, (point, previous) => point - previous).Select(Screen.ToChar);
        return string.Join(' ', actions);
    }
}

class Facility : IGame<GhostState> {
    static readonly Point[] DIRECTION_PRIORITY = [Screen.UP, Screen.RIGHT, Screen.LEFT, Screen.DOWN];

    readonly StateSearch<GhostState> stateSearch;

    readonly int width, height;
    readonly int maxLoops;
    readonly Dictionary<Point, char> grid = [];
    readonly Dictionary<char, Node> graph = [];
    Point start, exit;
    GhostState exitState = null!;

    public Facility(int width, int height, int maxLoops) {
        stateSearch = new(this, maxLoops * 2);
        this.width = width;
        this.height = height;
        this.maxLoops = maxLoops;
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public void ReadGrid() {
        for (var y = 0; y < height; y++) {
            string row = Debug.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char symbol = row[x];
                var point = new Point(x, y);
                grid[point] = symbol;
                if (symbol == Cell.START) {
                    start = point;
                } else if (symbol == Cell.EXIT) {
                    exit = point;
                }
            }
        }
    }

    public void BuildConnections() {
        foreach ((var point, char cell) in grid) {
            if (cell is not (Cell.WALL or Cell.FLOOR)) {
                graph.Add(cell, new Node(cell, point));
            }
        }
        foreach ((char idI, var nodeI) in graph) {
            foreach ((char idJ, var nodeJ) in graph) {
                if (idI == idJ) {
                    continue;
                }
                var path = Pathfinding.Shortest(nodeI.Point, nodeJ.Point, current => Screen.Directions4
                        .Select(it => current + it)
                        .Where(it => it == nodeJ.Point || grid[it] != Cell.WALL && !Cell.IsDoor(grid[it])));
                if (path == null) {
                    continue;
                }
                nodeI.Connections.Add(nodeJ.Id);
                nodeJ.Connections.Add(nodeI.Id);
            }
        }
    }

    public void ReachExit() {
        var targets = new Stack<char>();
        targets.Push(grid[exit]);
        exitState = stateSearch.FindBest(new GhostState([], targets))!;
    }

    void AddPath(GhostState state, List<Point> path) {
        RemoveDuplicate(state, path);
        if (state.Paths.Count > 0 && state.Paths.Last().Count != path.Count) {
            int maxCount = Math.Max(state.Paths.Last().Count, path.Count);
            state.Paths.ForEach(it => PadPath(it, maxCount));
            PadPath(path, maxCount);
        }
        AvoidCollisions(state, path);
        state.Paths.Add(path);
    }

    /// Removes an existing path if the new path has all of its switches and more.
    /// Ignoring switches that have connected doors in the path,
    /// as robots can't unlock and go through doors in their loop.
    [SuppressMessage("ReSharper", "InvertIf")]
    void RemoveDuplicate(GhostState state, List<Point> newPath) {
        var switches = Switches(newPath);
        for (int i = state.Paths.Count - 1; i >= 0; i--) {
            var existingSwitches = Switches(state.Paths[i]);
            if (switches.Count > existingSwitches.Count && existingSwitches.All(switches.Contains)) {
                Debug.Log($"removed duplicate: {GhostState.Actions(state.Paths[i])}", 2);
                state.Paths.RemoveAt(i);
                break;
            }
        }
        return;

        List<char> Switches(List<Point> path) {
            var cells = path
                    .Select(it => grid[it])
                    .Where(it => Cell.IsSwitch(it) || Cell.IsDoor(it))
                    .ToList();
            for (int i = cells.Count - 1; i >= 0; i--) {
                char cell = cells[i];
                if (!Cell.IsSwitch(cell)) {
                    continue;
                }
                if (cells.Any(it => Cell.IsDoor(it) && it == char.ToUpper(cell))) {
                    cells.RemoveAt(i);
                }
            }
            return [.. cells.Where(Cell.IsSwitch)];
        }
    }

    void PadPath(List<Point> path, int length) {
        while (path.Count < length) {
            path.Insert(0, start);
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    void AvoidCollisions(GhostState state, List<Point> path) {
        while (HasCollision(out var collision)) {
            for (int i = collision.i; i < path.Count; i++) {
                if (path[i] != state.Paths[collision.j][i]) {
                    path.Insert(i, path[i]);
                    state.Paths.ForEach(it => PadPath(it, path.Count));
                    break;
                }
            }
        }
        return;

        bool HasCollision(out (int i, int j) collision) {
            collision = default;
            for (var i = 0; i < path.Count; i++) {
                var point = path[i];
                if (point == start) {
                    continue;
                }
                for (var j = 0; j < state.Paths.Count; j++) {
                    if (state.Paths[j][i] == point) {
                        collision = (i, j);
                        Debug.Log($"found collision at path[{i}], ghostPaths[{j}]: {point}", 1);
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public void PrintActions() {
        foreach (string actions in exitState.Actions()) {
            Console.WriteLine(actions);
        }
    }

    IEnumerable<Point> Next(Point current, GhostState state) {
        var unlockedDoors = UnlockedDoors(state);
        return Screen.Directions4
                .Select(it => current + it)
                .Where(it => grid[it] != Cell.WALL)
                .Where(it => !Cell.IsDoor(grid[it]) || unlockedDoors.Contains(grid[it]))
                .OrderBy(it => Array.IndexOf(DIRECTION_PRIORITY, it - current));
    }

    IEnumerable<GhostState> IGame<GhostState>.Next(GhostState current) {
        Func<Point, IEnumerable<Point>> next = it => Next(it, current);
        if (!current.Targets.TryPeek(out char target)) {
            yield break;
        }
        Debug.Log($"searching {target}");
        if (current.Paths.Count > 0) {
            var source = current.Paths.Last().Last();
            var path = Pathfinding.Shortest(source, it => grid[it] == target, next);
            if (path != null && path.All(it => grid[it] != char.ToUpper(grid[source]))) {
                var copy = NextState();
                Debug.Log($"found {target} from {grid[source]} through {GhostState.Actions(path)}", 1);
                path = [.. copy.Paths.Last(), .. path.Skip(1)];
                Debug.Log($"combined: {GhostState.Actions(path)}", 2);
                copy.Paths.RemoveAt(copy.Paths.Count - 1);
                AddPath(copy, path);
                copy.Targets.Pop();
                yield return copy;
            }
        }
        {
            var path = Pathfinding.Shortest(start, it => grid[it] == target, next);
            if (path != null) {
                var copy = NextState();
                AddPath(copy, path);
                copy.Targets.Pop();
                Debug.Log($"found {target} through {GhostState.Actions(path)}", 1);
                yield return copy;
                yield break;
            }
        }
        Debug.Log($"can't find {target}", 1);
        var pathsToTarget = Pathfinding.All(graph[Cell.START], graph[target], it => {
                    return it.Connections
                            .Select(id => graph[id])
                            .Where(node => Cell.IsDoor(grid[node.Point]) || node == graph[target]);
                })
                .ToArray();
        Debug.Log($"shortest paths to {target}:");
        int shortestSteps = pathsToTarget.Min(it => it.Count);
        foreach (var nodes in pathsToTarget.Where(it => it.Count == shortestSteps)) {
            Debug.Log(string.Join(", ", nodes.Select(it => it.Id)), 1);
        }
        var unlockedDoors = UnlockedDoors(current);
        var lockedDoors = pathsToTarget
                .Where(it => it.Count == shortestSteps)
                .Select(it => it.Skip(1).First(node => !unlockedDoors.Contains(node.Id)).Id);
        foreach (char door in lockedDoors) {
            var copy = NextState();
            char doorSwitch = char.ToLower(door);
            copy.Targets.Push(doorSwitch);
            yield return copy;
        }
        yield break;

        GhostState NextState() {
            char[] currentTargets = [.. current.Targets];
            Array.Reverse(currentTargets);
            return new GhostState([.. current.Paths.Select(it => it.ToList())], new Stack<char>(currentTargets));
        }
    }

    HashSet<char> UnlockedDoors(GhostState state) {
        var unlockedDoors = state.Paths
                .SelectMany(it => it)
                .Select(it => grid[it])
                .Where(Cell.IsSwitch)
                .Select(char.ToUpper);
        return [.. unlockedDoors];
    }

    bool IGame<GhostState>.HasReachedEnd(GhostState state) {
        return state.Paths.Count > 0 && exit == state.Paths.Last().Last();
    }
}

static class Cell {
    public const char START = 'S';
    public const char EXIT = 'E';
    public const char WALL = '#';
    public const char FLOOR = '.';

    public static bool IsSwitch(char c) => char.IsLower(c);
    public static bool IsDoor(char c) => c is not (START or EXIT) && char.IsUpper(c);
}

record Node(char Id, Point Point) {
    public HashSet<char> Connections { get; } = [];

    public virtual bool Equals(Node? other) => Id == other?.Id;
    public override int GetHashCode() => Id;
}

readonly record struct Point(int X, int Y) {
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"{X},{Y}";
}

static class Screen {
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static char ToChar(Point direction) => direction switch {
        _ when direction == UP => 'U',
        _ when direction == DOWN => 'D',
        _ when direction == LEFT => 'L',
        _ when direction == RIGHT => 'R',
        _ => 'W'
    };
}

static class Pathfinding {
    /// BFS to end
    public static List<T>? Shortest<T>(T start, T end, Func<T, IEnumerable<T>> getNext) where T : IEquatable<T> {
        return Shortest(start, it => it.Equals(end), getNext);
    }

    /// BFS to endPredicate
    public static List<T>? Shortest<T>(T start, Predicate<T> endPredicate, Func<T, IEnumerable<T>> getNext)
            where T : IEquatable<T> {
        Dictionary<T, T> parents = [];
        Queue<T> queue = [];
        HashSet<T> visited = [];
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                return Reconstruct(parents, start, current);
            }
            foreach (var next in getNext(current)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next] = current;
            }
        }
        return null;
    }

    static List<T> Reconstruct<T>(Dictionary<T, T> parents, T start, T end) where T : IEquatable<T> {
        var path = new List<T>();
        var current = end;
        while (!current.Equals(start)) {
            path.Add(current);
            current = parents[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }

    /// DFS/Backtracking
    public static IEnumerable<List<T>> All<T>(T start, T end, Func<T, IEnumerable<T>> getNext)
            where T : IEquatable<T> {
        return All(start, end, getNext, [start], [start]);
    }

    static IEnumerable<List<T>> All<T>(T current, T end,
            Func<T, IEnumerable<T>> getNext, List<T> path, HashSet<T> visited) where T : IEquatable<T> {
        if (current.Equals(end)) {
            yield return [.. path];
            yield break;
        }
        foreach (var next in getNext(current)) {
            if (visited.Contains(next)) {
                continue;
            }
            foreach (var result in All(next, end, getNext, [.. path, next], [.. visited, next])) {
                yield return result;
            }
        }
    }
}

class StateSearch<T>(IGame<T> game, int depthMax) where T : IState<T> {
    /// DFS/Backtracking
    public T? FindBest(T current, int depth = 0) {
        if (game.HasReachedEnd(current)) {
            return current;
        }
        if (depth > depthMax) {
            return default;
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
