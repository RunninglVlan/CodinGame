using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

// Debug.Enabled = true;
Assert.Throw = true;
// Test.StepEnabled = true;
// Test.From = "09 Tumbler";
// Test.Until = "10 Validator 5";
Test.Run(expected => {
    // Navigate through a facility to reach the extraction point by coordinating robot actions across multiple time loops
    // https://www.codingame.com/contribute/view/1415851ba1aa5d4b0600a53ac02dd35417fd8b
    string[] inputs = Console.ReadLine()!.Split(' ');
    int width = Math.Clamp(int.Parse(inputs[0]), 3, 30);
    int height = Math.Clamp(int.Parse(inputs[1]), 3, 30);
    int maxLoops = Math.Clamp(int.Parse(inputs[2]), 1, 5);
    var facility = new Facility(width, height, maxLoops);
    facility.ReadGrid();
    facility.BuildConnections();
    Assert.ActualEqualsExpected(facility.ReachExit().ToString(), expected());
    Perf.Report("block");
});

record GhostState(List<List<Point>> Loops, List<char> Targets) : IState<GhostState> {
    public string Steps { get; private set; } = string.Empty;

    public GhostState Copy() {
        return new GhostState([.. Loops.Select(it => it.ToList())], [.. Targets]) {
            Steps = Steps
        };
    }

    public void AddTarget(char value) {
        Targets.Add(value);
        Steps += $"+{value}";
    }

    public void RemoveTarget(int index) {
        char target = Targets[index];
        Targets.RemoveAt(index);
        Steps += $"-{target}";
    }

