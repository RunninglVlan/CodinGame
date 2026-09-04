using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

// https://www.codingame.com/training/medium/source-code-analyser
int lineCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
/*
 * Source might contain multiple whitespaces: space, tab (\t), new line (\n) or linefeed (\r).
 */
var tokenizer = new Tokenizer();
var lines = Enumerable.Range(0, lineCount)
        .Select(_ => Debug.ReadLine()!)
        .Select(tokenizer.Filter);
string source = string.Join(" ", lines);
string[] functions = [.. tokenizer.FindFunctions(source)];
Debug.Log();
// If no library call encountered, output NONE
if (functions.Length == 0) {
    Console.WriteLine("NONE");
}
// Output lines shall be sorted by the function names (by ascending ascii values).
foreach (var group in functions.GroupBy(it => it).OrderBy(it => it.Key, StringComparer.Ordinal)) {
    Console.WriteLine($"{group.Key} {group.Count()}");
}

partial class Tokenizer {
    /// Following reserved words shall be not treated as function calls and omitted from output:
    ///"and", "array", "echo", "else", "elseif", "if", "for", "foreach", "function", "or", "return", "while", "new" (regardless of lower, upper or mixed case)
    // todo: complete list
    static readonly HashSet<string> KEYWORDS = ["and", "array", "echo", "if", "for", "foreach"];

    [GeneratedRegex(@"(function|class)\s*(\w+)\s*({|\()")]
    private static partial Regex Definition();

    // A function call can be identified by a valid name, immediately followed by an opening paranthesis (
    // Valid function names might contain upper and lower case letters, digits and underscore _.
    // If a function name is immediately preceded by a $, it shall be omitted (a PHP-specific rule, call of function with a name stored in a variable).
    [GeneratedRegex(@"(\$?\w+)\(")]
    private static partial Regex Function();

    readonly HashSet<string> userDefined = [];
    string? multilineChar;

    public IEnumerable<string> FindFunctions(string source) {
        // Class names and user-defined functions shall be excluded. If the source contains "function name(" or "new name(", then the "name" shall be omitted from output.
        foreach (Match match in Definition().Matches(source)) {
            string name = match.Groups[2].Value;
            userDefined.Add(name);
        }
        foreach (Match match in Function().Matches(source)) {
            string name = match.Groups[1].Value;
            // todo: refactor
            if (KEYWORDS.Contains(name.ToLower()) || userDefined.Contains(name) || name.StartsWith("$")) {
                continue;
            }
            // Function names shall be treated case sensitively.
            yield return name;
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    public string Filter(string line) {
        // Anything after single line comments (starting with //) or within multi-line comments (between /* and */ ) shall be omitted.
        // todo: refactor
        if (multilineChar != null) {
            int index = line.IndexOf(multilineChar, StringComparison.Ordinal);
            if (index != -1) {
                line = line[index..];
                multilineChar = null;
            } else {
                return string.Empty;
            }
        }
        int commentStart = line.IndexOf("//", StringComparison.Ordinal);
        if (commentStart != -1) {
            line = line[..commentStart];
        }
        commentStart = line.IndexOf("/*", StringComparison.Ordinal);
        if (commentStart != -1) {
            int commentEnd = line.IndexOf("*/", StringComparison.Ordinal);
            if (commentEnd != -1) {
                line = line[..commentStart] + line[commentEnd..];
            } else {
                multilineChar = "*/";
                return line[..commentStart];
            }
        }
        // Anything within string literals (within pair of single (') or double (") quotes) shall be omitted.
        // todo: support multiple in one line
        int stringStart = line.IndexOf('\'');
        if (stringStart != -1) {
            int stringEnd = line.LastIndexOf('\'');
            line = line[..stringStart] + line[stringEnd..];
        }
        stringStart = line.IndexOf('"');
        if (stringStart != -1) {
            int stringEnd = line.LastIndexOf('"');
            if (stringEnd != stringStart) {
                line = line[..stringStart] + line[stringEnd..];
            } else {
                multilineChar = "\"";
                return line[..stringStart];
            }
        }
        return line;
    }
}

static class Debug {
    const int TAB_LENGTH = 2;

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
