using System;

namespace StockExchangeLosses;

/// https://www.codingame.com/training/medium/stock-exchange-losses
static class StockExchangeLosses {
    static void Main() {
        int maxValue = (int)Math.Pow(2, 31) - 1;
        int n = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100_000 - 1);
        string[] inputs = Console.ReadLine()!.Split(' ');
        int? max = null, min = null;
        var maxLoss = 0;
        for (var i = 0; i < n; i++) {
            int value = Math.Clamp(int.Parse(inputs[i]), 1, maxValue);
            if (max != null && (min == null || value < min)) {
                min = value;
                maxLoss = value - max.Value;
            }
            if (max == null || value > max) {
                max = value;
            }
        }
        Console.WriteLine(maxLoss);
    }
}
