using System;
using System.Diagnostics.CodeAnalysis;
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
    readonly Rune[] runes;
    int runeIndex;
    string phrase = null!;

    public Game() {
        runes = new Rune[30];
        for (var i = 0; i < runes.Length; i++) {
            runes[i] = new Rune();
        }
    }

    public void ReadPhrase() => phrase = Console.ReadLine()!;

    public string SpellOutPhrase() {
        var actions = new StringBuilder();
        foreach (char character in phrase) {
            actions.Append(FindRune(character));
            actions.Append(Actions.ADD_CHAR);
        }
        return actions.ToString();
    }

    string FindRune(char character) {
        if (runes[runeIndex].Char == character) {
            return string.Empty;
        }
        int? runeDistance = MoveToRune(character);
        if (runeDistance != null) {
            return runeDistance > 0
                    ? new string(Actions.MOVE_RIGHT, runeDistance.Value)
                    : new string(Actions.MOVE_LEFT, -runeDistance.Value);
        }
        var actions = string.Empty;
        int? emptyRuneDistance = MoveToRune(Rune.SPACE)!;
        if (emptyRuneDistance != 0) {
            actions = emptyRuneDistance > 0
                    ? new string(Actions.MOVE_RIGHT, emptyRuneDistance.Value)
                    : new string(Actions.MOVE_LEFT, -emptyRuneDistance.Value);
        }
        var rune = runes[runeIndex];
        int charDistance = rune.ChangeTo(character);
        actions += charDistance > 0
                ? new string(Actions.NEXT_CHAR, charDistance)
                : new string(Actions.PREVIOUS_CHAR, -charDistance);
        return actions;
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    int? MoveToRune(char target) {
        int runeLength = runes.Length;
        int targetIndex = -1;
        for (var i = 1; i <= runeLength / 2; i++) {
            int indexNext = (runeIndex + i + runeLength) % runeLength;
            if (runes[indexNext].Char == target) {
                targetIndex = indexNext;
                break;
            }
            int indexPrevious = (runeIndex - i + runeLength) % runeLength;
            if (runes[indexPrevious].Char == target) {
                targetIndex = indexPrevious;
                break;
            }
        }
        if (targetIndex == -1) {
            return null;
        }
        int forwardDistance = (targetIndex - runeIndex + runeLength) % runeLength;
        int backwardDistance = (runeIndex - targetIndex + runeLength) % runeLength;
        runeIndex = targetIndex;
        return forwardDistance < backwardDistance ? forwardDistance : -backwardDistance;
    }
}

class Rune {
    public const char SPACE = ' ';
    const int CHARACTERS = 27;

    int charIndex;
    public int Char => charIndex == 0 ? SPACE : (char)(charIndex - 1 + 'A');

    public int ChangeTo(char target) {
        int targetIndex = Index(target);
        int forwardDistance = (targetIndex - charIndex + CHARACTERS) % CHARACTERS;
        int backwardDistance = (charIndex - targetIndex + CHARACTERS) % CHARACTERS;
        charIndex = targetIndex;
        return forwardDistance < backwardDistance ? forwardDistance : -backwardDistance;
    }

    static int Index(char character) => character == SPACE ? 0 : character - 'A' + 1;
}

static class Actions {
    public const char NEXT_CHAR = '+';
    public const char PREVIOUS_CHAR = '-';
    public const char ADD_CHAR = '.';
    public const char MOVE_RIGHT = '>';
    public const char MOVE_LEFT = '<';
}
