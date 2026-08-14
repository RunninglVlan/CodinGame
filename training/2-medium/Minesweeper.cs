using System;

// https://www.codingame.com/training/medium/minesweeper
string[] dimensions = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(dimensions[0]), 3, 20);
int width = Math.Clamp(int.Parse(dimensions[1]), 3, 20);
int mines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
for (var i = 0; i < height; i++) {
    string line = Console.ReadLine()!;
}
for (var i = 0; i < mines; i++) {
    Console.WriteLine("0 0");
}
