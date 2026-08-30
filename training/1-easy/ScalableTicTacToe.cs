using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/scaleable-tictactoe
var board = new Board();
board.Read();
board.AnalyzeState(out string result);
board.Print();
Console.WriteLine(result);

class Board {
    int size;
    int winningCells;
    char[,] squares = null!;

    public void Read() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        size = Math.Clamp(int.Parse(inputs[0]), 3, 12);
        winningCells = Math.Clamp(int.Parse(inputs[1]), 3, size);
        squares = new char[size, size];
        for (var y = 0; y < size; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                squares[x, y] = line[x];
            }
        }
    }

    public void AnalyzeState(out string result) {
        if (TryFindVerticalWinner(out var winner)) {
            foreach (var point in winner.cells) {
                squares[point.X, point.Y] = '|';
            }
            result = $"The winner is {winner.player}.";
            return;
        }
        result = "The game isn't over yet!";
    }

    bool TryFindVerticalWinner(out (char player, List<Point> cells) winner) {
        Dictionary<char, List<Point>> playerCells = [];
        for (var x = 0; x < size; x++) {
            playerCells.Clear();
            for (var y = 0; y < size; y++) {
                char square = squares[x, y];
                if (square == Square.EMPTY) {
                    continue;
                }
                var cells = playerCells.GetValueOrDefault(square, []);
                cells.Add(new Point(x, y));
                playerCells[square] = cells;
            }
            char winningPlayer = playerCells
                    .Where(it => it.Value.Count == winningCells)
                    .Select(it => it.Key)
                    .FirstOrDefault(Square.EMPTY);
            if (winningPlayer == Square.EMPTY) {
                continue;
            }
            winner = (winningPlayer, playerCells[winningPlayer]);
            return true;
        }
        winner = default;
        return false;
    }

    public void Print() {
        for (var y = 0; y < size; y++) {
            for (var x = 0; x < size; x++) {
                Console.Write(squares[x, y]);
            }
            Console.WriteLine();
        }
    }
}

static class Square {
    public const char EMPTY = ' ';
}

readonly record struct Point(int X, int Y);
