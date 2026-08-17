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
    readonly HashSet<Point> safeCells = [];

    public void ReadInput() {
        minesAround.Clear();
        unknownCells.Clear();
        for (var y = 0; y < height; y++) {
            string[] lineCells = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < width; x++) {
                string cell = lineCells[x];
                var point = new Point(x, y);
                if (cell == "?") {
                    if (!mines.Contains(point)) {
                        unknownCells.Add(point);
                    }
                    continue;
                }
                int count = cell == "." ? 0 : int.Parse(cell);
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
        safeCells.Clear();
        while (mines.Count < mineCount) {
            (int mines, int safeCells) before = (mines.Count, safeCells.Count);
            foreach ((var cell, int count) in minesAround) {
                if (count == 2 && TryFindOneTwoOneSafeCell(cell, out var safeCell)) {
                    safeCells.Add(safeCell);
                }
                FindMinesAndSafeCells(cell, count);
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
    void FindMinesAndSafeCells(Point cell, int count) {
        var neighbors = Point.Directions8.Select(it => cell + it).ToArray();
        var nearbyPotentialMines = neighbors.Where(it => unknownCells.Contains(it)).ToList();
        if (nearbyPotentialMines.Count == 0) {
            return;
        }
        int remainingMines = count - neighbors.Count(it => mines.Contains(it));
        if (remainingMines == 0) {
            // found our mines, extra cells around are safe
            AddSafeCells(nearbyPotentialMines, direct: true);
            return;
        }
        if (remainingMines == nearbyPotentialMines.Count) {
            // found new mines
            AddMines(nearbyPotentialMines, direct: true);
            return;
        }
        FindMinesAndSafeCellsAround(cell, remainingMines, nearbyPotentialMines);
    }

    void AddSafeCells(List<Point> cells, bool direct) {
        foreach (var cell in cells.Where(it => safeCells.Add(it))) {
            Log("Found safe cell using", cell, direct);
        }
    }

    void AddMines(List<Point> cells, bool direct) {
        foreach (var mine in cells) {
            Log("Found new mine using", mine, direct);
            mines.Add(mine);
            TurnMines.Add(mine);
            unknownCells.Remove(mine);
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    void FindMinesAndSafeCellsAround(Point cell, int cellMines, List<Point> potentialMines) {
        foreach ((var anotherCell, int count) in minesAround) {
            if (anotherCell == cell || count == 0) {
                continue;
            }
            var neighbors = Point.Directions8.Select(it => anotherCell + it).ToArray();
            var nearbyPotentialMines = neighbors.Where(it => unknownCells.Contains(it)).ToList();
            if (nearbyPotentialMines.Count == 0 || !potentialMines.All(nearbyPotentialMines.Contains)) {
                continue;
            }
            var extra = nearbyPotentialMines.Except(potentialMines).ToList();
            if (extra.Count == 0) {
                continue;
            }
            int remainingMines = count - neighbors.Count(it => mines.Contains(it));
            if (remainingMines == cellMines) {
                // found our mines, extra cells around are safe
                AddSafeCells(extra, direct: false);
                return;
            }
            if (remainingMines - extra.Count == cellMines) {
                // extra cells around are mines
                AddMines(extra, direct: false);
                return;
            }
        }
    }

    static void Log(string message, Point cell, bool direct) {
        string type = direct ? "direct deduction" : "adjacent-pair subset elimination";
        Console.Error.WriteLine($"{message} {type}: {cell}");
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
                Console.Error.WriteLine($"Found safe cell near 1-2-1 pattern: {safeCell}");
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
