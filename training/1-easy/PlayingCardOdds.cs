using System;

namespace PlayingCardOdds;

/// https://www.codingame.com/training/easy/playing-card-odds
static class PlayingCardOdds {
    const string SUITS = "CDHS";

    static void Main() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int removedCount = Math.Clamp(int.Parse(inputs[0]), 0, 9);
        int soughtCount = Math.Clamp(int.Parse(inputs[1]), 1, 9);
        int cards = 52, soughtCards = 0;
        for (var i = 0; i < removedCount; i++) {
            string removed = Console.ReadLine()!;
            cards -= CountCards(removed);
        }
        for (var i = 0; i < soughtCount; i++) {
            string sought = Console.ReadLine()!;
            soughtCards += CountCards(sought);
        }
        int soughtProbability = soughtCards * 100 / cards;
        Console.WriteLine($"{soughtProbability}%");
    }

    static int CountCards(string cards) {
        char lastCharacter = cards[^1];
        char? suit = SUITS.Contains(lastCharacter) ? lastCharacter : null;
        if (suit.HasValue && cards.Length == 1) {
            return 13;
        }
        string ranks = suit == null ? cards : cards[..^1];
        return suit == null ? ranks.Length * 4 : ranks.Length;
    }
}
