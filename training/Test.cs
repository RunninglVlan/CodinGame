using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

const string testCase = """
                        27
                        275 275 914 969 275 275 697 275 275 210 275 71 536 738 856 826 893 275 275 275 275 275 463 275 275 472 275
                        """;
Console.SetIn(new StringReader(testCase));
// https://codeforces.com/contest/2252/problem/A
int __ = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
List<int> cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse).OrderBy(it => it)];
Console.Error.WriteLine($"read cards: {string.Join(", ", cards)}");

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
Console.Error.WriteLine($"duplicates: {string.Join(", ", duplicates.Select(it => $"{it.Key}:{it.Value}"))}");
Console.Error.WriteLine($"unique: {string.Join(", ", unique)}");
foreach (int duplicate in duplicates.Keys) {
    unique.Remove(duplicate);
}
int damage = unique.Sum();
int uniqueCount = unique.Count + 2;
foreach ((int duplicate, int count) in duplicates) {
    for (var ___ = 0; ___ < count && uniqueCount > 0; ___++, uniqueCount--) {
        damage += duplicate;
    }
}
const int expected = 11495;
int actual = damage;
Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ expected {expected}, but found {actual}");
