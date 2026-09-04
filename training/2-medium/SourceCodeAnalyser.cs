using System;
using System.Collections.Generic;
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
    [GeneratedRegex(@"\w*\(")]
    private static partial Regex Function();

    public static IEnumerable<string> FindFunctions(string line) {
        foreach (Match match in Function().Matches(line)) {
            yield return match.Value[..^1];
        }
    }
}
