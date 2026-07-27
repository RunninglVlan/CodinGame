using System;

/// https://www.codingame.com/training/easy/chocolate-bar-cut
static class ChocolateBarCut {
    static void Main() {
        long maxSize = (long)Math.Pow(10, 12) - 1;
        int chocolateBars = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
        for (var i = 0; i < chocolateBars; i++) {
            string[] dimensions = Console.ReadLine()!.Split(' ');
            long width = Math.Clamp(long.Parse(dimensions[0]), 1, maxSize);
            long height = Math.Clamp(long.Parse(dimensions[1]), 1, maxSize);
            Console.WriteLine(CutSquares(width, height));
        }
    }

    static long CutSquares(long width, long height) {
        long max = Math.Max(width, height);
        long min = Math.Min(width, height);
        if (max == 1 || min == 1 || max == min) {
            return max;
        }
        var maxCommonDivisor = 1;
        for (int divisor = maxCommonDivisor + 1; divisor <= min; divisor++) {
            if (max % divisor == 0 && min % divisor == 0) {
                maxCommonDivisor = divisor;
            }
        }
        return max - maxCommonDivisor + min;
    }
}
