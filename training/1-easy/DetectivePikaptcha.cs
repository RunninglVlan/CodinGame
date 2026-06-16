using System;
using System.Linq;

namespace DetectivePikaptcha;

/// https://www.codingame.com/training/easy/detective-pikaptcha-ep1
static class DetectivePikaptcha {
    static void Main() {
        var grid = new Grid();
        grid.Init();
        grid.PrintPassableCells();
    }
}

class Grid {
    int width, height;
    CellType[,] cells = null!;

    public void Init() {
        string[] dimensions = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(dimensions[0]), 1, 100);
        height = Math.Clamp(int.Parse(dimensions[1]), 1, 100);
        cells = new CellType[width, height];
        for (var y = 0; y < height; y++) {
            string characters = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char c = characters[x];
                cells[x, y] = c switch {
                    '0' => CellType.Passage,
                    '#' => CellType.Wall,
                    _ => throw new ArgumentException($"Unexpected cell type: {c}")
                };
            }
        }
    }

    public void PrintPassableCells() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                if (cells[x, y] == CellType.Wall) {
                    Console.Write('#');
                    continue;
                }
                var point = new Point(x, y);
                int neighboringPassages = Point.Directions4
                        .Select(it => point + it)
                        .Where(it => it.X >= 0 && it.X < width && it.Y >= 0 && it.Y < height)
                        .Count(it => cells[it.X, it.Y] == CellType.Passage);
                Console.Write(neighboringPassages);
            }
            Console.WriteLine();
        }
    }
}

enum CellType {
    Passage,
    Wall
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
}
