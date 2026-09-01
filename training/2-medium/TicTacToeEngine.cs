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
        [Point.ZERO, Point.RIGHT, Point.RIGHT * 2],
        [Point.UP, Point.ONE, Point.RIGHT * 2 + Point.UP],
        [Point.UP * 2, Point.UP * 2 + Point.RIGHT, Point.ONE * 2],
        // columns
        [Point.ZERO, Point.UP, Point.UP * 2],
        [Point.RIGHT, Point.ONE, Point.UP * 2 + Point.RIGHT],
        [Point.RIGHT * 2, Point.RIGHT * 2 + Point.UP, Point.ONE * 2],
        // diagonals
        [Point.ZERO, Point.ONE, Point.ONE * 2],
        [Point.RIGHT * 2, Point.ONE, Point.UP * 2]
    ];

    /// Priorities on the screen: <code>
    /// 8 | 4 | 7 
    ///---+---+---
    /// 3 | 9 | 2 
    ///---+---+---
    /// 6 | 1 | 5 </code>
    static readonly Point[] PRIORITIES = [
        Point.ONE,
        Point.ZERO,
        Point.RIGHT * 2,
        Point.UP * 2,
        Point.UP * 2 + Point.RIGHT * 2,
        Point.RIGHT,
        Point.UP,
        Point.UP + Point.RIGHT * 2,
        Point.UP * 2 + Point.RIGHT
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
        var moves = FindBestMoves([]).ToArray();
        if (moves.Length == 0) {
            var blockingMoves = NextMoves(EnemyCanWin).ToArray();
            if (blockingMoves.Length > 0) {
                Set(blockingMoves.First(), player);
            }
            return;
        }
        var movesWithLeastTurns = moves.GroupBy(it => it.Count).First().ToArray();
        foreach (var square in PRIORITIES) {
            if (!movesWithLeastTurns.Select(it => it.First()).Contains(square)) {
                continue;
            }
            Set(square, player);
            return;
        }
    }

    /// Backtracking
    IEnumerable<List<Point>> FindBestMoves(List<Point> path) {
        var winningMoves = NextMoves(PlayerCanWin).ToArray();
        if (winningMoves.Length > 0) {
            yield return [.. path, winningMoves.First()];
            yield break;
        }
        foreach (var playerMove in NextMoves()) {
            Set(playerMove, player);
            if (NextMoves(EnemyCanWin).Any()) {
                Set(playerMove, Square.EMPTY);
                continue;
            }
            foreach (var enemyMove in NextMoves(PlayerCanWin)) {
                Set(enemyMove, enemy);
                foreach (var moves in FindBestMoves([.. path, playerMove])) {
                    yield return moves;
                }
                Set(enemyMove, Square.EMPTY);
            }
            Set(playerMove, Square.EMPTY);
        }
    }

    bool PlayerCanWin(Point point) => HasLineWithTwoSquares(point, player);
    bool EnemyCanWin(Point point) => HasLineWithTwoSquares(point, enemy);

    IEnumerable<Point> NextMoves(Predicate<Point>? predicate = null) {
        for (var y = 0; y < SIZE; y++) {
            for (var x = 0; x < SIZE; x++) {
                var point = new Point(x, y);
                if (Get(point) != Square.EMPTY) {
                    continue;
                }
                if (predicate != null && !predicate(point)) {
                    continue;
                }
                yield return point;
            }
        }
    }

    bool HasLineWithTwoSquares(Point point, char playerSquare) {
        return LINES.Where(it => it.Contains(point))
                .Select(line => line.Count(it => Get(it) == playerSquare))
                .Any(count => count == 2);
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
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);
    public static readonly Point UP = new(0, 1);
    public static readonly Point RIGHT = new(1, 0);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
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
