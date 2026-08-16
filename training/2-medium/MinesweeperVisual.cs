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
    readonly List<Point> unknownCells = [];
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
                minesAround.Add(point, cell == '.' ? 0 : (int)char.GetNumericValue(cell));
            }
        }
    }

    public Point NextCell() {
        return unknownCells.Count == width * height ? Point.Random(width, height) : NextSafeCell();
    }

    Point NextSafeCell() {
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
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions8 { get; } = [
        RIGHT, LEFT, UP, DOWN,
        new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)
    ];

    public static Point Random(int width, int height) => new(
            System.Random.Shared.Next(width),
            System.Random.Shared.Next(height)
    );

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"{X} {Y}";
}
