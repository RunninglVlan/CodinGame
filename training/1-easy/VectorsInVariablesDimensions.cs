using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/vectors-in-variables-dimensions
int dimensions = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
int pointCount = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 100);
Dictionary<string, int[]> points = [];
for (var y = 0; y < pointCount; y++) {
    string[] pointData = Console.ReadLine()!.Split('(');
    string name = pointData.First();
    string coordinateData = pointData.Last()[..^1];
    points[name] = [.. coordinateData.Split(',').Select(it => Math.Clamp(int.Parse(it), -20, 20))];
}
Console.WriteLine("shortest_vector");
Console.WriteLine("longest_vector");
