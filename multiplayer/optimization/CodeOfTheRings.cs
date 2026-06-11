using System;

namespace CodeOfTheRings;

/// https://www.codingame.com/multiplayer/optimization/code-of-the-rings
static class CodeOfTheRings {
    static void Main() {
        var game = new Game();
        game.Init();
        Console.WriteLine(game.GetActions());
    }
}

class Game {
    readonly Rune rune = new();
    char[] phrase = null!;

    public void Init() => phrase = Console.ReadLine()!.ToCharArray();

    public string GetActions() {
        var actions = string.Empty;
        var spelledPhrase = new char[phrase.Length];
        for (var phraseIndex = 0; phraseIndex < phrase.Length; phraseIndex++) {
            char currentCharacter = phrase[phraseIndex];
            while (spelledPhrase[phraseIndex] != currentCharacter) {
                while (rune.character != currentCharacter) {
                    rune.Next();
                    actions += Actions.NEXT;
                }
                actions += Actions.ADD;
                spelledPhrase[phraseIndex] = currentCharacter;
            }
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
