using System;

/// https://www.codingame.com/training/easy/city-lights-part-1
static class CityLights1 {
    static void Main() {
        int height = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
        int width = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
        var city = new City(width, height);
        for (var y = 0; y < height; y++) {
            string line = Console.ReadLine()!;
            city.Read(y, line);
        }
        city.PrintBrightness();
    }
}

class City(int width, int height) {
    const string RADIUS_CHARS = ".123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    readonly int[,] radiusCells = new int[width, height];

    public void Read(int y, string line) {
        for (var x = 0; x < width; x++) {
            radiusCells[x, y] = RADIUS_CHARS.IndexOf(line[x]);
        }
    }

    public void PrintBrightness() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                Console.Write(radiusCells[x, y]);
            }
            Console.WriteLine();
        }
    }
}
