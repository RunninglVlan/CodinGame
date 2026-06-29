using System;
using System.Collections.Generic;

namespace TicketToRideEurope;

static class TicketToRideEurope {
    static void Main() {
        var game = new Game();
        game.ReadInput();
        Console.WriteLine(game.CalculateScore());
    }
}

class Game {
    int trainCars;
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
        return 0;
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
    Engine,
    Gray
}

record Ticket(int Points, string CityA, string CityB);

record Route(int Length, int RequiredEngines, Card Color, string CityA, string CityB);
