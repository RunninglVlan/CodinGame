using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/medium/tic-tac-toe-engine
var board = new Board();
board.Read();
board.PlayBestMove();
board.Print();

class Board {
    const int SIZE = 3;

    static readonly Point[][] WINNING_LINES = [
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
        player = Console.ReadLine()!.First();
        enemy = player == Square.PLAYER_O ? Square.PLAYER_X : Square.PLAYER_O;
        for (var y = 0; y < SIZE; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                squares[x, y] = line[x];
            }
        }
    }

    char Get(Point point) => squares[point.X, point.Y];
    void Set(Point point, char square) => squares[point.X, point.Y] = square;

    public void PlayBestMove() {
        if (GetState() is not GameState.Playing) {
            return;
        }
        var bestMoves = SimulateGames(new Game([]))
                .GroupBy(it => it.First)
                .GroupBy(BestGame)
                .OrderBy(it => it.Key)
                .First()
                .Select(it => it.Key)
                .ToArray();
        var bestMove = SQUARE_PRIORITIES.First(it => bestMoves.Contains(it));
        Set(bestMove, player);
        return;

        int BestGame(IGrouping<Point, Game> firstGroup) {
            var results = firstGroup.GroupBy(it => it.State).Select(it => it.Key).ToHashSet();
            return results.Count != 1 ? 2
                    : results.Contains(GameState.PlayerWon) ? 0
                    : results.Contains(GameState.Draw) ? 1
                    : 2;
        }
    }

    /// Backtracking
    IEnumerable<Game> SimulateGames(Game game) {
        foreach (var playerMove in NextMoves(player, enemy)) {
            Set(playerMove, player);
            var state = GetState();
            var nextWithPlayer = new Game([.. game.Moves, (player, playerMove)]);
            if (state is not GameState.Playing) {
                Set(playerMove, Square.EMPTY);
                yield return nextWithPlayer with { State = state };
                continue;
            }
            foreach (var enemyMove in NextMoves(enemy, player)) {
                Set(enemyMove, enemy);
                state = GetState();
                var nextWithEnemy = new Game([.. nextWithPlayer.Moves, (enemy, enemyMove)]);
                if (state is not GameState.Playing) {
                    Set(enemyMove, Square.EMPTY);
                    yield return nextWithEnemy with { State = state };
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

    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    GameState GetState() {
        if (Squares(PlayerWon).Any()) {
            return GameState.PlayerWon;
        }
        if (Squares(EnemyWon).Any()) {
            return GameState.EnemyWon;
        }
        if (!Squares(IsEmpty).Any()) {
            return GameState.Draw;
        }
        return GameState.Playing;

        bool PlayerWon(Point point) => HasLineWithNSquares(point, 3, player);
        bool EnemyWon(Point point) => HasLineWithNSquares(point, 3, enemy);
    }

    Point[] NextMoves(char playerSquare, char enemySquare) {
        Func<Point[]>[] strategies = [Win, Block, SetUpWin, ForcedBlock, AnyEmpty];
        return strategies.Select(it => it()).First(moves => moves.Length > 0);

        Point[] Win() => [.. Squares(it => CanWin(it, playerSquare))];
        Point[] Block() => [.. Squares(it => CanWin(it, enemySquare))];
        Point[] SetUpWin() => [.. Squares(it => CanWinNext(it, playerSquare, enemySquare))];

        Point[] ForcedBlock() {
            foreach (var nextMove in Squares(it => CanWinNext(it, enemySquare, playerSquare))) {
                Set(nextMove, enemySquare);
                Point[] blockingMoves = [.. Squares(it => CanWin(it, enemySquare))];
                Set(nextMove, Square.EMPTY);
                if (blockingMoves.Length > 0) {
                    return blockingMoves;
                }
            }
            return [];
        }

        Point[] AnyEmpty() => [.. Squares(IsEmpty)];
    }

    bool IsEmpty(Point point) => Get(point) == Square.EMPTY;
    bool CanWin(Point point, char playerSquare) => IsEmpty(point) && HasLineWithNSquares(point, 2, playerSquare);

    bool CanWinNext(Point point, char playerSquare, char enemySquare) {
        if (!IsEmpty(point)) {
            return false;
        }
        int winningLines = WINNING_LINES
                .Where(it => it.Contains(point))
                .Select(it => it.Select(Get).ToArray())
                .Count(it => it.Contains(playerSquare) && !it.Contains(enemySquare));
        return winningLines >= 2;
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

    bool HasLineWithNSquares(Point point, int number, char playerSquare) {
        return WINNING_LINES.Where(it => it.Contains(point))
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

readonly record struct Game(List<(char square, Point point)> Moves, GameState State = GameState.Playing) {
    public int Turns => Moves.Count;
    public Point First => Moves.First().point;
}

enum GameState {
    Playing,
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
