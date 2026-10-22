using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

Debug.Enabled = true;
// https://www.codingame.com/contribute/view/152541078689bfa05bb36d9956b0461f4c5e61
var maze = new Maze(7);
maze.Read();
char pushableTile = Console.ReadLine()!.First();
maze.FindPath(pushableTile);
Console.WriteLine(maze);

class Maze(int size) {
    readonly Dictionary<Point, Cell> cells = [];
    Point player, treasure;
    HashSet<Point> foundPath = null!;

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public void Read() {
        for (var y = 0; y < size; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                char symbol = row[x];
                var cell = Cell.From(symbol);
                var point = new Point(x, y);
                cells[point] = cell;
                if (cell.Type == CellType.Player) {
                    player = point;
                } else if (cell.Type == CellType.Treasure) {
                    treasure = point;
                }
            }
        }
    }

    public void FindPath(char pushableTile) {
        var path = Pathfinding.Shortest(player, treasure, Next);
        if (path != null) {
            foundPath = [.. path];
        }
    }

    IEnumerable<Point> Next(Point current) {
        var currentCell = cells[current];
        foreach (var direction in Screen.Directions4) {
            var next = current + direction;
            if (!next.InBounds(size)) {
                continue;
            }
            if (CanExit(currentCell.Type, direction) && CanEnter(cells[next].Type, direction)) {
                yield return next;
            }
        }
    }

    static bool CanExit(CellType current, Point direction) => current switch {
        CellType.Vertical => direction == Screen.UP || direction == Screen.DOWN,
        CellType.Horizontal => direction == Screen.LEFT || direction == Screen.RIGHT,
        CellType.Left => direction == Screen.LEFT,
        CellType.Right => direction == Screen.RIGHT,
        CellType.Up => direction == Screen.UP,
        CellType.Down => direction == Screen.DOWN,
        _ => true
    };

    static bool CanEnter(CellType next, Point direction) => next switch {
        CellType.Vertical => direction == Screen.UP || direction == Screen.DOWN,
        CellType.Horizontal => direction == Screen.LEFT || direction == Screen.RIGHT,
        CellType.Left => direction != Screen.RIGHT,
        CellType.Right => direction != Screen.LEFT,
        CellType.Up => direction != Screen.DOWN,
        CellType.Down => direction != Screen.UP,
        CellType.Blocked => false,
        _ => true
    };

    public override string ToString() {
        var rows = new StringBuilder();
        for (var y = 0; y < size; y++) {
            for (var x = 0; x < size; x++) {
                var point = new Point(x, y);
                rows.Append(foundPath.Contains(point) ? cells[point].Symbol : Cell.BLOCKED);
            }
            if (y < size - 1) {
                rows.AppendLine();
            }
        }
        return rows.ToString();
    }
}

record struct Cell(char Symbol, CellType Type) {
    public const char BLOCKED = '#';

    public static Cell From(char symbol) {
        var type = symbol switch {
            'P' => CellType.Player,
            'T' => CellType.Treasure,
            '+' => CellType.Cross,
            '|' => CellType.Vertical,
            '-' => CellType.Horizontal,
            '<' => CellType.Left,
            '>' => CellType.Right,
            '^' => CellType.Up,
            'v' => CellType.Down,
            _ => CellType.Blocked
        };
        return new Cell(symbol, type);
    }
}

enum CellType {
    /// P
    Player,

    /// T
    Treasure,

    /// +: Can be entered and exited from any direction
    Cross,

    /// |: Can only be entered and exited from top and bottom
    Vertical,

    /// -: Can only be entered and exited from left and right
    Horizontal,

    /// &lt;: Can be entered from top, bottom, or right, but only exited to the left
    Left,

    /// >: Can be entered from top, bottom, or left, but only exited to the right
    Right,

    /// ^: Can be entered from bottom, left, or right, but only exited to the top
    Up,

    /// v: Can be entered from top, left, or right, but only exited to the bottom
    Down,

    /// #
    Blocked
}

readonly record struct Point(int X, int Y) {
    public bool InBounds(int width, int? height = null) {
        return X >= 0 && X < width && Y >= 0 && Y < (height ?? width);
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"({X}, {Y})";
}

static class Screen {
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];
}

static class Pathfinding {
    /// BFS to end
    public static List<T>? Shortest<T>(T start, T end, Func<T, IEnumerable<T>> getNext) where T : IEquatable<T> {
        Dictionary<T, T> parents = [];
        Queue<T> queue = [];
        HashSet<T> visited = [];
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (current.Equals(end)) {
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
}

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; }

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        if (!Enabled) {
            return;
        }
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
