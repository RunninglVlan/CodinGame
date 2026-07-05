using System;

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
        return Max();

        // DFS
        long Max(int i = 0) {
            if (i >= houses.Length) {
                return 0;
            }
            // check next house
            long next = Max(i + 1);
            // rob current house and check next valid one
            long nextValid = houses[i] + Max(i + 2);
            return Math.Max(next, nextValid);
        }
    }
}
