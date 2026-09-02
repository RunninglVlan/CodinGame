using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/medium/tic-tac-toe-engine
var board = new Board();
board.Read();
board.PlayBestMove();
board.Print();

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
        if (Squares(PlayerWon).Any() || Squares(EnemyWon).Any()) {
            return;
        }
        var moves = FindBestMoves([]).ToArray();
        if (moves.Length == 0) {
            var blockingMoves = Squares(EnemyCanWin).ToArray();
            if (blockingMoves.Length > 0) {
                Set(blockingMoves.First(), player);
            }
            return;
        }
        var movesWithLeastTurns = moves.GroupBy(it => it.Count).First().ToArray();
        foreach (var square in SQUARE_PRIORITIES) {
            if (!movesWithLeastTurns.Select(it => it.First()).Contains(square)) {
                continue;
            }
            Set(square, player);
            return;
        }
    }

    /// Backtracking
    IEnumerable<List<Point>> FindBestMoves(List<Point> path) {
        var winningMoves = Squares(PlayerCanWin).ToArray();
        if (winningMoves.Length > 0) {
            yield return [.. path, winningMoves.First()];
            yield break;
        }
        foreach (var playerMove in Squares(IsEmpty)) {
            Set(playerMove, player);
            if (Squares(EnemyCanWin).Any()) {
                Set(playerMove, Square.EMPTY);
                continue;
            }
            foreach (var enemyMove in Squares(PlayerCanWin)) {
                Set(enemyMove, enemy);
                foreach (var moves in FindBestMoves([.. path, playerMove])) {
                    yield return moves;
                }
                Set(enemyMove, Square.EMPTY);
            }
            Set(playerMove, Square.EMPTY);
        }
    }

    bool IsEmpty(Point point) => Get(point) == Square.EMPTY;
    bool PlayerCanWin(Point point) => IsEmpty(point) && HasLineWithNumberOfSquares(point, player, 2);
    bool EnemyCanWin(Point point) => IsEmpty(point) && HasLineWithNumberOfSquares(point, enemy, 2);
    bool PlayerWon(Point point) => HasLineWithNumberOfSquares(point, player, 3);
    bool EnemyWon(Point point) => HasLineWithNumberOfSquares(point, enemy, 3);

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
