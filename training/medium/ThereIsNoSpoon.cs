using System;

namespace ThereIsNoSpoon;

/// Don't let the machines win. You are humanity's last hope...<br/>
/// https://www.codingame.com/training/medium/there-is-no-spoon-episode-1
static class ThereIsNoSpoon {
    static void Main() {
        var grid = new Grid();
        grid.Init();
        grid.PrintNodeNeighbors();
    }
}

class Grid {
    int width, height;
    bool[] powerCells = null!;

    public void Init() {
        width = int.Parse(Console.ReadLine()!);
        height = int.Parse(Console.ReadLine()!);
        powerCells = new bool[width * height];
        for (var y = 0; y < height; y++) {
            string characters = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char c = characters[x];
                // A dot . represents an empty cell
                // A zero 0 represents a cell containing a node
                Set(x, y, c == '0');
            }
        }
    }

    void Set(int x, int y, bool cell) => powerCells[width * y + x] = cell;
    bool Get(int x, int y) => powerCells[width * y + x];

    public void PrintNodeNeighbors() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                bool isPowerNode = Get(x, y);
                if (!isPowerNode) {
                    continue;
                }
                (int rightX, int rightY) = RightNode(x + 1, y);
                (int bottomX, int bottomY) = BottomNode(x, y + 1);
                Console.WriteLine($"{x} {y} {rightX} {rightY} {bottomX} {bottomY}");
            }
        }
    }

    (int x, int y) RightNode(int startX, int y) {
        for (int x = startX; x < width; x++) {
            bool isPowerNode = Get(x, y);
            if (isPowerNode) {
                return (x, y);
            }
        }
        return (-1, -1);
    }

    (int x, int y) BottomNode(int x, int startY) {
        for (int y = startY; y < height; y++) {
            bool isPowerNode = Get(x, y);
            if (isPowerNode) {
                return (x, y);
            }
        }
        return (-1, -1);
    }
}
