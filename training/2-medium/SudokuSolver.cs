using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SudokuSolver;

/// https://www.codingame.com/training/medium/sudoku-solver
static class SudokuSolver {
    static void Main() {
        var sudoku = new Sudoku();
        sudoku.ReadInput();
        sudoku.Solve();
        Console.WriteLine(sudoku);
    }
}

class Sudoku {
    const int MAX = 9;
    readonly int[,] cells = new int[MAX, MAX];

    public void ReadInput() {
        for (var y = 0; y < MAX; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < MAX; x++) {
                cells[x, y] = int.Parse(line[x].ToString());
            }
        }
    }

    /// Backtracking
    [SuppressMessage("ReSharper", "TailRecursiveCall")]
    public void Solve() {
        if (!TrySolve()) {
            Console.Error.WriteLine("No solution found");
        }
        return;

        bool TrySolve(int x = 0, int y = 0) {
            if (x >= MAX) {
                x = 0;
                y++;
                if (y >= MAX) {
                    return true;
                }
            }
            if (cells[x, y] != 0) {
                return TrySolve(x + 1, y);
            }
            for (var digit = 1; digit <= MAX; digit++) {
                cells[x, y] = digit;
                if (IsValid(x, y) && TrySolve(x + 1, y)) {
                    return true;
                }
                // revert
                cells[x, y] = 0;
            }
            return false;
        }
    }

    bool IsValid(int testX, int testY) {
        int digit = cells[testX, testY];
        // check column
        for (var y = 0; y < MAX; y++) {
            if (y != testY && cells[testX, y] == digit) {
                return false;
            }
        }
        // check row
        for (var x = 0; x < MAX; x++) {
            if (x != testX && cells[x, testY] == digit) {
                return false;
            }
        }
        // check block
        int blockStartX = testX / 3 * 3;
        int blockStartY = testY / 3 * 3;
        for (int y = blockStartY; y < blockStartY + 3; y++) {
            for (int x = blockStartX; x < blockStartX + 3; x++) {
                if (x != testX && y != testY && cells[x, y] == digit) {
                    return false;
                }
            }
        }
        return true;
    }

    public override string ToString() {
        var text = new StringBuilder();
        for (var y = 0; y < MAX; y++) {
            for (var x = 0; x < MAX; x++) {
                text.Append(cells[x, y]);
            }
            if (y != MAX - 1) {
                text.AppendLine();
            }
        }
        return text.ToString();
    }
}
