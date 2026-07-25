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
            int cutSquares = PlotLineFromZero(width - 1, height - 1).Count();
            Console.WriteLine(cutSquares);
        }
    }

    static IEnumerable<(long, long)> PlotLineFromZero(long endX, long endY) {
        if (endX < endY) {
            (endX, endY) = (endY, endX);
        }
        long difference = 2 * endY - endX;
        for (long x = 0, y = 0; x <= endX && y <= endY; x++) {
            yield return (x, y);
            if (difference > 0) {
                y++;
                difference += 2 * (endY - endX);
            } else {
                difference += 2 * endY;
            }
        }
    }
}
