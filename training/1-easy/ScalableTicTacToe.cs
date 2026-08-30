using System;

// https://www.codingame.com/training/easy/scaleable-tictactoe
string[] inputs = Console.ReadLine()!.Split(' ');
int size = Math.Clamp(int.Parse(inputs[0]), 3, 12);
int winningCells = Math.Clamp(int.Parse(inputs[1]), 3, size);
for (var i = 0; i < size; i++) {
    string row = Console.ReadLine()!;
}
Console.WriteLine("Answer");
