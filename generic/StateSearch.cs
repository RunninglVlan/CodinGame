using System.Collections.Generic;

class StateSearch<T>(IGame<T> game, int depthMax) where T : IState<T>, IEquatable<T> {
    readonly HashSet<T> visited = [];

    /// DFS/Backtracking
    public T? FindBest(T start) {
        visited.Clear();
        visited.Add(start);
        return FindBest(start, 0);
    }

    T? FindBest(T current, int depth) {
        if (game.HasReachedEnd(current)) {
            return current;
        }
        if (depth > depthMax) {
            return default;
        }
        T? best = default;
        foreach (var next in game.Next(current)) {
            if (!visited.Add(next)) {
                continue;
            }
            var nextBest = FindBest(next, depth + 1);
            if (nextBest == null) {
                continue;
            }
            if (best == null || nextBest.IsBetterThan(best)) {
                best = nextBest;
            }
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<TState> Next(TState current);
    bool HasReachedEnd(TState state);
}

interface IState<in T> where T : IState<T> {
    bool IsBetterThan(T other);
}
