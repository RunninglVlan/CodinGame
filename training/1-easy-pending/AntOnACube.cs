using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/contribute/view/15719447898a8b794f809022fb88be55c75ff3
string tour = Console.ReadLine()!;
int[] corners = [.. tour.Select(it => (int)char.GetNumericValue(it))];
/* 8----------7
  /|         /|
 / |        / |
4----------3  |
|  |       |  |
|  5-------|--6
| /        | /
|/         |/
1----------2 */
Dictionary<int, Point> cube = new() {
    { 1, new Point(0, 0, 0) },
    { 2, new Point(1, 0, 0) },
    { 3, new Point(1, 1, 0) },
    { 4, new Point(0, 1, 0) },
    { 5, new Point(0, 0, 1) },
    { 6, new Point(1, 0, 1) },
    { 7, new Point(1, 1, 1) },
    { 8, new Point(0, 1, 1) }
};
var path = 0d;
for (var i = 0; i < corners.Length - 1; i++) {
    int current = corners[i];
    int next = corners[i + 1];
    path += Point.Distance(cube[current], cube[next]);
}
Console.WriteLine($"{path:F3}");

readonly record struct Point(int X, int Y, int Z) {
    public static double Distance(Point a, Point b) {
        int diffX = a.X - b.X;
        int diffY = a.Y - b.Y;
        int diffZ = a.Z - b.Z;
        return Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ);
    }
}
