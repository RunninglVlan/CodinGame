using System;
using System.Text;

namespace RetroTypewriterArt;

/// https://www.codingame.com/training/easy/retro-typewriter-art
static class RetroTypewriterArt {
    static void Main() {
        string recipe = Console.ReadLine()!;
        Console.WriteLine(new Recipe(recipe).Decode());
    }
}

class Recipe(string text) {
    const string NEW_LINE = "nl";
    const string SPACE = "sp";
    const string BACKSLASH = "bS";
    const string SINGLE_QUOTE = "sQ";

    public string Decode() {
        string[] chunks = text.Split(' ');
        var decoded = new StringBuilder();
        foreach (string chunk in chunks) {
            if (chunk == NEW_LINE) {
                decoded.AppendLine();
                continue;
            }
            (int count, char character) = CountAndCharacter(chunk);
            decoded.Append(character, count);
        }
        return decoded.ToString();
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
