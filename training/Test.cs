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

var (duplicates, unique) = DuplicatesAndUnique(cards);
Console.Error.WriteLine($"duplicates: {string.Join(", ", duplicates.Select(it => $"{it.Key}:{it.Value}"))}");
Console.Error.WriteLine($"unique: {string.Join(", ", unique)}");
int damage = unique.Sum();
int uniqueCount = unique.Count;
foreach ((int duplicate, int count) in duplicates) {
    for (var ___ = 0; ___ < count - 1 && uniqueCount > 0; ___++, uniqueCount--) {
        damage += duplicate;
    }
}
const int expected = 4;
int actual = damage;
Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ expected {expected}, but found {actual}");
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
    return (duplicates, unique);
}
