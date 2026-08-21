using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

// https://www.codingame.com/training/easy/hagrid-speaking
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
for (var _ = 0; _ < lines; _++) {
    string line = Console.ReadLine()!;
    (string dialogue, string tag) = SplitLine(line);
    Console.WriteLine(IsHagridSpeaking(tag) ? $"\"{Transform(dialogue)}\"{tag}" : line);
}
return;

[SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.")]
(string dialogue, string tag) SplitLine(string line) {
    const string pattern = """
                           "([^"]*)"(.*)
                           """;
    var match = Regex.Match(line, pattern);
    return (match.Groups[1].Value, match.Groups[2].Value);
}

bool IsHagridSpeaking(string tag) => tag.Contains("Hagrid");

string Transform(string dialogue) {
    var transformed = dialogue.Split(' ').Select(word => {
        if (word.Contains('\'') || word.Contains('-')) {
            return word;
        }
        (word, string punctuation) = SplitWord(word);
        return Replace(word) + punctuation;
    });
    return string.Join(' ', transformed);
}

(string word, string punctuation) SplitWord(string word) {
    string[] punctuationMarks = ["...", ",", ".", "?", "!"];
    foreach (string mark in punctuationMarks) {
        int length = mark.Length;
        if (word.EndsWith(mark)) {
            return (word[..^length], word[^length..]);
        }
    }
    return (word, string.Empty);
}

string Replace(string word) {
    string lowerCaseWord = word.ToLower();
    // replace words
    switch (lowerCaseWord) {
        case "you":
            return word.Replace("ou", "eh");
        case "to":
            return word.Replace("o", "er");
        case "and":
            return word.Replace("d", "'");
        case "me":
            return $"{word}h";
    }
    if (word.Length <= 2) {
        return word;
    }
    // replace letters
    char first = lowerCaseWord.First();
    char last = lowerCaseWord.Last();
    if (last is 'f' or 't' or 'd' or 'g') {
        word = $"{word[..^1]}'";
    }
    if (first is 'h') {
        word = $"'{word[1..]}";
    }
    return word;
}
