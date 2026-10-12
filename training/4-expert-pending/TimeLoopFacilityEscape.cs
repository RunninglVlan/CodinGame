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
facility.ReachExit();
facility.PrintActions();

record GhostState(List<List<Point>> Paths) : IState<GhostState> {
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

class Facility : IGraph<Point>, IGame<GhostState> {
    static readonly Point[] DIRECTION_PRIORITY = [Screen.UP, Screen.RIGHT, Screen.LEFT, Screen.DOWN];

    readonly Pathfinding<Point> pathfinding;
    readonly StateSearch<GhostState> stateSearch;

    readonly int width, height;
    readonly int maxLoops;
    readonly Dictionary<Point, char> grid = [];
    Point start, exit;
    GhostState exitState = null!;

    public Facility(int width, int height, int maxLoops) {
        pathfinding = new(this);
        stateSearch = new(this, maxLoops);
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

    public void ReachExit() {
        exitState = stateSearch.FindBest(new GhostState([]))!;
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

    IEnumerable<Point> IGraph<Point>.Next(Point current, GhostState state) {
        var unlockedDoors = UnlockedDoors(state);
        return Screen.Directions4
                .Select(it => current + it)
                .Where(it => grid[it] != Cell.WALL)
                .Where(it => !Cell.IsDoor(grid[it]) || unlockedDoors.Contains(grid[it]))
                .OrderBy(it => Array.IndexOf(DIRECTION_PRIORITY, it - current));
    }

    IEnumerable<GhostState> IGame<GhostState>.Next(GhostState current) {
        Debug.Log("searching exit");
        var exitPath = pathfinding.Path(start, exit, current);
        if (exitPath != null) {
            yield return NextState(start, exitPath);
            Debug.Log($"found exit through {GhostState.Actions(exitPath)}", 1);
        }
        foreach (char door in LockedDoors(current)) {
            char doorSwitch = char.ToLower(door);
            var source = maxLoops - current.Paths.Count == 1 ? current.Paths.Last().Last() : start;
            Debug.Log($"searching switch {doorSwitch}");
            var switchPath = pathfinding.Path(source, it => grid[it] == doorSwitch, current);
            if (switchPath == null) {
                continue;
            }
            Debug.Log($"unlocked door {door} through {GhostState.Actions(switchPath)}", 1);
            yield return NextState(source, switchPath);
        }
        yield break;

        GhostState NextState(Point source, List<Point> path) {
            var copy = new GhostState([.. current.Paths.Select(it => it.ToList())]);
            if (source != start) {
                path = [.. copy.Paths.Last(), .. path.Skip(1)];
                copy.Paths.RemoveAt(copy.Paths.Count - 1);
                Debug.Log($"combined: {GhostState.Actions(path)}", 2);
            }
            AddPath(copy, path);
            return copy;
        }
    }

    IEnumerable<char> LockedDoors(GhostState state) {
        var unlockedDoors = UnlockedDoors(state);
        return grid
                .Where(it => Cell.IsDoor(it.Value) && !unlockedDoors.Contains(it.Value))
                .Select(it => it.Value);
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

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly Dictionary<T, T> parents = [];

    /// BFS to end
    public List<T>? Path(T start, T end, GhostState state) {
        return Path(start, it => it.Equals(end), state);
    }

    /// BFS to endPredicate
    public List<T>? Path(T start, Predicate<T> endPredicate, GhostState state) {
        Queue<T> queue = [];
        HashSet<T> visited = [];
        parents.Clear();
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                return Reconstruct(start, current);
            }
            foreach (var next in graph.Next(current, state)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
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
}

interface IGraph<T> {
    IEnumerable<T> Next(T current, GhostState state);
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
