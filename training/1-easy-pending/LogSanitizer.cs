using System;
using System.Text.RegularExpressions;

// https://www.codingame.com/contribute/view/15475216bbf3fd5ab519bcb675b26f2cb7bd28
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
for (var _ = 0; _ < lines; _++) {
    string line = Console.ReadLine()!;
    Console.WriteLine(Solution.Sanitize(line));
}

static partial class Solution {
    [GeneratedRegex(@"(?<!\w)\d{4}-\d{4}-\d{4}-\d{4}(?!\w)")]
    private static partial Regex CardNumber();

    [GeneratedRegex(@"(?<=[^a-zA-Z\d]token=)[a-zA-Z\d]+")]
    private static partial Regex TokenValue();

    public static string Sanitize(string line) {
        line = CardNumber().Replace(line, "[CARD_REDACTED]");
        line = TokenValue().Replace(line, "[TOKEN_REDACTED]");
        return line;
    }
}
