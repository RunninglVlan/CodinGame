using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayingCardOdds;

/// https://www.codingame.com/training/easy/playing-card-odds
static class PlayingCardOdds {
    static void Main() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int removedCount = Math.Clamp(int.Parse(inputs[0]), 0, 9);
        int soughtCount = Math.Clamp(int.Parse(inputs[1]), 1, 9);
        HashSet<Card> cards = Card.All(), soughtCards = [];
        for (var i = 0; i < removedCount; i++) {
            string removed = Console.ReadLine()!;
            foreach (var card in Card.Convert(removed)) {
                cards.Remove(card);
            }
        }
        for (var i = 0; i < soughtCount; i++) {
            string sought = Console.ReadLine()!;
            foreach (var card in Card.Convert(sought)) {
                soughtCards.Add(card);
            }
        }
        int soughtProbability = soughtCards.Count * 100 / cards.Count;
        Console.WriteLine($"{soughtProbability}%");
    }
}

record struct Card(int Rank, char Suit) {
    const string SUITS = "CDHS";
    const string RANKS = "23456789TJQKA";

    public static HashSet<Card> All() => SUITS.SelectMany(SuitCards).ToHashSet();

    static IEnumerable<Card> SuitCards(char suit) {
        for (var i = 0; i < 13; i++) {
            yield return new Card(i + 2, suit);
        }
    }

    /// <param name="cards">Descriptive classification of cards, meaning a combination of ranks, suits, or both,
    /// where any of the given ranks must match, and any of the given suits must match</param>
    /// <example>
    /// <code>
    /// // no suits given, so suit can be any
    /// 8TA => 8 CDHS, T CDHS, A CDHS
    /// // no ranks given, so rank can be any
    /// H   => 2-A H
    /// // suit is club and 2 ranks given
    /// 45C => 4C, 5C
    /// </code>
    /// </example>
    public static IEnumerable<Card> Convert(string cards) {
        char lastCharacter = cards[^1];
        char? suit = SUITS.Contains(lastCharacter) ? lastCharacter : null;
        if (suit.HasValue && cards.Length == 1) {
            foreach (var card in SuitCards(suit.Value)) {
                yield return card;
            }
            yield break;
        }
        string ranks = suit == null ? cards : cards[..^1];
        foreach (char cardSuit in SUITS) {
            if (suit.HasValue && cardSuit != suit.Value) {
                continue;
            }
            foreach (char rank in ranks) {
                yield return new Card(RANKS.IndexOf(rank) + 2, cardSuit);
            }
        }
    }
}
