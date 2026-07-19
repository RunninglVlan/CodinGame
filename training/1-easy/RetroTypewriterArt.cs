using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RetroTypewriterArt;

/// https://www.codingame.com/training/easy/retro-typewriter-art
static partial class RetroTypewriterArt {
    [GeneratedRegex(@"^(\d+)(.+)$")]
    private static partial Regex CountAndCharsRegex();

    static void Main() {
        string recipe = Console.ReadLine()!;
        string[] lines = recipe.Split(Symbols.NEW_LINE);
        foreach (string line in lines) {
            var chunks = line.Split(' ').Where(it => it.Length > 0);
            foreach (string chunk in chunks) {
                Console.Write(Convert(chunk));
            }
            Console.WriteLine();
        }
    }

    static string Convert(string chunk) {
        var match = CountAndCharsRegex().Match(chunk);
        int count = int.Parse(match.Groups[1].Value);
        char character = match.Groups[2].Value switch {
            Symbols.SPACE => ' ',
            Symbols.BACKSLASH => '\\',
            Symbols.SINGLE_QUOTE => '\'',
            _ => match.Groups[2].Value[0]
        };
        var converted = string.Empty;
        for (var i = 0; i < count; i++) {
            converted += character;
        }
        return converted;
    }
}

static class Symbols {
    public const string NEW_LINE = "nl";
    public const string SPACE = "sp";
    public const string BACKSLASH = "bS";
    public const string SINGLE_QUOTE = "sQ";
}
