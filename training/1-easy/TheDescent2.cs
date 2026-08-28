using System;

// https://www.codingame.com/training/easy/the-descent---part-2
string[] dimensions = Console.ReadLine()!.Split(' ');
int width = Math.Clamp(int.Parse(dimensions[0]), 3, 15);
int height = Math.Clamp(int.Parse(dimensions[1]), 3, 15);
for (var y = 0; y < height; y++) {
    string[] mountainHeights = Console.ReadLine()!.Split(' ');
    for (var x = 0; x < width; x++) {
        int mountainHeight = Math.Clamp(int.Parse(mountainHeights[x]), 0, 100);
    }
}
dimensions = Console.ReadLine()!.Split(' ');
int starshipA = Math.Clamp(int.Parse(dimensions[0]), 1, 15);
int starshipB = Math.Clamp(int.Parse(dimensions[1]), 1, 15);
int maxShots = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 3000);
Console.WriteLine(1);
