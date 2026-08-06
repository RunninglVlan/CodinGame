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
    const string X_CHARS = "abcdefgh";
    const string Y_CHARS = "12345678";

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
                var coordinates = $"{X_CHARS[x]}{Y_CHARS[y]}";
                if (square == EMPTY) {
                    move.From = coordinates;
                } else {
                    move.To = coordinates;
                    move.Piece = char.ToLower(square) == 'n' ? "Knight" : "Other";
                    move.Capture = squareBefore != EMPTY;
                }
            }
        }
    }
}

class Move {
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Piece { get; set; } = string.Empty;
    public bool Capture { get; set; }

    public override string ToString() => $"{From}{(Capture ? 'x' : '-')}{To}";
}
