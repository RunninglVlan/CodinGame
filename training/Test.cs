using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

const string testCase = """
                        1
                        995
                        13
                        446
                        341
                        190
                        1
                        688
                        300
                        819
                        655
                        290
                        308
                        1
                        554
                        68
                        520
                        41
                        2
                        688
                        207
                        690
                        305
                        675
                        366
                        3
                        342
                        581
                        852
                        589
                        781
                        537
                        689
                        49
                        4
                        868
                        881
                        622
                        883
                        90
                        5
                        657
                        722
                        717
                        608
                        451
                        150
                        180
                        627
                        44
                        6
                        298
                        945
                        739
                        716
                        615
                        7
                        186
                        41
                        345
                        782
                        8
                        758
                        626
                        886
                        745
                        342
                        434
                        414
                        9
                        486
                        790
                        139
                        518
                        487
                        323
                        212
                        10
                        307
                        976
                        189
                        306
                        299
                        34
                        904
                        822
                        11
                        11
                        664
                        744
                        669
                        657
                        653
                        980
                        195
                        679
                        11
                        922
                        794
                        900
                        13
                        622
                        151
                        796
                        564
                        28
                        14
                        179
                        784
                        646
                        693
                        633
                        288
                        227
                        198
                        91
                        15
                        940
                        941
                        89
                        372
                        825
                        966
                        16
                        692
                        771
                        540
                        939
                        112
                        783
                        917
                        326
                        359
                        17
                        886
                        665
                        934
                        66
                        133
                        610
                        744
                        605
                        18
                        597
                        256
                        401
                        681
                        704
                        218
                        577
                        924
                        473
                        19
                        290
                        270
                        655
                        20
                        844
                        944
                        394
                        576
                        607
                        136
                        736
                        21
                        753
                        49
                        283
                        464
                        871
                        348
                        209
                        508
                        328
                        22
                        185
                        899
                        596
                        638
                        690
                        652
                        23
                        226
                        966
                        866
                        72
                        507
                        442
                        24
                        651
                        741
                        618
                        125
                        153
                        436
                        664
                        294
                        401
                        25
                        625
                        745
                        990
                        181
                        189
                        809
                        196
                        245
                        26
                        442
                        987
                        449
                        498
                        524
                        396
                        724
                        27
                        808
                        869
                        810
                        183
                        58
                        424
                        730
                        29
                        886
                        665
                        821
                        732
                        852
                        988
                        60
                        18
                        18
                        259
                        300
                        90
                        363
                        369
                        31
                        532
                        11
                        926
                        142
                        457
                        543
                        281
                        679
                        195
                        195
                        747
                        799
                        624
                        741
                        667
                        315
                        492
                        863
                        33
                        776
                        580
                        609
                        137
                        513
                        102
                        527
                        387
                        34
                        346
                        393
                        274
                        596
                        289
                        407
                        35
                        142
                        604
                        708
                        184
                        750
                        548
                        391
                        953
                        593
                        36
                        687
                        609
                        281
                        612
                        672
                        741
                        995
                        352
                        37
                        935
                        21
                        763
                        752
                        461
                        776
                        865
                        21
                        946
                        690
                        516
                        983
                        552
                        377
                        758
                        39
                        801
                        142
                        676
                        781
                        253
                        390
                        421
                        127
                        40
                        526
                        613
                        40
                        288
                        907
                        853
                        238
                        771
                        817
                        40
                        522
                        194
                        555
                        508
                        161
                        380
                        887
                        70
                        765
                        43
                        869
                        482
                        201
                        947
                        549
                        38
                        317
                        38
                        698
                        87
                        235
                        307
                        769
                        752
                        748
                        46
                        481
                        789
                        176
                        519
                        952
                        797
                        528
                        600
                        839
                        47
                        431
                        371
                        850
                        349
                        118
                        10
                        847
                        10
                        484
                        272
                        790
                        905
                        108
                        929
                        503
                        50
                        376
                        442
                        272
                        405
                        588
                        640
                        636
                        51
                        458
                        622
                        363
                        958
                        785
                        52
                        119
                        878
                        704
                        121
                        647
                        373
                        937
                        802
                        783
                        53
                        979
                        606
                        79
                        688
                        564
                        54
                        408
                        66
                        537
                        702
                        170
                        787
                        198
                        15
                        447
                        15
                        52
                        14
                        185
                        688
                        54
                        28
                        28
                        293
                        793
                        980
                        807
                        219
                        683
                        352
                        93
                        57
                        899
                        516
                        432
                        572
                        221
                        141
                        286
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

    public void Build() {
        while (true) {
            var edge = EdgeToCheck();
            Console.Error.WriteLine($"Checking {edge.A}-{edge.B}");
            TryFindEdge(edge, 1, out bool end);
            if (end) {
                return;
            }
        }
    }

    Edge EdgeToCheck() {
        foreach ((int a, int _) in connections.OrderBy(it => it.Value)) {
            foreach (int b in nodes.OrderBy(it => connections.ContainsKey(it))) {
                var edge = Edge.From(a, b);
                if (b != a && !query.Tried(edge)) {
                    return edge;
                }
            }
        }
        return Edge.From(1, 2);
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    bool TryFindEdge(Edge edge, int depth, out bool end) {
        end = false;
        Console.Error.WriteLine($"{new string(' ', depth)}depth: {depth}");
        if (edges.Contains(edge)) {
            Console.Error.WriteLine($"{new string(' ', depth)}Already found edge: {edge.A}-{edge.B}");
            return false;
        }
        if (!query.Try(edge, out int answer)) {
            Console.Error.WriteLine($"{new string(' ', depth)}Already did query {edge.A} {edge.B}");
            return false;
        }
        // Console.Error.WriteLine($"{new string(' ', depth)}=> {answer}");
        if (answer == edge.A) {
            AddEdge(edge, out end);
            Console.Error.WriteLine($"{new string(' ', depth)}Found edge: {edge.A}-{edge.B}, all ({edges.Count}):");
            Console.Error.WriteLine($"queries: {query.Count} / {query.Max}");
            // Console.Error.WriteLine($"{new string(' ', depth + 1)}{string.Join(", ", edges.Select(it => $"{it.A} {it.B}"))}");
            return true;
        }
        Console.Error.WriteLine($"{new string(' ', depth)}Found middle: {edge.A}- {answer} -{edge.B}");
        return TryFindEdge(Edge.From(edge.A, answer), depth + 1, out end) || TryFindEdge(Edge.From(answer, edge.B), depth + 1, out end);
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
    readonly HashSet<Edge> queries = [];
    public int Count => queries.Count;
    public int Max { get; } = 15 * nodes;

    public bool Tried(Edge edge) => queries.Contains(edge);

    /// Checks which node x minimizes |d(a,x)−d(b,x)|, where d(x,y) is the distance between nodes x and y.
    /// If more than one such node exists, returns the one which minimizes d(a,x).
    public bool Try(Edge edge, out int answer) {
        if (queries.Count > Max) {
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

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }
}
