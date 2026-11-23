using System;
using System.Linq;

// https://www.codingame.com/contribute/view/15719447898a8b794f809022fb88be55c75ff3
string tour = Console.ReadLine()!;
int[] corners = [.. tour.Select(it => (int)char.GetNumericValue(it))];
var path = 0f;
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
float Distance(int current, int next) => (current, next) switch {
    (1, 2) => 1,
    (1, 4) => 1,
    _ => throw new ArgumentException($"Unexpected: {(current, next)}")
};
