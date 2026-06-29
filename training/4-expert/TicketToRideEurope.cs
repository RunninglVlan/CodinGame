using System;

namespace TicketToRideEurope;

static class TicketToRideEurope {
    static void Main() {
        var game = new Game();
        game.ReadInput();
        Console.WriteLine(game.CalculateScore());
    }
}

class Game {
    public void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int trainCars = int.Parse(inputs[0]);
        int tickets = int.Parse(inputs[1]);
        int routes = int.Parse(inputs[2]);
        inputs = Console.ReadLine()!.Split(' ');
        int red = int.Parse(inputs[0]);
        int yellow = int.Parse(inputs[1]);
        int green = int.Parse(inputs[2]);
        int blue = int.Parse(inputs[3]);
        int white = int.Parse(inputs[4]);
        int black = int.Parse(inputs[5]);
        int orange = int.Parse(inputs[6]);
        int pink = int.Parse(inputs[7]);
        int engine = int.Parse(inputs[8]);
        for (var i = 0; i < tickets; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int points = int.Parse(inputs[0]);
            string cityA = inputs[1];
            string cityB = inputs[2];
        }
        for (var i = 0; i < routes; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int length = int.Parse(inputs[0]);
            int requiredEngines = int.Parse(inputs[1]);
            string color = inputs[2];
            string cityA = inputs[3];
            string cityB = inputs[4];
        }
    }

    public int CalculateScore() {
        return 0;
    }
}
