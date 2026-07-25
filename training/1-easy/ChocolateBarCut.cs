using System;
using System.Collections.Generic;
using System.Linq;

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
        // PlotLine works when endX > endY, so dimensions need to be flipped when it's the other way around
        (long endX, long endY) = width > height ? (width, height) : (height, width);
        return PlotLineFromZero(endX, endY).Count();
    }

    static IEnumerable<(long, long)> PlotLineFromZero(long endX, long endY) {
        long difference = 2 * endY - endX;
        for (long x = 0, y = 0; x < endX && y < endY;) {
            if (difference > 0) {
                difference += 2 * (endY - endX);
                if (difference != 0) {
                    yield return (x, y);
                }
                x++;
                yield return (x, y);
                y++;
            } else {
                yield return (x, y);
                x++;
                difference += 2 * endY;
            }
        }
    }
}
