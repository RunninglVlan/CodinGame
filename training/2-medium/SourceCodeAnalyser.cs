using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// https://www.codingame.com/training/medium/source-code-analyser
int lineCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
var tokenizer = new Tokenizer();
var lines = Enumerable.Range(0, lineCount)
        .Select(_ => Console.ReadLine()!)
        .Select(tokenizer.Filter);
string source = string.Join(" ", lines);
string[] functions = [.. tokenizer.FindFunctions(source)];
if (functions.Length == 0) {
    Console.WriteLine("NONE");
}
foreach (var group in functions.GroupBy(it => it).OrderBy(it => it.Key, StringComparer.Ordinal)) {
    Console.WriteLine($"{group.Key} {group.Count()}");
}

partial class Tokenizer {
    static readonly HashSet<string> KEYWORDS = [
        "and", "array", "echo", "else", "elseif", "if", "for", "foreach", "function", "or", "return", "while", "new"
    ];

    [GeneratedRegex(@"(function|class)\s*(?<name>\w+)\s*({|\()")]
    private static partial Regex Definition();

    [GeneratedRegex(@"(?<!\$)(?<name>\w+)\(")]
    private static partial Regex Function();

    readonly HashSet<string> userDefined = [];
    string? multilineToken;

    public string Filter(string line) {
        // todo: refactor
        if (multilineToken != null) {
            int index = line.IndexOf(multilineToken, StringComparison.Ordinal);
            if (index != -1) {
                line = line[index..];
                multilineToken = null;
            } else {
                return string.Empty;
            }
        }
        int commentStart = line.IndexOf(Tokens.COMMENT, StringComparison.Ordinal);
        if (commentStart != -1) {
            line = line[..commentStart];
        }
        commentStart = line.IndexOf(Tokens.COMMENT_START, StringComparison.Ordinal);
        while (commentStart != -1) {
            int commentEnd = line.IndexOf(Tokens.COMMENT_END, StringComparison.Ordinal);
            if (commentEnd != -1) {
                line = line[..commentStart] + line[(commentEnd + 1)..];
                commentStart = line.IndexOf(Tokens.COMMENT_START, StringComparison.Ordinal);
            } else {
                multilineToken = Tokens.COMMENT_END;
                return line[..commentStart];
            }
        }
        int stringStart = line.IndexOf(Tokens.QUOTE_SINGLE, StringComparison.Ordinal);
        while (stringStart != -1) {
            int stringEnd = line.IndexOf(Tokens.QUOTE_SINGLE, stringStart + 1, StringComparison.Ordinal);
            if (stringEnd == -1) {
                break;
            }
            line = line[..stringStart] + line[(stringEnd + 1)..];
            stringStart = line.IndexOf(Tokens.QUOTE_SINGLE, StringComparison.Ordinal);
        }
        stringStart = line.IndexOf(Tokens.QUOTE_DOUBLE, StringComparison.Ordinal);
        while (stringStart != -1) {
            int stringEnd = line.IndexOf(Tokens.QUOTE_DOUBLE, stringStart + 1, StringComparison.Ordinal);
            if (stringEnd != -1) {
                line = line[..stringStart] + line[(stringEnd + 1)..];
                stringStart = line.IndexOf(Tokens.QUOTE_DOUBLE, StringComparison.Ordinal);
            } else {
                multilineToken = Tokens.QUOTE_DOUBLE;
                return line[..stringStart];
            }
        }
        return line;
    }

    public IEnumerable<string> FindFunctions(string source) {
        foreach (Match match in Definition().Matches(source)) {
            string name = match.Groups["name"].Value;
            userDefined.Add(name);
        }
        foreach (Match match in Function().Matches(source)) {
            string name = match.Groups["name"].Value;
            if (KEYWORDS.Contains(name.ToLower()) || userDefined.Contains(name)) {
                continue;
            }
            yield return name;
        }
    }
}

static class Tokens {
    public const string COMMENT = "//";
    public const string COMMENT_START = "/*";
    public const string COMMENT_END = "*/";
    public const string QUOTE_SINGLE = "'";
    public const string QUOTE_DOUBLE = "\"";
}
