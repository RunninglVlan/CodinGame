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
        return Paths.Count < other.Paths.Count;
    }
}

class Facility : IGraph<Point>, IGame<GhostState> {
    static readonly Point[] DIRECTION_PRIORITY = [Screen.UP, Screen.RIGHT, Screen.LEFT, Screen.DOWN];

    readonly Pathfinding<Point> pathfinding;
    readonly StateSearch<GhostState> stateSearch;

    readonly int width, height;
    readonly int maxLoops;
    readonly Dictionary<Point, Cell> grid = [];
    Point start, exit;
    GhostState exitState = null!;

    /// maxLoops is for the maximum number of ghosts available (including the final robot)
    public Facility(int width, int height, int maxLoops) {
        pathfinding = new(this);
        stateSearch = new(this, 10);
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
                var cell = symbol switch {
                    'S' => new Cell(CellType.Start, symbol),
                    'E' => new Cell(CellType.Exit, symbol),
                    '#' => new Cell(CellType.Wall, symbol),
                    '.' => new Cell(CellType.Floor, symbol),
                    _ when char.IsLower(symbol) => new Cell(CellType.Switch, symbol),
                    _ => new Cell(CellType.Door, symbol)
                };
                var point = new Point(x, y);
                grid[point] = cell;
                if (cell.Type == CellType.Start) {
                    start = point;
                } else if (cell.Type == CellType.Exit) {
                    exit = point;
                }
            }
        }
    }

    public void ReachExit() {
        exitState = stateSearch.FindBest(new GhostState([]))!;
    }

    /*
    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    bool TryReach(Stack<char> targets, out char lockedDoor) {
        char target = targets.Peek();
        Debug.Log($"searching {target}");
        var source = target != grid[exit].Symbol && maxLoops - ghostPaths.Count == 1
                ? ghostPaths.Last().Last()
                : start;
        var path = pathfinding.Path(source, it => grid[it].Symbol == target);
        lockedDoor = '\0';
        foreach (var point in path) {
            var cell = grid[point];
            if (cell.Type != CellType.Door || IsUnlocked(cell)) {
                continue;
            }
            Debug.Log($"it's behind a door: {cell.Symbol}", 1);
            lockedDoor = cell.Symbol;
            return false;
        }
        Debug.Log($"reached {target} through {Actions(path)}", 1);
        targets.Pop();
        if (source == start) {
            AddPath(path);
        } else {
            ghostPaths.Last().AddRange(path);
            Debug.Log($"combined: {Actions(ghostPaths.Last())}", 2);
        }
        return true;
    }

    bool IsUnlocked(Cell door) {
        return ghostPaths.SelectMany(it => it).Any(it => grid[it].Symbol == char.ToLower(door.Symbol));
    }
    */

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
                Debug.Log($"removed duplicate: {Actions(state.Paths[i])}", 2);
                state.Paths.RemoveAt(i);
                break;
            }
        }
        return;

        List<char> Switches(List<Point> path) {
            var cells = path
                    .Select(it => grid[it])
                    .Where(it => it.Type is CellType.Switch or CellType.Door)
                    .ToList();
            for (int i = cells.Count - 1; i >= 0; i--) {
                var cell = cells[i];
                if (cell.Type != CellType.Switch) {
                    continue;
                }
                if (cells.Any(it => it.Type == CellType.Door && it.Symbol == char.ToUpper(cell.Symbol))) {
                    cells.RemoveAt(i);
                }
            }
            return [.. cells.Where(it => it.Type == CellType.Switch).Select(it => it.Symbol)];
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
        var listOfActions = exitState.Paths.Select(Actions).ToList();
        string finalActions = listOfActions.Last();
        foreach (string actions in listOfActions.SkipLast(1).OrderByDescending(it => it)) {
            Console.WriteLine(actions);
        }
        Console.WriteLine(finalActions);
    }

    string Actions(List<Point> path) {
        var actions = path.Zip(path.Prepend(start), (point, previous) => point - previous).Select(Screen.ToChar);
        return string.Join(' ', actions);
    }

    IEnumerable<Point> IGraph<Point>.Next(Point current, GhostState state) {
        var unlockedDoors = UnlockedDoors(state);
        return Screen.Directions4
                .Select(it => current + it)
                .Where(it => grid[it].Type is not CellType.Wall)
                .Where(it => grid[it].Type is not CellType.Door || unlockedDoors.Contains(grid[it].Symbol))
                .OrderBy(it => Array.IndexOf(DIRECTION_PRIORITY, it - current));
    }

    IEnumerable<GhostState> IGame<GhostState>.Next(GhostState current) {
        var exitPath = pathfinding.Path(start, exit, current);
        if (exitPath != null) {
            yield return CopyWith(exitPath);
            Debug.Log("found exit", 1);
            yield break;
        }
        foreach (var door in LockedDoors(current)) {
            char doorSwitch = char.ToLower(door.Symbol);
            var switchPath = pathfinding.Path(start, it => grid[it].Symbol == doorSwitch, current);
            if (switchPath == null) {
                continue;
            }
            Debug.Log($"unlocked door {door.Symbol}", 1);
            yield return CopyWith(switchPath);
        }
        yield break;

        GhostState CopyWith(List<Point> path) {
            var copy = new GhostState([.. current.Paths.Select(it => it.ToList())]);
            AddPath(copy, path);
            return copy;
        }

    }

    IEnumerable<Cell> LockedDoors(GhostState state) {
        var unlockedDoors = UnlockedDoors(state);
        return grid
                .Where(it => it.Value.Type is CellType.Door && !unlockedDoors.Contains(it.Value.Symbol))
                .Select(it => it.Value);
    }

    HashSet<char> UnlockedDoors(GhostState state) {
        var unlockedDoors = state.Paths
                .SelectMany(it => it)
                .Select(it => grid[it])
                .Where(it => it.Type is CellType.Switch)
                .Select(it => char.ToUpper(it.Symbol));
        return [.. unlockedDoors];
    }

    bool IGame<GhostState>.HasReachedEnd(GhostState state) {
        return state.Paths.Count > 0 && exit == state.Paths.Last().Last();
    }
}

readonly record struct Cell(CellType Type, char Symbol);

enum CellType {
    Start,
    Exit,
    Wall,
    Floor,
    Switch,
    Door
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
        if (game.HasReachedEnd(current) || depth > depthMax) {
            return current;
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
