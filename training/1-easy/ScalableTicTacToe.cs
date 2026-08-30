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
        for (var y = 0; y <= size - winningSquares; y++) {
            for (var x = 0; x <= size - winningSquares; x++) {
                if (!TryFindVerticalWinner(x, y, out var winner) && !TryFindHorizonalWinner(x, y, out winner)) {
                    continue;
                }
                foreach (var point in winner.Points) {
                    squares[point.X, point.Y] = winner.Line;
                }
                result = $"The winner is {winner.Player}.";
                return;
            }
        }
        result = "The game isn't over yet!";
    }

    bool TryFindVerticalWinner(int x, int y, out Winner winner) {
        var startPoints = Enumerable.Range(0, winningSquares).Select(it => new Point(x + it, y + 0));
        return TryFindWinner(x, y, startPoints, Point.UP, '|', out winner);
    }

    bool TryFindHorizonalWinner(int x, int y, out Winner winner) {
        var startPoints = Enumerable.Range(0, winningSquares).Select(it => new Point(x + 0, y + it));
        return TryFindWinner(x, y, startPoints, Point.RIGHT, '-', out winner);
    }

    bool TryFindWinner(int x, int y, IEnumerable<Point> startPoints, Point direction, char line, out Winner winner) {
        Dictionary<char, List<Point>> playerPoints = [];
        foreach (var start in startPoints) {
            playerPoints.Clear();
            for (var current = start; current.InBounds(x + winningSquares, y + winningSquares); current += direction) {
                char square = squares[current.X, current.Y];
                if (square == EMPTY) {
                    continue;
                }
                var points = playerPoints.GetValueOrDefault(square, []);
                points.Add(new Point(current.X, current.Y));
                playerPoints[square] = points;
            }
            char player = playerPoints
                    .Where(it => it.Value.Count == winningSquares)
                    .Select(it => it.Key)
                    .FirstOrDefault(EMPTY);
            if (player == EMPTY) {
                continue;
            }
            winner = new Winner(player, line, playerPoints[player]);
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

readonly record struct Point(int X, int Y) {
    public static readonly Point UP = new(0, 1);
    public static readonly Point RIGHT = new(1, 0);

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
}
