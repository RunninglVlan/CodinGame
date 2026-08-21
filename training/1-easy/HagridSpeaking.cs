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
    var transformed = dialogue.Split(' ').Select(word => {
        if (word.Contains('\'') || word.Contains('-')) {
            return word;
        }
        (word, string punctuation) = GetPunctuation(word);
        (string firstParenthesis, word, string lastParenthesis) = GetParentheses(word);
        return $"{firstParenthesis}{ReplaceWord(word)}{lastParenthesis}{punctuation}";
    });
    return string.Join(' ', transformed);
}

(string word, string punctuation) GetPunctuation(string word) {
    string[] punctuationMarks = ["...", ",", ".", "?", "!"];
    foreach (string mark in punctuationMarks) {
        int length = mark.Length;
        if (word.EndsWith(mark)) {
            return (word[..^length], word[^length..]);
        }
    }
    return (word, string.Empty);
}

(string firstParenthesis, string word, string lastParenthesis) GetParentheses(string word) {
    string[] parentheses = ["()", "[]", "{}"];
    foreach (string group in parentheses) {
        string firstParenthesis = group[..^1];
        string lastParenthesis = group[1..];
        if (word.StartsWith(firstParenthesis) && word.EndsWith(lastParenthesis)) {
            return (firstParenthesis, word[1..^1], lastParenthesis);
        }
    }
    return (string.Empty, word, string.Empty);
}

string ReplaceWord(string word) {
    string lowerCaseWord = word.ToLower();
    // replace words
    switch (lowerCaseWord) {
        case "you":
            return $"{word[0]}{ReplaceLetter(word[1], "e")}{ReplaceLetter(word[2], "h")}";
        case "to":
            return $"{word[0]}{ReplaceLetter(word[1], "er")}";
        case "me":
            return $"{word[0]}{ReplaceLetter(word[1], "eh")}";
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

string ReplaceLetter(char letter, string letters) {
    bool isUpper = char.IsUpper(letter);
    return isUpper ? letters.ToUpper() : letters;
}
