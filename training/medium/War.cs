using System;
using System.Collections.Generic;

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
    readonly Queue<int> player1Pile = new();
    readonly Queue<int> player2Pile = new();

    public void Init() {
        ReadCards(player1Deck);
        ReadCards(player2Deck);
    }

    static void ReadCards(Queue<int> deck) {
        int cards = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < cards; i++) {
            string card = Console.ReadLine()!;
            card = card.Remove(card.Length - 1);
            int value = card switch {
                "J" => 11,
                "Q" => 12,
                "K" => 13,
                "A" => 14,
                _ => int.Parse(card)
            };
            deck.Enqueue(value);
        }
    }

    public string Play() {
        var rounds = 0;
        while (player1Deck.Count > 0 && player2Deck.Count > 0) {
            // battle
            int player1Card = player1Deck.Dequeue();
            player1Pile.Enqueue(player1Card);
            int player2Card = player2Deck.Dequeue();
            player2Pile.Enqueue(player2Card);
            if (player1Card == player2Card) {
                // war
                if (player1Deck.Count < 4 || player2Deck.Count < 4) {
                    return "PAT";
                }
                for (var i = 0; i < 3; i++) {
                    int card = player1Deck.Dequeue();
                    player1Pile.Enqueue(card);
                }
                for (var i = 0; i < 3; i++) {
                    int card = player2Deck.Dequeue();
                    player2Pile.Enqueue(card);
                }
            } else {
                EnqueueFromWarPiles(player1Card > player2Card ? player1Deck : player2Deck);
                rounds++;
            }
        }
        int winner = player1Deck.Count > 0 ? 1 : 2;
        return $"{winner} {rounds}";
    }

    void EnqueueFromWarPiles(Queue<int> deck) {
        while (player1Pile.Count > 0) {
            int card = player1Pile.Dequeue();
            deck.Enqueue(card);
        }
        while (player2Pile.Count > 0) {
            int card = player2Pile.Dequeue();
            deck.Enqueue(card);
        }
    }
}
