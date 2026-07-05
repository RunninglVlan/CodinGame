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
    }

    public long MaxMoney() {
        long best = 0;
        for (var i = 0; i < houses.Length; i++) {
            long money = StartRobbing(i);
            if (money > best) {
                best = money;
            }
        }
        return best;
    }

    long StartRobbing(int startI) {
        List<int> robbed = [startI];
        long money = houses[startI];
        for (var i = 0; i < houses.Length; i++) {
            // ignore if already robbed, or it's next to the robbed house
            if (robbed.Any(j => j == i || Math.Abs(j - i) == 1)) {
                continue;
            }
            robbed.Add(i);
            money += houses[i];
        }
        return money;
    }
}
