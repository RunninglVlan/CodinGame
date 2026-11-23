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

double Distance(int current, int next) {
    const int shortest = 1;
    double diagonal = Math.Sqrt(2);
    double opposite = 2 * Math.Sqrt(1.25);
    return (current, next) switch {
        (1, 2) or (1, 4) or (1, 5) or (2, 3) or (2, 6) or (3, 4) or (3, 7)
                or (4, 8) or (5, 6) or (5, 8) or (6, 7) or (7, 8) => shortest,
        (1, 3) or (1, 6) or (1, 8) or (2, 4) or (2, 5) or (2, 7) or (3, 6)
                or (3, 8) or (4, 5) or (4, 7) or (5, 7) or (6, 8) => diagonal,
        (1, 7) or (2, 8) or (3, 5) or (4, 6) => opposite,
        _ => throw new ArgumentException($"{(current, next)}")
    };
}
