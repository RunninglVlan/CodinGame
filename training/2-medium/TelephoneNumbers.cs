using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/medium/telephone-numbers
static class TelephoneNumbers {
    static void Main() {
        int phoneCount = int.Parse(Console.ReadLine()!);
        var phones = new List<string>(phoneCount);
        for (var i = 0; i < phoneCount; i++) {
            string phoneNumber = Console.ReadLine()!;
            phones.Add(phoneNumber);
        }

        var phoneTries = new List<Trie>();
        foreach (string phone in phones) {
            var currentTries = phoneTries;
            foreach (char digit in phone) {
                if (currentTries.Any(it => it.Digit == digit)) {
                    var trie = currentTries.First(it => it.Digit == digit);
                    currentTries = trie.Children;
                } else {
                    var trie = new Trie(digit, []);
                    currentTries.Add(trie);
                    currentTries = trie.Children;
                }
            }
        }
        Console.WriteLine(phoneTries.Sum(it => it.CountAll()));
    }
}

record Trie(char Digit, List<Trie> Children) {
    public int CountAll() {
        return 1 + Children.Sum(it => it.CountAll());
    }
}
