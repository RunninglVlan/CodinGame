using System;

static class Assert {
    public static void ActualEqualsExpected(string actual, string expected) {
        Console.WriteLine(Equals(actual, expected) ? $"✅ {actual}" : $"❌ found {actual}, but expected {expected}");
    }
}
