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

record struct GameState(int Score, int TrainCars, Dictionary<Card, int> Cards, List<Route> CompletedRoutes) {
    public static GameState NewBest => new(int.MinValue, 0, null!, null!);

    public static GameState CurrentFrom(GameState state) {
        return new GameState(0, state.TrainCars, new Dictionary<Card, int>(state.Cards), []);
    }

    public override string ToString() {
        var text = new StringBuilder();
        text.AppendLine($"Score: {Score}");
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
    Dictionary<Card, int> cards = new();

    readonly List<Ticket> tickets = [];
    readonly List<Route> routes = [];
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
                .Select(it => (it, CanConnectCities(it, out var foundRoutes) ? foundRoutes : [[]]))
                .ToList();
        return BestTicketScore(ticketRoutes) + BestRouteScore();
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

    int BestTicketScore(List<(Ticket, List<List<Route>>)> ticketRoutes) {
        var start = new GameState(0, trainCars, cards, []);
        var best = GameState.NewBest;
        foreach (var permutation in Permutations(ticketRoutes)) {
            var current = GameState.CurrentFrom(start);
            foreach (var (ticket, path) in permutation) {
                var ticketBest = TicketBest(ticket, path, current);
                current.Score += ticketBest.Score;
                current.TrainCars = ticketBest.TrainCars;
                current.Cards = ticketBest.Cards;
                ticketBest.CompletedRoutes.ForEach(route => {
                    route.IsCompleted = true;
                    current.CompletedRoutes.Add(route);
                });
            }
            current.CompletedRoutes.ForEach(it => it.IsCompleted = false);
            if (current.Score <= best.Score) {
                continue;
            }
            best = current;
            Console.Error.WriteLine($"New ticket {best}");
        }
        trainCars = best.TrainCars;
        cards = best.Cards;
        best.CompletedRoutes.ForEach(it => it.IsCompleted = true);
        return best.Score;
    }

    static IEnumerable<List<T>> Permutations<T>(List<T> items) {
        if (items.Count <= 1) {
            yield return items;
            yield break;
        }
        for (var i = 0; i < items.Count; i++) {
            var rest = items.Where((_, index) => index != i).ToList();
            foreach (var permutation in Permutations(rest)) {
                yield return permutation.Prepend(items[i]).ToList();
            }
        }
    }

    static GameState TicketBest(Ticket ticket, List<List<Route>> path, GameState start) {
        var best = GameState.NewBest;
        foreach (var foundRoutes in path) {
            var current = GameState.CurrentFrom(start);
            bool completed = TryCompleteTicket(foundRoutes, ref current);
            current.Score += completed ? ticket.Points : -ticket.Points;
            current.CompletedRoutes.ForEach(it => it.IsCompleted = false);
            if (current.Score > best.Score) {
                best = current;
            }
        }
        return best;

        static bool TryCompleteTicket(List<Route> foundRoutes, ref GameState state) {
            bool completed = foundRoutes.Count > 0;
            foreach (var route in foundRoutes.Where(it => !it.IsCompleted)) {
                if (!route.TryComplete(ref state, out int points)) {
                    completed = false;
                    break;
                }
                state.Score += points;
                state.CompletedRoutes.Add(route);
            }
            return completed;
        }
    }

    int BestRouteScore() {
        var uncompletedRoutes = routes.Where(it => !it.IsCompleted).ToList();
        var best = int.MinValue;
        foreach (var subset in Subsets(uncompletedRoutes)) {
            var state = new GameState(0, trainCars, new Dictionary<Card, int>(cards), []);
            foreach (var route in subset.Where(it => !it.IsCompleted)) {
                if (!route.TryComplete(ref state, out int points)) {
                    continue;
                }
                state.Score += points;
                state.CompletedRoutes.Add(route);
            }
            state.CompletedRoutes.ForEach(it => it.IsCompleted = false);
            if (state.Score <= best) {
                continue;
            }
            best = state.Score;
            Console.Error.WriteLine($"New route {state}");
        }
        return best;
    }

    static IEnumerable<List<T>> Subsets<T>(List<T> items) {
        for (var mask = 0; mask < 1 << items.Count; mask++) {
            var subset = items.Where((_, i) => (mask & 1 << i) != 0).ToList();
            yield return subset;
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

    public bool IsCompleted { get; set; }

    public bool TryComplete(ref GameState state, out int points) {
        points = 0;
        if (state.TrainCars < Length) {
            return false;
        }
        if (Color == Card.Gray) {
            return TryCompleteGray(state.Cards) && Complete(ref state, out points);
        }
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
        return Complete(ref state, out points);
    }

    bool Complete(ref GameState state, out int points) {
        IsCompleted = true;
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
