using System;
using System.Collections.Generic;
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

    public Tree(int nodeCount) {
        nodes = new int[nodeCount];
        for (var i = 0; i < nodeCount; i++) {
            // nodes are indexed from 1 to count
            nodes[i] = i + 1;
        }
    }

    public void Build() {
        int maxQueries = 15 * nodes.Length;
        for (var _ = 0; _ < maxQueries; _++) {
            foreach (int a in nodes) {
                foreach (int b in nodes) {
                    if (a == b) {
                        continue;
                    }
                    var edge = Edge.From(a, b);
                    if (edges.Contains(edge)) {
                        continue;
                    }
                    int answer = Query(a, b);
                    if (answer == a) {
                        edges.Add(edge);
                    }
                }
            }
            if (edges.Count == nodes.Length - 1) {
                break;
            }
        }
    }

    /// Checks which node x minimizes |d(a,x)−d(b,x)|, where d(x,y) is the distance between nodes x and y.
    /// If more than one such node exists, returns the one which minimizes d(a,x).
    static int Query(int a, int b) {
        var query = $"? {a} {b}";
        Console.WriteLine(query);
        return int.Parse(Console.ReadLine()!);
    }

    public void PrintEdges() {
        Console.WriteLine($"! {string.Join(" ", edges.Select(it => $"{it.A} {it.B}"))}");
    }
}

record struct Edge(int A, int B) {
    public static Edge From(int a, int b) => new(Math.Min(a, b), Math.Max(a, b));
}
