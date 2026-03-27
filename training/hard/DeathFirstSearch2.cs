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
        var gatewayLinks = GatewayLinks();
        (Link? link, int path, int nodePath) best = (null, int.MinValue, int.MaxValue);
        foreach (var link in gatewayLinks) {
            link.Cut();
            int path = pathfinding.PathLength(agent, it => gateways.Contains(it)) ?? int.MinValue;
            int nodePath = pathfinding.PathLength(agent, link.Node) ?? int.MaxValue;
            if (path > best.path
                    || path == best.path && link.GatewayCount > best.link?.GatewayCount
                    || link.GatewayCount == best.link?.GatewayCount && nodePath < best.nodePath) {
                best = (link, path, nodePath);
            }
            link.Restore();
        }
        var bestLink = best.link ?? gatewayLinks.First();
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
    readonly Queue<PathNode> queue = new();
    readonly HashSet<int> visited = new();

    /// BFS to end
    public int? PathLength(Node start, Node end) {
        return PathLength(start, i => i == end.i);
    }

    /// BFS to endPredicate
    public int? PathLength(Node start, Predicate<int> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new PathNode(start);
        queue.Enqueue(startNode);
        visited.Add(start.i);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.node.i)) {
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
