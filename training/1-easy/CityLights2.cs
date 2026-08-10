using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/city-lights-part-2
int length = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
int width = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
int depth = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
Math.Clamp(int.Parse(Console.ReadLine()!), 1, 450);
var city = new City(length, width, depth);
city.ReadLightSources();
city.PrintBrightness();

class City(int width, int length, int depth) {
    const string CELL_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    readonly Dictionary<Point, int> lightSourceRadii = new();

    public void ReadLightSources() {
        for (var z = 0; z < depth; z++) {
            for (var y = 0; y < length; y++) {
                string line = Console.ReadLine()!;
                for (var x = 0; x < width; x++) {
                    int radius = CELL_CHARS.IndexOf(line[x]);
                    if (radius <= 0) {
                        continue;
                    }
                    lightSourceRadii.Add(new Point(x, y, z), radius);
                }
            }
            if (z != depth - 1) {
                Console.ReadLine();
            }
        }
    }

    public void PrintBrightness() {
        for (var z = 0; z < depth; z++) {
            for (var y = 0; y < length; y++) {
                for (var x = 0; x < width; x++) {
                    var brightness = 0;
                    foreach ((var cell, int radius) in lightSourceRadii) {
                        double distance = Point.Distance(new Point(x, y, z), cell);
                        brightness += Math.Max(radius - (int)Math.Round(distance), 0);
                    }
                    Console.Write(brightness < CELL_CHARS.Length ? CELL_CHARS[brightness] : CELL_CHARS.Last());
                }
                Console.WriteLine();
            }
            if (z != depth - 1) {
                Console.WriteLine();
            }
        }
    }
}

readonly record struct Point(int X, int Y, int Z) {
    public static double Distance(Point a, Point b) {
        int diffX = a.X - b.X;
        int diffY = a.Y - b.Y;
        int diffZ = a.Z - b.Z;
        return Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ);
    }
}
