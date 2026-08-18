using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

const string testCase = """
                        1
                        4
                        1
                        1
                        3
                        1
                        1
                        1
                        3
                        3
                        3
                        """;
Console.SetIn(new StringReader(testCase));
// https://codeforces.com/contest/2001/problem/C
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
for (var i = 0; i < tests; i++) {
    int nodes = Math.Clamp(int.Parse(Debug.ReadLine()!), 2, 1000);
    var tree = new Tree(nodes);
    tree.Build();
    tree.PrintEdges();
    Console.Error.WriteLine();
    // Console.Out.Flush();
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
                    Console.Error.WriteLine($" => {answer}");
                    if (answer == a) {
                        edges.Add(edge);
                    }
                    if (answer == a) {
                        edges.Add(Edge.From(a, b));
                        Console.Error.WriteLine($" Found edge: {a} {b}");
                    } else if (answer == b) {
                        Console.Error.WriteLine($" Found edge? Query returned: {answer}");
                        // check
                        // a.Connections.Add(b);
                        // b.Connections.Add(a);
                    } else {
                        Console.Error.WriteLine($" Query returned: {answer}");
                        // var next = nodes[queryAnswer];
                        // a.Connections.Add(next);
                        // next.Connections.Add(a);
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

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }
}

