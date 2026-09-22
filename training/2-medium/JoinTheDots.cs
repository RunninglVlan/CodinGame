using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/join-the-dots
string[] inputs = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(inputs[0]), 3, 40);
int width = Math.Clamp(int.Parse(inputs[1]), 3, 40);
var board = new Board(width, height);
board.Read();
board.Print();

class Board(int width, int height) {
    const string DOT_CHARS = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    readonly Dictionary<int, Point> dots = [];

    public void Read() {
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char c = row[x];
                int index = DOT_CHARS.IndexOf(c);
                if (index == -1) {
                    continue;
                }
                dots[index] = new Point(x, y);
            }
        }
    }

    public void Print() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                Console.Write(dots.ContainsValue(new Point(x, y)) ? "o" : " ");
            }
            Console.WriteLine();
        }
    }
}

readonly record struct Point(int X, int Y);
