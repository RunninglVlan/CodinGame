using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/medium/is-the-king-in-check-part-2
var board = new Board();
board.Read();
Console.WriteLine(board.IsKingInCheck() ? "Check" : "No Check");

class Board {
    const int SIZE = 8;

    Point king;
    readonly List<Enemy> enemies = [];

    public void Read() {
        for (var y = 0; y < SIZE; y++) {
            string[] chessRow = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < SIZE; x++) {
                string piece = chessRow[x];
                if (piece == Piece.KING) {
                    king = new Point(x, y);
                } else if (piece != Piece.EMPTY) {
                    enemies.Add(new(new Point(x, y), piece));
                }
            }
        }
    }

    public bool IsKingInCheck() {
        return IsCheckedBy(enemies.First()) || IsCheckedBy(enemies.Last());

        bool IsCheckedBy(Enemy enemy) => enemy.Piece switch {
            Piece.ROOK => IsCheckedByRook(enemy.Point),
            Piece.BISHOP => IsCheckedByBishop(enemy.Point),
            Piece.QUEEN => IsCheckedByRook(enemy.Point) || IsCheckedByBishop(enemy.Point),
            Piece.KNIGHT => IsCheckedByKnight(enemy.Point),
            _ => false
        };
    }

    bool IsCheckedByRook(Point enemy) => king.X == enemy.X || king.Y == enemy.Y;

    bool IsCheckedByBishop(Point enemy) {
        return Math.Abs(enemy.X - king.X) == Math.Abs(enemy.Y - king.Y);
    }

    bool IsCheckedByKnight(Point enemy) {
        return Math.Abs(enemy.X - king.X) == 1 && Math.Abs(enemy.Y - king.Y) == 2
                || Math.Abs(enemy.X - king.X) == 2 && Math.Abs(enemy.Y - king.Y) == 1;
    }
}

readonly record struct Enemy(Point Point, string Piece);

static class Piece {
    public const string EMPTY = "_";
    public const string KING = "k";
    public const string ROOK = "R";
    public const string BISHOP = "B";
    public const string QUEEN = "Q";
    public const string KNIGHT = "N";
}

readonly record struct Point(int X, int Y);
