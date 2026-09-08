using System;
using System.Collections.Generic;
using System.Linq;

// Visual
// https://www.codingame.com/training/hard/death-first-search-episode-2
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game : IGraph<Node> {
    Node[] nodes = null!;
    readonly HashSet<int> gateways = [];
    readonly Pathfinding<Node> pathfinding;

    public Game() {
        pathfinding = new Pathfinding<Node>(this);
    }

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

    IEnumerable<Node> IGraph<Node>.Next(Node current) {
        return current.Connections;
    }
}

record Node(int Id) {
    public List<Node> Connections { get; } = [];

    public virtual bool Equals(Node? other) => Id == other?.Id;
    public override int GetHashCode() => Id;
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

class Pathfinding<T>(IGraph<T> graph) where T : IEquatable<T> {
    readonly Dictionary<T, T> parents = new();

    /// BFS to end
    public List<T>? Path(T start, T end) {
        return Path(start, it => it.Equals(end));
    }

    /// BFS to endPredicate
    public List<T>? Path(T start, Predicate<T> endPredicate) {
        Queue<T> queue = [];
        HashSet<T> visited = [];
        parents.Clear();
        queue.Enqueue(start);
        visited.Add(start);
        while (queue.TryDequeue(out var current)) {
            if (endPredicate(current)) {
                return Reconstruct(start, current);
            }
            foreach (var next in graph.Next(current)) {
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue(next);
                parents[next] = current;
            }
        }
        return null;
    }

    List<T> Reconstruct(T start, T end) {
        var path = new List<T>();
        var current = end;
        while (!current.Equals(start)) {
            path.Add(current);
            current = parents[current];
        }
        path.Reverse();
        return path;
    }
}

interface IGraph<T> {
    IEnumerable<T> Next(T current);
}
