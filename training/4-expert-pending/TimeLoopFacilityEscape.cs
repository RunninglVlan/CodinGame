using System;
using System.Collections.Generic;
using System.Diagnostics;
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

class Facility : IGraph<Point> {
    static readonly Point[] DIRECTION_PRIORITY = [Screen.UP, Screen.RIGHT, Screen.LEFT, Screen.DOWN];

    readonly Pathfinding<Point> pathfinding;

    readonly int width, height;
    readonly int maxLoops;
    readonly Cell[,] grid;
    Point start, exit;
    readonly List<List<Point>> ghostPaths = [];

    /// maxLoops is for the maximum number of ghosts available (including the final robot)
    public Facility(int width, int height, int maxLoops) {
        pathfinding = new(this);
        this.width = width;
        this.height = height;
        this.maxLoops = maxLoops;
        grid = new Cell[width, height];
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
                grid[x, y] = cell;
                if (cell.Type == CellType.Start) {
                    start = new Point(x, y);
                } else if (cell.Type == CellType.Exit) {
                    exit = new Point(x, y);
                }
            }
        }
    }

    public void ReachExit() {
        var targets = new Stack<char>();
        targets.Push(grid[exit.X, exit.Y].Symbol);
        Debug.Log($"searching {targets.Peek()}");
        while (targets.Count > 0) {
            if (TryReach(targets, out char lockedDoor)) {
                continue;
            }
            char doorSwitch = char.ToLower(lockedDoor);
            targets.Push(doorSwitch);
            Debug.Log($"searching {targets.Peek()}");
        }
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    bool TryReach(Stack<char> targets, out char lockedDoor) {
        var path = pathfinding.Path(start, it => grid[it.X, it.Y].Symbol == targets.Peek());
        Debug.Log($"found {string.Join(", ", path)}", 1);
        lockedDoor = '\0';
        foreach (var point in path) {
            var cell = grid[point.X, point.Y];
            if (cell.Type != CellType.Door || IsUnlocked(cell)) {
                continue;
            }
            lockedDoor = cell.Symbol;
            return false;
        }
        Debug.Log($"reached {targets.Peek()}", 1);
        targets.Pop();
        AddPath(path);
        return true;

        bool IsUnlocked(Cell door) {
            var ghostPositions = ghostPaths.Select(it => it.Last());
            return ghostPositions.Any(it => grid[it.X, it.Y].Symbol == char.ToLower(door.Symbol));
        }
    }

    void AddPath(List<Point> path) {
        if (ghostPaths.Count == maxLoops) {
            throw new InvalidOperationException($"Maximum number of ghosts ({maxLoops}) reached");
        }
        if (ghostPaths.Count > 0 && ghostPaths.Last().Count != path.Count) {
            int maxCount = Math.Max(ghostPaths.Last().Count, path.Count);
            ghostPaths.ForEach(it => PadPath(it, maxCount));
            PadPath(path, maxCount);
        }
        ResolveCollisions(path);
        ghostPaths.Add(path);
    }

    void PadPath(List<Point> path, int length) {
        while (path.Count < length) {
            path.Insert(0, start);
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    void ResolveCollisions(List<Point> path) {
        while (HasCollision(out var collision)) {
            for (int i = collision.i; i < path.Count; i++) {
                if (path[i] != ghostPaths[collision.j][i]) {
                    path.Insert(i, path[i]);
                    ghostPaths.ForEach(it => PadPath(it, path.Count));
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
                for (var j = 0; j < ghostPaths.Count; j++) {
                    if (ghostPaths[j][i] == point) {
                        collision = (i, j);
                        Debug.Log($"found collision at path[{i}], ghostPaths[{j}]: {point}", 1);
                        return true;
                    }
                }
            }
            return false;
        }
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public void PrintActions() {
        foreach (var path in ghostPaths) {
            var actions = path.Zip(path.Prepend(start), (point, previous) => point - previous).Select(Screen.ToChar);
            Console.WriteLine(string.Join(' ', actions));
        }
    }

    IEnumerable<Point> IGraph<Point>.Next(Point current) => Screen.Directions4
            .Select(it => current + it)
            .Where(it => grid[it.X, it.Y].Type is not CellType.Wall)
            .OrderByDescending(it => grid[it.X, it.Y].Type is not CellType.Door)
            .ThenBy(it => Array.IndexOf(DIRECTION_PRIORITY, it - current));
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

    public override string ToString() => $"({X}, {Y})";
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

    /// BFS to endPredicate
    public List<T> Path(T start, Predicate<T> endPredicate) {
        Queue<T> queue = [];
        HashSet<T> visited = [];
        parents.Clear();
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                return Reconstruct(start, current);
            }
            foreach (var next in graph.Next(current)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next] = current;
            }
        }
        throw new UnreachableException("No path found");
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
    IEnumerable<T> Next(T current);
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
