using System;
using System.Collections.Generic;

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
        foreach (long money in Rob(0, 0, [])) {
            if (money <= best) {
                continue;
            }
            best = money;
            Console.Error.WriteLine($"🆕 best: {best}");
        }
        return best;
    }

    // DFS/Backtracking
    IEnumerable<long> Rob(int currentI, long currentMoney, HashSet<int> currentChecked) {
        if (currentChecked.Count == houses.Length) {
            yield return currentMoney;
            yield break;
        }
        for (int i = currentI; i < houses.Length; i++) {
            // ignore if already robbed, or it's next to the robbed house
            if (currentChecked.Contains(i)) {
                continue;
            }
            long nextMoney = currentMoney + houses[i];
            var nextChecked = new HashSet<int>(currentChecked) {
                i,
                Math.Clamp(i + 1, 0, houses.Length - 1),
                Math.Clamp(i - 1, 0, houses.Length - 1)
            };
            foreach (long money in Rob(i, nextMoney, nextChecked)) {
                yield return money;
            }
        }
    }
}
