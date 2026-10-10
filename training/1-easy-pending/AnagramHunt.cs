using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/contribute/view/1537621fc92617df1db4cda018100dceb3c47f
string mode = Console.ReadLine()!;
int playerCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
List<Player> players = [];
for (var i = 0; i < playerCount; i++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    string name = inputs[0];
    string hand = inputs[1];
    players.Add(new Player(name, [.. hand.OrderBy(it => it)]));
}
int dictionaryWords = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
for (var i = 0; i < dictionaryWords; i++) {
    string word = Console.ReadLine()!;
    foreach (var player in players) {
        player.TryPlay(word);
    }
}
var playersOrdered = players.Where(it => it.PlayedWord != null).OrderByDescending(it => it.Score);
Console.WriteLine(mode == Modes.HIGH ? playersOrdered.First() : playersOrdered.Last());

static class Modes {
    public const string HIGH = "high";
    public const string LOW = "low";
}

record Player(string Name, char[] Hand) {
    public int Score { get; private set; }
    public string? PlayedWord { get; private set; }

    public void TryPlay(string word) {
        if (!HasAnagram([.. word.OrderBy(it => it)], out int score)) {
            return;
        }
        Score = score;
        PlayedWord = word;
    }

    bool HasAnagram(char[] word, out int score) {
        score = 0;
        if (word.Length != Hand.Length) {
            return false;
        }
        for (var i = 0; i < word.Length; i++) {
            if (word[i] == Hand[i]) {
                score += LetterScore(word[i]);
            } else {
                return false;
            }
        }
        return true;
    }

    static int LetterScore(char c) {
        return c switch {
            'a' or 'e' or 'i' or 'l' or 'n' or 'o' or 'r' or 's' or 't' or 'u' => 1,
            'd' or 'g' => 2,
            'b' or 'c' or 'm' or 'p' => 3,
            'f' or 'h' or 'v' or 'w' or 'y' => 4,
            'k' => 5,
            'j' or 'x' => 8,
            'q' or 'z' => 10,
            _ => throw new ArgumentException($"Unexpected letter: {c}")
        };
    }

    public override string ToString() => $"{Name} {PlayedWord}";
}
