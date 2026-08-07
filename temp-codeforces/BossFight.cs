using System;
using System.Linq;

// https://codeforces.com/contest/2252/problem/A
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var i = 0; i < tests; i++) {
    int cardCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    int[] cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse)];
    Console.WriteLine(MaxDamage(cards));
}
return;

int MaxDamage(int[] cards, int currentI = 0, int currentDamage = 0, int previousSpell = 0, bool shield = false) {
    for (int i = currentI; i < cards.Length; i++) {
        int damage = cards[i];
        if (shield) {
            damage = 0;
        }
        if (previousSpell == damage) {
            shield = true;
        }
        int newDamage = MaxDamage(cards, currentI + 1, currentDamage + damage, damage, shield);
        if (newDamage > currentDamage) {
            return newDamage;
        }
    }
    return currentDamage;
}
