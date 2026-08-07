using System;
using System.Linq;

// https://codeforces.com/contest/2252/problem/A
int tests = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var i = 0; i < tests; i++) {
    int cardCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
    int[] cards = [.. Console.ReadLine()!.Split(' ').Select(int.Parse)];
    Console.WriteLine(cards.Sum());
}
