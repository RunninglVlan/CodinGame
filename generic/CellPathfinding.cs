using System;
using System.Collections.Generic;

class Pathfinding<T>(IGrid<T> grid) where T : IEquatable<T> {
    readonly Queue<Node> queue = [];
    readonly HashSet<T> visited = [];

    /// BFS to end
    public List<T>? Path(T start, T end) {
        return Path(start, cell => cell.Equals(end));
    }

    /// BFS to endPredicate
    public List<T>? Path(T start, Predicate<T> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(start);
        queue.Enqueue(startNode);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current.cell)) {
                return Reconstruct(startNode, current);
            }
            foreach (var next in grid.Next(current.cell)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(new Node(next) { parent = current });
            }
        }
        return null;
    }

    static List<T> Reconstruct(Node start, Node end) {
        var path = new List<T>();
        var current = end;
        while (current != start) {
            path.Add(current!.cell);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    class Node(T cell) {
        public readonly T cell = cell;
        public Node? parent;
    }
}

interface IGrid<T> {
    IEnumerable<T> Next(T current);
}
