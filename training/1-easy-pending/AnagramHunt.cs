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
var playersOrdered = players.OrderByDescending(it => it.Score);
Console.WriteLine(mode == Modes.HIGH ? playersOrdered.First() : playersOrdered.Last());

static class Modes {
    public const string HIGH = "high";
    public const string LOW = "low";
}

record Player(string Name, char[] Hand) {
    public int Score { get; private set; }
    string playedWord = null!;

    public void TryPlay(string word) {
        if (!HasAnagram([.. word.OrderBy(it => it)], out int score)) {
            return;
        }
        Score = score;
        playedWord = word;
    }

    bool HasAnagram(char[] word, out int score) {
        score = 0;
        for (var i = 0; i < word.Length; i++) {
            if (i < Hand.Length && word[i] == Hand[i]) {
                score++;
            } else {
                return false;
            }
        }
        return true;
    }

    public override string ToString() => $"{Name} {playedWord}";
}
