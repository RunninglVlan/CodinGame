using System;
using System.Collections.Generic;

// https://www.codingame.com/training/easy/is-the-king-in-check-part-1
var board = new Board();
board.Read();
Console.WriteLine(board.IsKingInCheck() ? "Check" : "No Check");

class Board {
    const int SIZE = 8;
    Point king, enemy;
    string enemyPiece = null!;

    public void Read() {
        enemyPiece = null!;
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
        _ => false
    };

    bool IsCheckedByRook() => king.X == enemy.X || king.Y == enemy.Y;

    bool IsCheckedByBishop() {
        List<Point> directions = [
            Point.UP + Point.RIGHT, Point.UP + Point.LEFT,
            Point.DOWN + Point.RIGHT, Point.DOWN + Point.LEFT
        ];
        foreach (var direction in directions) {
            var point = king;
            for (var _ = 0; _ < SIZE - 1; _++) {
                point += direction;
                if (point == enemy) {
                    return true;
                }
            }
        }
        return false;
    }
}

static class Piece {
    public const string EMPTY = "_";
    public const string KING = "K";
    public const string ROOK = "R";
    public const string BISHOP = "B";
    public const string QUEEN = "Q";
}

readonly record struct Point(int X, int Y) {
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
}
