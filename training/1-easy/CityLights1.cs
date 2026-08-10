using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/city-lights-part-1
int height = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
int width = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
var city = new City(width, height);
city.ReadLightSources();
city.PrintBrightness();

class City(int width, int height) {
    const string CELL_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    readonly Dictionary<Point, int> lightSourceRadii = new();

    public void ReadLightSources() {
        for (var y = 0; y < height; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                int radius = CELL_CHARS.IndexOf(line[x]);
                if (radius <= 0) {
                    continue;
                }
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
                Console.Write(brightness < CELL_CHARS.Length ? CELL_CHARS[brightness] : CELL_CHARS.Last());
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
