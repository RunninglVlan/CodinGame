using System;

/// https://www.codingame.com/training/medium/network-cabling
static class NetworkCabling {
    static void Main() {
        var maxCoord = (int)Math.Pow(2, 30);
        int buildings = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100_000);
        for (var i = 0; i < buildings; i++) {
            string[] coordinates = Console.ReadLine()!.Split(' ');
            int x = Math.Clamp(int.Parse(coordinates[0]), -maxCoord, maxCoord);
            int y = Math.Clamp(int.Parse(coordinates[1]), -maxCoord, maxCoord);
        }
        Console.WriteLine("answer");
    }
}
