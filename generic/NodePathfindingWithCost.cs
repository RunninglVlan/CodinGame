using System;
using System.Collections.Generic;

static class Pathfinding {
    /// Dijkstra
    public static List<T>? Cheapest<T>(T start, T end, Func<T, IEnumerable<(T, int cost)>> getNext)
            where T : IEquatable<T> {
        PriorityQueue<T, int> queue = new();
        Dictionary<T, T> parents = [];
        Dictionary<T, int> visited = [];
        queue.Enqueue(start, 0);
        visited.Add(start, 0);
        while (queue.TryDequeue(out var current, out int currentCost)) {
            if (current.Equals(end)) {
                return Reconstruct(parents, start, current);
            }
            foreach ((var next, int cost) in getNext(current)) {
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

    static List<T> Reconstruct<T>(Dictionary<T, T> parents, T start, T end) where T : IEquatable<T> {
        var path = new List<T>();
        var node = end;
        while (!node.Equals(start)) {
            path.Add(node);
            node = parents[node];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
}
