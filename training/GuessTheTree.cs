using System;

// https://codeforces.com/contest/2001/problem/C
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
for (var _ = 0; _ < tests; _++) {
    // nodes are indexed from 1 to count
    int nodes = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 1000);
    // A tree is an undirected acyclic connected graph. A tree with n nodes will always have n−1 edges.
    // max queries: 15 * nodes
    int a = 1, b = nodes;
    var query = $"? {a} {b}";
    Console.WriteLine(query);
    /*
"? a b" — Misuki will tell you which node x minimizes |d(a,x)−d(b,x)|, where d(x,y) is the distance between nodes x and y.
If more than one such node exists, Misuki will tell you the one which minimizes d(a,x).
     */
    int queryAnswer = int.Parse(Console.ReadLine()!);
    // find tree edges
    // if there are 2 nodes, query ? 1 2 will return 1
    var answer = $"! 1 2";
    Console.WriteLine(answer);
    Console.Out.Flush();
}
