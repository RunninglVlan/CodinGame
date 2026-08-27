using System;
using System.Linq;

// https://www.codingame.com/training/easy/tictactoe
var board = new Board();
board.Read();
if (!board.TryWin()) {
    Console.WriteLine("false");
    return;
}
board.Print();

class Board {
    const int SIZE = 3;

    readonly char[,] squares = new char[SIZE, SIZE];

    readonly Point[][] lines = [
        // rows
        [Point.ZERO, Point.RIGHT, Point.RIGHT * 2],
        [Point.UP, Point.ONE, new Point(2, 1)],
        [Point.UP * 2, new Point(1, 2), Point.ONE * 2],
        // columns
        [Point.ZERO, Point.UP, Point.UP * 2],
        [Point.RIGHT, Point.ONE, new Point(1, 2)],
        [Point.RIGHT * 2, new Point(2, 1), Point.ONE * 2],
        // diagonals
        [Point.ZERO, Point.ONE, Point.ONE * 2],
        [Point.RIGHT * 2, Point.ONE, Point.UP * 2]
    ];

    public void Read() {
        for (var y = 0; y < SIZE; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                squares[x, y] = line[x];
            }
        }
    }

    public bool TryWin() {
        for (var y = 0; y < SIZE; y++) {
            for (var x = 0; x < SIZE; x++) {
                if (squares[x, y] != Square.EMPTY) {
                    continue;
                }
                if (!Check(x, y)) {
                    continue;
                }
                squares[x, y] = Square.PLAYER;
                return true;
            }
        }
        return false;
    }

    bool Check(int x, int y) {
        var point = new Point(x, y);
        return lines.Where(it => it.Contains(point))
                .Select(line => line.Count(it => squares[it.X, it.Y] == Square.PLAYER))
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
    public const char ENEMY = 'X';
    public const char PLAYER = 'O';
}

readonly record struct Point(int X, int Y) {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);
    public static readonly Point UP = new(0, 1);
    public static readonly Point RIGHT = new(1, 0);

    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}
