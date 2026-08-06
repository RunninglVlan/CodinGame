using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/medium/telephone-numbers
static class TelephoneNumbers {
    static void Main() {
        int phoneCount = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 10_000);
        var phones = new List<string>(phoneCount);
        for (var i = 0; i < phoneCount; i++) {
            string phoneNumber = Console.ReadLine()!;
            phones.Add(phoneNumber);
        }

        var root = new Dictionary<char, Trie>();
        foreach (string phone in phones) {
            var current = root;
            foreach (char digit in phone) {
                if (current.TryGetValue(digit, out var trie)) {
                    current = trie.Children;
                } else {
                    trie = new Trie(new Dictionary<char, Trie>());
                    current.Add(digit, trie);
                    current = trie.Children;
                }
            }
        }
        Console.WriteLine(root.Values.Sum(it => it.CountAll()));
    }
}

record Trie(Dictionary<char, Trie> Children) {
    public int CountAll() {
        return 1 + Children.Values.Sum(it => it.CountAll());
    }
}
