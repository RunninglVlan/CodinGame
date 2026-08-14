using System;

// Visual
// catch up and win
// https://www.codingame.com/training/easy/catching-up
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game {
    public void Init() {
        // representing after how many turns the sus man will make another move
        int k = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 3);
        for (var y = 0; y < 10; y++) {
            string map = Console.ReadLine()!;
        }
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int eneY = Math.Clamp(int.Parse(inputs[0]), 1, 9); // the sus man's coordinate,two integers separated by space in a single line
        int eneX = Math.Clamp(int.Parse(inputs[1]), 1, 9); // the sus man's coordinate,two integers separated by space in a single line
        Console.WriteLine('U');
    }
}