    bool IState<GhostState>.IsBetterThan(GhostState other) {
        var actions = Actions();
        var otherActions = other.Actions();
        // Debug.Log("choosing best between this:", 1);
        // Debug.Log(this);
        // Debug.Log("and other:", 1);
        // Debug.Log(other);
        int turns = Loops.First().Count.CompareTo(other.Loops.First().Count);
        if (turns != 0) {
            return turns < 0;
        }
        int loops = Loops.Count.CompareTo(other.Loops.Count);
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

    List<string> Actions() => [.. Loops.Select(Actions)];

    public static string Actions(List<Point> loop) {
        var actions = loop.Skip(1).Zip(loop, (point, previous) => point - previous).Select(Screen.ToChar);
        return string.Join(' ', actions);
    }

    public virtual bool Equals(GhostState? other) {
        if (other == null) {
            return false;
        }
        return Targets.SequenceEqual(other.Targets)
                && Loops.Count == other.Loops.Count
                && Loops.Zip(other.Loops).All(it => it.First.SequenceEqual(it.Second));
    }

    public override int GetHashCode() {
        var hashCode = new HashCode();
        foreach (char target in Targets) {
            hashCode.Add(target);
        }
        hashCode.Add(Loops.Count);
        foreach (var loop in Loops) {
            hashCode.Add(loop.Count);
            foreach (var point in loop) {
                hashCode.Add(point);
            }
        }
        return hashCode.ToHashCode();
    }

    public override string ToString() {
        return string.Join("\n", Loops.Select(Actions));
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
    readonly Dictionary<char, List<List<Node>>> pathsToTargets = [];

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

    public GhostState ReachExit() {
        var startState = new GhostState([], []);
        startState.AddTarget(grid[exit]);
        return stateSearch.FindBest(startState)!;
    }

    bool AddLoop(GhostState state, List<Point> loop) {
        RemoveDuplicate(state, loop);
        if (state.Loops.Count > 0 && state.Loops.Last().Count != loop.Count) {
            int maxCount = Math.Max(state.Loops.Last().Count, loop.Count);
            state.Loops.ForEach(it => PadLoop(it, maxCount));
            PadLoop(loop, maxCount);
        }
        if (!AvoidCollisions(state, loop)) {
            return false;
        }
        state.Loops.Add(loop);
        return true;
    }

    /// Removes an existing loop if the new loop contains all of its switches and more.
    /// Ignoring switches that have connected doors in a loop,
    /// as robots can't unlock and go through doors in their loop.
    [SuppressMessage("ReSharper", "InvertIf")]
    void RemoveDuplicate(GhostState state, List<Point> newLoop) {
        return;
        var switches = Switches(newLoop);
        for (int i = state.Loops.Count - 1; i >= 0; i--) {
            var existingSwitches = Switches(state.Loops[i]);
            if (switches.Count > existingSwitches.Count && existingSwitches.All(switches.Contains)) {
                Debug.Log($"removed duplicate: {GhostState.Actions(state.Loops[i])}", 2);
                state.Loops.RemoveAt(i);
                break;
            }
        }
        return;

        List<char> Switches(List<Point> loop) {
            var cells = loop
                    .Select(it => grid[it])
                    .Where(it => Cell.IsSwitch(it) || Cell.IsDoor(it))
                    .ToList();
            for (int i = cells.Count - 1; i >= 0; i--) {
                char cell = cells[i];
                if (!Cell.IsSwitch(cell)) {
                    continue;
                }
                if (cells.Any(it => Cell.IsDoor(it) && it == Cell.ToDoor(cell))) {
                    cells.RemoveAt(i);
                }
            }
            return [.. cells.Where(Cell.IsSwitch)];
        }
    }

    void PadLoop(List<Point> loop, int length) {
        while (loop.Count < length) {
            loop.Insert(0, start);
        }
    }

    /// Simple collision avoidance, checks only one collision and tries to resolve it.
    /// If it can't, it fails
    [SuppressMessage("ReSharper", "InvertIf")]
    bool AvoidCollisions(GhostState state, List<Point> loop) {
        if (HasCollision(out var collision, loop, state)) {
            for (int i = collision.i; i < loop.Count; i++) {
                if (loop[i] != state.Loops[collision.j][i]) {
                    loop.Insert(i, loop[i]);
                    state.Loops.ForEach(it => PadLoop(it, loop.Count));
                    break;
                }
            }
        }
        return !HasCollision(out _, loop, state);
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    bool HasCollision(out (int i, int j) collision, List<Point> loop, GhostState state) {
        collision = default;
        for (var i = 0; i < loop.Count; i++) {
            var point = loop[i];
            if (point == start) {
                continue;
            }
            for (var j = 0; j < state.Loops.Count; j++) {
                if (state.Loops[j][i] == point) {
                    collision = (i, j);
                    Debug.Log($"found collision in ghostLoops[{j}] of state", 1);
                    Debug.Log(state);
                    Debug.Log($"with loop[{i}]: {GhostState.Actions(loop)}", 1);
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerable<Point> Next(Point current, GhostState state) {
        var unlockedDoors = UnlockedDoors(state);
        return Screen.Directions4
                .Select(it => current + it)
                .Where(it => grid[it] != Cell.WALL)
                .Where(it => !Cell.IsDoor(grid[it]) || unlockedDoors.Contains(grid[it]))
                .OrderBy(it => Array.IndexOf(DIRECTION_PRIORITY, it - current));
    }

    IEnumerable<GhostState> IGame<GhostState>.Next(GhostState current, int depth) {
        if (current.Loops.Count == maxLoops) {
            yield break;
        }
        var targets = current.Targets.ToList();
        int startIndex = current.Targets.Count == 1 ? 0 : 1;
        for (int i = current.Targets.Count - 1; i >= startIndex; i--) {
            var found = FindTarget(current, i, depth);
            if (found == null) {
                continue;
            }
            targets.Remove(current.Targets[i]);
            yield return found;
            var foundAndCombined = FindTargetAndCombine(current, i, depth);
            if (foundAndCombined != null) {
                yield return foundAndCombined;
            }
        }
        var unlockedDoors = UnlockedDoors(current);
        var lockedDoors = grid.Values.Where(it => Cell.IsDoor(it) && !unlockedDoors.Contains(it)).ToHashSet();
        // todo: refactor
        if (targets.Count > 0) {
            char unreachableTarget = targets.Last();
            foreach (var path in PathsTo(unreachableTarget, depth)) {
                var doors = path.Skip(1).SkipLast(1)
                        .Select(it => it.Id)
                        .Where(it => !unlockedDoors.Contains(it))
                        .Where(it => !current.Targets.Contains(Cell.ToSwitch(it)))
                        .ToList();
                foreach (var doorOrders in Permutations(doors)) {
                    var pathState = current.Copy();
                    foreach (char door in doorOrders) {
                        lockedDoors.Remove(door);
                        pathState.AddTarget(Cell.ToSwitch(door));
                    }
                    yield return pathState;
                }
            }
        }
        var moreDoors = lockedDoors.Where(it => !current.Targets.Contains(Cell.ToSwitch(it))).ToList();
        if (moreDoors.Count > 0) {
            Debug.Log($"trying to unlock remaining doors: {string.Join(", ", moreDoors)} (depth {depth})", depth + 1);
        }
        foreach (char door in moreDoors) {
            var doorState = current.Copy();
            doorState.AddTarget(Cell.ToSwitch(door));
            yield return doorState;
        }
    }

    GhostState? FindTarget(GhostState state, int index, int depth) {
        char target = state.Targets[index];
        Debug.Log($"searching {target} (depth {depth})", depth);
        var loop = Pathfinding.Shortest(start, it => grid[it] == target, it => Next(it, state));
        if (loop == null) {
            return null;
        }
        var copy = state.Copy();
        Debug.Log($"found {target} using {GhostState.Actions(loop)} (depth {depth})", depth + 1, ConsoleColor.Blue);
        if (!AddLoop(copy, loop)) {
            return null;
        }
        copy.RemoveTarget(index);
        return copy;
    }

    GhostState? FindTargetAndCombine(GhostState state, int index, int depth) {
        if (state.Loops.Count <= 0) {
            return null;
        }
        var source = state.Loops.Last().Last();
        char target = state.Targets[index];
        var loop = Pathfinding.Shortest(source, it => grid[it] == target, it => Next(it, state))!;
        if (loop.Any(it => grid[it] == Cell.ToDoor(grid[source]))) {
            return null;
        }
        var copy = state.Copy();
        Debug.Log($"found {target} from {grid[source]} using {GhostState.Actions(loop)} (depth {depth})", depth + 1, ConsoleColor.Blue);
        loop = [.. copy.Loops.Last(), .. loop.Skip(1)];
        Debug.Log($"combined: {GhostState.Actions(loop)} (depth {depth})", depth + 2, ConsoleColor.Blue);
        copy.Loops.RemoveAt(copy.Loops.Count - 1);
        if (!AddLoop(copy, loop)) {
            return null;
        }
        copy.RemoveTarget(index);
        return copy;
    }

    static IEnumerable<List<T>> Permutations<T>(List<T> items) {
        if (items.Count <= 1) {
            yield return items;
            yield break;
        }
        for (var i = 0; i < items.Count; i++) {
            var rest = items.Where((_, index) => index != i).ToList();
            foreach (var permutation in Permutations(rest)) {
                yield return [.. permutation.Prepend(items[i])];
            }
        }
    }

    HashSet<char> UnlockedDoors(GhostState state) {
        var unlockedDoors = state.Loops
                .SelectMany(it => it)
                .Select(it => grid[it])
                .Where(Cell.IsSwitch)
                .Select(Cell.ToDoor);
        return [.. unlockedDoors];
    }

    List<List<Node>> PathsTo(char target, int depth) {
        if (pathsToTargets.TryGetValue(target, out var paths)) {
            return paths;
        }
        Debug.Log($"can't find {target}, paths that lead there (depth {depth}):", depth + 1);
        var pathsToTarget = Pathfinding.All(graph[Cell.START], graph[target], it => {
                    return it.Connections
                            .Select(id => graph[id])
                            .Where(node => Cell.IsDoor(node.Id) && node.Id != Cell.ToDoor(target) || node == graph[target]);
                })
                .ToList();
        long t = Perf.Start();
        // todo: sets can be created once
        pathsToTarget = [.. pathsToTarget.OrderBy(it => it.Count)];
        for (var i = 0; i < pathsToTarget.Count; i++) {
            var shorter = pathsToTarget[i].ToHashSet();
            for (int j = pathsToTarget.Count - 1; j > i; j--) {
                if (pathsToTarget[j].ToHashSet().IsSupersetOf(shorter)) {
                    pathsToTarget.RemoveAt(j);
                }
            }
        }
        Perf.Stop(t);
        foreach (var path in pathsToTarget) {
            Debug.Log(string.Join('-', path.Select(it => grid[it.Point])), depth + 2);
        }
        return pathsToTargets[target] = pathsToTarget;
    }

    bool IGame<GhostState>.HasReachedEnd(GhostState state) {
        return state.Loops.Count > 0 && exit == state.Loops.Last().Last();
    }
}

static class Cell {
    public const char START = 'S';
    public const char EXIT = 'E';
    public const char WALL = '#';
    public const char FLOOR = '.';

    public static bool IsSwitch(char c) => char.IsLower(c);
    public static bool IsDoor(char c) => c is not (START or EXIT) && char.IsUpper(c);
    public static char ToSwitch(char c) => char.ToLower(c);
    public static char ToDoor(char c) => char.ToUpper(c);
}

record Node(char Id, Point Point) {
    public HashSet<char> Connections { get; } = [];

    public virtual bool Equals(Node? other) => Id == other?.Id;
    public override int GetHashCode() => Id;
}

readonly record struct Point(int X, int Y) {
    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

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

class StateSearch<T>(IGame<T> game, int depthMax) where T : IState<T>, IEquatable<T> {
    readonly HashSet<T> visited = [];

    /// DFS/Backtracking
    public T? FindBest(T start) {
        visited.Clear();
        visited.Add(start);
        return FindBest(start, 0);
    }

    T? FindBest(T current, int depth) {
        if (game.HasReachedEnd(current)) {
            return current;
        }
        if (depth > depthMax) {
            return default;
        }
        T? best = default;
        foreach (var next in game.Next(current, depth)) {
            if (!visited.Add(next)) {
                continue;
            }
            Debug.Log($"next: {next.Steps} (depth {depth})", depth);
            Debug.Log(next);
            Test.Step();
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
    IEnumerable<TState> Next(TState current, int depth);
    bool HasReachedEnd(TState state);
}

interface IState<in T> where T : IState<T> {
    public string Steps { get; }
    bool IsBetterThan(T other);
}

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; }

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        if (line != null) Test.Log(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        if (!Enabled) {
            return;
        }
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        var log = $"{padding}{line}";
        Console.Error.WriteLine(log);
        Test.Log(log);
        Console.ResetColor();
    }
}

static class Test {
    const string CASES_FILE = "../Test.Cases.txt";
    const string LOG_FILE = "../Test.Log.txt";

    static readonly TextReader consoleIn = Console.In;
    public static bool StepEnabled { get; set; }
    public static string? From { get; set; }
    public static string? Until { get; set; }

    public static void Run(Action<Func<string>> action) {
        File.WriteAllText(LOG_FILE, null);
        string cases = File.ReadAllText(CASES_FILE);
        if (From != null) {
            cases = cases[cases.IndexOf(From, StringComparison.Ordinal)..];
        }
        if (Until != null) {
            cases = cases[..cases.IndexOf(Until, StringComparison.Ordinal)];
        }
        Console.SetIn(new StringReader(cases));
        var totalTime = 0L;
        while (Console.In.Peek() != -1) {
            var stopwatch = Stopwatch.StartNew();
            string? name = Console.ReadLine();
            Log(name, console: true, color: ConsoleColor.Cyan);
            try {
                var expected = ReadUntilNewLine;
                action(expected);
            }
            finally {
                long time = stopwatch.ElapsedMilliseconds;
                totalTime += time;
                Log($"Test took {time}ms", console: true);
            }
            Log(console: true);
        }
        Log($"All tests took {totalTime}ms", console: true);
        return;

        static string ReadUntilNewLine() {
            List<string> lines = [];
            for (string line; !string.IsNullOrEmpty(line = Console.ReadLine()!);) {
                lines.Add(line);
            }
            return string.Join(Environment.NewLine, lines);
        }
    }

    public static void Step() {
        if (!StepEnabled) {
            return;
        }
        var current = Console.In;
        Console.SetIn(consoleIn);
        Console.ReadLine();
        Console.SetIn(current);
    }

    public static void Log(object? line = null, bool console = false, ConsoleColor? color = null) {
        File.AppendAllText(LOG_FILE, $"{line}{Environment.NewLine}");
        if (!console) {
            return;
        }
        if (color != null) {
            Console.ForegroundColor = color.Value;
        }
        Console.Error.WriteLine(line);
        Console.ResetColor();
    }
}

static class Assert {
    public static bool Throw { get; set; }

    public static void ActualEqualsExpected(object actual, object expected) {
        bool equals = Equals(actual, expected);
        Console.WriteLine(equals ? $"✅ {actual}" : $"❌ found {actual}, but expected {expected}");
        if (!equals && Throw) {
            throw new Exception();
        }
    }
}

static class Perf {
    static long ticks, calls;

    public static long Start() => Stopwatch.GetTimestamp();

    public static void Stop(long start) {
        ticks += Stopwatch.GetTimestamp() - start;
        calls++;
    }

    public static void Report(string name) {
        var total = Stopwatch.GetElapsedTime(0, ticks);
        Console.Error.WriteLine(
                $"{name}: {total.TotalMilliseconds:F1}ms total, {calls} calls, " +
                $"{total.TotalMilliseconds * 1000 / calls:F2}µs/call");
    }
}
