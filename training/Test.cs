using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

const string testCase = """
                        3
                        1 1 2
                        """;
Console.SetIn(new StringReader(testCase));
// https://codeforces.com/contest/2252/problem/A
int __ = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
List<int> cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse).OrderBy(it => it)];
Console.Error.WriteLine($"read cards: {string.Join(", ", cards)}");

HashSet<int> duplicates = [];
HashSet<int> unique = [];
for (var i = 0; i < cards.Count; i++) {
    int current = cards[i];
    if (i < cards.Count - 1 && current == cards[i + 1]) {
        duplicates.Add(current);
    } else {
        unique.Add(current);
    }
}
foreach (int duplicate in duplicates) {
    unique.Remove(duplicate);
}
cards = [];
int sum = unique.Sum();
if (sum > 0) {
    cards.Add(sum);
}
foreach (int duplicate in duplicates) {
    cards.Add(duplicate);
    cards.Add(duplicate);
    cards.Add(duplicate);
}
Console.Error.WriteLine($"reduced cards: {string.Join(", ", cards)}");
const int expected = 4;
int actual = MaxDamage(cards);
Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ expected {expected}, but found {actual}");
return;

int MaxDamage(List<int> cards, int currentDamage = 0, int previousSpell = 0, bool shield = false, int depth = 0) {
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
        Console.Error.WriteLine($"{new string(' ', depth)}currentDamage: {currentDamage}, spell: {currentSpell}, damage: {damage}, previousSpell: {previousSpell}, shield: {shield}");
        int newDamage = MaxDamage(cards, currentDamage + damage, currentSpell, shield, depth + 1);
        Console.Error.WriteLine($"{new string(' ', depth)}newDamage: {newDamage}");
        if (newDamage > maxDamage) {
            maxDamage = newDamage;
        }
        cards.Insert(i, currentSpell);
    }
    return maxDamage;
}
