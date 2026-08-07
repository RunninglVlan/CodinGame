using System;
using System.Collections.Generic;
using System.Linq;

// https://codeforces.com/contest/2252/problem/A
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var _ = 0; _ < tests; _++) {
    // card count
    Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    List<int> cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse).OrderBy(it => it)];
    var (duplicates, unique) = DuplicatesAndUnique(cards);
    int damage = unique.Sum();
    int uniqueCount = unique.Count + 2;
    foreach ((int duplicate, int count) in duplicates.OrderByDescending(it => it.Key)) {
        for (var ___ = 0; ___ < count && uniqueCount > 0; ___++, uniqueCount--) {
            damage += duplicate;
        }
    }
    Console.WriteLine(damage);
}
return;

(Dictionary<int, int>, HashSet<int>) DuplicatesAndUnique(List<int> cards) {
    Dictionary<int, int> duplicates = [];
    HashSet<int> unique = [];
    for (var i = 0; i < cards.Count; i++) {
        int current = cards[i];
        if (i < cards.Count - 1 && current == cards[i + 1]) {
            duplicates[current] = duplicates.GetValueOrDefault(current, 1) + 1;
        } else {
            unique.Add(current);
        }
    }
    foreach (int duplicate in duplicates.Keys) {
        unique.Remove(duplicate);
    }
    return (duplicates, unique);
}
