using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace TicketToRideEurope;

/// https://www.codingame.com/training/expert/ticket-to-ride-europe
static class TicketToRideEurope {
    static void Main() {
        var stopwatch = Stopwatch.StartNew();
        (string input, int expected) testCase23 = (
                """
                16 5 15
                3 2 0 2 4 3 3 2 5
                3 Berlin Wien
                9 Berlin Roma
                3 London Paris
                5 Dieppe Marseille
                7 Brest Marseille
                2 1 Gray Dieppe London
                1 0 Pink Dieppe Paris
                4 0 Gray Paris Marseille
                3 0 Black Brest Paris
                4 0 Red Marseille Pamplona
                3 0 Gray Paris Zurich
                2 0 Pink Zurich Marseille
                3 0 Green Berlin Wien
                2 0 Gray Zagrab Wien
                2 0 Gray Zagrab Venezia
                2 0 Black Roma Venezia
                4 0 Orange London Edinburgh
                2 0 Orange Dieppe Brest
                1 0 Black Amsterdam Bruxelles
                2 2 Gray London Amsterdam
                """,
                41
        );
        (string input, int expected) testCase24 = (
                """
                24 5 20
                4 3 5 3 3 3 6 6 4
                21 Cadiz Stockholm
                8 Marseille Essen
                8 Madrid Zurich
                8 Madrid Dieppe
                7 Amsterdam Pamplona
                3 0 Orange Cadiz Madrid
                3 0 White Madrid Pamplona
                2 0 Yellow Madrid Barcelona
                2 0 Gray Barcelona Pamplona
                4 0 Green Pamplona Paris
                2 0 Pink Marseille Zurich
                3 0 Gray Zurich Paris
                1 0 Pink Paris Dieppe
                2 0 Red Paris Bruxelles
                2 0 Green Dieppe Bruxelles
                2 0 Blue Bruxelles Frankfurt
                2 0 Yellow Zurich Munchen
                2 0 Pink Munchen Frankfurt
                2 0 Green Essen Frankfurt
                3 0 Orange Paris Frankfurt
                3 0 Yellow Amsterdam Essen
                2 0 White Frankfurt Amsterdam
                1 0 Black Bruxelles Amsterdam
                3 1 Gray Essen Kobenhavn
                3 0 White Kobenhavn Stockholm
                """,
                51
        );
        var testCase = testCase24;
        Console.SetIn(new StringReader(testCase.input));
        var game = new Game();
        game.ReadInput();
        int actual = game.CalculateScore();
        Console.WriteLine(testCase.expected == actual ? $"✅ {actual}" : $"❌ {testCase.expected} != {actual}");
        Console.Error.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}

class GameState : IState {
    public int RouteScore { get; set; }
    public int TicketScore { get; set; }
    public int Score => RouteScore + TicketScore;
    public int TrainCars { get; set; }
    public int[] Cards { get; set; } = null!;
    public bool[] CompletedRoutes { get; set; } = null!;

    public void Load(GameState from) {
        RouteScore = from.RouteScore;
        TrainCars = from.TrainCars;
        Cards = (int[])from.Cards.Clone();
        CompletedRoutes = (bool[])from.CompletedRoutes.Clone();
    }

    public bool Equals(IState? other) {
        if (other is not GameState that) {
            return false;
        }
        if (ReferenceEquals(this, that)) {
            return true;
        }
        return TrainCars == that.TrainCars
                && CompletedRoutes.SequenceEqual(that.CompletedRoutes)
                && Cards.SequenceEqual(that.Cards);
    }

