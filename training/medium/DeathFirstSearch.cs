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
    readonly Pathfinding pathfinding = new();

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
        int agent = int.Parse(Console.ReadLine()!);
        Console.WriteLine(CutGatewayLink(nodes[agent]));
    }

    string CutGatewayLink(Node agent) {
        (Node? gateway, int i, int length) closest = (null, -1, int.MaxValue);
        foreach (var gateway in nodes.Where(it => it is { gateway: true, connections.Count: > 0 })) {
            for (var i = 0; i < gateway.connections.Count; i++) {
                var connection = gateway.connections[i];
                int? length = pathfinding.PathLength(connection, agent);
                if (length == null || length >= closest.length) {
                    continue;
                }
                closest = (gateway, i, length.Value);
            }
        }
        if (closest.gateway == null) {
            throw new ArgumentException("Couldn't find path to agent");
        }
        var node = closest.gateway.connections[closest.i];
        closest.gateway.connections.RemoveAt(closest.i);
        node.connections.Remove(closest.gateway);
        return $"{closest.gateway.i} {node.i}";
    }
}

class Node(int i) {
    public readonly int i = i;
    public readonly List<Node> connections = new();
    public bool gateway;
}

class Pathfinding {
    readonly Queue<PathNode> queue = new();
    readonly Dictionary<int, PathNode> visited = new();

    /// BFS
    public int? PathLength(Node start, Node end) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.i, startNode);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (current.node == end) {
                return CountNodes(startNode, current);
            }
            foreach (var node in current.node.connections) {
                if (visited.ContainsKey(node.i)) {
                    continue;
                }
                var neighbour = new PathNode(node) { parent = current };
                queue.Enqueue(neighbour);
                visited.Add(node.i, neighbour);
            }
        }
        return null;
    }

    static int CountNodes(PathNode start, PathNode end) {
        var length = 0;
        var current = end;
        while (current != start) {
            length++;
            current = current?.parent;
        }
        return length;
    }
}

class PathNode(Node node) {
    public readonly Node node = node;
    public PathNode? parent;
}
