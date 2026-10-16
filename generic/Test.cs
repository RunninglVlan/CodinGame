using System;
using System.IO;

static class Test {
    const string FILE = "Test.Log.txt";

    static readonly TextReader consoleIn = Console.In;
    public static bool StepEnabled { get; set; }

    static Test() => File.WriteAllText(FILE, null);

    public static void Cases(string cases) {
        Console.SetIn(new StringReader(cases));
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

    public static void Log(object? line = null) => File.AppendAllText(FILE, $"{line}{Environment.NewLine}");
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
