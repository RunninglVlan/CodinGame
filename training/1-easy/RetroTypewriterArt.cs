using System;
using System.Linq;
using System.Text;

namespace RetroTypewriterArt;

/// https://www.codingame.com/training/easy/retro-typewriter-art
static class RetroTypewriterArt {
    static void Main() {
        string recipe = Console.ReadLine()!;
        Console.WriteLine(Recipe.Decode(recipe));
    }
}

static class Recipe {
    const string NEW_LINE = "nl";
    const string SPACE = "sp";
    const string BACKSLASH = "bS";
    const string SINGLE_QUOTE = "sQ";

    public static string Decode(string text) {
        string[] lines = text.Split(NEW_LINE);
        var decoded = new string[lines.Length];
        for (var i = 0; i < lines.Length; i++) {
            string line = lines[i];
            var chunks = line.Split(' ').Where(it => it.Length > 0);
            var decodedLine = new StringBuilder();
            foreach (string chunk in chunks) {
                (int count, char character) = CountAndCharacter(chunk);
                decodedLine.Append(character, count);
            }
            decoded[i] = decodedLine.ToString();
        }
        return string.Join(Environment.NewLine, decoded);
    }

    static (int, char) CountAndCharacter(string text) {
        // Symbols always start with a letter,
        // so if the second-to-last char is a digit,
        // it must be part of a (possibly multi-digit) count followed by a literal character
        if (char.IsDigit(text[^2])) {
            return (int.Parse(text[..^1]), text[^1]);
        }
        string symbol = text[^2..];
        char character = symbol switch {
            SPACE => ' ',
            BACKSLASH => '\\',
            SINGLE_QUOTE => '\'',
            _ => throw new ArgumentException($"Unexpected symbol: {symbol}")
        };
        return (int.Parse(text[..^2]), character);
    }
}
