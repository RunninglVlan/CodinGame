using System;

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
                if (PlayerSquaresInRow(y) != 2) {
                    continue;
                }
                squares[x, y] = Square.PLAYER;
                return true;
            }
        }
        return false;
    }

    int PlayerSquaresInRow(int y) {
        var count = 0;
        for (var x = 0; x < SIZE; x++) {
            if (squares[x, y] == Square.PLAYER) {
                count++;
            }
        }
        return count;
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
