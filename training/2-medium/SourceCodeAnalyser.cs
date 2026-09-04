using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

// https://www.codingame.com/training/medium/source-code-analyser
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 200);
/*
* Source might contain multiple whitespaces: space, tab (\t), new line (\n) or linefeed (\r).
* Function names shall be treated case sensitively.
* Following reserved words shall be not treated as function calls and omitted from output:
"and", "array", "echo", "else", "elseif", "if", "for", "foreach", "function", "or", "return", "while", "new" (regardless of lower, upper or mixed case)
* If a function name is immediately preceded by a $, it shall be omitted (a PHP-specific rule, call of function with a name stored in a variable).
* Class names and user-defined functions shall be excluded. If the source contains "function name(" or "new name(", then the "name" shall be omitted from output.
* Output lines shall be sorted by the function names (by ascending ascii values).
* If no library call encountered, output NONE
 */

/*
test
 */
var functions = Enumerable.Range(0, lines)
        .Select(_ => Console.ReadLine()!)
        .SelectMany(Tokenizer.FindFunctions);
foreach (var group in functions.GroupBy(it => it)) {
    Console.WriteLine($"{group.Key} {group.Count()}");
}

static partial class Tokenizer {
    static readonly HashSet<string> KEYWORDS = ["if"];

    // A function call can be identified by a valid name, immediately followed by an opening paranthesis (
    // Valid function names might contain upper and lower case letters, digits and underscore _.
    // todo: support digits
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
        // Anything after single line comments (starting with //) or within multi-line comments (between /* and */ ) shall be omitted.
        // todo: handle multi-lines
        int commentStart = line.IndexOf("/*", StringComparison.Ordinal);
        if (commentStart != -1) {
            int commentEnd = line.IndexOf("*/", StringComparison.Ordinal);
            line = line[..commentStart] + line[commentEnd..];
        }
        commentStart = line.IndexOf("//", StringComparison.Ordinal);
        if (commentStart != -1) {
            line = line[..commentStart];
        }
        // Anything within string literals (within pair of single (') or double (") quotes) shall be omitted.
        // todo: support multiple in one line, support ' strings
        int stringStart = line.IndexOf('"');
        if (stringStart != -1) {
            int stringEnd = line.LastIndexOf('"');
            line = line[..stringStart] + line[stringEnd..];
        }
        return line;
    }
}
