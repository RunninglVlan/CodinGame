using System;
using System.Diagnostics.CodeAnalysis;

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
        return max - MaxCommonDivisor(max, min) + min;
    }

    /// Euclidean algorithm
    [SuppressMessage("ReSharper", "TailRecursiveCall")]
    static long MaxCommonDivisor(long a, long b) {
        return b == 0 ? a : MaxCommonDivisor(b, a % b);
    }
}
