using System;
using System.Collections.Generic;

namespace MovesInMaze;

/// https://www.codingame.com/training/easy/moves-in-maze
static class MovesInMaze {
    static void Main() {
        var solution = new Solution();
        solution.ReadInput();
        solution.CountMoves();
        solution.PrintMaze();
    }
}

class Solution {
    const string MOVE_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    int width, height;
    Cell[,] maze = null!;
    Cell start = null!;

    public void ReadInput() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(inputs[0]), 3, 30);
        height = Math.Clamp(int.Parse(inputs[1]), 3, 30);
        maze = new Cell[width, height];
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                var cell = new Cell(new Point(x, y), row[x]);
                if (cell.type == CellType.START) {
                    start = cell;
                }
                maze[x, y] = cell;
            }
        }
    }

    public void CountMoves() {
        Queue<(Cell, int)> queue = [];
        HashSet<Cell> visited = [start];
        queue.Enqueue((start, 0));
        while (queue.TryDequeue(out var value)) {
            (var current, int moves) = value;
            current.Moves = moves;
            foreach (var direction in Point.Directions4) {
                (int x, int y) = current.point + direction;
                x = (x + width) % width;
                y = (y + height) % height;
                var next = maze[x, y];
                if (next.type == CellType.WALL || !visited.Add(next)) {
                    continue;
                }
                queue.Enqueue((next, moves + 1));
            }
        }
    }

    public void PrintMaze() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                var cell = maze[x, y];
                Console.Write(cell.Moves.HasValue ? MOVE_CHARS[cell.Moves.Value] : cell.type);
            }
            Console.WriteLine();
        }
    }
}

class Cell(Point point, char type) {
    public readonly Point point = point;
    public readonly char type = type;
    public int? Moves { get; set; }
}

static class CellType {
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
