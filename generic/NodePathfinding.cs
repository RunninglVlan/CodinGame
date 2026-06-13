class Pathfinding<T> where T : INode<T> {
    readonly List<T> path = new();
    readonly Queue<PathNode> queue = new();
    readonly HashSet<int> visited = new();

    /// BFS to end
    public List<T>? Path(T start, T end) {
        return Path(start, i => i == end.Id);
    }

    /// BFS to endPredicate
    public List<T>? Path(T start, Predicate<int> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.Id);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.node.Id)) {
                return Reconstruct(startNode, current);
            }
            foreach (var node in current.node.Connections) {
                if (visited.Contains(node.Id)) {
                    continue;
                }
                var neighbor = new PathNode(node) { parent = current };
                queue.Enqueue(neighbor);
                visited.Add(node.Id);
            }
        }
        return null;
    }

    List<T> Reconstruct(PathNode start, PathNode end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.node);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    // TODO: Replace with Dictionary<int, T> parents?
    class PathNode(T node) {
        public readonly T node = node;
        public PathNode? parent;
    }
}

interface INode<T> where T : INode<T> {
    int Id { get; }
    List<T> Connections { get; }
}
