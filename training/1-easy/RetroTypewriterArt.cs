using System;
using System.Linq;

namespace RetroTypewriterArt;

/// https://www.codingame.com/training/easy/retro-typewriter-art
static class RetroTypewriterArt {
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
        int count = int.Parse(chunk[..1]);
        string characterString = chunk[1..];
        char character = characterString switch {
            Symbols.SPACE => ' ',
            Symbols.BACKSLASH => '\\',
            Symbols.SINGLE_QUOTE => '\'',
            _ => characterString[0]
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
