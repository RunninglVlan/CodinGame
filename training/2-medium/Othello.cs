using System;

/// https://www.codingame.com/training/medium/othello
static class Othello {
    static void Main() {
        var board = new Board();
        board.Init();
        string[] inputs = Console.ReadLine()!.Split(' ');
        var color = inputs[0][0] == Color.WHITE ? Cell.White : Cell.Black;
        string move = inputs[1];
        Console.WriteLine("answer");
    }
}

class Board {
    const int SIZE = 8;

    readonly Cell[,] cells = new Cell[SIZE, SIZE];

    public void Init() {
        for (var y = 0; y < SIZE; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                cells[x, y] = row[x] switch {
                    Color.WHITE => Cell.White,
                    Color.BLACK => Cell.Black,
                    _ => Cell.Empty
                };
            }
        }
    }
}

enum Cell {
    Empty,
    Black,
    White
}

static class Color {
    public const char WHITE = 'W';
    public const char BLACK = 'B';
}
