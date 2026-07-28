using System;

/// https://www.codingame.com/training/medium/othello
static class Othello {
    static void Main() {
        var board = new Board();
        board.Init();
        string[] inputs = Console.ReadLine()!.Split(' ');
        var color = inputs[0][0] == Color.WHITE ? Cell.White : Cell.Black;
        string move = inputs[1];
        Console.WriteLine(board.Place(color, move));
    }
}

class Board {
    const int SIZE = 8;
    const string X_CHARS = "abcdefgh";
    const string Y_CHARS = "12345678";

    readonly Cell[,] cells = new Cell[SIZE, SIZE];
    readonly int[] counts = new int[Enum.GetValues<Cell>().Length];

    public void Init() {
        for (var y = 0; y < SIZE; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                var cell = row[x] switch {
                    Color.WHITE => Cell.White,
                    Color.BLACK => Cell.Black,
                    _ => Cell.Empty
                };
                counts[(int)cell]++;
                cells[x, y] = cell;
            }
        }
    }

    public string Place(Cell color, string move) {
        var point = new Point(X_CHARS.IndexOf(move[0]), Y_CHARS.IndexOf(move[1]));
        var cell = cells[point.X, point.Y];
        if (cell != Cell.Empty) {
            return Error.CELL_ALREADY_HAS_TOKEN;
        }
        var oppositeColor = color == Cell.White ? Cell.Black : Cell.White;
        foreach (var direction in Point.Directions8) {
            var neighbor = point + direction;
            if (!neighbor.InBounds(SIZE, SIZE)) {
                continue;
            }
            if (cells[neighbor.X, neighbor.Y] != oppositeColor) {
                continue;
            }
            if (!TryFindSandwich(neighbor, direction, oppositeColor, out int count)) {
                continue;
            }
            counts[(int)color] += 1 + count;
            counts[(int)oppositeColor] -= count;
            return $"{counts[(int)Cell.White]} {counts[(int)Cell.Black]}";
        }
        return Error.CANNOT_MAKE_SANDWICH;
    }

    bool TryFindSandwich(Point start, Point direction, Cell oppositeColor, out int count) {
        count = 1;
        while (true) {
            var next = start + direction;
            if (!next.InBounds(SIZE, SIZE)) {
                return false;
            }
            var cell = cells[next.X, next.Y];
            if (cell == Cell.Empty) {
                return false;
            }
            if (cell == oppositeColor) {
                count++;
            } else {
                return true;
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

static class Error {
    public const string CELL_ALREADY_HAS_TOKEN = "NOPE";
    public const string CANNOT_MAKE_SANDWICH = "NULL";
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions8 { get; } = [
        RIGHT, LEFT, UP, DOWN,
        new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)
    ];

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"({X}, {Y})";
}
