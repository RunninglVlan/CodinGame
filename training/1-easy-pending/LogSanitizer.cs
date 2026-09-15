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

    [GeneratedRegex(@"(?<!\d-)\b\d{4}-\d{4}-\d{4}-\d{4}\b(?!-\d)")]
    private static partial Regex CardNumber();

    [GeneratedRegex($"(?<![{TOKEN_CHAR}])(token=)+([{TOKEN_CHAR}]+)")]
    private static partial Regex TokenValue();

    public static string Sanitize(string line) {
        line = TokenValue().Replace(line, it => {
            var tokenGroup = it.Groups[1];
            return tokenGroup.Captures.Count > 1 ? it.Value : $"{tokenGroup}[TOKEN_REDACTED]";
        });
        line = CardNumber().Replace(line, "[CARD_REDACTED]");
        return line;
    }
}
