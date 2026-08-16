using System;
using System.Collections.Generic;
using System.Linq;

// Visual
// https://www.codingame.com/training/medium/minesweeper-1
var grid = new Grid(30, 16, 99);
while (true) {
    grid.ReadInput();
    Console.WriteLine($"{grid.NextCell()} {string.Join(' ', grid.Mines)}");
}

class Grid(int width, int height, int mineCount) {
    readonly Dictionary<Point, int> minesAround = [];
    readonly HashSet<Point> unknownCells = [];

    public HashSet<Point> Mines { get; } = [];

    public void ReadInput() {
        minesAround.Clear();
        unknownCells.Clear();
        for (var y = 0; y < height; y++) {
            string[] lineCells = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < width; x++) {
                char cell = lineCells[x].First();
                var point = new Point(x, y);
                if (cell == '?') {
                    unknownCells.Add(point);
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
        if (TryFindOneOneAnySafeCell(out var cell)) {
            return cell;
        }
        Mines.Clear();
        while (Mines.Count < mineCount) {
            foreach ((var point, int count) in minesAround) {
                var neighbors = Point.Directions8.Select(it => point + it).ToArray();
                var nearbyPotentialMines = neighbors.Where(it => unknownCells.Contains(it)).ToArray();
                if (nearbyPotentialMines.Length == 0) {
                    continue;
                }
                int nearbyMines = neighbors.Count(it => Mines.Contains(it));
                if (nearbyMines == count) {
                    return nearbyPotentialMines.First();
                }
                if (nearbyPotentialMines.Length + nearbyMines != count) {
                    continue;
                }
                foreach (var mine in nearbyPotentialMines) {
                    Mines.Add(mine);
                    unknownCells.Remove(mine);
                }
            }
        }
        throw new Exception($"Can't find safe cell. Mines found: {Mines.Count}");
    }

    /// Finds safe cell near 1-1-X pattern
    bool TryFindOneOneAnySafeCell(out Point safeCell) {
        foreach (var (cell, neighbor) in FindOneOneAnyPatterns()) {
            var potentialMines = Point.Directions8.Select(it => cell + it).Where(unknownCells.Contains).ToHashSet();
            if (potentialMines.Count != 2) {
                continue;
            }
            foreach (var direction in Point.Directions8) {
                var candidate = neighbor + direction;
                if (!unknownCells.Contains(candidate) || potentialMines.Contains(candidate)) {
                    continue;
                }
                safeCell = candidate;
                return true;
            }
        }
        safeCell = default;
        return false;
    }

    IEnumerable<(Point, Point)> FindOneOneAnyPatterns() {
        foreach (var (cell, _) in minesAround.Where(it => it.Value == 1)) {
            foreach (var direction in Point.Directions4) {
                var directionNeighbor = cell + direction;
                if (directionNeighbor.InBounds(width, height)) {
                    continue;
                }
                var oppositeDirection = direction * -1;
                var neighbor = cell + oppositeDirection;
                if (!minesAround.TryGetValue(neighbor, out int count) || count != 1) {
                    continue;
                }
                var beyond = neighbor + oppositeDirection;
                if (minesAround.TryGetValue(beyond, out count) && count != 0) {
                    yield return (cell, neighbor);
                }
            }
        }
    }
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point[] Directions8 { get; } = [
        RIGHT, LEFT, UP, DOWN,
        new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)
    ];

    public static Point Random(int width, int height) => new(
            System.Random.Shared.Next(width),
            System.Random.Shared.Next(height)
    );

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"{X} {Y}";
}
