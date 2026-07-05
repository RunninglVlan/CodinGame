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
        for (var i = 0; i < houses.Length; i++) {
            long money = StartRobbing(i);
            if (money <= best) {
                continue;
            }
            best = money;
            Console.Error.WriteLine($"🆕 best: {best}");
        }
        return best;
    }

    long StartRobbing(int startI) {
        Console.Error.WriteLine($"Starting with {startI}");
        HashSet<int> checkedHouses = [];
        long money = 0;
        Rob(startI);
        for (var i = 0; checkedHouses.Count < houses.Length; i++) {
            // ignore if already robbed, or it's next to the robbed house
            if (checkedHouses.All(j => j != i)) {
                Rob(i);
            }
        }
        return money;

        void Rob(int i) {
            money += houses[i];
            Console.Error.WriteLine($" Robbed {i}, +{houses[i]} => {money}");
            checkedHouses.Add(i);
            checkedHouses.Add(Math.Clamp(i + 1, 0, houses.Length - 1));
            checkedHouses.Add(Math.Clamp(i - 1, 0, houses.Length - 1));
        }
    }
}
