using System;

// https://www.codingame.com/training/easy/is-the-king-in-check-part-1
var board = new Board();
board.Read();
Console.WriteLine(board.IsKingInCheck() ? "Check" : "No Check");

class Board {
    const int SIZE = 8;
    Point king, enemy;
    string enemyPiece = null!;

    public void Read() {
        for (var y = 0; y < SIZE; y++) {
            string[] chessRow = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < SIZE; x++) {
                string piece = chessRow[x];
                if (piece == Piece.KING) {
                    king = new Point(x, y);
                } else if (piece != Piece.EMPTY) {
                    enemy = new Point(x, y);
                    enemyPiece = piece;
                }
            }
        }
    }

    public bool IsKingInCheck() {
        switch (enemyPiece) {
            case Piece.ROOK:
                return king.X == enemy.X || king.Y == enemy.Y;
        }
        return false;
    }
}

static class Piece {
    public const string EMPTY = "_";
    public const string KING = "K";
    public const string ROOK = "R";
}

readonly record struct Point(int X, int Y);
