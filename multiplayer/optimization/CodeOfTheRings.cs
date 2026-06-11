using System;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfTheRings;

/// https://www.codingame.com/multiplayer/optimization/code-of-the-rings
static class CodeOfTheRings {
    static void Main() {
        var game = new Game();
        game.ReadPhrase();
        Console.WriteLine(game.SpellOutPhrase());
    }
}

class Game {
    Rune rune = new();
    string phrase = null!;

    public void ReadPhrase() => phrase = Console.ReadLine()!;

    public string SpellOutPhrase() {
        var actions = string.Empty;
        foreach (char character in phrase) {
            actions += FindRune(character);
            actions += Actions.ADD;
        }
        return actions;
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    string FindRune(char character) {
        const int maxIterations = 30;
        if (rune.character == character) {
            return string.Empty;
        }
        var runeNext = rune.Clone();
        var runePrevious = rune.Clone();
        var iterations = 1;
        for (; iterations < maxIterations; iterations++) {
            runeNext.Next();
            runePrevious.Previous();
            if (runeNext.character == character) {
                rune = runeNext;
                break;
            }
            if (runePrevious.character == character) {
                rune = runePrevious;
                break;
            }
        }
        return rune == runeNext ? new string(Actions.NEXT, iterations) : new string(Actions.PREVIOUS, iterations);
    }
}

class Rune {
    const char SPACE = ' ';
    public char character = SPACE;

    public void Next() {
        switch (character) {
            case SPACE:
                character = 'A';
                break;
            case 'Z':
                character = SPACE;
                break;
            default:
                character++;
                break;
        }
    }

    public void Previous() {
        switch (character) {
            case SPACE:
                character = 'Z';
                break;
            case 'A':
                character = SPACE;
                break;
            default:
                character--;
                break;
        }
    }

    public Rune Clone() => new() {
        character = character
    };
}

static class Actions {
    public const char NEXT = '+';
    public const char PREVIOUS = '-';
    public const char ADD = '.';
}
