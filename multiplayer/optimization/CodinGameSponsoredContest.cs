using System;

Debug.Enabled = true;
// https://www.codingame.com/multiplayer/optimization/codingame-sponsored-contest
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game {
    static readonly char[] ACTIONS = ['A', 'B', 'C', 'D', 'E'];

    int thirdInitInput;
    int turn;

    public void Init() {
        int dimension1 = int.Parse(Console.ReadLine()!);
        int dimension2 = int.Parse(Console.ReadLine()!);
        thirdInitInput = int.Parse(Console.ReadLine()!);
        Debug.Log($"{dimension1}x{dimension2}, {thirdInitInput}");
    }

    public void Loop() {
        string gridView1 = Console.ReadLine()!;
        string gridView2 = Console.ReadLine()!;
        string gridView3 = Console.ReadLine()!;
        string gridView4 = Console.ReadLine()!;
        var lines = new string[thirdInitInput];
        for (var i = 0; i < thirdInitInput; i++) {
            string line = Console.ReadLine()!;
            lines[i] = line;
            string[] inputs = line.Split(' ');
            int fifthInput = int.Parse(inputs[0]);
            int sixthInput = int.Parse(inputs[1]);
        }
        if (turn++ > 0) {
            Debug.Log();
        }
        Debug.Log(gridView1);
        Debug.Log(gridView2);
        Debug.Log(gridView3);
        Debug.Log(gridView4);
        for (var i = 0; i < thirdInitInput; i++) {
            Debug.Log(lines[i]);
        }
        int random = Random.Shared.Next(0, ACTIONS.Length);
        // Console.WriteLine(ACTIONS[random]);
        Console.WriteLine(ACTIONS[2]);
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
