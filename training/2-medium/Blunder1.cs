using System;

// https://www.codingame.com/training/medium/blunder-episode-1
string[] dimensions = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(dimensions[0]), 4, 100);
int width = Math.Clamp(int.Parse(dimensions[1]), 4, 100);
for (var y = 0; y < height; y++) {
    string row = Debug.ReadLine()!;
}
Console.WriteLine("answer");

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
