using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace TicketToRideEurope;

/// https://www.codingame.com/training/expert/ticket-to-ride-europe
static class TicketToRideEurope {
    static void Main() {
        var game = new Game();
        game.ReadInput();
        Console.WriteLine(game.CalculateScore());
    }
}

record struct GameState(int RouteScore, int TicketScore,
        int TrainCars, Dictionary<Card, int> Cards, List<Route> CompletedRoutes) {
    public int Score => RouteScore + TicketScore;

    public static GameState Copy(GameState state) => new(state.RouteScore, state.TicketScore, state.TrainCars,
            new Dictionary<Card, int>(state.Cards), [..state.CompletedRoutes]);

    public override string ToString() {
        var text = new StringBuilder();
        text.AppendLine($" Score: {Score} = {RouteScore} + {TicketScore}");
        text.AppendLine($" TrainCars: {TrainCars}");
        text.AppendLine(' ' + string.Join(", ", Cards.Select(it => $"{it.Key}: {it.Value}")));
        text.Append(string.Join(Environment.NewLine, CompletedRoutes.Select(it => $"  ✅ {it.ToShort()}")));
        return text.ToString();
    }
}

class Game {
    readonly Pathfinding<City> pathfinding = new();

    /// Resource #1
    int trainCars;

    /// Resource #2
    readonly Dictionary<Card, int> cards = new();

    readonly List<Ticket> tickets = [];
    readonly List<Route> routes = [];
    readonly Dictionary<string, City> cities = new();
    Dictionary<Ticket, List<List<Route>>> ticketRoutes = null!;

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
        ticketRoutes = tickets
                .ToDictionary(it => it, it => CanConnectCities(it, out var foundRoutes) ? foundRoutes : [[]]);
    }

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

    bool CanConnectCities(Ticket ticket, out List<List<Route>> foundRoutes) {
        foundRoutes = null!;
        if (!cities.TryGetValue(ticket.CityFrom, out var cityFrom)) {
            Console.Error.WriteLine($"No route with city {ticket.CityFrom}");
            return false;
        }
        if (!cities.TryGetValue(ticket.CityTo, out var cityTo)) {
            Console.Error.WriteLine($"No route with city {ticket.CityTo}");
            return false;
        }
        var paths = pathfinding.Paths(cityFrom, cityTo).ToList();
        if (paths.Count == 0) {
            Console.Error.WriteLine($"No path found from {ticket.CityFrom} to {ticket.CityTo}");
            return false;
        }
        foundRoutes = paths.Select(path => path.Zip(path.Skip(1), (a, b) => routes.First(it =>
                                it.CityA == a.Name && it.CityB == b.Name || it.CityB == a.Name && it.CityA == b.Name))
                        .OrderByDescending(it => it.Length)
                        .ToList())
                .ToList();
        return true;
    }

    public int CalculateScore() {
        var best = int.MinValue;
        var current = new GameState(0, 0, trainCars, new Dictionary<Card, int>(cards), []);
        Backtrack();
        return best;

        void Backtrack() {
            current.TicketScore = TicketScore(current.CompletedRoutes);
            if (current.Score > best) {
                best = current.Score;
                Console.Error.WriteLine("New best");
                Console.Error.WriteLine(current);
            }
            for (var i = 0; i < routes.Count; i++) {
                var route = routes[i];
                if (current.TrainCars < route.Length) {
                    continue;
                }
                var previous = GameState.Copy(current);
                routes.RemoveAt(i);
                if (route.TryComplete(current.Cards)) {
                    current.RouteScore += route.Score;
                    current.TrainCars -= route.Length;
                    current.CompletedRoutes.Add(route);
                    Backtrack();
                    current = previous;
                }
                routes.Insert(i, route);
            }
        }
    }

    int TicketScore(List<Route> completedRoutes) {
        var score = 0;
        foreach (var (ticket, paths) in ticketRoutes) {
            bool completed = paths.Any(it => it.Count > 0 && it.All(completedRoutes.Contains));
            score += completed ? ticket.Points : -ticket.Points;
        }
        return score;
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

    public int Score => lengthScore[Length];

    public bool TryComplete(Dictionary<Card, int> cards) {
        if (Color == Card.Gray) {
            return TryCompleteGray(cards);
        }
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
    /// DFS
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public IEnumerable<List<T>> Paths(T start, T end) {
        return Walk(start, end, [start], [start.Id]);
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    static IEnumerable<List<T>> Walk(T current, T end, List<T> path, HashSet<int> visited) {
        if (current.Id == end.Id) {
            yield return [..path];
            yield break;
        }
        foreach (var next in current.Connections) {
            if (visited.Contains(next.Id)) {
                continue;
            }
            foreach (var result in Walk(next, end, [..path, next], [..visited, next.Id])) {
                yield return result;
            }
        }
    }
}

interface INode<T> where T : INode<T> {
    int Id { get; }
    List<T> Connections { get; }
}
