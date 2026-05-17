class Pathfinding(Grid grid) {
    readonly Dictionary<Point, PathNode> nodes = new();
    readonly List<Point> path = new();
    readonly PriorityQueue<PathNode, int> openQueue = new();

    /// A*
    public (List<Point>?, int cost) Path(Point start, Point end) {
        nodes.Clear();
        openQueue.Clear();
        var startNode = new PathNode(start) {
            gCost = 0,
            hCost = Point.ManhattanDistance(start, end)
        };
        nodes.Add(start, startNode);
        openQueue.Enqueue(startNode, startNode.Priority);
        while (openQueue.Count > 0) {
            var current = openQueue.Dequeue();
            // ignore stale nodes
            if (current.isClosed) {
                continue;
            }
            current.isOpen = false;
            current.isClosed = true;
            if (current.point == end) {
                return (Reconstruct(startNode, current), current.gCost);
            }

            foreach (var direction in Point.Directions4) {
                var neighbourPoint = current.point + direction;
                if (!grid.TryGet(neighbourPoint, out var cell) || !cell.IsWalkable) {
                    continue;
                }
                if (!nodes.TryGetValue(cell.point, out var neighbour)) {
                    neighbour = new PathNode(cell.point);
                    nodes.Add(cell.point, neighbour);
                }
                if (neighbour.isClosed) {
                    continue;
                }
                int gCost = current.gCost + NewGCost(cell);
                if (gCost >= neighbour.gCost && neighbour.isOpen) {
                    continue;
                }
                neighbour.gCost = gCost;
                neighbour.hCost = Point.ManhattanDistance(neighbourPoint, end);
                neighbour.parent = current;
                openQueue.Enqueue(neighbour, neighbour.Priority);
                neighbour.isOpen = true;
            }
        }
        return (null, int.MaxValue);
    }

    /// Stub
    int NewGCost(Cell cell) => throw new NotImplementedException();

    List<Point> Reconstruct(PathNode start, PathNode end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.point);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }
}

class PathNode(Point point) {
    public Point point = point;

    /// Distance from start
    public int gCost = int.MaxValue;

    /// Distance to end
    public int hCost = int.MaxValue;

    public PathNode? parent;
    public bool isOpen;
    public bool isClosed;

    int FCost => gCost + hCost;

    /// Order by FCost, then hCost
    public int Priority => FCost * 1000 + hCost;
}

/// Stub
class Grid(int width, int height) {
    readonly Cell[,] cells = new Cell[width, height];

    public bool TryGet(Point point, out Cell cell) {
        cell = null!;
        if (point.X < 0 || point.X >= width || point.Y < 0 || point.Y >= height) {
            return false;
        }
        cell = cells[point.X, point.Y];
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
    public static int ManhattanDistance(Point start, Point end) => throw new NotImplementedException();
    public static Point operator +(Point a, Point b) => throw new NotImplementedException();
}
