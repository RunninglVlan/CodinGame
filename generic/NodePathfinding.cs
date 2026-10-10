using System;
using System.Collections.Generic;

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly Dictionary<T, T> parents = [];

    /// BFS to end
    public List<T>? Path(T start, T end) {
        return Path(start, it => it.Equals(end));
    }

    /// BFS to endPredicate
    public List<T>? Path(T start, Predicate<T> endPredicate) {
        Queue<T> queue = [];
        HashSet<T> visited = [];
        parents.Clear();
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                return Reconstruct(start, current);
            }
            foreach (var next in graph.Next(current)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next] = current;
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

    /// DFS/Backtracking
    public IEnumerable<List<T>> Paths(T start, T end) {
        return Paths(start, end, [start], [start]);
    }

    IEnumerable<List<T>> Paths(T current, T end, List<T> path, HashSet<T> visited) {
        if (current.Equals(end)) {
            yield return [.. path];
            yield break;
        }
        foreach (var next in graph.Next(current)) {
            if (visited.Contains(next)) {
                continue;
            }
            foreach (var result in Paths(next, end, [.. path, next], [.. visited, next])) {
                yield return result;
            }
        }
    }
}

interface IGraph<T> {
    IEnumerable<T> Next(T current);
}
