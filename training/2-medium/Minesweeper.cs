using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/medium/minesweeper
string[] dimensions = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(dimensions[0]), 3, 20);
int width = Math.Clamp(int.Parse(dimensions[1]), 3, 20);
int mines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
var grid = new Grid(width, height, mines);
for (var y = 0; y < height; y++) {
    string line = Console.ReadLine()!;
    for (var x = 0; x < height; x++) {
        grid.ReadCell(x, y, line[x]);
    }
}
grid.FindMines();
grid.PrintMines();

class Grid(int width, int height, int mineCount) {
    readonly Dictionary<Point, int> minesAround = [];
    readonly List<Point> potentialMines = [];
    readonly HashSet<Point> mines = [];

    public void ReadCell(int x, int y, char cell) {
        var point = new Point(x, y);
        if (cell == '?') {
            potentialMines.Add(point);
            return;
        }
        minesAround.Add(point, cell == '.' ? 0 : (int)char.GetNumericValue(cell));
    }

    public void FindMines() {
        for (var i = 1; i <= 2; i++) {
            foreach (var (point, _) in minesAround.Where(it => it.Value == i)) {
                var potentialMinesAround = Point.Directions8
                        .Select(it => point + it)
                        .Where(it => it.InBounds(width, height) && potentialMines.Contains(it))
                        .ToArray();
                if (potentialMinesAround.Length != i) {
                    continue;
                }
                foreach (var nearbyMine in potentialMinesAround) {
                    mines.Add(nearbyMine);
                }
            }
        }
    }

    public void PrintMines() {
        foreach (var point in mines.OrderBy(it => it.X).ThenBy(it => it.Y)) {
            Console.WriteLine(point);
        }
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

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"{X} {Y}";
}
