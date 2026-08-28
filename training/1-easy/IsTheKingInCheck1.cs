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

    public bool IsKingInCheck() => enemyPiece switch {
        Piece.ROOK => IsCheckedByRook(),
        Piece.BISHOP => IsCheckedByBishop(),
        Piece.QUEEN => IsCheckedByRook() || IsCheckedByBishop(),
        Piece.KNIGHT => IsCheckedByKnight(),
        _ => false
    };

    bool IsCheckedByRook() => king.X == enemy.X || king.Y == enemy.Y;

    bool IsCheckedByBishop() {
        return Math.Abs(enemy.X - king.X) == Math.Abs(enemy.Y - king.Y);
    }

    bool IsCheckedByKnight() {
        return Math.Abs(enemy.X - king.X) == 1 && Math.Abs(enemy.Y - king.Y) == 2
                || Math.Abs(enemy.X - king.X) == 2 && Math.Abs(enemy.Y - king.Y) == 1;
    }
}

static class Piece {
    public const string EMPTY = "_";
    public const string KING = "K";
    public const string ROOK = "R";
    public const string BISHOP = "B";
    public const string QUEEN = "Q";
    public const string KNIGHT = "N";
}

readonly record struct Point(int X, int Y);
