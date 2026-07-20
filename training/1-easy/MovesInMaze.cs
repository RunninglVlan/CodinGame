using System;

namespace MovesInMaze;

/// https://www.codingame.com/training/easy/moves-in-maze
static class MovesInMaze {
    static void Main() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int width = Math.Clamp(int.Parse(inputs[0]), 3, 30);
        int height = Math.Clamp(int.Parse(inputs[1]), 3, 30);
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            Console.WriteLine(row);
        }
    }
}
