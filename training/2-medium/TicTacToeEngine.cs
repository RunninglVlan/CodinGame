using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/medium/tic-tac-toe-engine
var board = new Board();
board.Read();
board.PlayBestMove();
board.Print();

// todo: when priorities will be used again, refactor the logic
// todo: remove extra debug from submission, like point to string, log turns, enemy moves
class Board {
    const int SIZE = 3;

    static readonly Point[][] LINES = [
        // rows
        [Screen.ZERO, Screen.RIGHT, Screen.RIGHT * 2],
        [Screen.DOWN, Screen.ONE, Screen.RIGHT * 2 + Screen.DOWN],
        [Screen.DOWN * 2, Screen.DOWN * 2 + Screen.RIGHT, Screen.ONE * 2],
        // columns
        [Screen.ZERO, Screen.DOWN, Screen.DOWN * 2],
        [Screen.RIGHT, Screen.ONE, Screen.DOWN * 2 + Screen.RIGHT],
        [Screen.RIGHT * 2, Screen.RIGHT * 2 + Screen.DOWN, Screen.ONE * 2],
        // diagonals
        [Screen.ZERO, Screen.ONE, Screen.ONE * 2],
        [Screen.RIGHT * 2, Screen.ONE, Screen.DOWN * 2]
    ];

    /// <code>
    /// 8 | 4 | 7 
    ///---+---+---
    /// 3 | 9 | 2 
    ///---+---+---
    /// 6 | 1 | 5 </code>
    static readonly Point[] SQUARE_PRIORITIES = [
        Screen.ONE,
        Screen.ZERO,
        Screen.RIGHT * 2,
        Screen.DOWN * 2,
        Screen.DOWN * 2 + Screen.RIGHT * 2,
        Screen.RIGHT,
        Screen.DOWN,
        Screen.DOWN + Screen.RIGHT * 2,
        Screen.DOWN * 2 + Screen.RIGHT
    ];

    char player, enemy;
    readonly char[,] squares = new char[SIZE, SIZE];

