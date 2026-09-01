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

    char player, enemy;
    readonly char[,] squares = new char[SIZE, SIZE];

    readonly Point[][] lines = [
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

    /// Backtracking
    public bool PlayBestMove() {
        var points = EmptySquares(PlayerCanWin).Concat(EmptySquares(EnemyCanWin)).ToArray();
        if (points.Length > 0) {
            Set(points.First(), player);
            return true;
        }
        foreach (var playerPoint in EmptySquares()) {
            Set(playerPoint, player);
            foreach (var enemyPoint in EmptySquares()) {
                Set(enemyPoint, enemy);
                if (PlayBestMove()) {
                    Set(playerPoint, Square.EMPTY);
                    Set(enemyPoint, Square.EMPTY);
                    return true;
                }
                Set(enemyPoint, Square.EMPTY);
            }
            Set(playerPoint, Square.EMPTY);
        }
        return false;

        bool PlayerCanWin(Point cell) => HasLineWithTwoSquares(cell, player);
        bool EnemyCanWin(Point cell) => HasLineWithTwoSquares(cell, enemy);
    }

    IEnumerable<Point> EmptySquares(Predicate<Point>? predicate = null) {
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

    bool HasLineWithTwoSquares(Point point, char square) {
        return lines.Where(it => it.Contains(point))
                .Select(line => line.Count(it => Get(it) == square))
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
