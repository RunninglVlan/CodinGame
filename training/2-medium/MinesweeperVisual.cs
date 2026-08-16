using System;

// Visual
// https://www.codingame.com/training/medium/minesweeper-1
while (true) {
    for (var i = 0; i < 16; i++) {
        string[] inputs = Console.ReadLine()!.Split(' ');
        for (var j = 0; j < 30; j++) {
            string cell = inputs[j]; // '?' if unknown, '.' if no mines nearby, '1'-'8' otherwise
        }
    }
    Console.WriteLine("20 7");
}
