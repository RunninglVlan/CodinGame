using System;
using System.Collections.Generic;
using System.Linq;

class BeamSearch<T>(IGame<T> game) where T : class, IState {
    readonly HashSet<T> visited = new();

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
                return null;
            }
        }
        return currentLevel.First();
    }
}

interface IGame<TState> {
    IEnumerable<(TState, int cost)> Next(TState current, int level);
}

interface IState : IEquatable<IState>;
