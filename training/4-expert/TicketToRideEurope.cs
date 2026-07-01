using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TicketToRideEurope;

/// https://www.codingame.com/training/expert/ticket-to-ride-europe
static class TicketToRideEurope {
    static void Main() {
        var game = new Game();
        game.ReadInput();
        Console.WriteLine(game.CalculateScore());
    }
}

class Game {
    readonly Pathfinding<City> pathfinding = new();

    /// Resource #1
    int trainCars;

    /// Resource #2
    readonly Dictionary<Card, int> cards = new();

    readonly List<Ticket> tickets = new();
    readonly List<Route> routes = new();
    readonly Dictionary<string, City> cities = new();

    public void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        trainCars = Math.Clamp(int.Parse(inputs[0]), 0, 29);
        int ticketLength = Math.Clamp(int.Parse(inputs[1]), 0, 29);
        int routeLength = Math.Clamp(int.Parse(inputs[2]), 0, 29);
        inputs = Console.ReadLine()!.Split(' ');
        foreach (var type in Enum.GetValues<Card>()) {
            if (type == Card.Gray) {
                break;
            }
            cards[type] = int.Parse(inputs[(int)type]);
        }
        LogCards();
        for (var i = 0; i < ticketLength; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int points = int.Parse(inputs[0]);
            string cityA = inputs[1];
            string cityB = inputs[2];
            var ticket = new Ticket(points, cityA, cityB);
            tickets.Add(ticket);
            Console.Error.WriteLine(ticket);
        }
        ReadRoutes(routeLength);
    }

    void LogCards() => Console.Error.WriteLine(string.Join(", ", cards.Select(it => $"{it.Key}: {it.Value}")));

    void ReadRoutes(int routeLength) {
        for (var i = 0; i < routeLength; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int length = int.Parse(inputs[0]);
            int requiredEngines = int.Parse(inputs[1]);
            string color = inputs[2];
            string cityAName = inputs[3];
            string cityBName = inputs[4];
            var route = new Route(length, requiredEngines, Enum.Parse<Card>(color), cityAName, cityBName);
            routes.Add(route);
            Console.Error.WriteLine(route);
            if (!cities.TryGetValue(cityAName, out var cityA)) {
                cities[cityAName] = cityA = new City(cityAName);
            }
            if (!cities.TryGetValue(cityBName, out var cityB)) {
                cities[cityBName] = cityB = new City(cityBName);
            }
            cities[cityAName].Connections.Add(cityB);
            cities[cityBName].Connections.Add(cityA);
        }
    }

    public int CalculateScore() {
        var ticketRoutes = tickets
                .ToDictionary(it => it, it => CanConnectCities(it, out var foundRoutes) ? foundRoutes : []);
        return BestScore(ticketRoutes);
    }

    bool CanConnectCities(Ticket ticket, out List<Route> foundRoutes) {
        foundRoutes = null!;
        if (!cities.TryGetValue(ticket.CityFrom, out var cityFrom)) {
            Console.Error.WriteLine($"No route with city {ticket.CityFrom}");
            return false;
        }
        if (!cities.TryGetValue(ticket.CityTo, out var cityTo)) {
            Console.Error.WriteLine($"No route with city {ticket.CityTo}");
            return false;
        }
        var path = pathfinding.Path(cityFrom, cityTo);
        if (path == null) {
            Console.Error.WriteLine($"No path found from {ticket.CityFrom} to {ticket.CityTo}");
            return false;
        }
        foundRoutes = path.Zip(path.Skip(1), (a, b) => routes.First(it =>
                        it.CityA == a.Name && it.CityB == b.Name || it.CityB == a.Name && it.CityA == b.Name))
                .OrderByDescending(it => it.Length)
                .ToList();
        return true;
    }

    int BestScore(Dictionary<Ticket, List<Route>> ticketRoutes) {
        var best = int.MinValue;
        Backtrack(trainCars, cards, 0, new HashSet<Route>(), 0);
        return best;

        void Backtrack(int bTrainCars, Dictionary<Card, int> bCards,
                int startIndex, HashSet<Route> completedRoutes, int routeScore) {
            var ticketScore = 0;
            foreach (var (ticket, path) in ticketRoutes) {
                bool completed = path.Count > 0 && path.All(completedRoutes.Contains);
                ticketScore += completed ? ticket.Points : -ticket.Points;
            }
            if (routeScore + ticketScore > best) {
                best = routeScore + ticketScore;
            }
            for (int i = startIndex; i < routes.Count; i++) {
                int trainCarsCopy = bTrainCars;
                var cardsCopy = new Dictionary<Card, int>(bCards);
                if (!routes[i].TryComplete(ref trainCarsCopy, cardsCopy, out int points)) {
                    continue;
                }
                completedRoutes.Add(routes[i]);
                Backtrack(trainCarsCopy, cardsCopy, i + 1, completedRoutes, routeScore + points);
                completedRoutes.Remove(routes[i]);
            }
        }
    }
}

