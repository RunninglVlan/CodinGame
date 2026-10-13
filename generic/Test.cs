using System;
using System.IO;

static class Test {
    static TextReader consoleIn = null!;
    static StringReader stringIn = null!;

    public static void Cases(string cases) {
        consoleIn = Console.In;
        Console.SetIn(stringIn = new StringReader(cases));
    }

    public static void ReadLine() {
        Console.SetIn(consoleIn);
        Console.ReadLine();
        Console.SetIn(stringIn);
    }
}

static class Assert {
    public static void ActualEqualsExpected(string actual, string expected) {
        bool equals = Equals(actual, expected);
        Console.WriteLine(equals ? $"✅ {actual}" : $"❌ found {actual}, but expected {expected}");
        if (!equals) {
            throw new Exception();
        }
    }
}
