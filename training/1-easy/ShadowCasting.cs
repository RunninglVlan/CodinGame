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
    readonly List<List<char>> shadows = [];

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
                AddShadow(new Point(x + 1, y + 1), SHADOW_DARKER);
                AddShadow(new Point(x + 2, y + 2), SHADOW_LIGHTER);
            }
        }
    }

    void AddShadow(Point point, char c) {
        while (point.Y >= shadows.Count) {
            shadows.Add([]);
        }
        var line = shadows[point.Y];
        while (point.X >= line.Count) {
            line.Add(EMPTY);
        }
        line[point.X] = c;
    }

    public void Print() {
        for (var y = 0; y < Math.Max(lines.Count, shadows.Count); y++) {
            int width = y < lines.Count ? Math.Max(lines[y].Count, shadows[y].Count) : shadows[y].Count;
            for (var x = 0; x < width; x++) {
                bool foundChar = TryGet(x, y, lines, out char lineChar);
                bool foundShadow = TryGet(x, y, shadows, out char shadowChar);
                if (!foundChar && !foundShadow) {
                    continue;
                }
                Console.Write(lineChar != EMPTY ? lineChar : shadowChar);
            }
            Console.WriteLine();
        }
        return;

        bool TryGet(int x, int y, List<List<char>> charLines, out char c) {
            c = EMPTY;
            if (y >= charLines.Count) {
                return false;
            }
            var line = charLines[y];
            if (x >= line.Count) {
                return false;
            }
            c = line[x];
            return true;
        }
    }
}

readonly record struct Point(int X, int Y);
