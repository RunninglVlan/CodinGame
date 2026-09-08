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
            foreach ((var node, int cost) in graph.Next(current)) {
                int newCost = currentCost + cost;
                if (newCost >= visited.GetValueOrDefault(node, int.MaxValue)) {
                    continue;
                }
                queue.Enqueue(node, newCost);
                visited[node] = newCost;
                parents[node] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        var path = new List<T>();
        var current = end;
        while (!current.Equals(start)) {
            path.Add(current);
            current = parents[current];
        }
        path.Reverse();
        return path;
    }
}

interface IGraph<T> {
    IEnumerable<(T, int cost)> Next(T current);
}
