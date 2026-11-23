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
Console.WriteLine(GetValue(Debug.ReadLine()!));
return;

int GetValue(string x) {
    int bracket = x.IndexOf(leftBracket);
    string name = x[..bracket];
    int index;
    int afterBracket = bracket + 1;
    if (char.IsLetter(x[afterBracket])) {
        index = GetValue(x[afterBracket..]);
    } else {
        (int indexStart, int indexEnd) = (afterBracket, x.IndexOf(']'));
        index = int.Parse(x[indexStart..indexEnd]);
    }
    return arrays[name][index];
}

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
