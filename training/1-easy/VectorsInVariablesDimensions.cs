using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/vectors-in-variables-dimensions
Point.Dimensions = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
int pointCount = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 100);
var points = new Point[pointCount];
for (var i = 0; i < pointCount; i++) {
    string[] pointData = Console.ReadLine()!.Split(Point.PARENTHESIS_LEFT);
    string name = pointData.First();
    string coordinateData = pointData.Last()[..^1];
    int[] coordinates = [.. coordinateData.Split(Point.SEPARATOR).Select(it => Math.Clamp(int.Parse(it), -20, 20))];
    points[i] = new Point(name, coordinates);
}
List<(Point a, Point b, double distance)> distances = [];
for (var a = 0; a < pointCount; a++) {
    var aPoint = points[a];
    for (var b = 0; b < pointCount; b++) {
        if (b == a) {
            continue;
        }
        var bPoint = points[b];
        distances.Add((aPoint, bPoint, Point.Distance(aPoint, bPoint)));
    }
}
var min = distances.MinBy(it => it.distance);
var max = distances.MaxBy(it => it.distance);
Console.WriteLine(min.b - min.a);
Console.WriteLine(max.b - max.a);

record Point(string Name, int[] Coordinates) {
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

    public static Point operator -(Point a, Point b) {
        string name = b.Name + a.Name;
        var coordinates = new int[Dimensions];
        for (var i = 0; i < Dimensions; i++) {
            int diff = a.Coordinates[i] - b.Coordinates[i];
            coordinates[i] = diff;
        }
        return new Point(name, coordinates);
    }

    public override string ToString() {
        string coordinates = string.Join(SEPARATOR, Coordinates);
        return $"{Name}{PARENTHESIS_LEFT}{coordinates}{PARENTHESIS_RIGHT}";
    }
}
