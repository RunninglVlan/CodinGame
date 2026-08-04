using System;
using System.Linq;

/// https://www.codingame.com/training/medium/network-cabling
static class NetworkCabling {
    static void Main() {
        var solution = new Solution();
        solution.ReadInput();
        Console.WriteLine(solution.CableLength());
    }
}

class Solution {
    Point[] buildings = null!;

    public void ReadInput() {
        var maxCoord = (int)Math.Pow(2, 30);
        int buildingCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100_000);
        buildings = new Point[buildingCount];
        for (var i = 0; i < buildingCount; i++) {
            string[] coordinates = Console.ReadLine()!.Split(' ');
            int x = Math.Clamp(int.Parse(coordinates[0]), -maxCoord, maxCoord);
            int y = Math.Clamp(int.Parse(coordinates[1]), -maxCoord, maxCoord);
            buildings[i] = new Point(x, y);
        }
    }

    public long CableLength() {
        int minX = buildings.Min(it => it.X);
        int maxX = buildings.Max(it => it.X);
        int main = maxX - minX;
        long[] ys = buildings.Select(it => (long)it.Y).Distinct().ToArray();
        long minRemaining = ys.Select(mainY => ys.Sum(y => Math.Abs(y - mainY))).Min();
        return main + minRemaining;
    }
}

readonly record struct Point(int X, int Y) {
    public override string ToString() => $"({X}, {Y})";
}
