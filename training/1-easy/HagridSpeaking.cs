using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

// https://www.codingame.com/training/easy/hagrid-speaking
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
for (var _ = 0; _ < lines; _++) {
    string line = Console.ReadLine()!;
    (string dialogue, string tag) = Split(line);
    Console.WriteLine(IsHagridSpeaking(tag) ? $"\"{Transform(dialogue)}\"{tag}" : line);
}
return;

[SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.")]
(string dialogue, string tag) Split(string line) {
    const string pattern = """
                           "([^"]*)"(.*)
                           """;
    var match = Regex.Match(line, pattern);
    return (match.Groups[1].Value, match.Groups[2].Value);
}

bool IsHagridSpeaking(string tag) => tag.Contains("Hagrid");

string Transform(string dialogue) {
    char last = dialogue.Last();
    bool hasQuestionMark = last is '?';
    if (hasQuestionMark) {
        dialogue = dialogue[..^1];
    }
    string transformed = string.Join(' ', dialogue.Split(' ').Select(Replace));
    return hasQuestionMark ? $"{transformed}?" : transformed;
}

string Replace(string word) {
    // replace words
    if (word.Contains("you", StringComparison.OrdinalIgnoreCase)) {
        return word.Replace("ou", "eh");
    }
    if (word.Contains("to", StringComparison.OrdinalIgnoreCase)) {
        return word.Replace("o", "er");
    }
    if (word.Contains("and", StringComparison.OrdinalIgnoreCase)) {
        return word.Replace("d", "'");
    }
    if (word.Contains("me", StringComparison.OrdinalIgnoreCase)) {
        return $"{word}h";
    }
    // replace letters
    if (word.Length <= 2) {
        return word;
    }
    string lowerCaseWord = word.ToLower();
    char first = lowerCaseWord.First();
    char last = lowerCaseWord.Last();
    if (last is 'f' or 't' or 'd' or 'g') {
        word = $"{word[..^1]}'";
    }
    if (first is 'h') {
        word = $"'{word[..^1]}";
    }
    return word;
}
