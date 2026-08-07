using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// let's do the test ourselves
const string testCase = """
                        4
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
for (var _ = 0; _ < tests; _++) {
    int __ = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    List<int> cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse).OrderBy(it => it)];
    Console.Error.WriteLine($"read cards: {string.Join(", ", cards)}");

    var duplicates = Duplicates(cards);
    Console.Error.WriteLine($"duplicates: {string.Join(", ", duplicates.Select(it => $"{it.Key}:{it.Value}"))}");
    if (duplicates.Count == 0) {
        Console.WriteLine(cards.Sum());
        continue;
    }
    var maxFrequencySpell = duplicates.MaxBy(it => it.Value);
    int[] otherCards = [.. cards.Where(it => it != maxFrequencySpell.Key)];
    int sumOfOtherSpells = otherCards.Sum();
    // int expected = int.Parse(Console.ReadLine()!);
    int actual = sumOfOtherSpells + maxFrequencySpell.Key * Math.Min(maxFrequencySpell.Value, otherCards.Length + 2);
    Console.WriteLine(actual);
    // Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ expected {expected}, but found {actual}");
}
return;

Dictionary<int, int> Duplicates(List<int> cards) {
    Dictionary<int, int> duplicates = [];
    for (var i = 0; i < cards.Count - 1; i++) {
        int current = cards[i];
        if (current == cards[i + 1]) {
            duplicates[current] = duplicates.GetValueOrDefault(current, 1) + 1;
        }
    }
    return duplicates;
}
