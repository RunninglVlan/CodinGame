using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

    [SuppressMessage("ReSharper", "InvertIf")]
    public Point NextCell() {
        bool firstTurn = unknownCells.Count == width * height;
        if (firstTurn) {
            return RandomUnknown();
        }
        TurnMines.Clear();
        var safeCells = new HashSet<Point>();
        while (mines.Count < mineCount) {
            (int mines, int safeCells) before = (mines.Count, safeCells.Count);
            foreach ((var cell, int count) in minesAround) {
                if (count == 2 && TryFindOneTwoOneSafeCell(cell, out var safeCell)) {
                    safeCells.Add(safeCell);
                }
                FindMinesAndSafeCells(cell, count, safeCells);
            }
            if (mines.Count == before.mines && safeCells.Count == before.safeCells) {
                break;
            }
        }
        if (safeCells.Count > 0) {
            return safeCells.First();
        }
        if (mines.Count == mineCount) {
            Console.Error.WriteLine("Found all mines, returning first non-mine cell");
            return unknownCells.Except(mines).First();
        }
        return RandomUnknown();
    }

    Point RandomUnknown() {
        var cells = unknownCells.Where(cell => !NeighborHasMinesAround(cell)).ToArray();
        var message = $"Can't find safe cell, mines found: {mines.Count}, returning random";
        if (cells.Length != 0) {
            message += " with no numbered neighbor";
        } else {
            cells = [.. unknownCells];
        }
        Console.Error.WriteLine(message);
        int random = Random.Shared.Next(0, cells.Length);
        return cells[random];

        bool NeighborHasMinesAround(Point cell) {
            foreach (var direction in Point.Directions8) {
                var neighbor = cell + direction;
                if (minesAround.TryGetValue(neighbor, out int count) && count > 0) {
                    return true;
                }
            }
            return false;
        }
    }

    /// Marks cells as safe or mines using direct deduction and adjacent-pair subset elimination
    void FindMinesAndSafeCells(Point cell, int count, HashSet<Point> safeCells) {
        var neighbors = Point.Directions8.Select(it => cell + it).ToArray();
        var nearbyPotentialMines = neighbors.Where(it => unknownCells.Contains(it)).ToList();
        if (nearbyPotentialMines.Count == 0) {
            return;
        }
        int nearbyMines = neighbors.Count(it => mines.Contains(it));
        switch (count - nearbyMines) {
            case 0:
                // found our mines, extra cells around are safe
                safeCells.UnionWith(nearbyPotentialMines);
                return;
            case 1 when nearbyPotentialMines.Count == 2:
                // check if nearby cell needs the same 1 mine among a superset of these cells
                FindMinesAndSafeCellsAround(cell, nearbyPotentialMines, safeCells);
                return;
        }
        if (count - nearbyMines != nearbyPotentialMines.Count) {
            return;
        }
        // found new mines
        foreach (var mine in nearbyPotentialMines) {
            mines.Add(mine);
            TurnMines.Add(mine);
            unknownCells.Remove(mine);
        }
    }

    void FindMinesAndSafeCellsAround(Point cell, List<Point> twoCellsWithOneMine, HashSet<Point> safeCells) {
        foreach (var direction in Point.Directions4) {
            var nearbyCell = cell + direction;
            if (!minesAround.TryGetValue(nearbyCell, out int count) || count == 0) {
                continue;
            }
            var neighbors = Point.Directions8.Select(it => nearbyCell + it).ToArray();
            var nearbyPotentialMines = neighbors.Where(it => unknownCells.Contains(it)).ToList();
            if (nearbyPotentialMines.Count == 0 || !twoCellsWithOneMine.All(nearbyPotentialMines.Contains)) {
                continue;
            }
            var extra = nearbyPotentialMines.Except(twoCellsWithOneMine).ToList();
            if (extra.Count == 0) {
                continue;
            }
            int nearbyMines = neighbors.Count(it => mines.Contains(it));
            if (count - nearbyMines == 1) {
                // found our mines, extra cells around are safe
                safeCells.UnionWith(extra);
                return;
            }
            if (count - nearbyMines - extra.Count != 1) {
                continue;
            }
            // extra cells around are mines
            foreach (var mine in extra) {
                mines.Add(mine);
                TurnMines.Add(mine);
                unknownCells.Remove(mine);
            }
            return;
        }
    }

    /// Finds safe cell near 1-2-1 pattern
    bool TryFindOneTwoOneSafeCell(Point cell, out Point safeCell) {
        foreach (var direction in Point.Axes) {
            if (!minesAround.TryGetValue(cell + direction, out int count) || count != 1) {
                continue;
            }
            if (!minesAround.TryGetValue(cell + direction * -1, out count) || count != 1) {
                continue;
            }
            Point[] sideDirections = [direction.TurnLeft(), direction.TurnRight()];
            foreach (var sideDirection in sideDirections) {
                if (!minesAround.TryGetValue(cell + sideDirection, out count) || count != 0) {
                    continue;
                }
                var candidate = cell + sideDirection * -1;
                if (!unknownCells.Contains(candidate)) {
                    continue;
                }
                safeCell = candidate;
                return true;
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

    public Point TurnLeft() => new(Y, -X);
    public Point TurnRight() => new(-Y, X);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"{X} {Y}";
}
