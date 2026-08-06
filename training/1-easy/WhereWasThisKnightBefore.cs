using System;

/// https://www.codingame.com/training/easy/where-was-this-knight-before
static class WhereWasThisKnightBefore {
    static void Main() {
        const int size = 8;
        string pieces = Console.ReadLine()!;
        var board = new char[size, size];
        for (int y = size - 1; y >= 0; y--) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                board[x, y] = line[x];
            }
        }
        for (int y = size - 1; y >= 0; y--) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                char square = line[x];
            }
        }
        Console.WriteLine("answer");
    }
}
