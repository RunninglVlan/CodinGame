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
class Grid(int width, int height) {
    readonly Cell[,] cells = new Cell[width, height];
    public Cell Get(Point point) => cells[point.X, point.Y];

    public bool TryGet(Point point, out Cell cell) {
        cell = null!;
        if (point.X < 0 || point.X >= width || point.Y < 0 || point.Y >= height) {
            return false;
        }
        cell = cells[point.X, point.Y];
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
    public bool IsWalkable { get; }
}

/// Stub
readonly record struct Point(int X, int Y) {
    public static Point[] Directions4 { get; } = [];
    public static Point operator +(Point a, Point b) => throw new NotImplementedException();
}
