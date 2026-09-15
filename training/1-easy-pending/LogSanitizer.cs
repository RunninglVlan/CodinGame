using System;
using System.Text;
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
        line = CardNumber().Replace(line, "[CARD_REDACTED]");
        var sanitized = new StringBuilder();
        var lastEnd = 0;
        foreach (Match match in TokenValue().Matches(line)) {
            var tokenGroup = match.Groups[1];
            if (tokenGroup.Captures.Count > 1) {
                continue;
            }
            sanitized.Append(line, lastEnd, match.Index - lastEnd);
            sanitized.Append($"{tokenGroup}[TOKEN_REDACTED]");
            lastEnd = match.Index + match.Length;
        }
        sanitized.Append(line, lastEnd, line.Length - lastEnd);
        return sanitized.ToString();
    }
}
