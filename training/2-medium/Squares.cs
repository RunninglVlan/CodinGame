using System;

// https://www.codingame.com/training/medium/squares
string[] inputs = Console.ReadLine()!.Split(' ');
int gridSide = Math.Clamp(int.Parse(inputs[0]), 1, 20_000);
int drawnSquares = Math.Clamp(int.Parse(inputs[1]), 1, 4);
int targetTiles = Math.Clamp(int.Parse(inputs[2]), 1, 100);
for (var _ = 0; _ < drawnSquares; _++) {
    inputs = Console.ReadLine()!.Split(' ');
    int x = Math.Clamp(int.Parse(inputs[0]), 1, gridSide);
    int y = Math.Clamp(int.Parse(inputs[1]), 1, gridSide);
    int side = Math.Clamp(int.Parse(inputs[2]), 1, gridSide);
}
for (var _ = 0; _ < targetTiles; _++) {
    string[] coordinates = Console.ReadLine()!.Split(' ');
    int x = Math.Clamp(int.Parse(coordinates[0]), 1, gridSide);
    int y = Math.Clamp(int.Parse(coordinates[1]), 1, gridSide);
}
Console.WriteLine("answer");
