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
    Debug.ReadLine();
    int number = Math.Clamp(int.Parse(Debug.ReadLine()!), 1, 1_000_000);
    int expected = int.Parse(Console.ReadLine()!);
    {
        var stopwatch = Stopwatch.StartNew();
        int actual;
        try {
            actual = await Task.Run(() => Display(number)).WaitAsync(TimeSpan.FromSeconds(1));
        } catch (TimeoutException) {
            Debug.Log($"Timed out: {stopwatch.ElapsedMilliseconds}");
            continue;
        }
        Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ found {actual}, but expected {expected}");
        Debug.Log($"Elapsed: {stopwatch.ElapsedMilliseconds}");
        Debug.Log();
    }
    {
    }
}
return;

int Display(int required) {
    var queue = new PriorityQueue<int, int>();
    queue.Enqueue(0, 0);
    while (queue.TryDequeue(out int current, out int operations)) {
        if (current == required) {
            return operations;
        }
        foreach (int next in Next(current)) {
            queue.Enqueue(next, operations + 1);
        }
    }
    throw new UnreachableException();

    IEnumerable<int> Next(int current) {
        yield return current + 1;
        if (current > 0) {
            yield return current - 1;
            yield return current * 2;
        }
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
