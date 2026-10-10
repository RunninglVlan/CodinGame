using System;
using System.Linq;

// https://www.codingame.com/contribute/view/154026f4c56967fa2dfb15ca7bbd572c9fe863
Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100); // cartLines
int[] carts = [.. Console.ReadLine()!.Split(' ').Select(it => Math.Clamp(int.Parse(it), 0, 250))];
string actions = Console.ReadLine()!;
Console.WriteLine("number of carts per line or IMPOSSIBLE");
