using System;
using System.Collections.Generic;
using System.Linq;

// https://codeforces.com/contest/2252/problem/A
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var i = 0; i < tests; i++) {
    // card count
    int _ = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    List<int> cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse)];
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
