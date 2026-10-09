using System;

// https://www.codingame.com/training/easy/the-leaking-bathtub
int surfaceArea = int.Parse(Console.ReadLine()!);
int height = int.Parse(Console.ReadLine()!);
int flow = int.Parse(Console.ReadLine()!);
int leaks = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1000);
for (var _ = 0; _ < leaks; _++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    int leakHeight = Math.Clamp(int.Parse(inputs[0]), 0, height - 1);
    int leakFlow = int.Parse(inputs[1]);
}
Console.WriteLine("00:00:00");
