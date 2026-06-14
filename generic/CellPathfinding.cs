class Pathfinding(Grid grid) {
    readonly List<Point> path = new();
    readonly Queue<Node> queue = new();
    readonly Dictionary<Point, Node> visited = new();

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
        visited.Add(start, startNode);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.cell)) {
                return Reconstruct(startNode, current);
            }
            foreach (var cell in grid.Neighbors(current.cell)) {
                if (!cell.IsWalkable) {
                    continue;
                }
                if (visited.ContainsKey(cell.point)) {
                    continue;
                }
                var neighbor = new Node(cell) { parent = current };
                queue.Enqueue(neighbor);
                visited.Add(cell.point, neighbor);
            }
        }
        return null;
    }

    List<Point> Reconstruct(Node start, Node end) {
        path.Clear();
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
    public IEnumerable<Cell> Neighbors(Cell cell) => throw new NotImplementedException();
}

/// Stub
class Cell(int x, int y) {
    public Point point = new(x, y);
    public bool IsWalkable { get; }
}

/// Stub
readonly record struct Point(int X, int Y);
