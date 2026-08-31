using System;

// Compute how many points you scored with your new words!
// https://www.codingame.com/training/easy/scrabble-score
string[] inputs;
int tiles = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 30);
for (var i = 0; i < tiles; i++) {
    inputs = Console.ReadLine()!.Split(' ');
    string character = inputs[0];
    int score = int.Parse(inputs[1]);
}
inputs = Console.ReadLine()!.Split(' ');
int width = Math.Clamp(int.Parse(inputs[0]), 4, 15);
int height = Math.Clamp(int.Parse(inputs[1]), 4, 15);
// empty board with special cells
for (var i = 0; i < height; i++) {
    string emptyBoardRow = Console.ReadLine()!;
}
// words already played
for (var i = 0; i < height; i++) {
    string previousWordBoardRow = Console.ReadLine()!;
}
// words after you play
for (var i = 0; i < height; i++) {
    string playedWordBoardRow = Console.ReadLine()!;
}
Console.WriteLine("OF 5");
Console.WriteLine("Total 5");
