using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/join-the-dots
string[] inputs = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(inputs[0]), 3, 40);
int width = Math.Clamp(int.Parse(inputs[1]), 3, 40);
var board = new Board(width, height);
board.Read();
board.JoinDots();
board.Print();

class Board(int width, int height) {
    const string DOT_CHARS = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    readonly Dictionary<int, Point> dots = [];
    readonly Dictionary<Point, char> lines = [];

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

    public void JoinDots() {
        for (var i = 0; i < DOT_CHARS.Length; i++) {
            if (i + 1 == dots.Count) {
                break;
            }
            var current = dots[i];
            var next = dots[i + 1];
            if (current.Y == next.Y) {
                for (int x = Math.Min(current.X, next.X) + 1; x < Math.Max(current.X, next.X); x++) {
                    lines[current with { X = x }] = '-';
                }
            } else if (current.X == next.X) {
                for (int y = Math.Min(current.Y, next.Y) + 1; y < Math.Max(current.Y, next.Y); y++) {
                    lines[current with { Y = y }] = '|';
                }
            }
        }
    }

    public void Print() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                var point = new Point(x, y);
                char c = lines.TryGetValue(point, out char lineChar)
                        ? lineChar
                        : dots.ContainsValue(point) ? 'o' : ' ';
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }
}

readonly record struct Point(int X, int Y);
