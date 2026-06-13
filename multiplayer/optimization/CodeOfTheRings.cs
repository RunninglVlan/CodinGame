using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CodeOfTheRings;

/// https://www.codingame.com/multiplayer/optimization/code-of-the-rings
static class CodeOfTheRings {
    static void Main() {
        var game = new Game();
        game.Init();
        Console.WriteLine(game.SpellOutPhrase());
    }
}

abstract class RuneState {
    protected readonly Rune[] runes;
    protected int runeIndex;

    protected RuneState(int length) {
        runes = new Rune[length];
        for (var i = 0; i < length; i++) {
            runes[i] = new Rune();
        }
    }

    public void Copy(RuneState from) {
        for (var i = 0; i < from.runes.Length; i++) {
            runes[i].CharIndex = from.runes[i].CharIndex;
        }
        runeIndex = from.runeIndex;
    }
}

class Game() : RuneState(RUNES) {
    public const int RUNES = 30;

    string phrase = null!;
    ActionOption[] options = null!;

    public void Init() {
        phrase = Console.ReadLine()!;
        options = [
            new ExactMatchOption(),
            new BestModificationOption(phrase),
            new EmptyRuneOption()
        ];
    }

    public string SpellOutPhrase() {
        var actions = string.Empty;
        for (var i = 0; i < phrase.Length; i++) {
            char character = phrase[i];
            actions += FindRune(character, i);
            actions += Action.ADD_CHAR;
        }
        return actions;
    }

    string FindRune(char character, int phraseIndex) {
        foreach (var option in options) {
            option.Copy(this);
            option.Calculate(character, phraseIndex);
        }
        var best = options.OrderBy(it => it.Instructions).First();
        Copy(best);
        return best.Actions;
    }
}

abstract class ActionOption() : RuneState(Game.RUNES) {
    const string WHILE_NOT_SPACE = "[{0}]";

    public int Instructions { get; protected set; }
    public string Actions { get; protected set; } = null!;

    public abstract void Calculate(char target, int phraseIndex);

    protected string FindEmptyRune() {
        const int loopChars = 3;
        int? distance = MoveToRune(Rune.SPACE)!;
        if (distance == 0) {
            return string.Empty;
        }
        if (Math.Abs(distance.Value) < loopChars) {
            return distance > 0
                    ? new string(Action.MOVE_RIGHT, distance.Value)
                    : new string(Action.MOVE_LEFT, -distance.Value);
        }
        char moveAction = distance > 0 ? Action.MOVE_RIGHT : Action.MOVE_LEFT;
        return string.Format(WHILE_NOT_SPACE, moveAction);
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    protected int? MoveToRune(char target) {
        if (runes[runeIndex].Char == target) {
            return 0;
        }
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
        int distance = MinDistance(runeIndex, targetIndex, runes.Length);
        runeIndex = targetIndex;
        return distance;
    }

    public static int MinDistance(int sourceIndex, int targetIndex, int length) {
        int forwardDistance = (targetIndex - sourceIndex + length) % length;
        int backwardDistance = (sourceIndex - targetIndex + length) % length;
        return forwardDistance < backwardDistance ? forwardDistance : -backwardDistance;
    }
}

class ExactMatchOption : ActionOption {
    public override void Calculate(char target, int _) {
        if (!Array.Exists(runes, it => it.Char == target)) {
            Actions = string.Empty;
            Instructions = int.MaxValue;
            return;
        }
        int runeDistance = MoveToRune(target)!.Value;
        Actions = runeDistance > 0
                ? new string(Action.MOVE_RIGHT, runeDistance)
                : new string(Action.MOVE_LEFT, -runeDistance);
        Instructions = Actions.Length;
    }
}

class BestModificationOption(string phrase) : ActionOption {
    public override void Calculate(char target, int phraseIndex) {
        if (!Array.Exists(runes, it => it.Char != Rune.SPACE)) {
            Actions = string.Empty;
            Instructions = int.MaxValue;
            return;
        }
        (int cost, int index) best = (int.MaxValue, -1);
        for (var i = 0; i < runes.Length; i++) {
            var rune = runes[i];
            if (rune.Char == Rune.SPACE) {
                continue;
            }
            int cost = MinDistance(runeIndex, i, runes.Length)
                    + MinDistance(rune.CharIndex, Rune.Index(target), Rune.CHARACTERS);
            if (rune.Char != target && RemainingCount(phraseIndex + 1, rune.Char) >= 1) {
                cost += Game.RUNES;
            }
            if (cost < best.cost) {
                best = (cost, i);
            }
        }
        int moveDistance = MinDistance(runeIndex, best.index, runes.Length);
        Actions = moveDistance > 0
                ? new string(Action.MOVE_RIGHT, moveDistance)
                : new string(Action.MOVE_LEFT, -moveDistance);
        runeIndex = best.index;
        int charDistance = runes[runeIndex].ChangeTo(target);
        Actions += charDistance > 0
                ? new string(Action.NEXT_CHAR, charDistance)
                : new string(Action.PREVIOUS_CHAR, -charDistance);
        Instructions = Actions.Length;
    }

    int RemainingCount(int phraseIndex, char target) {
        var count = 0;
        for (int i = phraseIndex; i < phrase.Length; i++) {
            if (phrase[i] == target) {
                count++;
            }
        }
        return count;
    }
}

class EmptyRuneOption : ActionOption {
    public override void Calculate(char target, int _) {
        if (!Array.Exists(runes, it => it.Char == Rune.SPACE)) {
            Actions = string.Empty;
            Instructions = int.MaxValue;
            return;
        }
        Actions = FindEmptyRune();
        int charDistance = runes[runeIndex].ChangeTo(target);
        Actions += charDistance > 0
                ? new string(Action.NEXT_CHAR, charDistance)
                : new string(Action.PREVIOUS_CHAR, -charDistance);
        Instructions = Actions.Length;
    }
}

class Rune {
    public const char SPACE = ' ';
    public const int CHARACTERS = 27;

    public int CharIndex { get; set; }
    public char Char => CharIndex == 0 ? SPACE : (char)(CharIndex - 1 + 'A');

    public static int Index(char character) => character == SPACE ? 0 : character - 'A' + 1;

    public int ChangeTo(char target) {
        int targetIndex = Index(target);
        if (targetIndex == CharIndex) {
            return 0;
        }
        int distance = ActionOption.MinDistance(CharIndex, targetIndex, CHARACTERS);
        CharIndex = targetIndex;
        return distance;
    }
}

static class Action {
    public const char NEXT_CHAR = '+';
    public const char PREVIOUS_CHAR = '-';
    public const char ADD_CHAR = '.';
    public const char MOVE_RIGHT = '>';
    public const char MOVE_LEFT = '<';
}
