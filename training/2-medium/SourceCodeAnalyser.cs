using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

// https://www.codingame.com/training/medium/source-code-analyser
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
var functions = Enumerable.Range(0, lines)
        .Select(_ => Console.ReadLine()!)
        .SelectMany(Tokenizer.FindFunctions);
foreach (var group in functions.GroupBy(it => it)) {
    Console.WriteLine($"{group.Key} {group.Count()}");
}

static partial class Tokenizer {
    static readonly HashSet<string> KEYWORDS = ["if"];

    [GeneratedRegex(@"\w*\(")]
    private static partial Regex Function();

    public static IEnumerable<string> FindFunctions(string line) {
        line = Filter(line);
        foreach (Match match in Function().Matches(line)) {
            string name = match.Value[..^1];
            if (KEYWORDS.Contains(name)) {
                continue;
            }
            yield return name;
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    static string Filter(string line) {
        int commentStart = line.IndexOf("/*", StringComparison.Ordinal);
        if (commentStart != -1) {
            int commentEnd = line.IndexOf("*/", StringComparison.Ordinal);
            line = line[..commentStart] + line[commentEnd..];
        }
        commentStart = line.IndexOf("//", StringComparison.Ordinal);
        if (commentStart != -1) {
            line = line[..commentStart];
        }
        int stringStart = line.IndexOf('"');
        if (stringStart != -1) {
            int stringEnd = line.LastIndexOf('"');
            line = line[..stringStart] + line[stringEnd..];
        }
        return line;
    }
}
