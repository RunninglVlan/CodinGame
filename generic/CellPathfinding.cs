using System;
using System.Collections.Generic;

class Pathfinding(Grid grid) {
    readonly Queue<Node> queue = new();
    readonly HashSet<Point> visited = new();

    /// BFS to end
    public List<Point>? Path(Point start, Point end) {
        return Path(start, cell => cell.point == end);
    }

    /// BFS to endPredicate
    public List<Point>? Path(Point start, Predicate<Cell> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(grid.Get(start));
        queue.Enqueue(startNode);
        visited.Add(start);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.cell)) {
                return Reconstruct(startNode, current);
            }
            foreach (var nextCell in grid.Next(current.cell)) {
                if (!visited.Add(nextCell.point)) {
                    continue;
                }
                var next = new Node(nextCell) { parent = current };
                queue.Enqueue(next);
            }
        }
        return null;
    }

    static List<Point> Reconstruct(Node start, Node end) {
        var path = new List<Point>();
        var current = end;
        while (current != start) {
            path.Add(current!.cell.point);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    class Node(Cell cell) {
        public readonly Cell cell = cell;
        public Node? parent;
    }
}

/// Stub
class Grid {
    public Cell Get(Point point) => throw new NotImplementedException();
    public IEnumerable<Cell> Next(Cell cell) => throw new NotImplementedException();
}

/// Stub
class Cell(int x, int y) {
    public Point point = new(x, y);
}

/// Stub
readonly record struct Point(int X, int Y);
