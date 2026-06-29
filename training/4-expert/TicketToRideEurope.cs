using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TicketToRideEurope;

static class TicketToRideEurope {
    static void Main() {
        var game = new Game();
        game.ReadInput();
        Console.WriteLine(game.CalculateScore());
    }
}

class Game {
    /// Resource #1
    int trainCars;

    /// Resource #2
    readonly Dictionary<Card, int> cards = new();

    readonly List<Ticket> tickets = new();
    readonly List<Route> routes = new();

    public void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        trainCars = Math.Clamp(int.Parse(inputs[0]), 0, 29);
        int ticketLength = Math.Clamp(int.Parse(inputs[1]), 0, 29);
        int routeLength = Math.Clamp(int.Parse(inputs[2]), 0, 29);
        inputs = Console.ReadLine()!.Split(' ');
        foreach (var value in Enum.GetValues<Card>()) {
            if (value == Card.Gray) {
                break;
            }
            cards[value] = int.Parse(inputs[(int)value]);
        }
        for (var i = 0; i < ticketLength; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int points = int.Parse(inputs[0]);
            string cityA = inputs[1];
            string cityB = inputs[2];
            tickets.Add(new Ticket(points, cityA, cityB));
        }
        for (var i = 0; i < routeLength; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int length = int.Parse(inputs[0]);
            int requiredEngines = int.Parse(inputs[1]);
            string color = inputs[2];
            string cityA = inputs[3];
            string cityB = inputs[4];
            routes.Add(new Route(length, requiredEngines, Enum.Parse<Card>(color), cityA, cityB));
        }
    }

    public int CalculateScore() {
        var score = 0;
        foreach (var ticket in tickets) {
            Console.Error.WriteLine(ticket);
            if (CanConnectCities(ticket, out int routeScore)) {
                score += ticket.Points;
                score += routeScore;
            } else {
                score -= ticket.Points;
            }
        }
        foreach (var route in routes) {
            Console.Error.WriteLine(route);
            if (!route.IsCompleted && route.TryCompleteSimple(ref trainCars, cards, out int points)) {
                score += points;
            }
        }
        foreach (var route in routes) {
            if (!route.IsCompleted && route.TryComplete(ref trainCars, cards, out int points)) {
                score += points;
            }
        }
        return score;
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    bool CanConnectCities(Ticket ticket, out int score) {
        score = 0;
        string currentCity = ticket.CityA;
        while (currentCity != ticket.CityB) {
            var foundRoutes = routes.Where(it => it.CityA == currentCity).ToArray();
            if (foundRoutes.Length == 0) {
                Console.Error.WriteLine($" No route found with {nameof(Route.CityA)}: {currentCity}");
                return false;
            }
            if (foundRoutes.Length > 1) {
                throw new ArgumentException(
                        $"More than 1 route found with {nameof(Route.CityA)}: {currentCity} - {foundRoutes.Length}");
            }
            var route = foundRoutes.First();
            if (!route.TryCompleteSimple(ref trainCars, cards, out int points)) {
                throw new ArgumentException(
                        $"Not enough {nameof(trainCars)}: {trainCars} or {nameof(cards)}: {cards[route.Color]} to complete {route}");
            }
            score += points;
            currentCity = route.CityB;
        }
        return true;
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

record Ticket(int Points, string CityA, string CityB);

record Route(int Length, int RequiredEngines, Card Color, string CityA, string CityB) {
    readonly Dictionary<int, int> lengthScore = new() {
        { 1, 1 },
        { 2, 2 },
        { 3, 4 },
        { 4, 7 },
        { 6, 15 }
    };

    public bool IsCompleted { get; private set; }

    public bool TryCompleteSimple(ref int trainCars, Dictionary<Card, int> cards, out int points) {
        return TryComplete(ref trainCars, cards, useWildAndProcessGray: false, out points);
    }

    public bool TryComplete(ref int trainCars, Dictionary<Card, int> cards, out int points) {
        return TryComplete(ref trainCars, cards, useWildAndProcessGray: true, out points);
    }

    bool TryComplete(ref int trainCars, Dictionary<Card, int> cards, bool useWildAndProcessGray, out int points) {
        points = 0;
        if (trainCars < Length) {
            return false;
        }
        if (!useWildAndProcessGray) {
            if (Color == Card.Gray || cards[Color] < Length) {
                return false;
            }
            cards[Color] -= Length;
            return Complete(ref trainCars, out points);
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
        IsCompleted = true;
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
}
