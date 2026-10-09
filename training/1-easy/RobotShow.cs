using System;

// https://www.codingame.com/training/easy/robot-show
int ductLength = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 1000);
int bots = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
string[] inputs = Console.ReadLine()!.Split(' ');
for (var i = 0; i < bots; i++) {
    int bot = int.Parse(inputs[i]);
}
Console.WriteLine("answer");
