using System;
using System.Collections.Generic;
using System.Linq;

namespace RobberyOptimisation;

/// https://www.codingame.com/training/medium/robbery-optimisation
static class RobberyOptimisation {
    static void Main() {
        var robbery = new Robbery();
        robbery.ReadHouseValues();
        Console.WriteLine(robbery.MaxMoney());
    }
}

class Robbery {
    long[] houses = null!;

    public void ReadHouseValues() {
        long maxValue = (long)Math.Pow(10, 13) - 1;
        int count = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
        houses = new long[count];
        for (var i = 0; i < count; i++) {
            houses[i] = Math.Clamp(long.Parse(Console.ReadLine()!), long.MinValue, maxValue);
        }
        Console.Error.WriteLine($"House values: {string.Join(", ", houses)}");
    }

    public long MaxMoney() {
        long best = 0;
        foreach (var robbed in Rob(0, [], [])) {
            long money = robbed.Sum(j => houses[j]);
            if (money <= best) {
                continue;
            }
            best = money;
            Console.Error.WriteLine($"🆕 best: {best}");
            long total = 0;
            var lines = robbed.Select(r => $" Robbed {r}, +{houses[r]} => {total += houses[r]}");
            Console.Error.WriteLine(string.Join(Environment.NewLine, lines));
        }
        return best;
    }

    // DFS/Backtracking
    IEnumerable<List<int>> Rob(int current, List<int> currentRobbed, HashSet<int> currentChecked) {
        if (currentChecked.Count == houses.Length) {
            yield return currentRobbed;
            yield break;
        }
        for (int i = current; i < houses.Length; i++) {
            // ignore if already robbed, or it's next to the robbed house
            if (currentChecked.Contains(i)) {
                continue;
            }
            int next = i + 2;
            List<int> nextRobbed = [..currentRobbed, i];
            var nextChecked = new HashSet<int>(currentChecked) {
                i,
                Math.Clamp(i + 1, 0, houses.Length - 1),
                Math.Clamp(i - 1, 0, houses.Length - 1)
            };
            foreach (var result in Rob(next, nextRobbed, nextChecked)) {
                yield return result;
            }
        }
    }
}
