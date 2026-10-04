using System;

/// https://www.codingame.com/training/easy/reverse-minesweeper
static class ReverseMinesweeper {
    static void Main() {
        int width = int.Parse(Console.ReadLine()!);
        int height = int.Parse(Console.ReadLine()!);
        var grid = new Grid(width, height);
        grid.Read();
        grid.Write();
    }
}

class Grid(int width, int height) {
    const char MINE = 'x';
    const string EMPTY = ".";

    readonly char[,] cells = new char[width, height];

    public void Read() {
        for (var y = 0; y < height; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                cells[x, y] = line[x];
            }
        }
    }

    public void Write() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                if (cells[x, y] == MINE) {
                    Console.Write(EMPTY);
                    continue;
                }
                int minesAround = MinesAround(x, y);
                Console.Write(minesAround == 0 ? EMPTY : minesAround);
            }
            Console.WriteLine();
        }
    }

    int MinesAround(int pointX, int pointY) {
        var sum = 0;
        for (int y = -1; y <= 1; y++) {
            for (int x = -1; x <= 1; x++) {
                int gridX = pointX + x;
                int gridY = pointY + y;
                if (gridX < 0 || gridX > width - 1 || gridY < 0 || gridY > height - 1) {
                    continue;
                }
                char value = cells[gridX, gridY];
                if (value == MINE) {
                    sum++;
                }
            }
        }
        return sum;
    }
}
