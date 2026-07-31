using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

class Pathfinding<T> where T : INode<T> {
    readonly Queue<T> queue = new();
    readonly Dictionary<int, T> parents = new();
    readonly HashSet<int> visited = new();

    /// BFS to end
    public List<T>? Path(T start, T end) {
        return Path(start, it => it.Id == end.Id);
    }

    /// BFS to endPredicate
    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public List<T>? Path(T start, Predicate<T> endPredicate) {
        queue.Clear();
        parents.Clear();
        visited.Clear();
        queue.Enqueue(start);
        visited.Add(start.Id);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                return Reconstruct(start, current);
            }
            foreach (var next in current.Connections) {
                if (!visited.Add(next.Id)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next.Id] = current;
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

    /// DFS/Backtracking
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public IEnumerable<List<T>> Paths(T start, T end) {
        return Paths(start, end, [start], [start.Id]);
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    static IEnumerable<List<T>> Paths(T current, T end, List<T> path, HashSet<int> visited) {
        if (current.Id == end.Id) {
            yield return [.. path];
            yield break;
        }
        foreach (var next in current.Connections) {
            if (visited.Contains(next.Id)) {
                continue;
            }
            foreach (var result in Paths(next, end, [.. path, next], [.. visited, next.Id])) {
                yield return result;
            }
        }
    }
}

interface INode<T> where T : INode<T> {
    int Id { get; }
    List<T> Connections { get; }
}
