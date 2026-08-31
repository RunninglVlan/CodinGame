using System;
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

    public void PlayBestMove() {
        var point = FindBestMove(PlayerCanWin);
        point ??= FindBestMove(EnemyCanWin);
        point ??= FindBestMove(PlayerCanWinNext);
        if (!point.HasValue) {
            Console.Error.WriteLine("No best move");
            return;
        }
        (int x, int y) = point.Value;
        squares[x, y] = player;
        return;

        bool PlayerCanWin(Point cell) => HasLineWithTwoSquares(cell, player);
        bool EnemyCanWin(Point cell) => HasLineWithTwoSquares(cell, enemy);

        bool PlayerCanWinNext(Point cell) {
            var nextLines = lines.Where(it => it.Contains(cell))
                    .Where(line => line.Any(it => squares[it.X, it.Y] == player))
                    .Where(line => line.Any(it => squares[it.X, it.Y] == Square.EMPTY));
            return nextLines.Count() >= 2;
        }
    }

    Point? FindBestMove(Predicate<Point> predicate) {
        for (var y = 0; y < SIZE; y++) {
            for (var x = 0; x < SIZE; x++) {
                var point = new Point(x, y);
                if (squares[x, y] != Square.EMPTY || !predicate(point)) {
                    continue;
                }
                return point;
            }
        }
        return null;
    }

    bool HasLineWithTwoSquares(Point point, char square) {
        return lines.Where(it => it.Contains(point))
                .Select(line => line.Count(it => squares[it.X, it.Y] == square))
                .Any(count => count == 2);
    }

    public void Print() {
        for (var y = 0; y < SIZE; y++) {
            for (var x = 0; x < SIZE; x++) {
                Console.Write(squares[x, y]);
            }
            Console.WriteLine();
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
    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null) {
        Console.Error.WriteLine(line);
    }
}
