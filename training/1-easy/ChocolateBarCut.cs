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

    /// Based on last logic from https://playtechs.blogspot.com/2007/03/raytracing-on-grid.html
    static IEnumerable<(long, long)> PlotLineFromZero(long endX, long endY) {
        if (endX < endY) {
            (endX, endY) = (endY, endX);
        }
        bool isEvenlyDivisible = endY + 1 != 0 && (endX + 1) % (endY + 1) == 0;
        long x = 0, y = 0;
        long dx = endX, dy = endY;
        yield return (x, y);
        long error = dx - dy;
        dx *= 2;
        dy *= 2;
        while (true) {
            if (error > 0) {
                x++;
                if (x > endX) {
                    break;
                }
                yield return (x, y);
                error -= dy;
            } else {
                y++;
                if (y > endY) {
                    break;
                }
                if (!isEvenlyDivisible) {
                    yield return (x, y);
                }
                error += dx;
            }
        }
    }
}
