using System;

namespace RobberyOptimisation;

/// https://www.codingame.com/training/medium/robbery-optimisation
static class RobberyOptimisation {
    static void Main() {
        long maxValue = (long)Math.Pow(10, 13) - 1;
        int houseCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
        var houses = new long[houseCount];
        for (var i = 0; i < houseCount; i++) {
            houses[i] = Math.Clamp(long.Parse(Console.ReadLine()!), long.MinValue, maxValue);
        }
        Console.WriteLine(0);
    }
}
