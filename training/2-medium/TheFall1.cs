using System;
using System.Diagnostics.CodeAnalysis;

namespace TheFall1;

/// https://www.codingame.com/training/medium/the-fall-episode-1
static class TheFall1 {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
            // One line containing the X Y coordinates of the room in which you believe Indy will be on the next turn
            Console.WriteLine("0 0");
        }
    }
}

class Game {
    int width, height;

    public void Init() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(inputs[0]), 1, 20);
        height = Math.Clamp(int.Parse(inputs[1]), 1, 20);
        for (var y = 0; y < height; y++) {
            // represents a line in the grid and contains W integers. Each integer represents one room of a given type
            string line = Console.ReadLine()!;
        }
        int xExit = Math.Clamp(int.Parse(Console.ReadLine()!), 0, width - 1);
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int x = Math.Clamp(int.Parse(inputs[0]), 0, width - 1);
        int y = Math.Clamp(int.Parse(inputs[1]), 0, height - 1);
        // Indy's entrance point into the current room:
        // - TOP if Indy enters from above,
        // - LEFT if Indy enters from the left and
        // - RIGHT if Indy enters from the right
        string position = inputs[2];
    }
}
