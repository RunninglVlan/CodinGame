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
    const char EMPTY = ' ';

    int size;
    int winningSquares;
    char[,] squares = null!;

    public void Read() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        size = Math.Clamp(int.Parse(inputs[0]), 3, 12);
        winningSquares = Math.Clamp(int.Parse(inputs[1]), 3, size);
        squares = new char[size, size];
        for (var y = 0; y < size; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                squares[x, y] = line[x];
            }
        }
    }

    public void AnalyzeState(out string result) {
        if (TryFindVerticalWinner(out var winner) || TryFindHorizonalWinner(out winner)) {
            foreach (var point in winner.Points) {
                squares[point.X, point.Y] = winner.Line;
            }
            result = $"The winner is {winner.Player}.";
            return;
        }
        result = "The game isn't over yet!";
    }

    bool TryFindVerticalWinner(out Winner winner) {
        Dictionary<char, List<Point>> playerPoints = [];
        for (var x = 0; x < size; x++) {
            playerPoints.Clear();
            for (var y = 0; y < size; y++) {
                char square = squares[x, y];
                if (square == EMPTY) {
                    continue;
                }
                var points = playerPoints.GetValueOrDefault(square, []);
                points.Add(new Point(x, y));
                playerPoints[square] = points;
            }
            char player = playerPoints
                    .Where(it => it.Value.Count == winningSquares)
                    .Select(it => it.Key)
                    .FirstOrDefault(EMPTY);
            if (player == EMPTY) {
                continue;
            }
            winner = new Winner(player, '|', playerPoints[player]);
            return true;
        }
        winner = default;
        return false;
    }

    bool TryFindHorizonalWinner(out Winner winner) {
        Dictionary<char, List<Point>> playerPoints = [];
        for (var y = 0; y < size; y++) {
            playerPoints.Clear();
            for (var x = 0; x < size; x++) {
                char square = squares[x, y];
                if (square == EMPTY) {
                    continue;
                }
                var points = playerPoints.GetValueOrDefault(square, []);
                points.Add(new Point(x, y));
                playerPoints[square] = points;
            }
            char player = playerPoints
                    .Where(it => it.Value.Count == winningSquares)
                    .Select(it => it.Key)
                    .FirstOrDefault(EMPTY);
            if (player == EMPTY) {
                continue;
            }
            winner = new Winner(player, '-', playerPoints[player]);
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

readonly record struct Winner(char Player, char Line, List<Point> Points);

readonly record struct Point(int X, int Y);
