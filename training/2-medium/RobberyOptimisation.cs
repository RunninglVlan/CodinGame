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

    /// Top-down Dynamic programming
    public long MaxMoney() {
        var calculated = new Dictionary<int, long>();
        return Max();

        long Max(int i = 0) {
            if (i >= houses.Length) {
                return 0;
            }
            if (calculated.TryGetValue(i, out long cached)) {
                return cached;
            }
            // check next house
            long skip = Max(i + 1);
            // rob current house and check next valid one
            long rob = houses[i] + Max(i + 2);
            long max = Math.Max(skip, rob);
            calculated[i] = max;
            return max;
        }
    }
}
