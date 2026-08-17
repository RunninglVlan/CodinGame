using System;
using System.Collections.Generic;
using System.Linq;

// Visual
// https://www.codingame.com/training/medium/minesweeper-1
var grid = new Grid(30, 16, 99);
while (true) {
    grid.ReadInput();
    Console.WriteLine($"{grid.NextCell()} {string.Join(' ', grid.TurnMines)}");
}

class Grid(int width, int height, int mineCount) {
    readonly Dictionary<Point, int> minesAround = [];
    readonly HashSet<Point> unknownCells = [];
    readonly HashSet<Point> mines = [];
    public HashSet<Point> TurnMines { get; } = [];

    public void ReadInput() {
        minesAround.Clear();
        unknownCells.Clear();
        for (var y = 0; y < height; y++) {
            string[] lineCells = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < width; x++) {
                char cell = lineCells[x].First();
                var point = new Point(x, y);
                if (cell == '?') {
                    if (!mines.Contains(point)) {
                        unknownCells.Add(point);
                    }
                    continue;
                }
                int count = cell == '.' ? 0 : (int)char.GetNumericValue(cell);
                minesAround.Add(point, count);
            }
        }
    }

    public Point NextCell() {
        bool firstTurn = unknownCells.Count == width * height;
        return firstTurn ? Point.Random(width, height) : NextSafeCell();
    }

    Point NextSafeCell() {
        if (TryFindOneTwoOneSafeCell(out var safeCell)) {
            return safeCell;
        }
        TurnMines.Clear();
        var safeCells = new HashSet<Point>();
        while (mines.Count < mineCount) {
            (int mines, int safeCells) before = (mines.Count, safeCells.Count);
            foreach ((var cell, int count) in minesAround) {
                FindMinesAndSafeCells(cell, count, safeCells);
            }
            if (mines.Count == before.mines && safeCells.Count == before.safeCells) {
                break;
            }
        }
        return safeCells.Count > 0
                ? safeCells.First()
                : throw new Exception($"Can't find safe cell. Mines found: {mines.Count}");
    }

    /// Marks cells as safe or mines using direct deduction and adjacent-pair subset elimination
    void FindMinesAndSafeCells(Point cell, int count, HashSet<Point> safeCells) {
        var neighbors = Point.Directions8.Select(it => cell + it).ToArray();
        var nearbyPotentialMines = neighbors.Where(it => unknownCells.Contains(it)).ToList();
        if (nearbyPotentialMines.Count == 0) {
            return;
        }
        int nearbyMines = neighbors.Count(it => mines.Contains(it));
        if (nearbyMines == count) {
            // found our mines, extra cells around are safe
            safeCells.UnionWith(nearbyPotentialMines);
            return;
        }
        if (count - nearbyMines == 1 && nearbyPotentialMines.Count == 2) {
            // check if nearby cell needs the same 1 mine among a superset of these cells - its extra cells are safe
            var safeCellsNearby = FindSafeCellsNearby(cell, nearbyPotentialMines);
            if (safeCellsNearby != null) {
                safeCells.UnionWith(safeCellsNearby);
                return;
            }
        }
        if (nearbyPotentialMines.Count + nearbyMines != count) {
            return;
        }
        // found new mines
        foreach (var mine in nearbyPotentialMines) {
            mines.Add(mine);
            TurnMines.Add(mine);
            unknownCells.Remove(mine);
        }
    }

    List<Point>? FindSafeCellsNearby(Point cell, List<Point> potentialMines) {
        foreach (var direction in Point.Directions4) {
            var nearbyCell = cell + direction;
            if (!minesAround.TryGetValue(nearbyCell, out int count) || count == 0) {
                continue;
            }
            var neighbors = Point.Directions8.Select(it => nearbyCell + it).ToArray();
            var nearbyPotentialMines = neighbors.Where(it => unknownCells.Contains(it)).ToList();
            int nearbyMines = neighbors.Count(it => mines.Contains(it));
            if (count - nearbyMines != 1 || !potentialMines.All(nearbyPotentialMines.Contains)) {
                continue;
            }
            var safeCells = nearbyPotentialMines.Except(potentialMines).ToList();
            if (safeCells.Count == 0) {
                continue;
            }
            return safeCells;
        }
        return null;
    }

    /// Finds safe cell near 1-2-1 pattern
    bool TryFindOneTwoOneSafeCell(out Point safeCell) {
        foreach (var (point, _) in minesAround.Where(it => it.Value == 2)) {
            foreach (var direction in Point.Axes) {
                if (!minesAround.TryGetValue(point + direction, out int count) || count != 1) {
                    continue;
                }
                if (!minesAround.TryGetValue(point + direction * -1, out count) || count != 1) {
                    continue;
                }
                Point[] sideDirections = [direction.TurnLeft(), direction.TurnRight()];
                foreach (var sideDirection in sideDirections) {
                    if (!minesAround.TryGetValue(point + sideDirection, out count) || count != 0) {
                        continue;
                    }
                    var candidate = point + sideDirection * -1;
                    if (!unknownCells.Contains(candidate)) {
                        continue;
                    }
                    safeCell = candidate;
                    return true;
                }
            }
        }
        safeCell = default;
        return false;
    }
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Axes { get; } = [UP, RIGHT];

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point[] Directions8 { get; } = [
        RIGHT, LEFT, UP, DOWN,
        new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)
    ];

    public static Point Random(int width, int height) => new(
            System.Random.Shared.Next(width),
            System.Random.Shared.Next(height)
    );

    public Point TurnLeft() => new(Y, -X);
    public Point TurnRight() => new(-Y, X);

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"{X} {Y}";
}
