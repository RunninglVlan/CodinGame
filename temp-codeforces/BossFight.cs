using System;
using System.Collections.Generic;
using System.Linq;

// https://codeforces.com/contest/2252/problem/A
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var i = 0; i < tests; i++) {
    int cardCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    List<int> cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse)];
    Console.WriteLine(MaxDamage(cards));
}
return;

int MaxDamage(List<int> cards, int currentI = 0, int currentDamage = 0, int previousSpell = 0, bool shield = false) {
    for (int i = cards.Count - 1; i >= currentI; i--) {
        int damage = cards[i];
        if (shield) {
            damage = 0;
        }
        if (previousSpell == damage) {
            shield = true;
        }
        cards.RemoveAt(i);
        int newDamage = MaxDamage(cards, i, currentDamage + damage, damage, shield);
        if (newDamage > currentDamage) {
            return newDamage;
        }
        cards.Insert(i, damage);
    }
    return currentDamage;
}
