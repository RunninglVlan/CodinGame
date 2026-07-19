using System;

namespace PlayingCardOdds;

/// https://www.codingame.com/training/easy/playing-card-odds
static class PlayingCardOdds {
    static void Main() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int removedCount = Math.Clamp(int.Parse(inputs[0]), 0, 9);
        int soughtCount = Math.Clamp(int.Parse(inputs[1]), 1, 9);
        for (var i = 0; i < removedCount; i++) {
            string removed = Console.ReadLine()!;
        }
        for (var i = 0; i < soughtCount; i++) {
            string sought = Console.ReadLine()!;
        }
        Console.WriteLine("0%");
    }
}
