using System.Collections.Generic;

class Pathfinding<T> where T : INode<T> {
    readonly PriorityQueue<T, int> queue = new();
    readonly Dictionary<int, T> parents = new();
    readonly Dictionary<int, int> visited = new();

    /// Dijkstra
    public List<T>? Path(T start, T end) {
        queue.Clear();
        parents.Clear();
        visited.Clear();
        queue.Enqueue(start, 0);
        visited.Add(start.Id, 0);
        while (queue.TryDequeue(out var current, out int currentDistance)) {
            if (current.Id == end.Id) {
                return Reconstruct(start, current);
            }
            foreach ((var node, int distance) in current.Connections) {
                int newDistance = currentDistance + distance;
                if (newDistance >= visited.GetValueOrDefault(node.Id, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(node, newDistance);
                visited[node.Id] = newDistance;
                parents[node.Id] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        var path = new List<T>();
        var current = end;
        while (current.Id != start.Id) {
            path.Add(current);
            current = parents[current.Id];
        }
        path.Reverse();
        return path;
    }
}

interface INode<T> where T : INode<T> {
    int Id { get; }
    List<(T, int distance)> Connections { get; }
}
