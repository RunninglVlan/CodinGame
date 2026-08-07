using System;
using System.Collections.Generic;
using System.Linq;

// https://codeforces.com/contest/2252/problem/A
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var _ = 0; _ < tests; _++) {
    // card count
    Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    List<int> cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse).OrderBy(it => it)];
    var duplicates = Duplicates(cards);
    if (duplicates.Count == 0) {
        Console.WriteLine(cards.Sum());
        continue;
    }
    var maxFrequencyCard = duplicates.MaxBy(it => it.Value);
    int[] otherCards = [.. cards.Where(it => it != maxFrequencyCard.Key)];
    int damage = otherCards.Sum() + maxFrequencyCard.Key * Math.Min(maxFrequencyCard.Value, otherCards.Length + 2);
    Console.WriteLine(damage);
}
return;

Dictionary<int, int> Duplicates(List<int> sortedNumbers) {
    Dictionary<int, int> counts = [];
    for (var i = 1; i < sortedNumbers.Count; i++) {
        int current = sortedNumbers[i];
        if (current == sortedNumbers[i - 1]) {
            // default is 1 because when first duplicate is found, count should be 2
            counts[current] = counts.GetValueOrDefault(current, 1) + 1;
        }
    }
    return counts;
}