enum Card {
    Red,
    Yellow,
    Green,
    Blue,
    White,
    Black,
    Orange,
    Pink,

    /// Wild card - can be used as a substitute for any colored card
    Engine,

    Gray
}

record Ticket(int Points, string CityFrom, string CityTo) {
    public override string ToString() => $"{nameof(Ticket)} {{ P: {Points}, {CityFrom} -> {CityTo} }}";
    public string ToShort() => $"T({Points} {CityFrom[..2]}->{CityTo[..2]})";
}

record Route(int Length, int RequiredEngines, Card Color, string CityA, string CityB) {
    readonly Dictionary<int, int> lengthScore = new() {
        { 1, 1 },
        { 2, 2 },
        { 3, 4 },
        { 4, 7 },
        { 6, 15 }
    };

    public bool TryComplete(ref int trainCars, Dictionary<Card, int> cards, out int points) {
        points = 0;
        if (trainCars < Length) {
            return false;
        }
        if (Color != Card.Gray) {
            if (cards[Color] + cards[Card.Engine] < Length) {
                return false;
            }
            int requiredCards = cards[Color];
            if (requiredCards >= Length) {
                cards[Color] -= Length;
            } else {
                cards[Color] = 0;
                cards[Card.Engine] -= Length - requiredCards;
            }
            return Complete(ref trainCars, out points);
        }
        var cardsCopy = new Dictionary<Card, int>(cards);
        if (!TryCompleteGray(cardsCopy)) {
            return false;
        }
        foreach ((var color, int value) in cardsCopy) {
            cards[color] = value;
        }
        return Complete(ref trainCars, out points);
    }

    bool Complete(ref int trainCars, out int points) {
        trainCars -= Length;
        points = lengthScore[Length];
        return true;
    }

    bool TryCompleteGray(Dictionary<Card, int> cards) {
        int required = Length;
        if (RequiredEngines > 0) {
            if (cards[Card.Engine] < RequiredEngines) {
                return false;
            }
            required -= RequiredEngines;
            cards[Card.Engine] -= RequiredEngines;
        }
        if (UseNonEngineCards()) {
            return true;
        }
        int used = Math.Min(required, cards[Card.Engine]);
        cards[Card.Engine] -= used;
        required -= used;
        return required == 0 || UseNonEngineCards();

        bool UseNonEngineCards() {
            foreach ((var color, int count) in cards) {
                if (color == Card.Engine || count < required) {
                    continue;
                }
                cards[color] -= required;
                return true;
            }
            return false;
        }
    }

    public override string ToString() {
        return $"{nameof(Route)} {{ L: {Length}, RE: {RequiredEngines}, C: {Color}, {CityA} - {CityB} }}";
    }

    public string ToShort() => $"R({Length} {CityA[..2]}-{CityB[..2]})";
}

record City(string Name) : INode<City> {
    public int Id { get; } = Name.GetHashCode();
    public List<City> Connections { get; } = [];
}

class Pathfinding<T> where T : INode<T> {
    readonly Queue<T> queue = new();
    readonly Dictionary<int, T> parents = new();
    readonly HashSet<int> visited = new();

    /// BFS to end
    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public List<T>? Path(T start, T end) {
        queue.Clear();
        parents.Clear();
        visited.Clear();
        queue.Enqueue(start);
        visited.Add(start.Id);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (current.Id == end.Id) {
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
        path.Add(start);
        path.Reverse();
        return path;
    }
}

interface INode<T> where T : INode<T> {
    int Id { get; }
    List<T> Connections { get; }
}
