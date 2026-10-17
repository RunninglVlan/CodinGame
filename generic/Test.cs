using System;
using System.Diagnostics;
using System.IO;

static class Test {
    const string CASES_FILE = "Test.Cases.txt";
    const string LOG_FILE = "Test.Log.txt";

    static readonly TextReader consoleIn = Console.In;
    public static bool StepEnabled { get; set; }
    public static string? From { get; set; }
    public static string? Until { get; set; }

    public static void Run(Action<Func<string>> action) {
        File.WriteAllText(LOG_FILE, null);
        string cases = File.ReadAllText(CASES_FILE);
        if (From != null) {
            cases = cases[cases.IndexOf(From, StringComparison.Ordinal)..];
        }
        if (Until != null) {
            cases = cases[..cases.IndexOf(Until, StringComparison.Ordinal)];
        }
        Console.SetIn(new StringReader(cases));
        var totalTime = 0L;
        while (Console.In.Peek() != -1) {
            var stopwatch = Stopwatch.StartNew();
            string? name = Console.ReadLine();
            Log(name, debug: true, color: ConsoleColor.Cyan);
            try {
                action(Expected);
            }
            finally {
                long time = stopwatch.ElapsedMilliseconds;
                totalTime += time;
                Log($"Test took {time}ms", debug: true);
            }
            Log(debug: true);
        }
        Log($"All tests took {totalTime}ms", debug: true);
        return;

        static string Expected() {
            List<string> lines = [];
            for (string line; !string.IsNullOrEmpty(line = Console.ReadLine()!);) {
                lines.Add(line);
            }
            return string.Join("\n", lines);
        }
    }

    public static void Step() {
        if (!StepEnabled) {
            return;
        }
        var current = Console.In;
        Console.SetIn(consoleIn);
        Console.ReadLine();
        Console.SetIn(current);
    }

    public static void Log(object? line = null, bool debug = false, ConsoleColor? color = null) {
        File.AppendAllText(LOG_FILE, $"{line}{Environment.NewLine}");
        if (!debug) {
            return;
        }
        if (color != null) {
            Console.ForegroundColor = color.Value;
        }
        Console.Error.WriteLine(line);
        Console.ResetColor();
    }
}

static class Assert {
    public static bool Throw { get; set; }

    public static void ActualEqualsExpected(string actual, string expected) {
        bool equals = Equals(actual, expected);
        Console.WriteLine(equals ? $"✅ {actual}" : $"❌ found {actual}, but expected {expected}");
        if (!equals && Throw) {
            throw new Exception();
        }
    }
}
