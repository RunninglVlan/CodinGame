using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

const string testCase = """
                        2
                        "You going to the forest?" Asked Hagrid.
                        "No it's ok Hagrid. I just want to reach the castle before lunch." Replied Harry fearfully.
                        """;
Console.SetIn(new StringReader(testCase));
// https://www.codingame.com/training/easy/hagrid-speaking
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
for (var _ = 0; _ < lines; _++) {
    string line = Console.ReadLine()!;
    (string dialogue, string tag) = Split(line);
    Console.WriteLine(IsHagridSpeaking(tag) ? $"{Transform(dialogue)}{tag}" : line);
}
return;

[SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.")]
(string dialogue, string tag) Split(string line) {
    const string pattern = """
                           "([^"]*)"(.*)
                           """;
    string[] parts = Regex.Split(line, pattern);
    return (parts.First(), parts.Last());
}

bool IsHagridSpeaking(string tag) => tag.Contains("Hagrid");

// refactor
string Transform(string line) {
    var transformed = line.Split(' ').Select(word => {
        // replace words
        if (word.Contains("you", StringComparison.OrdinalIgnoreCase)) {
            return word.Replace("ou", "eh");
        }
        if (word.Contains("to", StringComparison.OrdinalIgnoreCase)) {
            return word.Replace("o", "ter");
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
            word = word[..^1] + '\'';
        }
        if (first is 'h') {
            word = '\'' + word[1..];
        }
        return word;
    });
    return string.Join(' ', transformed);
}
