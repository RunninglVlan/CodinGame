using System;
using System.Linq;

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
            var decodedLine = string.Empty;
            foreach (string chunk in chunks) {
                (int count, char character) = CountAndCharacter(chunk);
                for (var _ = 0; _ < count; _++) {
                    decodedLine += character;
                }
            }
            decoded[i] = decodedLine;
        }
        return string.Join(Environment.NewLine, decoded);
    }

    static (int, char) CountAndCharacter(string text) {
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
