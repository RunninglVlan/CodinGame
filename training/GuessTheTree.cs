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
    readonly int[] nodes;
    readonly HashSet<Edge> edges = [];
    readonly Dictionary<int, int> connections = [];

    public Tree(int nodeCount) {
        nodes = new int[nodeCount];
        for (var i = 0; i < nodeCount; i++) {
            // nodes are indexed from 1 to count
            nodes[i] = i + 1;
        }
    }

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public void Build() {
        int maxQueries = 15 * nodes.Length;
        HashSet<int> processed = [];
        for (var _ = 0; _ < maxQueries; _++) {
            int a = edges.Count == 0
                    ? nodes.First()
                    : connections.First(it => it.Value == 1 && !processed.Contains(it.Key)).Key;
            foreach (int b in nodes) {
                if (a == b) {
                    continue;
                }
                if (TryFindEdge(a, b)) {
                    return;
                }
            }
            processed.Add(a);
        }
    }

    [SuppressMessage("ReSharper", "TailRecursiveCall")]
    bool TryFindEdge(int a, int b) {
        var edge = Edge.From(a, b);
        if (edges.Contains(edge)) {
            return false;
        }
        int answer = Query(a, b);
        if (answer == a) {
            AddEdge(edge);
            return edges.Count == nodes.Length - 1;
        }
        int node = edges.Contains(Edge.From(a, answer)) ? b : a;
        return TryFindEdge(node, answer);
    }

    /// Checks which node x minimizes |d(a,x)−d(b,x)|, where d(x,y) is the distance between nodes x and y.
    /// If more than one such node exists, returns the one which minimizes d(a,x).
    static int Query(int a, int b) {
        var query = $"? {a} {b}";
        Console.WriteLine(query);
        return int.Parse(Console.ReadLine()!);
    }

    void AddEdge(Edge edge) {
        edges.Add(edge);
        int count = connections.GetValueOrDefault(edge.A, 0);
        connections[edge.A] = ++count;
        count = connections.GetValueOrDefault(edge.B, 0);
        connections[edge.B] = ++count;
    }

    public void PrintEdges() {
        Console.WriteLine($"! {string.Join(" ", edges.Select(it => $"{it.A} {it.B}"))}");
    }
}

record struct Edge(int A, int B) {
    public static Edge From(int a, int b) => new(Math.Min(a, b), Math.Max(a, b));
}
