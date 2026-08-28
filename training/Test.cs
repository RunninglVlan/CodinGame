const string testCase = """
                        _ _ _ _ _ _ _ _
                        _ _ _ _ _ _ _ _
                        _ _ _ _ _ _ _ _
                        _ _ _ _ _ _ _ _
                        _ _ _ _ _ _ _ _
                        _ B k _ R _ _ _
                        _ _ _ _ _ _ _ _
                        _ _ _ _ _ _ _ _
                        Check
                        """;
// let's write some more tests
// - checked by rook
Console.SetIn(new StringReader(testCase));

// https://www.codingame.com/training/medium/is-the-king-in-check-part-2
var board = new Board();
board.Read();
string? expected = Console.ReadLine();
string actual = board.IsKingInCheck() ? "Check" : "No Check";
Console.WriteLine(expected == actual ? $"✅ {actual}" : $"❌ expected {expected}, but found {actual}");

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
            Piece.ROOK => IsCheckedOrthogonally(enemy.Point, other.Point),
            Piece.BISHOP => IsCheckedDiagonally(enemy.Point, other.Point),
            Piece.QUEEN => IsCheckedOrthogonally(enemy.Point, other.Point)
                    || IsCheckedDiagonally(enemy.Point, other.Point),
            Piece.KNIGHT => IsCheckedByKnight(enemy.Point),
            _ => false
        };
    }

    bool IsCheckedOrthogonally(Point enemy, Point other) {
        var toKing = enemy - king;
        if (!IsOnOrthogonal(toKing)) {
            return false;
        }
        var otherToKing = other - king;
        if (!IsOnOrthogonal(otherToKing)) {
            return true;
        }
        // bool sameOrthogonal = toKing.X == otherToKing.X || toKing.Y == otherToKing.Y;
        // if (sameOrthogonal) {
        //     return false;
        // }
        Console.Error.WriteLine($"toKing: {toKing}, otherToKing: {otherToKing}");
        return true;

        bool IsOnOrthogonal(Point difference) => Math.Abs(difference.X) == 0 || Math.Abs(difference.Y) == 0;
    }

    bool IsCheckedDiagonally(Point enemy, Point other) {
        var toKing = enemy - king;
        if (!IsOnDiagonal(toKing)) {
            return false;
        }
        var otherToKing = other - king;
        if (!IsOnDiagonal(otherToKing)) {
            return true;
        }
        bool sameDiagonal = Math.Sign(toKing.X) == Math.Sign(otherToKing.X)
                && Math.Sign(toKing.Y) == Math.Sign(otherToKing.Y);
        if (!sameDiagonal) {
            return true;
        }
        bool otherIsBeyond = Math.Abs(otherToKing.X) > Math.Abs(toKing.X)
                && Math.Abs(otherToKing.Y) > Math.Abs(toKing.Y);
        return otherIsBeyond;

        bool IsOnDiagonal(Point difference) => Math.Abs(difference.X) == Math.Abs(difference.Y);
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
