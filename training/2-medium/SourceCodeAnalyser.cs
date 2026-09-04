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
Test 8: string literals
6
// string literals are not function calls. String literals can also span multiple lines
fscanf(STDIN, "%s", $s);
$len = strlen($s);
$a = 'strlen()';
echo "
strlen()";

fscanf 1
strlen 1
 */
var tokenizer = new Tokenizer();
var functions = Enumerable.Range(0, lines)
        .Select(_ => Debug.ReadLine()!)
        .SelectMany(tokenizer.FindFunctions);
functions = [.. functions];
Debug.Log();
foreach (var group in functions.GroupBy(it => it).OrderBy(it => it.Key)) {
    Console.WriteLine($"{group.Key} {group.Count()}");
}

partial class Tokenizer {
    static readonly HashSet<string> KEYWORDS = ["if"];

    // A function call can be identified by a valid name, immediately followed by an opening paranthesis (
    // Valid function names might contain upper and lower case letters, digits and underscore _.
    [GeneratedRegex(@"\w*\(")]
    private static partial Regex Function();

    string? multilineChar;

    public IEnumerable<string> FindFunctions(string line) {
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
    string Filter(string line) {
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
