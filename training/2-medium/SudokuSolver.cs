using System;
using System.Text;

namespace SudokuSolver;

/// https://www.codingame.com/training/medium/sudoku-solver
static class SudokuSolver {
    static void Main() {
        var sudoku = new Sudoku();
        sudoku.ReadInput();
        sudoku.Solve();
        Console.WriteLine(sudoku.Print());
    }
}

class Sudoku {
    readonly int[,] cells = new int[9, 9];

    public void ReadInput() {
        for (var y = 0; y < 9; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < 9; x++) {
                cells[x, y] = int.Parse(line[x].ToString());
            }
        }
    }

    public void Solve() {
        // TODO
    }

    public string Print() {
        var text = new StringBuilder();
        for (var y = 0; y < cells.GetLength(1); y++) {
            for (var x = 0; x < cells.GetLength(0); x++) {
                text.Append(cells[x, y]);
            }
            text.AppendLine();
        }
        return text.ToString();
    }
}
