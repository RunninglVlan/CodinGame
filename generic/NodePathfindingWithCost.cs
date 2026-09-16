using System;
using System.Collections.Generic;

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly PriorityQueue<T, int> queue = new();
    readonly Dictionary<T, T> parents = new();
    readonly Dictionary<T, int> visited = new();

    /// Dijkstra
    public List<T>? Path(T start, T end) {
        queue.Clear();
        parents.Clear();
        visited.Clear();
        queue.Enqueue(start, 0);
        visited.Add(start, 0);
        while (queue.TryDequeue(out var current, out int currentCost)) {
            if (current.Equals(end)) {
                return Reconstruct(start, current);
            }
            foreach ((var next, int cost) in graph.Next(current)) {
                int newCost = currentCost + cost;
                if (newCost >= visited.GetValueOrDefault(next, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(next, newCost);
                visited[next] = newCost;
                parents[next] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        var path = new List<T>();
        var node = end;
        while (!node.Equals(start)) {
            path.Add(node);
            node = parents[node];
        }
        path.Reverse();
        return path;
    }
}

interface IGraph<T> {
    IEnumerable<(T, int cost)> Next(T current);
}
