using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// Compute how many points you scored with your new words!
// https://www.codingame.com/training/easy/scrabble-score
var scrabble = new Scrabble();
scrabble.ReadTileScores();
scrabble.ReadBoard();
var wordScores = scrabble.ComputeScore();
foreach ((string word, int score) in wordScores) {
    Console.WriteLine($"{word} {score}");
}
Console.WriteLine($"Total {wordScores.Sum(it => it.Value)}");

class Scrabble {
    const char EMPTY = '.';

    readonly Dictionary<char, int> tileScores = [];
    int width, height;
    Cell[,] board = null!;
    readonly List<Point> newTiles = [];

    public void ReadTileScores() {
        int tiles = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 30);
        for (var i = 0; i < tiles; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            char character = inputs.First()[0];
            int score = int.Parse(inputs.Last());
            tileScores.Add(character, score);
        }
    }

    public void ReadBoard() {
        InitBoard();
        for (var y = 0; y < height; y++) {
            string previousWordBoardRow = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char c = previousWordBoardRow[x];
                if (c == EMPTY) {
                    continue;
                }
                board[x, y].Tile = c;
            }
        }
        for (var y = 0; y < height; y++) {
            string playedWordBoardRow = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char c = playedWordBoardRow[x];
                if (c == EMPTY || c == board[x, y].Tile) {
                    continue;
                }
                board[x, y].Tile = c;
                newTiles.Add(new Point(x, y));
            }
        }
    }

    void InitBoard() {
        string[] dimensions = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(dimensions[0]), 4, 15);
        height = Math.Clamp(int.Parse(dimensions[1]), 4, 15);
        board = new Cell[width, height];
        for (var y = 0; y < height; y++) {
            string emptyBoardRow = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                board[x, y] = new Cell(emptyBoardRow[x] switch {
                    'l' => CellType.DoubleLetter,
                    'L' => CellType.TripleLetter,
                    'w' => CellType.DoubleWord,
                    'W' => CellType.TripleWord,
                    _ => CellType.Normal
                });
            }
        }
    }

    public List<Score> ComputeScore() => [.. newTiles.Select(ScoreAt)];

    Score ScoreAt(Point point) {
        var cell = board[point.X, point.Y];
        var word = $"{cell.Tile}";
        int score = tileScores[cell.Tile!.Value];
        foreach (var direction in Point.Directions4) {
            var neighbor = point + direction;
            if (!neighbor.InBounds(width, height)) {
                continue;
            }
            var cellNeighbor = board[neighbor.X, neighbor.Y];
            if (cellNeighbor.Tile == null) {
                continue;
            }
            if (direction == Point.UP || direction == Point.RIGHT) {
                word += cellNeighbor.Tile;
            } else {
                word = $"{cellNeighbor.Tile}{word}";
            }
            score += tileScores[cellNeighbor.Tile!.Value];
        }
        return new Score(word, score);
    }
}

record Cell(CellType Type) {
    public char? Tile { get; set; }
}

enum CellType {
    Normal,
    DoubleLetter,
    TripleLetter,
    DoubleWord,
    TripleWord
}

record Score(string Word, int Value);

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
readonly record struct Point(int X, int Y) {
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
}