    public override bool Equals(object? other) => Equals(other as IState);

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode() {
        var hash = new HashCode();
        hash.Add(TrainCars);
        foreach (int count in Cards) {
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
    int[] cards = null!;

    readonly List<Ticket> tickets = new();
    readonly List<Route> routes = new();
    readonly Dictionary<string, City> cities = new();
    Dictionary<Ticket, List<HashSet<Route>>> ticketRoutes = null!;

    public Game() {
        beamSearch = new BeamSearch<GameState>(this);
    }

    public void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        trainCars = Math.Clamp(int.Parse(inputs[0]), 0, 29);
        int ticketLength = Math.Clamp(int.Parse(inputs[1]), 0, 29);
        int routeLength = Math.Clamp(int.Parse(inputs[2]), 0, 29);
        inputs = Console.ReadLine()!.Split(' ');
        cards = new int[Enum.GetValues<Card>().Length - 1];
        foreach (var type in Enum.GetValues<Card>()) {
            if (type == Card.Gray) {
                break;
            }
            cards[(int)type] = int.Parse(inputs[(int)type]);
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
                .ToDictionary(it => it, it => CanConnectCities(it, out var foundRoutes) ? foundRoutes : []);
    }

    void ReadRoutes(int routeLength) {
        for (var i = 0; i < routeLength; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int length = int.Parse(inputs[0]);
            int requiredEngines = int.Parse(inputs[1]);
            string color = inputs[2];
            string cityAName = inputs[3];
            string cityBName = inputs[4];
            var route = new Route(i, length, requiredEngines, Enum.Parse<Card>(color), cityAName, cityBName);
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

    bool CanConnectCities(Ticket ticket, out List<HashSet<Route>> foundRoutes) {
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
                        .ToHashSet())
                .ToList();
        return true;
    }

    public int CalculateScore() {
        var start = new GameState {
            TrainCars = trainCars,
            Cards = (int[])cards.Clone(),
            CompletedRoutes = new bool[routes.Count]
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
            if (current.TrainCars < route.Length) {
                continue;
            }
            var next = new GameState();
            next.Load(current);
            if (!route.TryComplete(next.Cards)) {
                continue;
            }
            next.CompletedRoutes[i] = true;
            next.RouteScore += route.Score;
            next.TrainCars -= route.Length;
            next.TicketScore = TicketScore(next.CompletedRoutes);
            yield return next;
        }
    }

    int TicketScore(bool[] completedRoutes) {
        var score = 0;
        foreach (var (ticket, paths) in ticketRoutes) {
            score += Completed(paths) ? ticket.Points : -ticket.Points;
        }
        return score;

        bool Completed(List<HashSet<Route>> paths) {
            if (paths.Count == 0) {
                return false;
            }
            foreach (var path in paths) {
                int uncompleted = path.Count;
                for (var i = 0; i < completedRoutes.Length; i++) {
                    if (completedRoutes[i] && path.Contains(routes[i])) {
                        uncompleted--;
                    }
                }
                if (uncompleted == 0) {
                    return true;
                }
            }
            return false;
        }
    }

    string IGame<GameState>.ToString(GameState state) {
        var text = new StringBuilder();
        text.AppendLine($" Score: {state.Score} = {state.RouteScore} + {state.TicketScore}");
        text.AppendLine($" TrainCars: {state.TrainCars}");
        text.AppendLine(' ' + string.Join(", ", state.Cards.Select((it, i) => $"{(Card)i}: {it}")));
        var lines = state.CompletedRoutes
                .Select((completed, i) => (completed, route: routes[i]))
                .Where(it => it.completed)
                .Select(it => $"  ✅ {it.route.ToShort()}");
        text.Append(string.Join(Environment.NewLine, lines));
        return text.ToString();
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

record Route(int Id, int Length, int RequiredEngines, Card Color, string CityA, string CityB) {
    readonly Dictionary<int, int> lengthScore = new() {
        { 1, 1 },
        { 2, 2 },
        { 3, 4 },
        { 4, 7 },
        { 6, 15 }
    };

    public int Score => lengthScore[Length];

    public bool TryComplete(int[] cards) {
        if (Color == Card.Gray) {
            return TryCompleteGray(cards);
        }
        if (cards[(int)Color] + cards[(int)Card.Engine] < Length) {
            return false;
        }
        int requiredCards = cards[(int)Color];
        if (requiredCards >= Length) {
            cards[(int)Color] -= Length;
        } else {
            cards[(int)Color] = 0;
            cards[(int)Card.Engine] -= Length - requiredCards;
        }
        return true;
    }

    bool TryCompleteGray(int[] cards) {
        int required = Length;
        if (RequiredEngines > 0) {
            if (cards[(int)Card.Engine] < RequiredEngines) {
                return false;
            }
            required -= RequiredEngines;
            cards[(int)Card.Engine] -= RequiredEngines;
        }
        if (UseNonEngineCards()) {
            return true;
        }
        int used = Math.Min(required, cards[(int)Card.Engine]);
        cards[(int)Card.Engine] -= used;
        required -= used;
        return required == 0 || UseNonEngineCards();

        bool UseNonEngineCards() {
            for (var c = 0; c < cards.Length; c++) {
                if (c == (int)Card.Engine || cards[c] < required) {
                    continue;
                }
                cards[c] -= required;
                return true;
            }
            return false;
        }
    }

    public virtual bool Equals(Route? other) {
        return other is not null && (ReferenceEquals(this, other) || Id == other.Id);
    }

    public override int GetHashCode() => Id;

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

class BeamSearch<T>(IGame<T> game) where T : class, IState {
    readonly HashSet<T> visited = new();

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public T FindBest(T start, int levels, int beamWidth = 6_750) {
        var best = start;
        Console.Error.WriteLine("Start");
        Console.Error.WriteLine(game.ToString(best));
        var currentLevel = new List<T> { start };
        for (var level = 0; level < levels; level++) {
            Console.Error.WriteLine($"Level: {level}");
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
            if (best.Score >= bestCurrent.Score) {
                continue;
            }
            best = bestCurrent;
            Console.Error.WriteLine("New best");
            Console.Error.WriteLine(game.ToString(best));
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<TState> Next(TState current);
    string ToString(TState state);
}

interface IState : IEquatable<IState> {
    int Score { get; }
}
