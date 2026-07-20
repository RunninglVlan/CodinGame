using System;
using System.Diagnostics.CodeAnalysis;

/// https://www.codingame.com/training/medium/stock-exchange-losses
static class StockExchangeLosses {
    static void Main() {
        var stock = new Stock();
        stock.ReadValues();
        Console.WriteLine(stock.MaxLoss());
    }
}

class Stock {
    int[] stockValues = null!;

    public void ReadValues() {
        int maxValue = (int)Math.Pow(2, 31) - 1;
        int n = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100_000 - 1);
        stockValues = new int[n];
        string[] inputs = Console.ReadLine()!.Split(' ');
        for (var i = 0; i < n; i++) {
            int value = Math.Clamp(int.Parse(inputs[i]), 1, maxValue);
            stockValues[i] = value;
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    public int MaxLoss() {
        int? max = null, min = null;
        var maxLoss = 0;
        foreach (int value in stockValues) {
            if (value < max && (min == null || value < min)) {
                min = value;
                maxLoss = Math.Min(maxLoss, value - max.Value);
            }
            if (max == null || value > max) {
                max = value;
                min = null;
            }
        }
        return maxLoss;
    }
}
