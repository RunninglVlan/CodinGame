using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CodeOfTheRings;

/// https://www.codingame.com/multiplayer/optimization/code-of-the-rings
static class CodeOfTheRings {
    static void Main() {
        var game = new Game(Console.ReadLine()!);
        Console.WriteLine(game.SpellOutPhrase());
    }
}

class GameState : IState {
    public const int RUNES = 30;

    public Rune[] Runes { get; }
    public int RuneIndex { get; set; }
    public string Actions { get; set; } = string.Empty;

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

    public bool Equals(IState? other) {
        if (other is not GameState that) {
            return false;
        }
        if (ReferenceEquals(this, that)) {
            return true;
        }
        return RuneIndex == that.RuneIndex && Runes.AsSpan().SequenceEqual(that.Runes);
    }

    public override bool Equals(object? other) => Equals(other as IState);

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode() {
        var hash = new HashCode();
        hash.Add(RuneIndex);
        for (var i = 0; i < RUNES; i++) {
            hash.Add(Runes[i].CharIndex);
        }
        return hash.ToHashCode();
    }
}

class Game : IGame<GameState> {
    readonly string phrase;
    readonly BeamSearch<GameState> beamSearch;
    readonly ActionOption[] options;

    public Game(string phrase) {
        this.phrase = phrase;
        beamSearch = new(this);
        options = [
            new ExactMatchOption(),
            new BestModificationOption(phrase),
            new EmptyRuneOption()
        ];
    }

    public string SpellOutPhrase() {
        return beamSearch.FindBest(new GameState(), phrase.Length, beamWidth: 250)?.Actions
                ?? string.Empty;
    }

    public IEnumerable<(GameState, int cost)> Next(GameState current, int level) {
        char targetChar = phrase[level];
        foreach (var option in options) {
            option.State.Load(current);
            if (!option.Exists(targetChar)) {
                continue;
            }
            string actions = option.Calculate(targetChar, level);
            var next = new GameState();
            next.Load(option.State);
            next.Actions = current.Actions + actions + Action.ADD_CHAR;
            yield return (next, next.Actions.Length);
        }
    }
}

abstract class ActionOption {
    protected const string WHILE_NOT_SPACE = "[{0}]";

    public GameState State { get; } = new();

    public abstract bool Exists(char target);
    public abstract string Calculate(char target, int phraseIndex);

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
    public override bool Exists(char target) {
        return Array.Exists(State.Runes, it => it.Char == target);
    }

    public override string Calculate(char target, int _) {
        int runeDistance = MoveToRune(target)!.Value;
        return runeDistance > 0
                ? new string(Action.MOVE_RIGHT, runeDistance)
                : new string(Action.MOVE_LEFT, -runeDistance);
    }
}

class BestModificationOption(string phrase) : ActionOption {
    public override bool Exists(char _) {
        return Array.Exists(State.Runes, it => it.Char != Rune.SPACE);
    }

    public override string Calculate(char target, int phraseIndex) {
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
        string actions = moveDistance > 0
                ? new string(Action.MOVE_RIGHT, moveDistance)
                : new string(Action.MOVE_LEFT, -moveDistance);
        State.RuneIndex = best.index;
        int charDistance = State.Runes[State.RuneIndex].ChangeTo(target);
        actions += charDistance > 0
                ? new string(Action.NEXT_CHAR, charDistance)
                : new string(Action.PREVIOUS_CHAR, -charDistance);
        return actions;
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
    public override bool Exists(char _) {
        return Array.Exists(State.Runes, it => it.Char == Rune.SPACE);
    }

    public override string Calculate(char target, int _) {
        string actions = FindEmptyRune();
        int charDistance = State.Runes[State.RuneIndex].ChangeTo(target);
        actions += charDistance > 0
                ? new string(Action.NEXT_CHAR, charDistance)
                : new string(Action.PREVIOUS_CHAR, -charDistance);
        return actions;
    }

    string FindEmptyRune() {
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
}

record struct Rune {
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

class BeamSearch<T>(IGame<T> game) where T : IState {
    readonly HashSet<T> visited = [];

    public T? FindBest(T start, int levels, int beamWidth = 10) {
        var currentLevel = new List<T> { start };
        for (var level = 0; level < levels; level++) {
            var nextLevel = new PriorityQueue<T, int>();
            visited.Clear();
            foreach (var current in currentLevel) {
                foreach ((var next, int cost) in game.Next(current, level)) {
                    nextLevel.Enqueue(next, cost);
                }
            }
            currentLevel.Clear();
            while (nextLevel.TryDequeue(out var state, out _) && currentLevel.Count < beamWidth) {
                if (!visited.Add(state)) {
                    continue;
                }
                currentLevel.Add(state);
            }
            if (currentLevel.Count == 0) {
                return default;
            }
        }
        return currentLevel.First();
    }
}

interface IGame<TState> {
    IEnumerable<(TState, int cost)> Next(TState current, int level);
}

interface IState : IEquatable<IState>;
