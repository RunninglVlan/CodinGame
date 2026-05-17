class Pathfinding(Grid grid) {
    readonly List<Point> path = new();
    readonly Queue<PathNode> queue = new();
    readonly Dictionary<Point, PathNode> visited = new();

    /// BFS to end
    public List<Point>? Path(Point start, Point end) {
        return Path(start, cell => cell.point == end);
    }

    /// BFS to endPredicate
    public List<Point>? Path(Point start, Predicate<Cell> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(grid.Get(start));
        queue.Enqueue(startNode);
        visited.Add(start, startNode);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.cell)) {
                return Reconstruct(startNode, current);
            }
            foreach (var direction in Point.Directions4) {
                var neighbourPoint = current.cell.point + direction;
                if (!grid.TryGet(neighbourPoint, out var cell)) {
                    continue;
                }
                if (!cell.IsWalkable && !endPredicate(cell)) {
                    continue;
                }
                if (visited.ContainsKey(cell.point)) {
                    continue;
                }
                var neighbour = new PathNode(cell) { parent = current };
                queue.Enqueue(neighbour);
                visited.Add(cell.point, neighbour);
            }
        }
        return null;
    }

    List<Point> Reconstruct(PathNode start, PathNode end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.cell.point);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }
}

class PathNode(Cell cell) {
    public readonly Cell cell = cell;
    public PathNode? parent;
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
        cell = Get(point);
        return true;
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
