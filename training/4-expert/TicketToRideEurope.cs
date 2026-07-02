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

class GameState(int routeLength) : IState {
    public int RouteScore { get; set; }
    public int TicketScore { get; set; }
    public int Score => RouteScore + TicketScore;
    public int TrainCars { get; set; }
    public Dictionary<Card, int> Cards { get; set; } = null!;
    public bool[] CompletedRoutes { get; } = new bool[routeLength];

    public void Load(GameState from) {
        RouteScore = from.RouteScore;
        TrainCars = from.TrainCars;
        Cards = new Dictionary<Card, int>(from.Cards);
        Array.Copy(from.CompletedRoutes, CompletedRoutes, CompletedRoutes.Length);
    }

    public bool Equals(IState? other) {
        if (other is not GameState that) {
            return false;
        }
        if (ReferenceEquals(this, that)) {
            return true;
        }
        if (TrainCars != that.TrainCars || !CompletedRoutes.SequenceEqual(that.CompletedRoutes)) {
            return false;
        }
        return Cards.Keys.All(type => Cards[type] == that.Cards[type]);
    }

    public override bool Equals(object? other) => Equals(other as IState);

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode() {
        var hash = new HashCode();
        hash.Add(TrainCars);
        foreach (int count in Cards.Values) {
            hash.Add(count);
        }
        foreach (bool route in CompletedRoutes) {
            hash.Add(route);
        }
        return hash.ToHashCode();
    }
}

class Game : IGame<GameState> {
    readonly Pathfinding<City> pathfinding = new();
    readonly BeamSearch<GameState> beamSearch;

    /// Resource #1
    int trainCars;

    /// Resource #2
    readonly Dictionary<Card, int> cards = new();

    readonly List<Ticket> tickets = new();
    readonly List<Route> routes = new();
    readonly Dictionary<string, City> cities = new();
    Dictionary<Ticket, List<Route>> ticketRoutes = null!;

    public Game() {
        beamSearch = new BeamSearch<GameState>(this);
    }

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
        ticketRoutes = tickets
                .ToDictionary(it => it, it => CanConnectCities(it, out var foundRoutes) ? foundRoutes : []);
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

    public int CalculateScore() {
        var start = new GameState(routes.Count) {
            TrainCars = trainCars,
            Cards = new Dictionary<Card, int>(cards)
        };
        start.TicketScore = TicketScore(start.CompletedRoutes);
        return beamSearch.FindBest(start, routes.Count).Score;
    }

    IEnumerable<GameState> IGame<GameState>.Next(GameState current) {
        for (var i = 0; i < current.CompletedRoutes.Length; i++) {
            if (current.CompletedRoutes[i]) {
                continue;
            }
            var route = routes[i];
            var next = new GameState(routes.Count);
            next.Load(current);
            if (!route.TryComplete(next, out int points)) {
                continue;
            }
            next.CompletedRoutes[i] = true;
            next.RouteScore += points;
            next.TicketScore = TicketScore(next.CompletedRoutes);
            yield return next;
        }
    }

    int TicketScore(bool[] completedRoutes) {
        var score = 0;
        foreach (var (ticket, path) in ticketRoutes) {
            score += Completed(path) ? ticket.Points : -ticket.Points;
        }
        return score;

        bool Completed(List<Route> path) {
            if (path.Count == 0) {
                return false;
            }
            int uncompleted = path.Count;
            for (var i = 0; i < completedRoutes.Length; i++) {
                if (completedRoutes[i] && path.Contains(routes[i])) {
                    uncompleted--;
                }
            }
            return uncompleted == 0;
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

    public bool TryComplete(GameState state, out int points) {
        points = 0;
        if (state.TrainCars < Length) {
            return false;
        }
        if (Color != Card.Gray) {
            if (state.Cards[Color] + state.Cards[Card.Engine] < Length) {
                return false;
            }
            int requiredCards = state.Cards[Color];
            if (requiredCards >= Length) {
                state.Cards[Color] -= Length;
            } else {
                state.Cards[Color] = 0;
                state.Cards[Card.Engine] -= Length - requiredCards;
            }
            return Complete(state, out points);
        }
        var cardsCopy = new Dictionary<Card, int>(state.Cards);
        if (!TryCompleteGray(cardsCopy)) {
            return false;
        }
        foreach ((var color, int value) in cardsCopy) {
            state.Cards[color] = value;
        }
        return Complete(state, out points);
    }

    bool Complete(GameState state, out int points) {
        state.TrainCars -= Length;
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

class BeamSearch<T>(IGame<T> game) where T : class, IState {
    readonly HashSet<T> visited = new();

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public T FindBest(T start, int levels, int beamWidth = 10) {
        var best = start;
        var currentLevel = new List<T> { start };
        for (var level = 0; level < levels; level++) {
            var nextLevel = new List<T>();
            visited.Clear();
            foreach (var current in currentLevel) {
                nextLevel.AddRange(game.Next(current));
            }
            currentLevel.Clear();
            nextLevel.Sort((a, b) => b.Score.CompareTo(a.Score));
            foreach (var state in nextLevel) {
                if (!visited.Add(state)) {
                    continue;
                }
                if (currentLevel.Count >= beamWidth) {
                    break;
                }
                currentLevel.Add(state);
            }
            if (currentLevel.Count == 0) {
                break;
            }
            var bestCurrent = currentLevel.First();
            if (best.Score < bestCurrent.Score) {
                best = bestCurrent;
            }
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<TState> Next(TState current);
}

interface IState : IEquatable<IState> {
    int Score { get; }
}
