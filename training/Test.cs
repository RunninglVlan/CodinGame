using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                        """;
Console.SetIn(new StringReader(testCase));
// https://codeforces.com/contest/2001/problem/C
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
for (var i = 0; i < tests; i++) {
    Console.Error.WriteLine($"Test {i + 1}");
    int nodes = Math.Clamp(int.Parse(Debug.ReadLine()!), 2, 1000);
    var tree = new Tree(nodes);
    tree.Build();
    tree.PrintEdges();
    Console.Error.WriteLine();
    // Console.Out.Flush();
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

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public void Build() {
        HashSet<int> processed = [];
        while (true) {
            int a = edges.Count == 0
                    ? nodes.First()
                    : connections.First(it => it.Value == 1 && !processed.Contains(it.Key)).Key;
            Console.Error.WriteLine($"Processing {a}");
            foreach (int b in nodes) {
                if (a == b) {
                    continue;
                }
                TryFindEdge(a, b, 1, out bool end);
                if (end) {
                    return;
                }
            }
            processed.Add(a);
            Console.Error.WriteLine();
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    [SuppressMessage("ReSharper", "TailRecursiveCall")]
    bool TryFindEdge(int a, int b, int depth, out bool end) {
        end = false;
        // Console.Error.WriteLine($"{new string(' ', depth)}Finding edge {a} {b}");
        var edge = Edge.From(a, b);
        if (edges.Contains(edge)) {
            Console.Error.WriteLine($"{new string(' ', depth)}Already found {edge}");
            return false;
        }
        if (!query.Try(a, b, out int answer)) {
            Console.Error.WriteLine($"{new string(' ', depth)}Already did query {a} {b}");
            return false;
        }
        // Console.Error.WriteLine($"{new string(' ', depth)}=> {answer}");
        if (answer == a) {
            AddEdge(edge);
            Console.Error.WriteLine($"{new string(' ', depth)}Found edge: {edge.A}-{edge.B}, all ({edges.Count}):");
            Console.Error.WriteLine($"{new string(' ', depth + 1)}{string.Join(", ", edges.Select(it => $"{it.A} {it.B}"))}");
            if (edges.Count == nodes.Length - 1) {
                end = true;
            }
            return true;
        }
        Console.Error.WriteLine($"{new string(' ', depth)}Found middle: {edge.A}- {answer} -{edge.B}");
        var edgeLeft = Edge.From(a, answer);
        bool noEdgeBefore = !edges.Contains(edgeLeft);
        if (TryFindEdge(a, answer, depth + 1, out end)) {
            if (noEdgeBefore && edges.Contains(edgeLeft)) {
                // assume right edge exists too
                var edgeRight = Edge.From(answer, b);
                AddEdge(edgeRight);
                Console.Error.WriteLine($"{new string(' ', depth)}Seemingly found edge: {edgeRight.A}-{edgeRight.B}, all ({edges.Count}):");
                Console.Error.WriteLine($"{new string(' ', depth + 1)}{string.Join(", ", edges.Select(it => $"{it.A} {it.B}"))}");
            }
            return true;
        }
        return TryFindEdge(answer, b, depth + 1, out end);
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

class Query(int nodes) {
    readonly int maxQueries = 15 * nodes;
    readonly HashSet<(int a, int b)> queries = [];

    /// Checks which node x minimizes |d(a,x)−d(b,x)|, where d(x,y) is the distance between nodes x and y.
    /// If more than one such node exists, returns the one which minimizes d(a,x).
    public bool Try(int a, int b, out int answer) {
        if (queries.Count > maxQueries) {
            Console.Error.WriteLine("Max queries reached");
        }
        if (!queries.Add((a, b))) {
            answer = 0;
            return false;
        }
        Console.WriteLine($"? {a} {b}");
        answer = int.Parse(Console.ReadLine()!);
        return true;
    }
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }
}
