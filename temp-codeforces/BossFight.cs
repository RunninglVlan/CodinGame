using System;
using System.Collections.Generic;
using System.Linq;

// https://codeforces.com/contest/2252/problem/A
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var _ = 0; _ < tests; _++) {
    // card count
    int __ = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    List<int> cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse).OrderBy(it => it)];
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
    cards = [];
    int sum = unique.Sum();
    if (sum > 0) {
        cards.Add(sum);
    }
    foreach ((int duplicate, int count) in duplicates) {
        for (var ___ = 0; ___ < Math.Min(3, count); ___++) {
            cards.Add(duplicate);
        }
    }
    Console.WriteLine(MaxDamage(cards));
}
return;

int MaxDamage(List<int> cards, int currentDamage = 0, int previousSpell = 0, bool shield = false) {
    int maxDamage = currentDamage;
    for (var i = 0; i < cards.Count; i++) {
        int currentSpell = cards[i];
        int damage = cards[i];
        if (shield) {
            damage = 0;
        }
        if (previousSpell == damage) {
            shield = true;
        }
        cards.RemoveAt(i);
        int newDamage = MaxDamage(cards, currentDamage + damage, currentSpell, shield);
        if (newDamage > maxDamage) {
            maxDamage = newDamage;
        }
        cards.Insert(i, currentSpell);
    }
    return maxDamage;
}
