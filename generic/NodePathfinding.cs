class Pathfinding {
    readonly List<Node> path = new();
    readonly Queue<PathNode> queue = new();
    readonly HashSet<int> visited = new();

    /// BFS to end
    public List<Node>? Path(Node start, Node end) {
        return Path(start, i => i == end.i);
    }

    /// BFS to endPredicate
    public List<Node>? Path(Node start, Predicate<int> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.i);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.node.i)) {
                return Reconstruct(startNode, current);
            }
            foreach (var node in current.node.connections) {
                if (visited.Contains(node.i)) {
                    continue;
                }
                var neighbour = new PathNode(node) { parent = current };
                queue.Enqueue(neighbour);
                visited.Add(node.i);
            }
        }
        return null;
    }

    List<Node> Reconstruct(PathNode start, PathNode end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.node);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    // TODO: Replace with Dictionary<int, Node> parents?
    class PathNode(Node node) {
        public readonly Node node = node;
        public PathNode? parent;
    }
}

// TODO: Replace with INode?
class Node(int i) {
    public readonly int i = i;
    public readonly List<Node> connections = new();
}
