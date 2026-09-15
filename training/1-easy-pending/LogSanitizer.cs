using System;
using System.Text.RegularExpressions;

// https://www.codingame.com/contribute/view/15475216bbf3fd5ab519bcb675b26f2cb7bd28
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
for (var _ = 0; _ < lines; _++) {
    string line = Console.ReadLine()!;
    Console.WriteLine(Solution.Sanitize(line));
}

static partial class Solution {
    const string TOKEN_CHAR = @"a-zA-Z\d";
    const string CARD_NUMBER = @"\d{4}-\d{4}-\d{4}-\d{4}";

    [GeneratedRegex($"(?<![{TOKEN_CHAR}]){CARD_NUMBER}(?![{TOKEN_CHAR}])")]
    private static partial Regex CardNumber();

    [GeneratedRegex($"(?<=[^{TOKEN_CHAR}]token=)[{TOKEN_CHAR}]+")]
    private static partial Regex TokenValue();

    public static string Sanitize(string line) {
        line = CardNumber().Replace(line, "[CARD_REDACTED]");
        line = TokenValue().Replace(line, "[TOKEN_REDACTED]");
        return line;
    }
}
