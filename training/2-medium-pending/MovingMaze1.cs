using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

// https://www.codingame.com/contribute/view/152541078689bfa05bb36d9956b0461f4c5e61
var maze = new Maze(7);
maze.Read();
char pushableTile = Console.ReadLine()!.First();
maze.FindPath(Cell.From(pushableTile));
Console.WriteLine(maze);

class Maze(int size) {
    readonly Dictionary<Point, Cell> cells = [];
    (Point from, Cell tile)? pushed;
    HashSet<Point> foundPath = null!;

    public void Read() {
        for (var y = 0; y < size; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                char symbol = row[x];
                var cell = Cell.From(symbol);
                var point = new Point(x, y);
                cells[point] = cell;
            }
        }
    }

    public void FindPath(Cell pushable) {
        if (TryFindPath()) {
            return;
        }
        for (var i = 1; i < size; i += 2) {
            pushed = (new Point(i, 0), pushable);
            if (TryFindPath()) {
                return;
            }
            pushed = (new Point(i, size - 1), pushable);
            if (TryFindPath()) {
                return;
            }
            pushed = (new Point(0, i), pushable);
            if (TryFindPath()) {
                return;
            }
            pushed = (new Point(size - 1, i), pushable);
            if (TryFindPath()) {
                return;
            }
        }
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    bool TryFindPath() {
        Point? player = null, treasure = null;
        foreach (var point in cells.Keys) {
            var cell = Get(point);
            if (cell.Type == CellType.Player) {
                player = point;
            } else if (cell.Type == CellType.Treasure) {
                treasure = point;
            }
        }
        if (player == null || treasure == null) {
            return false;
        }
        var path = Pathfinding.Shortest(player.Value, treasure.Value, Next);
        if (path == null) {
            return false;
        }
        foundPath = [.. path];
        return true;
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    Cell Get(Point point) {
        if (pushed == null) {
            return cells[point];
        }
        var pushedFrom = pushed.Value.from;
        if (point.X == pushedFrom.X) {
            if (point.Y == pushedFrom.Y) {
                return pushed.Value.tile;
            }
            if (pushedFrom.Y == 0) {
                return cells[point with { Y = point.Y - 1 }];
            }
            if (pushedFrom.Y == size - 1) {
                return cells[point with { Y = point.Y + 1 }];
            }
        }
        if (point.Y == pushedFrom.Y) {
            if (point.X == pushedFrom.X) {
                return pushed.Value.tile;
            }
            if (pushedFrom.X == 0) {
                return cells[point with { X = point.X - 1 }];
            }
            if (pushedFrom.X == size - 1) {
                return cells[point with { X = point.X + 1 }];
            }
        }
        return cells[point];
    }

    IEnumerable<Point> Next(Point current) {
        var currentCell = Get(current);
        foreach (var direction in Screen.Directions4) {
            var next = current + direction;
            if (!next.InBounds(size)) {
                continue;
            }
            var nextCell = Get(next);
            if (currentCell.CanExit(direction) && nextCell.CanEnter(direction)) {
                yield return next;
            }
        }
    }

    public override string ToString() {
        var rows = new StringBuilder();
        for (var y = 0; y < size; y++) {
            for (var x = 0; x < size; x++) {
                var point = new Point(x, y);
                rows.Append(foundPath.Contains(point) ? Get(point).Symbol : Cell.BLOCKED);
            }
            if (y < size - 1) {
                rows.AppendLine();
            }
        }
        return rows.ToString();
    }
}

readonly record struct Cell(char Symbol, CellType Type) {
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

    public bool CanExit(Point direction) => Type switch {
        CellType.Vertical => direction == Screen.UP || direction == Screen.DOWN,
        CellType.Horizontal => direction == Screen.LEFT || direction == Screen.RIGHT,
        CellType.Left => direction == Screen.LEFT,
        CellType.Right => direction == Screen.RIGHT,
        CellType.Up => direction == Screen.UP,
        CellType.Down => direction == Screen.DOWN,
        _ => true
    };

    public bool CanEnter(Point direction) => Type switch {
        CellType.Vertical => direction == Screen.UP || direction == Screen.DOWN,
        CellType.Horizontal => direction == Screen.LEFT || direction == Screen.RIGHT,
        CellType.Left => direction != Screen.RIGHT,
        CellType.Right => direction != Screen.LEFT,
        CellType.Up => direction != Screen.DOWN,
        CellType.Down => direction != Screen.UP,
        CellType.Blocked => false,
        _ => true
    };
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
