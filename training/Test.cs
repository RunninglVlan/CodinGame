using System.Diagnostics;

const string testCase = """
                        2
                        Test 4
                        1023
                        12
                        Test 5
                        35641
                        22
                        """;
Console.SetIn(new StringReader(testCase));

// https://www.codingame.com/training/easy/button-mash
int tests = int.Parse(Console.ReadLine()!);
for (var _ = 0; _ < tests; _++) {
    var stopwatch = Stopwatch.StartNew();
    Debug.ReadLine();
    int number = Math.Clamp(int.Parse(Debug.ReadLine()!), 1, 1_000_000);
    int actual;
    try {
        actual = await Task.Run(() => Display(number)).WaitAsync(TimeSpan.FromSeconds(1));
    } catch (TimeoutException) {
        Debug.Log($"Timed out: {stopwatch.ElapsedMilliseconds}");
        continue;
    }
    int expected = int.Parse(Console.ReadLine()!);
    Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ found {actual}, but expected {expected}");
    Debug.Log($"Elapsed: {stopwatch.ElapsedMilliseconds}");
}
return;

int Display(int required) {
    var queue = new Queue<(int value, int operations)>();
    queue.Enqueue((0, 0));
    while (queue.TryDequeue(out var current)) {
        if (current.value == required) {
            return current.operations;
        }
        foreach (int next in Next(current.value)) {
            queue.Enqueue((next, current.operations + 1));
        }
    }
    throw new UnreachableException();

    IEnumerable<int> Next(int current) {
        yield return current + 1;
        yield return current - 1;
        yield return current * 2;
    }
}

static class Debug {
    const int TAB_LENGTH = 1;

    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
