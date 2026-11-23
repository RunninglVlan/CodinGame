using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/offset-arrays
const char leftBracket = '[';
Dictionary<string, Dictionary<int, int>> arrays = [];
int assignments = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var _ = 0; _ < assignments; _++) {
    string assignment = Debug.ReadLine()!;
    int arrayBracket = assignment.IndexOf(leftBracket);
    string arrayName = assignment[..arrayBracket];
    Dictionary<int, int> array = [];
    string[] parts = assignment[arrayBracket..].Split(" = ");
    string[] range = parts.First()[1..^1].Split("..");
    int startIndex = int.Parse(range.First());
    string[] values = parts.Last().Split(' ');
    foreach (string value in values) {
        array[startIndex++] = int.Parse(value);
    }
    arrays[arrayName] = array;
}
string x = Console.ReadLine()!;
int bracket = x.IndexOf(leftBracket);
string name = x[..bracket];
(int start, int end, int value) index = (bracket + 1, x.IndexOf(']'), 0);
index.value = int.Parse(x[index.start..index.end]);
Console.WriteLine(arrays[name][index.value]);

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; } = true;

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
