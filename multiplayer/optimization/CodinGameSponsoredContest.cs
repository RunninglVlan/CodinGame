using System;

// https://www.codingame.com/multiplayer/optimization/codingame-sponsored-contest
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game {
    static readonly char[] ACTIONS = ['A', 'B', 'C', 'D', 'E'];

    int thirdInitInput;

    public void Init() {
        int firstInitInput = int.Parse(Console.ReadLine()!);
        int secondInitInput = int.Parse(Console.ReadLine()!);
        thirdInitInput = int.Parse(Console.ReadLine()!);
    }

    public void Loop() {
        string firstInput = Console.ReadLine()!;
        string secondInput = Console.ReadLine()!;
        string thirdInput = Console.ReadLine()!;
        string fourthInput = Console.ReadLine()!;
        for (var i = 0; i < thirdInitInput; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int fifthInput = int.Parse(inputs[0]);
            int sixthInput = int.Parse(inputs[1]);
        }
        int random = Random.Shared.Next(0, ACTIONS.Length);
        Console.WriteLine(ACTIONS[random]);
    }
}
