using System;

// https://www.codingame.com/training/easy/the-descent---part-2
string[] dimensions = Console.ReadLine()!.Split(' ');
int width = Math.Clamp(int.Parse(dimensions[0]), 3, 15);
int height = Math.Clamp(int.Parse(dimensions[1]), 3, 15);
var grid = new Grid(width, height);
grid.Read();
int maxShots = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 3000);
int shotsToLand = grid.ShotsToLand();
Console.WriteLine(shotsToLand <= maxShots ? shotsToLand : "Not Possible");

class Grid(int width, int height) {
    readonly int[,] mountainHeights = new int[width, height];
    Starship starship;

    public void Read() {
        for (var y = 0; y < height; y++) {
            string[] heights = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < width; x++) {
                mountainHeights[x, y] = Math.Clamp(int.Parse(heights[x]), 0, 100);
            }
        }
        string[] dimensions = Console.ReadLine()!.Split(' ');
        int a = Math.Clamp(int.Parse(dimensions[0]), 1, 15);
        int b = Math.Clamp(int.Parse(dimensions[1]), 1, 15);
        starship = new Starship(a, b);
    }

    public int ShotsToLand() => 0;
}

readonly record struct Starship(int A, int B);
