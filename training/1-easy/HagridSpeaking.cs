using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

// https://www.codingame.com/training/easy/hagrid-speaking
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
for (var _ = 0; _ < lines; _++) {
    string line = Console.ReadLine()!;
    Console.WriteLine(Solution.Transform(line));
}

static class Solution {
    const string DIALOGUE_AND_TAG = """
                                    "([^"]*)"(.*)
                                    """;

    public static string Transform(string line) {
        (string dialogue, string tag) = Split(line);
        if (!tag.Contains("Hagrid")) {
            return line;
        }
        var transformed = dialogue.Split(' ').Select(word => {
            if (word.Contains('\'') || word.Contains('-')) {
                return word;
            }
            (word, string punctuation) = GetPunctuation(word);
            (string firstParenthesis, word, string lastParenthesis) = GetParentheses(word);
            return $"{firstParenthesis}{ReplaceWord(word)}{lastParenthesis}{punctuation}";
        });
        return $"\"{string.Join(' ', transformed)}\"{tag}";
    }

    [SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.")]
    static (string dialogue, string tag) Split(string line) {
        var match = Regex.Match(line, DIALOGUE_AND_TAG);
        return (match.Groups[1].Value, match.Groups[2].Value);
    }

    static (string word, string punctuation) GetPunctuation(string word) {
        string[] punctuationMarks = ["...", ",", ".", "?", "!"];
        foreach (string mark in punctuationMarks) {
            int length = mark.Length;
            if (word.EndsWith(mark)) {
                return (word[..^length], word[^length..]);
            }
        }
        return (word, string.Empty);
    }

    static (string first, string word, string last) GetParentheses(string word) {
        string[] parentheses = ["()", "[]", "{}"];
        foreach (string group in parentheses) {
            string first = group[..^1];
            string last = group[1..];
            if (word.StartsWith(first) && word.EndsWith(last)) {
                return (first, word[1..^1], last);
            }
        }
        return (string.Empty, word, string.Empty);
    }

    static string ReplaceWord(string word) {
        string lowerCaseWord = word.ToLower();
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
        if (lowerCaseWord.Last() is 'f' or 't' or 'd' or 'g') {
            word = $"{word[..^1]}'";
        }
        if (lowerCaseWord.First() is 'h') {
            word = $"'{word[1..]}";
        }
        return word;
    }

    static string ReplaceLetter(char letter, string withLetters) {
        bool isUpper = char.IsUpper(letter);
        return isUpper ? withLetters.ToUpper() : withLetters;
    }
}
