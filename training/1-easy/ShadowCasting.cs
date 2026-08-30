using System;
using System.Collections.Generic;

// https://www.codingame.com/training/easy/shadow-casting
var ascii = new Ascii();
ascii.Read();
ascii.AddShadows();
ascii.Print();

class Ascii {
    const char EMPTY = ' ';
    const char SHADOW_DARKER = '-';
    const char SHADOW_LIGHTER = '`';

    readonly List<List<char>> lines = [];
    readonly List<List<char>> linesFinal = [];

    public void Read() {
        int lineCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
        for (var _ = 0; _ < lineCount; _++) {
            lines.Add([.. Console.ReadLine()!]);
        }
    }

    public void AddShadows() {
        for (var y = 0; y < lines.Count; y++) {
            var line = lines[y];
            for (var x = 0; x < line.Count; x++) {
                if (line[x] == EMPTY) {
                    continue;
                }
                SetFinal(x + 1, y + 1, SHADOW_DARKER);
                SetFinal(x + 2, y + 2, SHADOW_LIGHTER);
            }
        }
        for (var y = 0; y < lines.Count; y++) {
            var line = lines[y];
            for (var x = 0; x < line.Count; x++) {
                char c = line[x];
                if (c != EMPTY) {
                    SetFinal(x, y, c);
                }
            }
        }
    }

    void SetFinal(int x, int y, char c) {
        while (y >= linesFinal.Count) {
            linesFinal.Add([]);
        }
        var line = linesFinal[y];
        while (x >= line.Count) {
            line.Add(EMPTY);
        }
        line[x] = c;
    }

    public void Print() => linesFinal.ForEach(line => {
        line.ForEach(Console.Write);
        Console.WriteLine();
    });
}
