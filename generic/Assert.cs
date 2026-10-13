using System;

static class Assert {
    public static void ActualEqualsExpected(string actual, string expected) {
        bool equals = Equals(actual, expected);
        Console.WriteLine(equals ? $"✅ {actual}" : $"❌ found {actual}, but expected {expected}");
        if (!equals) {
            throw new Exception();
        }
    }
}