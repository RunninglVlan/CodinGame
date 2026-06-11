using System;

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
    readonly Rune rune = new();
    char[] phrase = null!;

    public void ReadPhrase() => phrase = Console.ReadLine()!.ToCharArray();

    public string SpellOutPhrase() {
        var actions = string.Empty;
        var spelledPhrase = new char[phrase.Length];
        for (var i = 0; i < phrase.Length; i++) {
            char currentCharacter = phrase[i];
            while (spelledPhrase[i] != currentCharacter) {
                actions += FindRune(currentCharacter);
                actions += Actions.ADD;
                spelledPhrase[i] = currentCharacter;
            }
        }
        return actions;
    }

    string FindRune(char character) {
        var actions = string.Empty;
        while (rune.character != character) {
            rune.Next();
            actions += Actions.NEXT;
        }
        return actions;
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
}

static class Actions {
    public const char NEXT = '+';
    public const char ADD = '.';
}
