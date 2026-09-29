using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/vectors-in-variables-dimensions
Point.Dimensions = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
int pointCount = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 100);
Dictionary<string, Point> points = [];
for (var y = 0; y < pointCount; y++) {
    string[] pointData = Console.ReadLine()!.Split(Point.PARENTHESIS_LEFT);
    string name = pointData.First();
    string coordinateData = pointData.Last()[..^1];
    int[] coordinates = [.. coordinateData.Split(Point.SEPARATOR).Select(it => Math.Clamp(int.Parse(it), -20, 20))];
    points[name] = new Point(coordinates);
}
var first = points.First();
var last = points.Last();
Console.Error.WriteLine($"{first.Key}-{last.Key}: {Point.Distance(first.Value, last.Value)}");
Console.WriteLine("shortest_vector");
Console.WriteLine("longest_vector");

readonly record struct Point(int[] Coordinates) {
    public const char PARENTHESIS_LEFT = '(';
    const char PARENTHESIS_RIGHT = ')';
    public const char SEPARATOR = ',';

    public static int Dimensions { private get; set; }

    public static double Distance(Point a, Point b) {
        var sum = 0;
        for (var i = 0; i < Dimensions; i++) {
            int diff = a.Coordinates[i] - b.Coordinates[i];
            sum += diff * diff;
        }
        return Math.Sqrt(sum);
    }

    public override string ToString() {
        string coordinates = string.Join(SEPARATOR, Coordinates);
        return $"{PARENTHESIS_LEFT}{coordinates}{PARENTHESIS_RIGHT}";
    }
}
