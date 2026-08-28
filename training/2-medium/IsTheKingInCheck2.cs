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
        return IsCheckedBy(enemies.First(), enemies.Last()) || IsCheckedBy(enemies.Last(), enemies.First());

        bool IsCheckedBy(Enemy enemy, Enemy other) => enemy.Piece switch {
            Piece.ROOK => IsCheckedByRook(enemy.Point, other),
            Piece.BISHOP => IsCheckedByBishop(enemy.Point, other),
            Piece.QUEEN => IsCheckedByRook(enemy.Point, other) || IsCheckedByBishop(enemy.Point, other),
            Piece.KNIGHT => IsCheckedByKnight(enemy.Point),
            _ => false
        };
    }

    bool IsCheckedByRook(Point self, Enemy other) {
        if (king.X != self.X && king.Y != self.Y) {
            return false;
        }
        return other.Piece is not (Piece.BISHOP or Piece.KNIGHT);
    }

    bool IsCheckedByBishop(Point self, Enemy other) {
        var selfDifference = self - king;
        if (Math.Abs(selfDifference.X) != Math.Abs(selfDifference.Y)) {
            return false;
        }
        if (other.Piece is not (Piece.ROOK or Piece.KNIGHT)) {
            return true;
        }
        var otherDifference = other.Point - king;
        if (Math.Abs(otherDifference.X) != Math.Abs(otherDifference.Y)) {
            return true;
        }
        if (Math.Sign(selfDifference.X) != Math.Sign(otherDifference.X)
                || Math.Sign(selfDifference.Y) != Math.Sign(otherDifference.Y)) {
            return true;
        }
        return Math.Abs(otherDifference.X) > Math.Abs(selfDifference.X)
                && Math.Abs(otherDifference.Y) > Math.Abs(selfDifference.Y);
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

readonly record struct Point(int X, int Y) {
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
}
