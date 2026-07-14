using System.Collections.Generic;

class Pathfinding<T>(IGame<T> game, int depthMax) where T : IState {
    /// DFS
    public (T state, string action)? FindBest(T current, int depth = 0) {
        if (game.HasReachedEnd(current) || depth > depthMax) {
            return (current, string.Empty);
        }
        (T state, string action)? best = null;
        foreach ((var next, string action) in game.Next(current)) {
            var nextBest = FindBest(next, depth + 1);
            if (nextBest == null) {
                continue;
            }
            var (state, _) = nextBest.Value;
            if (best == null || state.Score > best.Value.state.Score) {
                best = (state, action);
            }
        }
        return best;
    }
}

interface IGame<TState> {
    IEnumerable<(TState, string action)> Next(TState current);
    bool HasReachedEnd(TState state);
}

interface IState {
    int Score { get; }
}
