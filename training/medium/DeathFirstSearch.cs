using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DeathFirstSearch;

/// https://www.codingame.com/training/medium/death-first-search-episode-1
static class DeathFirstSearch {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
        }
    }
}

class Game {
    Node[] nodes = null!;

    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int nodeCount = int.Parse(inputs[0]);
        nodes = new Node[nodeCount];
        for (var i = 0; i < nodeCount; i++) {
            nodes[i] = new Node(i);
        }
        int linkCount = int.Parse(inputs[1]);
        int exits = int.Parse(inputs[2]);
        for (var i = 0; i < linkCount; i++) {
            string[] link = Console.ReadLine()!.Split(' ');
            int node1 = int.Parse(link[0]);
            int node2 = int.Parse(link[1]);
            nodes[node1].connections.Add(nodes[node2]);
            nodes[node2].connections.Add(nodes[node1]);
        }
        for (var i = 0; i < exits; i++) {
            int gateway = int.Parse(Console.ReadLine()!);
            nodes[gateway].gateway = true;
        }
    }

    public void Loop() {
        int bobnet = int.Parse(Console.ReadLine()!);
        Console.WriteLine(CutGatewayLink());
    }

    string CutGatewayLink() {
        var gateway = nodes.First(it => it is { gateway: true, connections.Count: > 0 });
        var node = gateway.connections.First();
        gateway.connections.RemoveAt(0);
        node.connections.Remove(gateway);
        return $"{gateway.i} {node.i}";
    }
}

class Node(int i) {
    public readonly int i = i;
    public readonly List<Node> connections = new();
    public bool gateway;
}
