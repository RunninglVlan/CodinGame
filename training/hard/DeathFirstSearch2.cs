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
        var link = CutGatewayLink(nodes[agent]);
        Console.WriteLine(link);
    }

    Link CutGatewayLink(Node agent) {
        (Link? link, int futurePath, int nodePath) best = (null, int.MinValue, int.MaxValue);
        foreach (var link in GatewayLinks()) {
            link.Cut();
            var agentPath = pathfinding.Path(agent, it => gateways.Contains(it));
            if (agentPath == null) {
                return link;
            }
            var futureAgent = nodes[agentPath.First().i];
            int path = pathfinding.Path(agent, it => gateways.Contains(it))?.Count ?? int.MinValue;
            int nodePath = pathfinding.Path(futureAgent, link.Node)?.Count ?? int.MaxValue;
            int futureBestPath = FutureBestPath(futureAgent, path);
            if (futureBestPath > best.futurePath
                    || futureBestPath == best.futurePath && link.GatewayCount > best.link?.GatewayCount
                    || link.GatewayCount == best.link?.GatewayCount && nodePath < best.nodePath) {
                best = (link, futureBestPath, nodePath);
            }
            link.Restore();
        }
        var bestLink = best.link!.Value;
        bestLink.Cut();
        return bestLink;
    }

    List<Link> GatewayLinks() {
        var links = new List<Link>();
        foreach (var node in nodes) {
            Node? gateway = null;
            var gatewayCount = 0;
            foreach (var connectedGateway in node.connections.Where(it => gateways.Contains(it.i))) {
                gateway = connectedGateway;
                gatewayCount++;
            }
            if (gateway == null) {
                continue;
            }
            links.Add(new Link(gateway, node, gatewayCount));
        }
        return links;
    }

    int FutureBestPath(Node agent, int previousPath) {
        int bestPath = previousPath;
        foreach (var link in GatewayLinks()) {
            link.Cut();
            int path = pathfinding.Path(agent, it => gateways.Contains(it))?.Count ?? int.MaxValue;
            if (path > bestPath) {
                bestPath = path;
            }
            link.Restore();
        }
        return bestPath;
    }
}

class Node(int i) {
    public readonly int i = i;
    public readonly List<Node> connections = new();
}

readonly record struct Link(Node Gateway, Node Node, int GatewayCount) {
    public void Cut() {
        Gateway.connections.Remove(Node);
        Node.connections.Remove(Gateway);
    }

    public void Restore() {
        Gateway.connections.Add(Node);
        Node.connections.Add(Gateway);
    }

    public override string ToString() => $"{Gateway.i} {Node.i}";
}

class Pathfinding {
    readonly List<Node> path = new();
    readonly Queue<PathNode> queue = new();
    readonly HashSet<int> visited = new();

    /// BFS to end
    public List<Node>? Path(Node start, Node end) {
        return Path(start, i => i == end.i);
    }

    /// BFS to endPredicate
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
                var neighbour = new PathNode(node) { parent = current };
                queue.Enqueue(neighbour);
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
