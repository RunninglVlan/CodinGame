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
    readonly Dictionary<int, Node> nodes;
    readonly HashSet<Edge> edges = [];

    public Tree(int nodeCount) {
        nodes = new Dictionary<int, Node>();
        // nodes are indexed from 1 to count
        for (var i = 1; i <= nodeCount; i++) {
            nodes.Add(i, new Node(i));
        }
    }

    public void Build() {
        int maxQueries = 15 * nodes.Count;
        for (var _ = 0; _ < maxQueries; _++) {
            var notCheckedNodes = nodes.Values.Where(it => !it.Checked).ToArray();
            if (notCheckedNodes.Length == 0) {
                break;
            }
            var a = notCheckedNodes.First();
            foreach (var b in nodes.Values) {
                if (b.Index == a.Index) {
                    continue;
                }
                var query = $"? {a.Index} {b.Index}";
                Console.WriteLine(query);
                int queryAnswer = int.Parse(Console.ReadLine()!);
                if (queryAnswer != a.Index) {
                    continue;
                }
                edges.Add(Edge.From(a.Index, b.Index));
            }
            a.Checked = true;
        }
    }

    public void PrintEdges() {
        Console.WriteLine($"! {string.Join(" ", edges.Select(it => $"{it.A} {it.B}"))}");
    }
}

class Node(int index) {
    public int Index { get; } = index;
    public bool Checked { get; set; }
}

record struct Edge(int A, int B) {
    public static Edge From(int a, int b) => new(Math.Min(a, b), Math.Max(a, b));
}
