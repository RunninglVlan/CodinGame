using System;
using System.Text;

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
    string phrase = null!;

    public void ReadPhrase() => phrase = Console.ReadLine()!;

    public string SpellOutPhrase() {
        var actions = new StringBuilder();
        foreach (char character in phrase) {
            actions.Append(FindRune(character));
            actions.Append(Actions.ADD);
        }
        return actions.ToString();
    }

    string FindRune(char character) {
        int distance = rune.MoveTo(character);
        if (distance == 0) {
            return string.Empty;
        }
        return distance > 0
                ? new string(Actions.NEXT, distance)
                : new string(Actions.PREVIOUS, -distance);
    }
}

class Rune {
    const int CHARACTERS = 27;
    int charIndex;

    public int MoveTo(char target) {
        int targetIndex = GetCharIndex(target);
        if (targetIndex == charIndex) {
            return 0;
        }
        int forwardDistance = (targetIndex - charIndex + CHARACTERS) % CHARACTERS;
        int backwardDistance = (charIndex - targetIndex + CHARACTERS) % CHARACTERS;
        charIndex = targetIndex;
        return forwardDistance < backwardDistance ? forwardDistance : -backwardDistance;
    }

    static int GetCharIndex(char value) => value == ' ' ? 0 : value - 'A' + 1;
}

static class Actions {
    public const char NEXT = '+';
    public const char PREVIOUS = '-';
    public const char ADD = '.';
}
