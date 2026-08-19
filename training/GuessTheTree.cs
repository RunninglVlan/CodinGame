using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://codeforces.com/contest/2001/problem/C
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
for (var _ = 0; _ < tests; _++) {
    int nodes = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 1000);
    var tree = new Tree(nodes);
    tree.Build();
    tree.PrintEdges();
    Console.Out.Flush();
}

/// Undirected acyclic connected graph. A tree with n nodes will always have n−1 edges.
class Tree {
    readonly Query query;
    readonly int[] nodes;
    readonly HashSet<Edge> edges = [];
    readonly Dictionary<int, int> connections = [];

    public Tree(int nodeCount) {
        query = new Query(nodeCount);
        nodes = new int[nodeCount];
        for (var i = 0; i < nodeCount; i++) {
            // nodes are indexed from 1 to count
            nodes[i] = i + 1;
        }
    }

    public void Build() {
        while (true) {
            var edge = EdgeToCheck();
            TryFindEdge(edge, out bool end);
            if (end) {
                return;
            }
        }
    }

    Edge EdgeToCheck() {
        foreach ((int a, int _) in connections.OrderBy(it => it.Value)) {
            foreach (int b in nodes.OrderBy(it => connections.ContainsKey(it))) {
                var edge = Edge.From(a, b);
                if (b != a && !edges.Contains(edge) && !query.Tried(edge)) {
                    return edge;
                }
            }
        }
        return Edge.From(1, 2);
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    bool TryFindEdge(Edge edge, out bool end) {
        end = false;
        if (edges.Contains(edge) || !query.Try(edge, out int answer)) {
            return false;
        }
        if (answer == edge.A) {
            AddEdge(edge, out end);
            return true;
        }
        return TryFindEdge(Edge.From(edge.A, answer), out end) || TryFindEdge(Edge.From(answer, edge.B), out end);
    }

    void AddEdge(Edge edge, out bool end) {
        edges.Add(edge);
        int count = connections.GetValueOrDefault(edge.A, 0);
        connections[edge.A] = ++count;
        count = connections.GetValueOrDefault(edge.B, 0);
        connections[edge.B] = ++count;
        end = edges.Count == nodes.Length - 1;
    }

    public void PrintEdges() {
        Console.WriteLine($"! {string.Join(" ", edges.Select(it => $"{it.A} {it.B}"))}");
    }
}

record struct Edge(int A, int B) {
    public static Edge From(int a, int b) => new(Math.Min(a, b), Math.Max(a, b));
}

class Query(int nodes) {
    readonly int maxQueries = 15 * nodes;
    readonly HashSet<Edge> queries = [];

    public bool Tried(Edge edge) => queries.Contains(edge);

    /// Checks which node x minimizes |d(a,x)−d(b,x)|, where d(x,y) is the distance between nodes x and y.
    /// If more than one such node exists, returns the one which minimizes d(a,x).
    public bool Try(Edge edge, out int answer) {
        if (queries.Count > maxQueries) {
            Console.Error.WriteLine("Max queries reached");
        }
        if (!queries.Add(edge)) {
            answer = 0;
            return false;
        }
        Console.WriteLine($"? {edge.A} {edge.B}");
        answer = int.Parse(Console.ReadLine()!);
        return true;
    }
}
