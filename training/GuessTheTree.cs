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
                if (queryAnswer == a.Index) {
                    a.Connections.Add(b);
                    b.Connections.Add(a);
                } else {
                    var next = nodes[queryAnswer];
                    a.Connections.Add(next);
                    next.Connections.Add(a);
                }
            }
            a.Checked = true;
        }
    }

    public void PrintEdges() {
        HashSet<(int a, int b)> edges = [];
        foreach (var node in nodes.Values) {
            foreach (var connection in node.Connections) {
                edges.Add((Math.Min(node.Index, connection.Index), Math.Max(node.Index, connection.Index)));
            }
        }
        Console.WriteLine($"! {string.Join(" ", edges.Select(it => $"{it.a} {it.b}"))}");
    }
}

class Node(int index) {
    public int Index { get; } = index;
    public HashSet<Node> Connections { get; } = [];
    public bool Checked { get; set; }
}
