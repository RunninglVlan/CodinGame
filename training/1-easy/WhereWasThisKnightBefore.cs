using System;

/// https://www.codingame.com/training/easy/where-was-this-knight-before
static class WhereWasThisKnightBefore {
    static void Main() {
        var board = new Board();
        board.ReadPieces();
        board.ReadTurn();
        board.ReadNextTurn(out var move);
        Console.WriteLine(move);
        Console.WriteLine(move.Piece);
    }
}

class Board {
    const int SIZE = 8;
    const char EMPTY = '.';

    string pieces = string.Empty;
    readonly char[,] squares = new char[SIZE, SIZE];

    public void ReadPieces() => pieces = Console.ReadLine()!;

    public void ReadTurn() {
        for (int y = SIZE - 1; y >= 0; y--) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                char square = line[x];
                squares[x, y] = pieces.Contains(char.ToUpper(square)) ? square : EMPTY;
            }
        }
    }

    public void ReadNextTurn(out Move move) {
        move = new Move();
        for (int y = SIZE - 1; y >= 0; y--) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                char square = line[x];
                if (!pieces.Contains(char.ToUpper(square))) {
                    square = EMPTY;
                }
                char squareBefore = squares[x, y];
                if (square == squareBefore) {
                    continue;
                }
                var coordinates = new Point(x, y);
                if (square == EMPTY) {
                    move.From = coordinates;
                } else {
                    move.To = coordinates;
                    move.Capture = squareBefore != EMPTY;
                }
            }
        }
        move.Piece = move.IsKnight ? "Knight" : "Other";
    }
}

class Move {
    const string X_CHARS = "abcdefgh";
    const string Y_CHARS = "12345678";

    public Point From { get; set; }
    public Point To { get; set; }
    public string Piece { get; set; } = string.Empty;
    public bool Capture { get; set; }

    public bool IsKnight {
        get {
            int distX = Math.Abs(From.X - To.X);
            int distY = Math.Abs(From.Y - To.Y);
            return distX == 2 && distY == 1 || distX == 1 && distY == 2;
        }
    }

    public override string ToString() {
        var from = $"{X_CHARS[From.X]}{Y_CHARS[From.Y]}";
        var to = $"{X_CHARS[To.X]}{Y_CHARS[To.Y]}";
        return $"{from}{(Capture ? 'x' : '-')}{to}";
    }
}

readonly record struct Point(int X, int Y) {
    public override string ToString() => $"({X}, {Y})";
}
