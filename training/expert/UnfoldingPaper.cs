using System;
using System.Collections.Generic;

/// https://www.codingame.com/training/expert/unfolding-paper
static class UnfoldingPaper {
    static void Main() {
        int numberOfFolds = int.Parse(Console.ReadLine()!);
        string[] dimensions = Console.ReadLine()!.Split(' ');
        int width = int.Parse(dimensions[0]);
        int height = int.Parse(dimensions[1]);
        var paper = new FoldedPaper(width, height);
        paper.Init();
        while (numberOfFolds > 0) {
            paper = paper.Unfold();
            numberOfFolds--;
        }
        Console.WriteLine(paper.NumberOfPieces());
    }
}

class FoldedPaper(int width, int height) {
    readonly Grid grid = new(width, height);
    readonly HashSet<Point> visitedFilled = new();

    public void Init() {
        for (var y = 0; y < grid.height; y++) {
            string row = Console.ReadLine()!;
            SetGridRow(row, y);
        }
    }

    void SetGridRow(string row, int y) {
        for (var x = 0; x < grid.width; x++) {
            char c = row[x];
            var cell = new Cell(x, y) {
                type = c == '#' ? CellType.Filled : CellType.Empty
            };
            if (cell.type == CellType.Filled) {
                grid.filled++;
            }
            grid.Set(x, y, cell);
        }
    }

    /// For local tests
    public void Init(string input) {
        string[] rows = input.Split(Environment.NewLine);
        for (var y = 0; y < grid.height; y++) {
            string row = rows[y];
            SetGridRow(row, y);
        }
    }

    /// Down-to-up and Right-to-left
    public FoldedPaper Unfold() {
        var paper = new FoldedPaper(grid.width * 2, grid.height * 2);
        for (var y = 0; y < grid.height; y++) {
            for (var x = 0; x < grid.width; x++) {
                var cell = grid.Get(x, y);
                int xRight = grid.width + x;
                int xLeft = paper.grid.width - 1 - grid.width - x;
                int yBottom = grid.height + y;
                int yTop = paper.grid.height - 1 - grid.height - y;
                cell.point = new Point(xRight, yBottom);
                paper.grid.Set(xRight, yBottom, cell);
                cell.point = new Point(xRight, yTop);
                paper.grid.Set(xRight, yTop, cell);
                cell.point = new Point(xLeft, yBottom);
                paper.grid.Set(xLeft, yBottom, cell);
                cell.point = new Point(xLeft, yTop);
                paper.grid.Set(xLeft, yTop, cell);
            }
        }
        paper.grid.filled = grid.filled * 4;
        return paper;
    }

    public int NumberOfPieces() {
        visitedFilled.Clear();
        var pieces = 0;
        while (grid.filled > visitedFilled.Count) {
            var notVisitedFilled =
                grid.Get(cell => cell.type == CellType.Filled && !visitedFilled.Contains(cell.point));
            if (notVisitedFilled == null) {
                throw new ArgumentException("Visited all filled cells");
            }
            pieces++;
            MarkPieceVisited(notVisitedFilled.Value);
        }
        return pieces;
    }

    void MarkPieceVisited(Cell start) {
        Queue<Point> queue = new();
        queue.Enqueue(start.point);
        visitedFilled.Add(start.point);
        while (queue.Count > 0) {
            var point = queue.Dequeue();
            foreach (var direction in Grid.FourDirections) {
                var neighbour = point + direction;
                if (!grid.TryGet(neighbour, out var cell) || cell.type != CellType.Filled) {
                    continue;
                }
                if (visitedFilled.Contains(cell.point)) {
                    continue;
                }
                queue.Enqueue(cell.point);
                visitedFilled.Add(cell.point);
            }
        }
    }

    /// For copying challenge parameters and using them in local tests
    public void PrintDebug() {
        for (var y = 0; y < height; y++) {
            var row = string.Empty;
            for (var x = 0; x < width; x++) {
                var cell = grid.Get(x, y);
                row += cell.type == CellType.Filled ? '#' : '.';
            }
            Console.Error.WriteLine(row);
        }
    }
}

class Grid(int width, int height) {
    public static Point[] FourDirections { get; } = [
        Point.RIGHT, Point.LEFT, Point.UP, Point.DOWN
    ];

    public readonly int width = width;
    public readonly int height = height;
    readonly Cell[] cells = new Cell[width * height];
    public int filled;

    public void Set(int x, int y, Cell cell) => cells[width * y + x] = cell;
    public Cell Get(int x, int y) => cells[width * y + x];
    Cell Get(Point point) => Get(point.x, point.y);

    public Cell? Get(Predicate<Cell> predicate) {
        foreach (var cell in cells) {
            if (predicate(cell)) {
                return cell;
            }
        }
        return null;
    }

    public bool TryGet(Point point, out Cell cell) {
        cell = default;
        if (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height) {
            return false;
        }
        cell = Get(point);
        return true;
    }
}

struct Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
}

readonly record struct Point(int x, int y) {
    public readonly int x = x;
    public readonly int y = y;

    public static readonly Point UP = new(0, 1);
    public static readonly Point RIGHT = new(1, 0);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);

    public static Point operator +(Point a, Point b) {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public override string ToString() => $"({x}, {y})";
}

enum CellType {
    Empty,
    Filled
}
