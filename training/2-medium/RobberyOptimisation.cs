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
        foreach (long money in Rob()) {
            if (money <= best) {
                continue;
            }
            best = money;
            Console.Error.WriteLine($"🆕 best: {best}");
        }
        return best;
    }

    // DFS
    IEnumerable<long> Rob(int i = 0, long currentMoney = 0) {
        if (i >= houses.Length) {
            yield return currentMoney;
            yield break;
        }
        // check next house
        foreach (long money in Rob(i + 1, currentMoney)) {
            yield return money;
        }
        // rob current house and check next valid one
        foreach (long money in Rob(i + 2, currentMoney + houses[i])) {
            yield return money;
        }
    }
}