    public void Read() {
        player = Debug.ReadLine()!.First();
        enemy = player == Square.PLAYER_O ? Square.PLAYER_X : Square.PLAYER_O;
        for (var y = 0; y < SIZE; y++) {
            string line = Debug.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                squares[x, y] = line[x];
            }
        }
        Debug.Log();
    }

    char Get(Point point) => squares[point.X, point.Y];
    void Set(Point point, char square) => squares[point.X, point.Y] = square;

    public void PlayBestMove() {
        if (GameOver() is not null) {
            return;
        }
        var games = SimulateGames(new GameState([]))
                .GroupBy(it => it.First)
                .Select(firstGroup => {
                    var result = new Dictionary<GameResult, (GameState[] states, float chance)>();
                    var resultGroups = firstGroup.GroupBy(it => it.Result).ToArray();
                    foreach (var resultGroup in resultGroups) {
                        float chance = resultGroup.Count() / (float)resultGroups.Select(it => it.Count()).Sum();
                        result.Add(resultGroup.Key!.Value, ([.. resultGroup], chance));
                    }
                    return result;
                }).ToArray();
        var winningGames = WinningGames(games);
        if (winningGames.Length == 0) {
            var bestLosingMove = games
                    .SelectMany(it => it)
                    .OrderByDescending(it => it.Value.chance)
                    .SelectMany(it => it.Value.states)
                    .OrderByDescending(it => it.Turns)
                    .Select(it => it.First)
                    .First();
            Set(bestLosingMove, player);
            return;
        }
        var firstGroups = winningGames
                .SelectMany(it => it)
                .SelectMany(it => it.Value.states)
                .GroupBy(it => it.First)
                .ToArray();
        foreach (var square in SQUARE_PRIORITIES) {
            var filtered = firstGroups.Where(group => group.Key == square).ToArray();
            if (filtered.Length == 0) {
                continue;
            }
            Set(filtered.First().Key, player);
            return;
        }
    }

    static Dictionary<GameResult, (GameState[] states, float chance)>[] WinningGames(
            Dictionary<GameResult, (GameState[] states, float chance)>[] games) {
        var result = games
                .Where(it => it.Count == 1 && it.ContainsKey(GameResult.PlayerWon))
                .ToArray();
        if (result.Length > 0) {
            return result;
        }
        result = games
                .Where(it => it.TryGetValue(GameResult.PlayerWon, out var won) && won.chance >= 0.5f)
                .ToArray();
        if (result.Length > 0) {
            return result;
        }
        result = games
                .Where(it => it.Count == 1 && it.ContainsKey(GameResult.Draw))
                .ToArray();
        return result.Length > 0 ? result : games;
    }

    /// Backtracking
    IEnumerable<GameState> SimulateGames(GameState state) {
        foreach (var playerMove in NextMoves(player, enemy)) {
            Set(playerMove, player);
            var gameResult = GameOver();
            var nextWithPlayer = new GameState([.. state.Moves, (player, playerMove)]);
            if (gameResult is not null) {
                Set(playerMove, Square.EMPTY);
                yield return nextWithPlayer with { Result = gameResult.Value };
                continue;
            }
            foreach (var enemyMove in NextMoves(enemy, player)) {
                Set(enemyMove, enemy);
                gameResult = GameOver();
                var nextWithEnemy = new GameState([.. nextWithPlayer.Moves, (enemy, enemyMove)]);
                if (gameResult is not null) {
                    Set(enemyMove, Square.EMPTY);
                    yield return nextWithEnemy with { Result = gameResult.Value };
                    continue;
                }
                foreach (var nextMoves in SimulateGames(nextWithEnemy)) {
                    yield return nextMoves;
                }
                Set(enemyMove, Square.EMPTY);
            }
            Set(playerMove, Square.EMPTY);
        }
    }

    GameResult? GameOver() {
        if (Squares(PlayerWon).Any()) {
            return GameResult.PlayerWon;
        }
        if (Squares(EnemyWon).Any()) {
            return GameResult.EnemyWon;
        }
        if (!Squares(IsEmpty).Any()) {
            return GameResult.Draw;
        }
        return null;

        bool PlayerWon(Point point) => HasLineWithNumberOfSquares(point, player, 3);
        bool EnemyWon(Point point) => HasLineWithNumberOfSquares(point, enemy, 3);
    }

    IEnumerable<Point> NextMoves(char playerSquare, char enemySquare) {
        Point[] winningMoves = [.. Squares(it => CanWin(it, playerSquare))];
        if (winningMoves.Length > 0) {
            foreach (var move in winningMoves) {
                yield return move;
            }
            yield break;
        }
        Point[] blockingMoves = [.. Squares(it => CanWin(it, enemySquare))];
        if (blockingMoves.Length > 0) {
            foreach (var move in blockingMoves) {
                yield return move;
            }
            yield break;
        }
        Point[] nextWinningMoves = [.. Squares(it => CanWinNext(it, playerSquare, enemySquare))];
        if (nextWinningMoves.Length > 0) {
            foreach (var move in nextWinningMoves) {
                yield return move;
            }
            yield break;
        }
        Point[] nextBlockingMoves = [.. Squares(it => CanWinNext(it, enemySquare, playerSquare))];
        if (nextBlockingMoves.Length > 0) {
            foreach (var nextMove in nextBlockingMoves) {
                Set(nextMove, enemySquare);
                blockingMoves = [.. Squares(it => CanWin(it, enemySquare))];
                Set(nextMove, Square.EMPTY);
                if (blockingMoves.Length > 0) {
                    foreach (var move in blockingMoves) {
                        yield return move;
                    }
                    yield break;
                }
            }
        }
        foreach (var move in Squares(IsEmpty)) {
            yield return move;
        }
    }

    bool IsEmpty(Point point) => Get(point) == Square.EMPTY;
    bool CanWin(Point point, char playerSquare) => IsEmpty(point) && HasLineWithNumberOfSquares(point, playerSquare, 2);

    bool CanWinNext(Point point, char playerSquare, char enemySquare) {
        if (!IsEmpty(point)) {
            return false;
        }
        var nextLines = LINES.Where(line => {
            if (!line.Contains(point)) {
                return false;
            }
            char[] lineSquares = [.. line.Select(it => squares[it.X, it.Y])];
            return lineSquares.Contains(playerSquare) && !lineSquares.Contains(enemySquare);
        });
        return nextLines.Count() >= 2;
    }

    static IEnumerable<Point> Squares(Predicate<Point> predicate) {
        for (var y = 0; y < SIZE; y++) {
            for (var x = 0; x < SIZE; x++) {
                var point = new Point(x, y);
                if (predicate(point)) {
                    yield return point;
                }
            }
        }
    }

    bool HasLineWithNumberOfSquares(Point point, char playerSquare, int number) {
        return LINES.Where(it => it.Contains(point))
                .Select(line => line.Count(it => Get(it) == playerSquare))
                .Any(count => count == number);
    }

    public void Print() {
        foreach (int y in Enumerable.Range(0, SIZE)) {
            var line = Enumerable.Range(0, SIZE).Select(x => squares[x, y]);
            Console.WriteLine(string.Concat(line));
        }
    }
}

static class Square {
    public const char EMPTY = '.';
    public const char PLAYER_X = 'X';
    public const char PLAYER_O = 'O';
}

readonly record struct Point(int X, int Y) {
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
}

readonly record struct GameState(List<(char square, Point point)> Moves, GameResult? Result = null) {
    public int Turns => Moves.Count;
    public Point First => Moves.First().point;
}

enum GameResult {
    PlayerWon,
    EnemyWon,
    Draw
}

static class Screen {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point RIGHT = new(1, 0);
}

static class Debug {
    const int TAB_LENGTH = 2;

    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
