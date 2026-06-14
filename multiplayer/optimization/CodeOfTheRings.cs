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

class GameState {
    public const int RUNES = 30;

    public Rune[] Runes { get; }
    public int RuneIndex { get; set; }

    public GameState() {
        Runes = new Rune[RUNES];
        for (var i = 0; i < Runes.Length; i++) {
            Runes[i] = new Rune();
        }
    }

    public void Load(GameState from) {
        for (var i = 0; i < from.Runes.Length; i++) {
            Runes[i].CharIndex = from.Runes[i].CharIndex;
        }
        RuneIndex = from.RuneIndex;
    }
}

class Game {
    readonly GameState state = new();
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
            option.State.Load(state);
            option.Calculate(character, phraseIndex);
        }
        var best = options.OrderBy(it => it.Instructions).First();
        state.Load(best.State);
        return best.Actions;
    }
}

abstract class ActionOption {
    const string WHILE_NOT_SPACE = "[{0}]";

    public GameState State { get; } = new();
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
        if (State.Runes[State.RuneIndex].Char == target) {
            return 0;
        }
        int runeLength = State.Runes.Length;
        int targetIndex = -1;
        for (var i = 1; i <= runeLength / 2; i++) {
            int indexNext = (State.RuneIndex + i + runeLength) % runeLength;
            if (State.Runes[indexNext].Char == target) {
                targetIndex = indexNext;
                break;
            }
            int indexPrevious = (State.RuneIndex - i + runeLength) % runeLength;
            if (State.Runes[indexPrevious].Char == target) {
                targetIndex = indexPrevious;
                break;
            }
        }
        if (targetIndex == -1) {
            return null;
        }
        int distance = MinDistance(State.RuneIndex, targetIndex, State.Runes.Length);
        State.RuneIndex = targetIndex;
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
        if (!Array.Exists(State.Runes, it => it.Char == target)) {
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
        if (!Array.Exists(State.Runes, it => it.Char != Rune.SPACE)) {
            Actions = string.Empty;
            Instructions = int.MaxValue;
            return;
        }
        (int cost, int index) best = (int.MaxValue, -1);
        for (var i = 0; i < State.Runes.Length; i++) {
            var rune = State.Runes[i];
            if (rune.Char == Rune.SPACE) {
                continue;
            }
            int cost = MinDistance(State.RuneIndex, i, State.Runes.Length)
                    + MinDistance(rune.CharIndex, Rune.Index(target), Rune.CHARACTERS);
            if (rune.Char != target && IsUsedLater(phraseIndex, rune.Char)) {
                cost += GameState.RUNES;
            }
            if (cost < best.cost) {
                best = (cost, i);
            }
        }
        int moveDistance = MinDistance(State.RuneIndex, best.index, State.Runes.Length);
        Actions = moveDistance > 0
                ? new string(Action.MOVE_RIGHT, moveDistance)
                : new string(Action.MOVE_LEFT, -moveDistance);
        State.RuneIndex = best.index;
        int charDistance = State.Runes[State.RuneIndex].ChangeTo(target);
        Actions += charDistance > 0
                ? new string(Action.NEXT_CHAR, charDistance)
                : new string(Action.PREVIOUS_CHAR, -charDistance);
        Instructions = Actions.Length;
    }

    bool IsUsedLater(int phraseIndex, char target) {
        for (int i = phraseIndex + 1; i < phrase.Length; i++) {
            if (phrase[i] == target) {
                return true;
            }
        }
        return false;
    }
}

class EmptyRuneOption : ActionOption {
    public override void Calculate(char target, int _) {
        if (!Array.Exists(State.Runes, it => it.Char == Rune.SPACE)) {
            Actions = string.Empty;
            Instructions = int.MaxValue;
            return;
        }
        Actions = FindEmptyRune();
        int charDistance = State.Runes[State.RuneIndex].ChangeTo(target);
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
