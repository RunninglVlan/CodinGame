using System;
using System.Collections.Generic;

// https://www.codingame.com/training/easy/shadow-casting
var ascii = new Ascii();
ascii.Read();
ascii.AddShadows();
ascii.Print();

class Ascii {
    const char EMPTY = ' ';
    const char ASCII_CHAR = '#';
    const char SHADOW_DARKER = '-';
    const char SHADOW_LIGHTER = '`';

    readonly List<List<char>> lines = [];

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
                char c = line[x];
                if (c != ASCII_CHAR) {
                    continue;
                }
                char darker = Get(x + 1, y + 1);
                if (darker != ASCII_CHAR) {
                    Set(x + 1, y + 1, SHADOW_DARKER);
                }
                char lighter = Get(x + 2, y + 2);
                if (lighter != ASCII_CHAR) {
                    Set(x + 2, y + 2, SHADOW_LIGHTER);
                }
            }
        }
    }

    /// Also adds necessary chars when non-existing is requested
    char Get(int x, int y) {
        while (y >= lines.Count) {
            lines.Add([]);
        }
        var line = lines[y];
        while (x >= line.Count) {
            line.Add(EMPTY);
        }
        return lines[y][x];
    }

    void Set(int x, int y, char c) => lines[y][x] = c;

    public void Print() => lines.ForEach(line => {
        line.ForEach(Console.Write);
        Console.WriteLine();
    });
}
