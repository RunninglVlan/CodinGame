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
    readonly Pathfinding<Node> pathfinding = new();

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
            nodes[node1].Connections.Add(nodes[node2]);
            nodes[node2].Connections.Add(nodes[node1]);
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
            var path = pathfinding.Path(agent, it => gateways.Contains(it));
            if (path == null) {
                return link;
            }
            var futureAgent = nodes[path.First().Id];
            int pathLength = path.Count;
            int nodePath = pathfinding.Path(futureAgent, link.Node)?.Count ?? int.MaxValue;
            int futureBestPath = FutureBestPath(futureAgent, pathLength);
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
            foreach (var connectedGateway in node.Connections.Where(it => gateways.Contains(it.Id))) {
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

class Node(int i) : INode<Node> {
    public int Id { get; } = i;
    public List<Node> Connections { get; } = new();
}

readonly record struct Link(Node Gateway, Node Node, int GatewayCount) {
    public void Cut() {
        Gateway.Connections.Remove(Node);
        Node.Connections.Remove(Gateway);
    }

    public void Restore() {
        Gateway.Connections.Add(Node);
        Node.Connections.Add(Gateway);
    }

    public override string ToString() => $"{Gateway.Id} {Node.Id}";
}

class Pathfinding<T> where T : INode<T> {
    readonly List<T> path = new();
    readonly Queue<PathNode> queue = new();
    readonly HashSet<int> visited = new();

    /// BFS to end
    public List<T>? Path(T start, T end) {
        return Path(start, i => i == end.Id);
    }

    /// BFS to endPredicate
    public List<T>? Path(T start, Predicate<int> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.Id);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.node.Id)) {
                return Reconstruct(startNode, current);
            }
            foreach (var node in current.node.Connections) {
                if (visited.Contains(node.Id)) {
                    continue;
                }
                var neighbor = new PathNode(node) { parent = current };
                queue.Enqueue(neighbor);
                visited.Add(node.Id);
            }
        }
        return null;
    }

    List<T> Reconstruct(PathNode start, PathNode end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.node);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    class PathNode(T node) {
        public readonly T node = node;
        public PathNode? parent;
    }
}

interface INode<T> where T : INode<T> {
    int Id { get; }
    List<T> Connections { get; }
}
