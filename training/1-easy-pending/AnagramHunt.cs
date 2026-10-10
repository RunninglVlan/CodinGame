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
    char[] wordOrdered = [.. word.OrderBy(it => it)];
    foreach (var player in players) {
        if (IsAnagram(wordOrdered, player.Hand, out int score)) {
            player.Score = score;
            player.PlayedWord = word;
        }
    }
}
var playersOrdered = players.OrderByDescending(it => it.Score);
Console.WriteLine(mode == Modes.HIGH ? playersOrdered.First() : playersOrdered.Last());
return;

bool IsAnagram(char[] word, char[] playerHand, out int score) {
    score = 0;
    for (var i = 0; i < word.Length; i++) {
        if (word[i] == playerHand[i]) {
            score++;
        } else {
            return false;
        }
    }
    return true;
}

static class Modes {
    public const string HIGH = "high";
    public const string LOW = "low";
}

record Player(string Name, char[] Hand) {
    public int Score { get; set; }
    public string PlayedWord { get; set; } = null!;

    public override string ToString() => $"{Name} {PlayedWord}";
}
