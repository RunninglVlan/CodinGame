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
            var path = pathfinding.Path(agent, it => gateways.Contains(it.Id));
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
            int path = pathfinding.Path(agent, it => gateways.Contains(it.Id))?.Count ?? int.MaxValue;
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
    readonly Queue<T> queue = new();
    readonly Dictionary<int, T> parents = new();
    readonly HashSet<int> visited = new();

    /// BFS to end
    public List<T>? Path(T start, T end) {
        return Path(start, it => it.Id == end.Id);
    }

    /// BFS to endPredicate
    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public List<T>? Path(T start, Predicate<T> endPredicate) {
        queue.Clear();
        parents.Clear();
        visited.Clear();
        queue.Enqueue(start);
        visited.Add(start.Id);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current)) {
                return Reconstruct(start, current);
            }
            foreach (var next in current.Connections) {
                if (!visited.Add(next.Id)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next.Id] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        var path = new List<T>();
        var current = end;
        while (current.Id != start.Id) {
            path.Add(current);
            current = parents[current.Id];
        }
        path.Reverse();
        return path;
    }
}

interface INode<T> where T : INode<T> {
    int Id { get; }
    List<T> Connections { get; }
}
