using System;
using System.Linq;

// https://www.codingame.com/training/medium/tic-tac-toe-engine
var board = new Board();
board.Read();
board.Print();

class Board {
    const int SIZE = 3;

    char player;
    readonly char[,] squares = new char[SIZE, SIZE];

    public void Read() {
        player = Console.ReadLine()!.First();
        for (var y = 0; y < SIZE; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                squares[x, y] = line[x];
            }
        }
    }

    public void Print() {
        for (var y = 0; y < SIZE; y++) {
            for (var x = 0; x < SIZE; x++) {
                Console.Write(squares[x, y]);
            }
            Console.WriteLine();
        }
    }
}
