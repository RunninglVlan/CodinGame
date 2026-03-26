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
        var gateway = nodes.First(it => it is { gateway: true, connections.Count: > 0 });
        (int i, int distance) closest = (-1, int.MaxValue);
        for (var i = 0; i < gateway.connections.Count; i++) {
            var connection = gateway.connections[i];
            var path = pathfinding.Path(connection, agent);
            if (path == null || path.Count >= closest.distance) {
                continue;
            }
            closest = (i, path.Count);
        }
        if (closest.i < 0) {
            throw new ArgumentException("Couldn't find path to agent");
        }
        var node = gateway.connections[closest.i];
        gateway.connections.RemoveAt(closest.i);
        node.connections.Remove(gateway);
        return $"{gateway.i} {node.i}";
    }
}

class Node(int i) {
    public readonly int i = i;
    public readonly List<Node> connections = new();
    public bool gateway;
}

class Pathfinding {
    readonly List<int> path = new();
    readonly Queue<PathNode> queue = new();
    readonly Dictionary<int, PathNode> visited = new();

    /// BFS
    public List<int>? Path(Node start, Node end) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.i, startNode);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (current.node == end) {
                return Reconstruct(startNode, current);
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

    List<int> Reconstruct(PathNode start, PathNode end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.node.i);
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
