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
        int firstInitInput = int.Parse(Debug.ReadLine()!);
        int secondInitInput = int.Parse(Debug.ReadLine()!);
        thirdInitInput = int.Parse(Debug.ReadLine()!);
    }

    public void Loop() {
        string firstInput = Debug.ReadLine()!;
        string secondInput = Debug.ReadLine()!;
        string thirdInput = Debug.ReadLine()!;
        string fourthInput = Debug.ReadLine()!;
        for (var i = 0; i < thirdInitInput; i++) {
            string[] inputs = Debug.ReadLine()!.Split(' ');
            int fifthInput = int.Parse(inputs[0]);
            int sixthInput = int.Parse(inputs[1]);
        }
        int random = Random.Shared.Next(0, ACTIONS.Length);
        // Console.WriteLine(ACTIONS[random]);
        Console.WriteLine(ACTIONS[0]);
    }
}

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; }

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        if (!Enabled) {
            return;
        }
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
