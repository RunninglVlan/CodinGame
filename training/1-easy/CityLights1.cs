using System;
using System.Collections.Generic;

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
    const string CELL_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    readonly Dictionary<Point, int> lightSourceRadii = new();

    public void Read(int y, string line) {
        for (var x = 0; x < width; x++) {
            int radius = CELL_CHARS.IndexOf(line[x]);
            if (radius > 0) {
                lightSourceRadii.Add(new Point(x, y), radius);
            }
        }
    }

    public void PrintBrightness() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                var brightness = 0;
                foreach ((var cell, int radius) in lightSourceRadii) {
                    double distance = Point.Distance(new Point(x, y), cell);
                    brightness += Math.Max(radius - (int)Math.Round(distance), 0);
                }
                Console.Write(CELL_CHARS[brightness]);
            }
            Console.WriteLine();
        }
    }
}

readonly record struct Point(int X, int Y) {
    public static double Distance(Point a, Point b) {
        int diffX = a.X - b.X;
        int diffY = a.Y - b.Y;
        return Math.Sqrt(diffX * diffX + diffY * diffY);
    }
}
