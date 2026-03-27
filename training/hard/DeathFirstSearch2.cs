using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DeathFirstSearch2;

/// https://www.codingame.com/training/hard/death-first-search-episode-2
static class DeathFirstSearch2 {
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
    readonly HashSet<int> gateways = new();
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
            gateways.Add(gateway);
        }
    }

    public void Loop() {
        int agent = int.Parse(Console.ReadLine()!);
        Console.WriteLine(CutGatewayLink(nodes[agent]));
    }

    string CutGatewayLink(Node agent) {
        var dangerousNode = FindDangerousNodes(agent).First();
        var node = nodes[dangerousNode.I];
        var gateway = node.connections.First(it => gateways.Contains(it.i));
        gateway.connections.Remove(node);
        node.connections.Remove(gateway);
        return $"{gateway.i} {node.i}";
    }

    IEnumerable<DangerousNode> FindDangerousNodes(Node agent) {
        return Nodes()
            .OrderByDescending(it => it.Path == 0)
            .ThenByDescending(it => it.GatewayLinks)
            .ThenBy(it => it.Path);

        IEnumerable<DangerousNode> Nodes() {
            foreach (var node in nodes) {
                int gatewayLinks = node.connections.Count(it => gateways.Contains(it.i));
                if (gatewayLinks <= 0) {
                    continue;
                }
                int? path = node == agent
                    ? 0
                    : pathfinding.PathLength(node, agent);
                if (path == null) {
                    continue;
                }
                yield return new DangerousNode(node.i, gatewayLinks, path.Value);
            }
        }
    }
}

class Node(int i) {
    public readonly int i = i;
    public readonly List<Node> connections = new();
}

record struct DangerousNode(int I, int GatewayLinks, int Path);

class Pathfinding {
    readonly Queue<PathNode> queue = new();
    readonly HashSet<int> visited = new();

    /// BFS
    public int? PathLength(Node start, Node end) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.i);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (current.node == end) {
                return CountLinks(current);
            }
            foreach (var node in current.node.connections) {
                if (visited.Contains(node.i)) {
                    continue;
                }
                var neighbour = new PathNode(node) { parent = current };
                queue.Enqueue(neighbour);
                visited.Add(node.i);
            }
        }
        return null;
    }

    static int CountLinks(PathNode end) {
        var length = 0;
        var current = end;
        while (current != null) {
            length++;
            current = current.parent;
        }
        return length - 1;
    }
}

class PathNode(Node node) {
    public readonly Node node = node;
    public PathNode? parent;
}
