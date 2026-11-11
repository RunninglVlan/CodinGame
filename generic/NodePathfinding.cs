using System;
using System.Collections.Generic;

static class Pathfinding<T> where T : IEquatable<T> {
    /// BFS to end
    public static List<T>? Shortest(T start, T end, Func<T, IEnumerable<T>> getNext) {
        return Shortest(start, it => it.Equals(end), getNext);
    }

    /// BFS to endPredicate
    public static List<T>? Shortest(T start, Predicate<T> endPredicate, Func<T, IEnumerable<T>> getNext) {
        Dictionary<T, T> parents = [];
        Queue<T> queue = [];
        HashSet<T> visited = [];
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                return Reconstruct(parents, start, current);
            }
            foreach (var next in getNext(current)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next] = current;
            }
        }
        return null;
    }

    static List<T> Reconstruct(Dictionary<T, T> parents, T start, T end) {
        var path = new List<T>();
        var current = end;
        while (!current.Equals(start)) {
            path.Add(current);
            current = parents[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }

    /// DFS/Backtracking
    public static IEnumerable<List<T>> All(T start, T end, Func<T, IEnumerable<T>> getNext) {
        return All(start, end, getNext, [start], [start]);
    }

    static IEnumerable<List<T>> All(T current, T end,
            Func<T, IEnumerable<T>> getNext, List<T> path, HashSet<T> visited) {
        if (current.Equals(end)) {
            yield return [.. path];
            yield break;
        }
        foreach (var next in getNext(current)) {
            if (visited.Contains(next)) {
                continue;
            }
            foreach (var result in All(next, end, getNext, [.. path, next], [.. visited, next])) {
                yield return result;
            }
        }
    }
}
