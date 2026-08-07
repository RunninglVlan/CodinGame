using System;
using System.Collections.Generic;
using System.Linq;

const string testCase = """
                        4
                        2
                        10 15
                        1
                        100
                        4
                        10 5 10 10
                        5
                        1 2 3 4 5
                        6
                        7 7 7 7 7 7
                        """;
Console.SetIn(new StringReader(testCase));
// https://codeforces.com/contest/2252/problem/A
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var i = 0; i < tests; i++) {
    int cardCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    List<int> cards = [.. Debug.ReadLine().Split(' ').Select(int.Parse)];
    Console.WriteLine(MaxDamage(cards));
}
return;

int MaxDamage(List<int> cards, int currentI = 0, int currentDamage = 0, int previousSpell = 0, bool shield = false) {
    if (currentI >= cards.Count) {
        return currentDamage;
    }
    // we start with 0
    // we should try all combinations
    // if we have 10 and 15, count 2
    // we should try 10 and 15
    // and 15 and 10
    // we use reversed for to iterate and remove from list
    // so first we use 15
    // iterate from index 1 to 0
    // 2nd iteration should have 
    for (int i = currentI; i < cards.Count; i++) {
        int damage = cards[i];
        int currentSpell = cards[i];
        if (shield) {
            damage = 0;
        }
        if (previousSpell == damage) {
            shield = true;
        }
        // cards.RemoveAt(i);
        int newDamage = MaxDamage(cards, currentI + 1, currentDamage + damage, damage, shield);
        Console.Error.WriteLine($"currentDamage: {currentDamage}, spell: {currentSpell}, damage: {damage}, newDamage: {newDamage}, previousSpell: {previousSpell}, shield: {shield}");
        if (newDamage > currentDamage) {
            return newDamage;
        }
        // cards.Insert(i, damage);
    }
    return currentDamage;
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string ReadLine() {
        string line = Console.ReadLine()!;
        Console.Error.WriteLine(line);
        return line;
    }
}
