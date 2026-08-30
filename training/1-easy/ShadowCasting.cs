using System;
using System.Collections.Generic;

// https://www.codingame.com/training/easy/shadow-casting
var ascii = new Ascii();
ascii.Read();
ascii.Print();

class Ascii {
    readonly List<List<char>> lines = [];

    public void Read() {
        int lineCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
        for (var _ = 0; _ < lineCount; _++) {
            lines.Add([.. Console.ReadLine()!]);
        }
    }

    public void Print() => lines.ForEach(line => {
        line.ForEach(Console.Write);
        Console.WriteLine();
    });
}
