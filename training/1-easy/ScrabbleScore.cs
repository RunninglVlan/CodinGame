using System;
using System.Collections.Generic;
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
Console.WriteLine($"Total {wordScores.Sum(it => it.score)}");

class Scrabble {
    const char EMPTY = '.';

    readonly Dictionary<char, int> tileScores = [];
    Cell[,] board = null!;

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
        for (var y = 0; y < board.GetLength(1); y++) {
            string previousWordBoardRow = Console.ReadLine()!;
            for (var x = 0; x < board.GetLength(0); x++) {
                char c = previousWordBoardRow[x];
                if (c == EMPTY) {
                    continue;
                }
                board[x, y].TileAlreadyPlayed = c;
            }
        }
        for (var y = 0; y < board.GetLength(1); y++) {
            string playedWordBoardRow = Console.ReadLine()!;
            for (var x = 0; x < board.GetLength(0); x++) {
                char c = playedWordBoardRow[x];
                if (c == EMPTY) {
                    continue;
                }
                board[x, y].TileAfterPlay = c;
            }
        }
    }

    void InitBoard() {
        string[] dimensions = Console.ReadLine()!.Split(' ');
        int width = Math.Clamp(int.Parse(dimensions[0]), 4, 15);
        int height = Math.Clamp(int.Parse(dimensions[1]), 4, 15);
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

    public List<(string word, int score)> ComputeScore() {
        return [("OF", 5)];
    }
}

record Cell(CellType Type) {
    public char? TileAlreadyPlayed { get; set; }
    public char? TileAfterPlay { get; set; }
}

enum CellType {
    Normal,
    DoubleLetter,
    TripleLetter,
    DoubleWord,
    TripleWord
}
