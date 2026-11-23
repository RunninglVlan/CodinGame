using System;
using System.Linq;

// https://www.codingame.com/contribute/view/15719447898a8b794f809022fb88be55c75ff3
string tour = Console.ReadLine()!;
int[] corners = [.. tour.Select(it => (int)char.GetNumericValue(it))];
var path = 0d;
for (var i = 0; i < corners.Length - 1; i++) {
    int current = corners[i];
    int next = corners[i + 1];
    path += Distance(Math.Min(current, next), Math.Max(current, next));
}
Console.WriteLine($"{path:F3}");
return;

/* 8----------7
  /|         /|
 / |        / |
4----------3  |
|  |       |  |
|  5-------|--6
| /        | /
|/         |/
1----------2 */
double Distance(int current, int next) {
    const int shortest = 1;
    double diagonal = Math.Sqrt(2);
    double opposite = 2 * Math.Sqrt(1.25);
    return (current, next) switch {
        (1, 2) or (1, 4) => shortest,
        (4, 7) or (6, 8) => diagonal,
        (3, 5) => opposite,
        _ => throw new ArgumentException($"{(current, next)}")
    };
}
