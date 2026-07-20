using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MovesInMaze;

/// https://www.codingame.com/training/easy/moves-in-maze
static class MovesInMaze {
    static void Main() {
        var solution = new Solution();
        solution.ReadInput();
        solution.CountMoves();
        solution.PrintGrid();
    }
}

class Solution {
    const string MOVES = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    int width, height;
    Cell[,] cells = null!;
    Cell start = null!;

    public void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(inputs[0]), 3, 30);
        height = Math.Clamp(int.Parse(inputs[1]), 3, 30);
        cells = new Cell[width, height];
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                var cell = new Cell(new Point(x, y), row[x]);
                if (cell.Type == CellType.START) {
                    start = cell;
                }
                cells[x, y] = cell;
            }
        }
    }

    public void CountMoves() {
        Queue<(Cell, int)> queue = [];
        HashSet<Cell> visited = [];
        queue.Enqueue((start, 0));
        visited.Add(start);
        while (queue.TryDequeue(out var value)) {
            (var current, int moves) = value;
            current.Moves = moves;
            foreach (var direction in Point.Directions4) {
                (int x, int y) = current.Point + direction;
                if (x < 0) {
                    x = width - 1;
                } else if (x > width - 1) {
                    x = 0;
                }
                if (y < 0) {
                    y = height - 1;
                } else if (y > height - 1) {
                    y = 0;
                }
                var next = cells[x, y];
                if (next.Type == CellType.WALL) {
                    continue;
                }
                if (!visited.Add(next)) {
                    continue;
                }
                queue.Enqueue((next, moves + 1));
            }
        }
    }

    public void PrintGrid() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                var cell = cells[x, y];
                Console.Write(cell.Moves.HasValue ? MOVES[cell.Moves.Value] : cell.Type);
            }
            Console.WriteLine();
        }
    }
}

record Cell(Point Point, char Type) {
    public int? Moves { get; set; }

    public virtual bool Equals(Cell? other) => ReferenceEquals(this, other);
    public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);
}

static class CellType {
    public const char EMPTY = '.';
    public const char WALL = '#';
    public const char START = 'S';
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
}
