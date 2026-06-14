using System;
using System.Collections.Generic;

class Grid(int width, int height) {
    readonly Cell[,] cells = new Cell[width, height];

    public void Set(int x, int y, Cell cell) => cells[x, y] = cell;
    public Cell Get(Point point) => cells[point.X, point.Y];

    public bool TryGet(Point point, out Cell cell) {
        cell = null!;
        if (!point.InBounds(width, height)) {
            return false;
        }
        cell = Get(point);
        return true;
    }

    public IEnumerable<Cell> Neighbors(Cell cell) {
        foreach (var point in Point.Directions4) {
            if (TryGet(cell.point + point, out var neighbor)) {
                yield return neighbor;
            }
        }
    }
}

/// Stub
class Cell(int x, int y) {
    public Point point = new(x, y);
}

/// Stub
readonly record struct Point(int X, int Y) {
    public static Point[] Directions4 { get; } = [];
    public bool InBounds(int width, int height) => throw new NotImplementedException();
    public static Point operator +(Point a, Point b) => throw new NotImplementedException();
}
