using System;
using System.Linq;
using System.Text;

// https://www.codingame.com/contribute/view/152541078689bfa05bb36d9956b0461f4c5e61
var maze = new Maze(7);
maze.Read();
char pushableTile = Console.ReadLine()!.First();
maze.FindPath(pushableTile);
Console.WriteLine(maze);

class Maze(int size) {
    readonly char[,] cells = new char[size, size];

    public void Read() {
        for (var y = 0; y < size; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < size; x++) {
                cells[x, y] = row[x];
            }
        }
    }

    public void FindPath(char pushableTile) { }

    public override string ToString() {
        var rows = new StringBuilder();
        for (var y = 0; y < size; y++) {
            for (var x = 0; x < size; x++) {
                rows.Append(cells[x, y]);
            }
            if (y < size - 1) {
                rows.AppendLine();
            }
        }
        return rows.ToString();
    }
}
