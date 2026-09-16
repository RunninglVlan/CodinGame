using System;
using System.Collections.Generic;

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly Dictionary<T, Node> nodes = new();
    readonly PriorityQueue<Node, (int, int)> queue = new();

    /// A*
    public (List<T>, int cost)? Path(T start, T end) {
        nodes.Clear();
        queue.Clear();
        var startNode = new Node(start) { hCost = graph.Heuristic(start, end) };
        nodes.Add(start, startNode);
        queue.Enqueue(startNode, startNode.Priority);
        while (queue.TryDequeue(out var current, out _)) {
            // ignore stale nodes
            if (current.isClosed) {
                continue;
            }
            current.isOpen = false;
            current.isClosed = true;
            if (current.value.Equals(end)) {
                return (Reconstruct(startNode, current), current.gCost);
            }
            foreach ((var value, int cost) in graph.Next(current.value, end)) {
                if (!nodes.TryGetValue(value, out var next)) {
                    nodes.Add(value, next = new Node(value));
                }
                if (next.isClosed) {
                    continue;
                }
                int gCost = current.gCost + cost;
                if (gCost >= next.gCost && next.isOpen) {
                    continue;
                }
                next.gCost = gCost;
                next.hCost = graph.Heuristic(value, end);
                next.parent = current;
                queue.Enqueue(next, next.Priority);
                next.isOpen = true;
            }
        }
        return null;
    }

    static List<T> Reconstruct(Node start, Node end) {
        var path = new List<T>();
        var node = end;
        while (!node!.Equals(start)) {
            path.Add(node.value);
            node = node.parent;
        }
        path.Reverse();
        return path;
    }

    class Node(T value) {
        public readonly T value = value;

        /// Cost from the start
        public int gCost;

        /// Cost to the end
        public int hCost;

        public Node? parent;
        public bool isOpen;
        public bool isClosed;
        int FCost => gCost + hCost;
        public (int, int) Priority => (FCost, hCost);
    }
}

interface IGraph<T> {
    IEnumerable<(T, int cost)> Next(T current, T end);
    int Heuristic(T start, T end);
}
