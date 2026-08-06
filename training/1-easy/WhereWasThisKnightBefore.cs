using System;

/// https://www.codingame.com/training/easy/where-was-this-knight-before
static class WhereWasThisKnightBefore {
    const string X_CHARS = "abcdefgh";
    const string Y_CHARS = "12345678";

    static void Main() {
        const int size = 8;
        const char empty = '.';
        string pieces = Console.ReadLine()!;
        var board = new char[size, size];
        for (int y = size - 1; y >= 0; y--) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                board[x, y] = line[x];
            }
        }
        var move = new Move();
        for (int y = size - 1; y >= 0; y--) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                char square = line[x];
                if (square == board[x, y]) {
                    continue;
                }
                var coordinates = $"{X_CHARS[x]}{Y_CHARS[y]}";
                if (square == empty) {
                    move.From = coordinates;
                } else {
                    move.To = coordinates;
                    move.Piece = square;
                }
            }
        }
        Console.WriteLine(move);
        Console.WriteLine(move.Piece == 'K' ? "Knight" : "Other");
    }
}

class Move {
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public char Piece { get; set; }

    public override string ToString() => $"{From}-{To}";
}
