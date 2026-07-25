using System;

/// https://www.codingame.com/training/easy/chocolate-bar-cut
static class ChocolateBarCut {
    static void Main() {
        long maxSize = (long)Math.Pow(10, 12) - 1;
        int chocolateBars = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
        var cutSquares = new long[chocolateBars];
        for (var i = 0; i < chocolateBars; i++) {
            string[] dimensions = Console.ReadLine()!.Split(' ');
            long width = Math.Clamp(long.Parse(dimensions[0]), 1, maxSize);
            long height = Math.Clamp(long.Parse(dimensions[1]), 1, maxSize);
            cutSquares[i] = CutSquares(width, height);
        }
        foreach (long pieces in cutSquares) {
            Console.WriteLine(pieces);
        }
    }

    static long CutSquares(long width, long height) {
        if (width == 1 || height == 1) {
            return Math.Max(width, height);
        }
        if (width == height) {
            return width;
        }
        throw new NotImplementedException($"{width}x{height}");
    }
}
