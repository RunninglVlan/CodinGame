using System;
using System.Linq;

// https://www.codingame.com/training/medium/tic-tac-toe-engine
var board = new Board();
board.Read();
board.PlayBestMove();
board.Print();

class Board {
    const int SIZE = 3;

    char player;
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
        player = Console.ReadLine()!.First();
        for (var y = 0; y < SIZE; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                squares[x, y] = line[x];
            }
        }
    }

    public void PlayBestMove() {
        TryWin();
    }

    bool TryWin() {
        for (var y = 0; y < SIZE; y++) {
            for (var x = 0; x < SIZE; x++) {
                if (squares[x, y] != Square.EMPTY) {
                    continue;
                }
                if (!HasLineWithTwoPlayerSquares(x, y)) {
                    continue;
                }
                squares[x, y] = player;
                return true;
            }
        }
        return false;
    }

    bool HasLineWithTwoPlayerSquares(int x, int y) {
        var point = new Point(x, y);
        return lines.Where(it => it.Contains(point))
                .Select(line => line.Count(it => squares[it.X, it.Y] == player))
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
}

readonly record struct Point(int X, int Y) {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);
    public static readonly Point UP = new(0, 1);
    public static readonly Point RIGHT = new(1, 0);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
}
