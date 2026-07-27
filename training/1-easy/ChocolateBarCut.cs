using System;
using System.Collections.Generic;
using System.Linq;

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
        if (width == 1 || height == 1 || width == height) {
            return Math.Max(width, height);
        }
        return PlotActualLine(width, height).Distinct().Count();
    }

    static IEnumerable<(long, long)> PlotActualLine(long endX, long endY) {
        const double fraction = 0.000_001;
        if (endX < endY) {
            (endX, endY) = (endY, endX);
        }
        double m = (double)endY / endX;
        for (double x = 0; x < endX; x++) {
            double pointX = x + fraction;
            var point = ((long)pointX, (long)(m * pointX));
            yield return point;
            pointX = x + 1 - fraction;
            point = ((long)pointX, (long)(m * pointX));
            yield return point;
        }
    }
}
