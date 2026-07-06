using System;
using System.Collections.Generic;

class Pathfinding(Grid grid) {
    readonly Dictionary<Point, Node> nodes = new();
    readonly PriorityQueue<Node, int> queue = new();

    /// A*
    public (List<Point>?, int cost) Path(Point start, Point end) {
        nodes.Clear();
        queue.Clear();
        var startNode = new Node(start) {
            gCost = 0,
            hCost = Point.ManhattanDistance(start, end)
        };
        nodes.Add(start, startNode);
        queue.Enqueue(startNode, startNode.Priority);
        while (queue.TryDequeue(out var current, out _)) {
            // ignore stale nodes
            if (current.isClosed) {
                continue;
            }
            current.isOpen = false;
            current.isClosed = true;
            if (current.point == end) {
                return (Reconstruct(startNode, current), current.gCost);
            }

            foreach ((var cell, int cellGCost) in grid.Next(current.point, end)) {
                if (!nodes.TryGetValue(cell.point, out var neighbor)) {
                    neighbor = new Node(cell.point);
                    nodes.Add(cell.point, neighbor);
                }
                if (neighbor.isClosed) {
                    continue;
                }
                int gCost = current.gCost + cellGCost;
                if (gCost >= neighbor.gCost && neighbor.isOpen) {
                    continue;
                }
                neighbor.gCost = gCost;
                neighbor.hCost = Point.ManhattanDistance(cell.point, end);
                neighbor.parent = current;
                queue.Enqueue(neighbor, neighbor.Priority);
                neighbor.isOpen = true;
            }
        }
        return (null, int.MaxValue);
    }

    static List<Point> Reconstruct(Node start, Node end) {
        var path = new List<Point>();
        var current = end;
        while (current != start) {
            path.Add(current!.point);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    class Node(Point point) {
        public readonly Point point = point;

        /// Distance from start
        public int gCost = int.MaxValue;

        /// Distance to end
        public int hCost = int.MaxValue;

        public Node? parent;
        public bool isOpen;
        public bool isClosed;

        int FCost => gCost + hCost;

        /// Order by FCost, then hCost
        public int Priority => FCost * 1000 + hCost;
    }
}

/// Stub
class Grid {
    public IEnumerable<(Cell, int)> Next(Point point, Point end) => throw new NotImplementedException();
}

/// Stub
class Cell(int x, int y) {
    public Point point = new(x, y);
}

/// Stub
readonly record struct Point(int X, int Y) {
    public static int ManhattanDistance(Point start, Point end) => throw new NotImplementedException();
    public static Point operator +(Point a, Point b) => throw new NotImplementedException();
}
