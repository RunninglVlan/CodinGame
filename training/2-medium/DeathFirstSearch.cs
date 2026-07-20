using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
        var path = pathfinding.Path(agent, i => gateways.Contains(i));
        if (path == null) {
            throw new ArgumentException("Couldn't find path from agent to gateway");
        }
        var node = path.First();
        agent.connections.Remove(node);
        node.connections.Remove(agent);
        return $"{agent.i} {node.i}";
    }
}

class Node(int i) {
    public readonly int i = i;
    public readonly List<Node> connections = new();
}

class Pathfinding {
    readonly List<Node> path = new();
    readonly Queue<PathNode> queue = new();
    readonly HashSet<int> visited = new();

    /// BFS
    public List<Node>? Path(Node start, Predicate<int> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.i);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.node.i)) {
                return Reconstruct(startNode, current);
            }
            foreach (var node in current.node.connections) {
                if (visited.Contains(node.i)) {
                    continue;
                }
                var neighbor = new PathNode(node) { parent = current };
                queue.Enqueue(neighbor);
                visited.Add(node.i);
            }
        }
        return null;
    }

    List<Node> Reconstruct(PathNode start, PathNode end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.node);
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
