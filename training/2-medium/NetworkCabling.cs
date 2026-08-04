using System;
using System.Linq;

/// https://www.codingame.com/training/medium/network-cabling
static class NetworkCabling {
    static void Main() {
        var maxCoord = (int)Math.Pow(2, 30);
        int buildingCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100_000);
        var buildings = new Point[buildingCount];
        for (var i = 0; i < buildingCount; i++) {
            string[] coordinates = Console.ReadLine()!.Split(' ');
            int x = Math.Clamp(int.Parse(coordinates[0]), -maxCoord, maxCoord);
            int y = Math.Clamp(int.Parse(coordinates[1]), -maxCoord, maxCoord);
            buildings[i] = new Point(x, y);
        }

        int minX = buildings.Min(it => it.X);
        int maxX = buildings.Max(it => it.X);
        long medianY = buildings.Select(it => (long)it.Y).OrderBy(it => it).ElementAt(buildings.Length / 2);
        int mainCable = maxX - minX;
        long remainingCables = buildings.Sum(it => Math.Abs(it.Y - medianY));
        Console.WriteLine(mainCable + remainingCables);
    }
}

readonly record struct Point(int X, int Y);
