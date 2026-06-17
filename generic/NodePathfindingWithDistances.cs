using System.Collections.Generic;

class Pathfinding<T> where T : INode<T> {
    readonly List<T> path = new();
    readonly PriorityQueue<T, int> queue = new();
    readonly Dictionary<int, int> distances = new();
    readonly Dictionary<int, T> parents = new();

    /// Dijkstra
    public List<T>? Path(T start, T end) {
        queue.Clear();
        distances.Clear();
        parents.Clear();
        queue.Enqueue(start, 0);
        distances.Add(start.Id, 0);
        while (queue.TryDequeue(out var current, out int currentDistance)) {
            if (current.Id == end.Id) {
                return Reconstruct(start, current);
            }
            // skip if we've already found a better path
            if (currentDistance > distances.GetValueOrDefault(current.Id, int.MaxValue)) {
                continue;
            }
            foreach ((var node, int distance) in current.Connections) {
                int newDistance = currentDistance + distance;
                if (newDistance >= distances.GetValueOrDefault(node.Id, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(node, newDistance);
                distances[node.Id] = newDistance;
                parents[node.Id] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        path.Clear();
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
