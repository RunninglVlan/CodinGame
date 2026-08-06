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

        string first = phones.First();
        var phoneTries = new Trie(first[0], []);
        var lastTrie = phoneTries;
        for (var i = 1; i < first.Length; i++) {
            char digit = first[i];
            var trie = new Trie(digit, []);
            lastTrie.Children.Add(trie);
            lastTrie = trie;
        }
        // the number of elements (referencing a number) stored in the structure
        Console.WriteLine(phoneTries.CountAll());
    }
}

record Trie(char Digit, List<Trie> Children) {
    public int CountAll() {
        return 1 + Children.Sum(child => child.CountAll());
    }
}
