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
        var paths = pathfinding.ShortestPaths(agent, i => gateways.Contains(i));
        (Node? gateway, Node? node, int links) important = (null, null, int.MinValue);
        foreach (var path in paths) {
            var pathGateway = path[^1];
            var pathNode = path[^2];
            int gatewayLinks = pathNode.connections.Count(it => gateways.Contains(it.i));
            if (gatewayLinks > important.links) {
                important = (pathGateway, pathNode, gatewayLinks);
            }
        }
        var (gateway, node) = (important.gateway, important.node);
        if (gateway == null || node == null) {
            throw new ArgumentException("Couldn't find path to agent");
        }
        gateway.connections.Remove(node);
        node.connections.Remove(gateway);
        return $"{gateway.i} {node.i}";
    }
}

class Node(int i) {
    public readonly int i = i;
    public readonly List<Node> connections = new();
}

class Pathfinding {
    readonly List<List<Node>> paths = new();
    readonly Queue<PathNode> queue = new();
    readonly HashSet<int> visited = new();

    /// BFS
    public List<List<Node>> ShortestPaths(Node start, Predicate<int> endPredicate) {
        paths.Clear();
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.i);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.node.i)) {
                var path = Reconstruct(current);
                if (paths.Count > 0 && path.Count > paths.First().Count) {
                    return paths;
                }
                paths.Add(path);
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
        return paths;
    }

    static List<Node> Reconstruct(PathNode end) {
        var path = new List<Node>();
        var current = end;
        while (current != null) {
            path.Add(current.node);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }
}

class PathNode(Node node) {
    public readonly Node node = node;
    public PathNode? parent;
}
