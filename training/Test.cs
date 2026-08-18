using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

const string testCase = """
                        200
                        2
                        1
                        3
                        1
                        1
                        3
                        3
                        1
                        3
                        2
                        3
                        1
                        2
                        2
                        3
                        4
                        1
                        1
                        1
                        4
                        1
                        2
                        2
                        2
                        3
                        2
                        2
                        4
                        2
                        4
                        1
                        4
                        1
                        4
                        4
                        3
                        4
                        1
                        1
                        2
                        2
                        4
                        2
                        4
                        1
                        2
                        1
                        2
                        3
                        2
                        4
                        1
                        2
                        2
                        4
                        4
                        3
                        """;
Console.SetIn(new StringReader(testCase));
// https://codeforces.com/contest/2001/problem/C
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
for (var _ = 0; _ < tests; _++) {
    int nodes = Math.Clamp(int.Parse(Debug.ReadLine()!), 2, 1000);
    var tree = new Tree(nodes);
    tree.Build();
    tree.PrintEdges();
    Console.Error.WriteLine();
    // Console.Out.Flush();
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
            var nodesWithoutEdges = nodes.Values.Where(it => it.Connections.Count == 0).ToArray();
            if (nodesWithoutEdges.Length == 0) {
                break;
            }
            var a = nodesWithoutEdges.First();
            var connections = 0;
            foreach (var b in nodes.Values) {
                if (b.Index == a.Index) {
                    continue;
                }
                var query = $"? {a.Index} {b.Index}";
                Console.WriteLine(query);
                /*
                "? a b" — Misuki will tell you which node x minimizes |d(a,x)−d(b,x)|, where d(x,y) is the distance between nodes x and y.
                If more than one such node exists, Misuki will tell you the one which minimizes d(a,x).
                 */
                int queryAnswer = int.Parse(Console.ReadLine()!);
                Console.Error.WriteLine($" => {queryAnswer}");
                if (queryAnswer == a.Index) {
                    a.Connections.Add(b);
                    b.Connections.Add(a);
                } /* else if (queryAnswer == b.Index) {
                    // check
                    a.Connections.Add(b);
                    b.Connections.Add(a);
                } */ else {
                    var next = nodes[queryAnswer];
                    a.Connections.Add(next);
                    next.Connections.Add(a);
                }
                if (a.Connections.Count > connections) {
                    Console.Error.WriteLine($"Found edge: {a.Index} {a.Connections.Last().Index}");
                    connections++;
                }
            }
        }
    }

    public void PrintEdges() {
        HashSet<(int a, int b)> edges = [];
        foreach (var node in nodes.Values) {
            foreach (var connection in node.Connections) {
                edges.Add((Math.Min(node.Index, connection.Index), Math.Max(node.Index, connection.Index)));
            }
        }
        if (edges.Count != nodes.Count - 1) {
            Console.Error.WriteLine("WRONG ANSWER");
        }
        Console.WriteLine($"! {string.Join(" ", edges.Select(it => $"{it.a} {it.b}"))}");
    }
}

class Node(int index) {
    public int Index { get; } = index;
    public HashSet<Node> Connections { get; } = [];
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }
}

