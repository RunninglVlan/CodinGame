using System;
using System.Collections.Generic;

class Pathfinding<T>(IGraph<T> graph) where T : IState {
    readonly PriorityQueue<Node, int> queue = new();
    readonly Dictionary<T, int> visited = new();

    /// Dijkstra
    public List<(Point point, string action)>? Path(T start, Point end) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(start);
        queue.Enqueue(startNode, 0);
        visited.Add(start, 0);
        while (queue.TryDequeue(out var current, out int currentCost)) {
            if (current.state.Point == end) {
                return Reconstruct(startNode, current);
            }
            foreach ((var nextState, int cost) in graph.Next(current.state)) {
                int newCost = currentCost + cost;
                if (newCost >= visited.GetValueOrDefault(nextState, int.MaxValue)) {
                    continue;
                }
                var next = new Node(nextState) { parent = current };
                queue.Enqueue(next, newCost);
                visited[nextState] = newCost;
            }
        }
        return null;
    }

    /// Constructs path from start to end consisting of actions
    /// that need to happen at specific point to get to the next point
    static List<(Point, string)> Reconstruct(Node start, Node end) {
        var path = new List<(Point, string)>();
        var current = end;
        while (current != start) {
            string action = current.state.Action;
            current = current.parent;
            path.Add((current!.state.Point, action));
        }
        path.Reverse();
        return path;
    }

    class Node(T state) {
        public readonly T state = state;
        public Node? parent;
    }
}

interface IGraph<TState> {
    IEnumerable<(TState, int cost)> Next(TState current);
}

interface IState : IEquatable<IState> {
    Point Point { get; }
    string Action { get; }
}

/// Stub
readonly record struct Point(int X, int Y);
