using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/expert/unfolding-paper
static class UnfoldingPaper {
    static void Main() {
        int numberOfFolds = int.Parse(Console.ReadLine()!);
        string[] dimensions = Console.ReadLine()!.Split(' ');
        int width = int.Parse(dimensions[0]);
        int height = int.Parse(dimensions[1]);
        var paper = new FoldedPaper(width, height);
        paper.Init();
        Console.WriteLine(paper.NumberOfPieces(numberOfFolds));
    }
}

public class FoldedPaper(int width, int height) {
    readonly Grid grid = new(width, height);

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

    public int NumberOfPieces(int numberOfFolds) {
        var pieces = CountPieces();
        while (numberOfFolds > 0) {
            var newPieces = new Dictionary<PieceType, int>();
            foreach ((var type, int typeCount) in pieces) {
                foreach ((var newType, int newTypeCount) in Unfold(type, typeCount)) {
                    int count = newPieces.GetValueOrDefault(newType, 0);
                    newPieces[newType] = count + newTypeCount;
                }
            }
            pieces = newPieces;
            numberOfFolds--;
        }
        return pieces.Values.Sum();
    }

    Dictionary<PieceType, int> CountPieces() {
        var visitedFilled = new HashSet<Point>();
        var pieces = new Dictionary<PieceType, int>();
        while (grid.filled > visitedFilled.Count) {
            var notVisitedFilled =
                grid.Get(cell => cell.type == CellType.Filled && !visitedFilled.Contains(cell.point));
            if (notVisitedFilled == null) {
                throw new ArgumentException("Visited all filled cells");
            }
            var type = MarkPieceVisited(notVisitedFilled.Value, visitedFilled);
            int count = pieces.GetValueOrDefault(type, 0);
            pieces[type] = count + 1;
        }
        return pieces;
    }

    PieceType MarkPieceVisited(Cell start, HashSet<Point> visitedFilled) {
        var type = PieceType.Inside;
        Queue<Point> queue = new();
        queue.Enqueue(start.point);
        visitedFilled.Add(start.point);
        while (queue.Count > 0) {
            var point = queue.Dequeue();
            foreach (var direction in Grid.FourDirections) {
                var neighbor = point + direction;
                if (!grid.TryGet(neighbor, out var cell)) {
                    if (neighbor.x < 0) {
                        type |= PieceType.EdgeLeft;
                    } else if (neighbor.x >= grid.width) {
                        type |= PieceType.EdgeRight;
                    }
                    if (neighbor.y < 0) {
                        type |= PieceType.EdgeTop;
                    } else if (neighbor.y >= grid.height) {
                        type |= PieceType.EdgeBottom;
                    }
                    continue;
                }
                if (cell.type != CellType.Filled || visitedFilled.Contains(cell.point)) {
                    continue;
                }
                queue.Enqueue(cell.point);
                visitedFilled.Add(cell.point);
            }
        }
        return type;
    }

    static IEnumerable<(PieceType, int)> Unfold(PieceType type, int count) {
        switch (type) {
            case PieceType.Inside:
                yield return (PieceType.Inside, count * 4);
                break;
            case PieceType.EdgeTop:
                yield return (PieceType.Inside, count * 2);
                break;
            case PieceType.EdgeBottom:
                yield return (PieceType.EdgeTop, count * 2);
                yield return (PieceType.EdgeBottom, count * 2);
                break;
            case PieceType.EdgeLeft:
                yield return (PieceType.Inside, count * 2);
                break;
            case PieceType.EdgeRight:
                yield return (PieceType.EdgeLeft, count * 2);
                yield return (PieceType.EdgeRight, count * 2);
                break;
            case PieceType.CornerTopLeft:
                yield return (PieceType.Inside, count);
                break;
            case PieceType.CornerBottomLeft:
                yield return (PieceType.EdgeTop, count);
                yield return (PieceType.EdgeBottom, count);
                break;
            case PieceType.CornerTopRight:
                yield return (PieceType.EdgeLeft, count);
                yield return (PieceType.EdgeRight, count);
                break;
            case PieceType.CornerBottomRight:
                yield return (PieceType.CornerTopLeft, count);
                yield return (PieceType.CornerBottomLeft, count);
                yield return (PieceType.CornerTopRight, count);
                yield return (PieceType.CornerBottomRight, count);
                break;
            case PieceType.LineTopBottom:
                yield return (PieceType.LineTopBottom, count * 2);
                break;
            case PieceType.LineLeftRight:
                yield return (PieceType.LineLeftRight, count * 2);
                break;
            case PieceType.EdgeTop | PieceType.CornerBottomLeft:
                yield return (PieceType.LineTopBottom, count);
                break;
            case PieceType.EdgeRight | PieceType.CornerTopLeft:
                yield return (PieceType.LineLeftRight, count);
                break;
            case PieceType.EdgeBottom | PieceType.CornerTopRight:
                yield return (PieceType.EdgeTop | PieceType.EdgeLeft | PieceType.EdgeBottom, count);
                yield return (PieceType.EdgeTop | PieceType.EdgeRight | PieceType.EdgeBottom, count);
                break;
            case PieceType.EdgeLeft | PieceType.CornerBottomRight:
                yield return (PieceType.EdgeLeft | PieceType.EdgeTop | PieceType.EdgeRight, count);
                yield return (PieceType.EdgeLeft | PieceType.EdgeBottom | PieceType.EdgeRight, count);
                break;
            case PieceType.EdgeAll:
                yield return (PieceType.EdgeAll, count);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    #region Testing

    /// For local tests
    public void Init(string input) {
        string[] rows = input.Split(Environment.NewLine);
        for (var y = 0; y < grid.height; y++) {
            string row = rows[y];
            SetGridRow(row, y);
        }
    }

    /// Down-to-up and Right-to-left, for testing visual representation
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

    /// Gets number of pieces of current folded paper
    public int NumberOfPieces() {
        return CountPieces().Values.Sum();
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

    #endregion
}

[Flags]
enum PieceType {
    Inside = 0b_0000,
    EdgeTop = 0b_0001,
    EdgeBottom = 0b_0010,
    EdgeLeft = 0b_0100,
    EdgeRight = 0b_1000,
    CornerTopLeft = EdgeTop | EdgeLeft,
    CornerTopRight = EdgeTop | EdgeRight,
    CornerBottomRight = EdgeBottom | EdgeRight,
    CornerBottomLeft = EdgeBottom | EdgeLeft,
    LineTopBottom = EdgeTop | EdgeBottom,
    LineLeftRight = EdgeLeft | EdgeRight,
    EdgeAll = EdgeTop | EdgeBottom | EdgeLeft | EdgeRight
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
