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
                if (!cards.Contains(card)) {
                    continue;
                }
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

    public static HashSet<Card> All() => SUITS.SelectMany(it => SuitCards(it, RANKS)).ToHashSet();

    static IEnumerable<Card> SuitCards(char suit, string ranks) {
        return ranks.Select(it => new Card(RANKS.IndexOf(it) + 2, suit));
    }

    /// <param name="cards">Descriptive classification of cards, meaning a combination of ranks, suits, or both,
    /// where any of the given ranks must match, and any of the given suits must match</param>
    /// <example>
    /// <code>
    /// // no suits given, so suit can be any
    /// 8TA   => 8 CDHS, T CDHS, A CDHS
    /// // no ranks given, so rank can be any
    /// H     => 2-A H
    /// // suit is club and 2 ranks given
    /// 45C   => 4C, 5C
    /// // all suits are given, this is equivalent to just 7
    /// 7CDHS => 7 CDHS
    /// </code>
    /// </example>
    public static IEnumerable<Card> Convert(string cards) {
        var suits = new HashSet<char>();
        for (var i = 0; i < SUITS.Length; i++) {
            int iFromEnd = cards.Length - 1 - i;
            if (iFromEnd < 0 || !SUITS.Contains(cards[iFromEnd])) {
                break;
            }
            suits.Add(cards[iFromEnd]);
        }
        string ranks = cards[..^suits.Count];
        if (ranks.Length == 0) {
            ranks = RANKS;
        }
        if (suits.Count == 0) {
            suits = SUITS.ToHashSet();
        }
        foreach (var card in suits.SelectMany(it => SuitCards(it, ranks))) {
            yield return card;
        }
    }
}
