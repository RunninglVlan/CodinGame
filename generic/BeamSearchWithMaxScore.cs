using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

class BeamSearch<T>(IGame<T> game) where T : IState {
    readonly HashSet<T> visited = [];

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public T FindBest(T start, int levels, int beamWidth = 10) {
        var best = start;
        var currentLevel = new List<T> { start };
        for (var level = 0; level < levels; level++) {
            var nextLevel = new List<T>();
            visited.Clear();
            foreach (var current in currentLevel) {
                nextLevel.AddRange(game.Next(current));
            }
            currentLevel.Clear();
            // by score descending
            nextLevel.Sort((a, b) => b.Score.CompareTo(a.Score));
            foreach (var state in nextLevel) {
                if (!visited.Add(state)) {
                    continue;
                }
                if (currentLevel.Count >= beamWidth) {
                    break;
                }
                currentLevel.Add(state);
            }
            if (currentLevel.Count == 0) {
                break;
            }
            var bestCurrent = currentLevel.First();
            if (best.Score >= bestCurrent.Score) {
                continue;
            }
            best = bestCurrent;
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<TState> Next(TState current);
}

interface IState : IEquatable<IState> {
    int Score { get; }
}
