using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace War;

/// https://www.codingame.com/training/medium/winamax-battle
static class War {
    static void Main() {
        var game = new Game();
        game.Init();
        Console.WriteLine(game.Play());
    }
}

class Game {
    readonly Queue<int> player1Deck = new();
    readonly Queue<int> player2Deck = new();
    readonly List<int> player1Pile = new();
    readonly List<int> player2Pile = new();

    public void Init() {
        ReadCards(player1Deck);
        ReadCards(player2Deck);
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    static void ReadCards(Queue<int> deck) {
        int cards = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < cards; i++) {
            string card = Console.ReadLine()!;
            char c = card[0];
            var value = (int)char.GetNumericValue(c);
            if (value < 0) {
                value = c switch {
                    'J' => 11,
                    'Q' => 12,
                    'K' => 13,
                    _ => 14
                };
            } else if (value == 1) {
                value = 10;
            }
            deck.Enqueue(value);
        }
    }

    public string Play() {
        var rounds = 0;
        while (player1Deck.Count > 0 && player2Deck.Count > 0) {
            int player1Card = player1Deck.Dequeue();
            player1Pile.Add(player1Card);
            int player2Card = player2Deck.Dequeue();
            player2Pile.Add(player2Card);
            if (player1Card == player2Card) {
                if (WarIsDraw()) {
                    return "PAT";
                }
                rounds++;
                continue;
            }
            AddToBottomFromWarPiles(player1Card > player2Card ? player1Deck : player2Deck);
            rounds++;
        }
        int winner = player1Deck.Count > 0 ? 1 : 2;
        return $"{winner} {rounds}";
    }

    [SuppressMessage("ReSharper", "TailRecursiveCall")]
    bool WarIsDraw() {
        if (player1Deck.Count < 4 || player2Deck.Count < 4) {
            return true;
        }
        for (var i = 0; i < 3; i++) {
            int card = player1Deck.Dequeue();
            player1Pile.Add(card);
        }
        for (var i = 0; i < 3; i++) {
            int card = player2Deck.Dequeue();
            player2Pile.Add(card);
        }
        int player1Card = player1Deck.Dequeue();
        player1Pile.Add(player1Card);
        int player2Card = player2Deck.Dequeue();
        player2Pile.Add(player2Card);
        if (player1Card == player2Card) {
            return WarIsDraw();
        }
        AddToBottomFromWarPiles(player1Card > player2Card ? player1Deck : player2Deck);
        return false;
    }

    void AddToBottomFromWarPiles(Queue<int> deck) {
        foreach (int card in player1Pile) {
            deck.Enqueue(card);
        }
        player1Pile.Clear();
        foreach (int card in player2Pile) {
            deck.Enqueue(card);
        }
        player2Pile.Clear();
    }
}
