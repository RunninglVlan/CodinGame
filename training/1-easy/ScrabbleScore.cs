using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// Compute how many points you scored with your new words!
// https://www.codingame.com/training/easy/scrabble-score
var scrabble = new Scrabble();
scrabble.ReadTileScores();
scrabble.ReadBoard();
var wordScores = scrabble.ComputeScore();
foreach (var score in wordScores) {
    Console.WriteLine($"{score.Word} {score.Value}");
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

    public List<Score> ComputeScore() {
        List<Score> scores = [];
        foreach (var point in newTiles.Where(point => !scores.Any(it => it.Points.Contains(point)))) {
            scores.Add(ScoreAt(point));
        }
        return scores;
    }

    Score ScoreAt(Point point) {
        var (start, direction) = FindWordStart(point);
        var score = new Score();
        AddTile(score, start);
        var current = start;
        while (true) {
            var next = current + direction;
            if (!next.InBounds(width, height)) {
                break;
            }
            var nextCell = board[next.X, next.Y];
            if (nextCell.Tile == null) {
                break;
            }
            current = next;
            AddTile(score, current);
        }
        return score;
    }

    (Point point, Point direction) FindWordStart(Point point) {
        foreach (var direction in Screen.Directions4) {
            var neighbor = point + direction;
            if (!neighbor.InBounds(width, height)) {
                continue;
            }
            if (board[neighbor.X, neighbor.Y].Tile == null) {
                continue;
            }
            if (direction == Screen.RIGHT || direction == Screen.DOWN) {
                return (point, direction);
            }
            var current = neighbor;
            while (true) {
                var next = current + direction;
                if (!next.InBounds(width, height)) {
                    return (current, direction * -1);
                }
                var nextCell = board[next.X, next.Y];
                if (nextCell.Tile == null) {
                    return (current, direction * -1);
                }
                current = next;
            }
        }
        throw new UnreachableException();
    }

    void AddTile(Score score, Point point) {
        var cell = board[point.X, point.Y];
        int multiplier = cell.Type switch {
            CellType.DoubleLetter => 2,
            CellType.TripleLetter => 3,
            _ => 1
        };
        score.Word += cell.Tile;
        score.Value += tileScores[cell.Tile!.Value] * multiplier;
        score.Points.Add(point);
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

class Score {
    public string Word { get; set; } = string.Empty;
    public int Value { get; set; }
    public List<Point> Points { get; } = [];
}

readonly record struct Point(int X, int Y) {
    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
}

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
static class Screen {
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [LEFT, UP, RIGHT, DOWN];
}
