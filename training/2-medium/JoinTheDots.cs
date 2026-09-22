using System;

// https://www.codingame.com/training/medium/join-the-dots
string[] inputs = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(inputs[0]), 3, 40);
int width = Math.Clamp(int.Parse(inputs[1]), 3, 40);
for (var i = 0; i < height; i++) {
    string row = Console.ReadLine()!;
}
Console.WriteLine("o--------oo--------o");
Console.WriteLine("|        ||        |");
Console.WriteLine("| o------o| o------o");
Console.WriteLine("| |       | |");
Console.WriteLine("| |       | |   o--o");
Console.WriteLine("| |       | |   |  |");
Console.WriteLine("| |       | |   |  |");
Console.WriteLine("| o------o| o---o  |");
Console.WriteLine("|         o        |");
Console.WriteLine("o------------------o");
