using System;

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string ReadLine() {
        string line = Console.ReadLine()!;
        Console.Error.WriteLine(line);
        return line;
    }
}
