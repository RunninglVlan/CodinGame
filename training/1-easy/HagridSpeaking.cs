using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// https://www.codingame.com/training/easy/hagrid-speaking
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
for (var _ = 0; _ < lines; _++) {
    string line = Console.ReadLine()!;
    Console.WriteLine(Solution.Transform(line));
}

static partial class Solution {
    static readonly Dictionary<string, string> wordReplacements = new(StringComparer.OrdinalIgnoreCase) {
        { "you", "yeh" },
        { "to", "ter" },
        { "and", "an'" },
        { "me", "meh" }
    };

    [GeneratedRegex("""
                    "([^"]*)"(.*)
                    """)]
    private static partial Regex DialogueTag();

    [GeneratedRegex("""
                    ([^\w]*)([\w]*)([^\w]*)
                    """)]
    private static partial Regex PrefixWordPostfix();

    [GeneratedRegex("^h|[ftdg]$", RegexOptions.IgnoreCase)]
    private static partial Regex ApostropheLetters();

    public static string Transform(string line) {
        (string dialogue, string tag, _) = Split(line, DialogueTag());
        if (!tag.Contains("Hagrid")) {
            return line;
        }
        var transformed = dialogue.Split(' ').Select(word => {
            if (word.Contains('\'') || word.Contains('-')) {
                return word;
            }
            (string prefix, word, string postfix) = Split(word, PrefixWordPostfix());
            return $"{prefix}{Replace(word)}{postfix}";
        });
        return $"\"{string.Join(' ', transformed)}\"{tag}";
    }

    static (string first, string second, string third) Split(string line, Regex regex) {
        var match = regex.Match(line);
        string first = match.Groups[1].Value;
        string second = match.Groups[2].Value;
        string third = match.Groups.Count > 3 ? match.Groups[3].Value : string.Empty;
        return (first, second, third);
    }

    static string Replace(string word) {
        if (!wordReplacements.TryGetValue(word, out string? replacement)) {
            return word.Length <= 2
                    ? word
                    : ApostropheLetters().Replace(word, "'");
        }
        char[] letters = replacement.ToCharArray();
        for (var i = 0; i < letters.Length; i++) {
            char original = i < word.Length ? word[i] : word.Last();
            letters[i] = char.IsUpper(original) ? char.ToUpper(letters[i]) : letters[i];
        }
        return new string(letters);
    }
}
