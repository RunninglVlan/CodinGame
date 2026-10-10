using System;

// Navigate through a facility to reach the extraction point by coordinating robot actions across multiple time loops
// https://www.codingame.com/contribute/view/1415851ba1aa5d4b0600a53ac02dd35417fd8b
string[] inputs = Console.ReadLine()!.Split(' ');
int width = Math.Clamp(int.Parse(inputs[0]), 3, 30);
int height = Math.Clamp(int.Parse(inputs[1]), 3, 30);
var facility = new Facility(width, height);
// maximum number of ghosts available
int maxLoops = Math.Clamp(int.Parse(inputs[2]), 1, 5);
facility.ReadGrid();
facility.ReachExit();
facility.PrintMoves();

class Facility(int width, int height) {
    readonly Cell[,] grid = new Cell[width, height];

    public void ReadGrid() {
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                char symbol = row[x];
                grid[x, y] = symbol switch {
                    'S' => new Cell(CellType.Start, symbol),
                    'E' => new Cell(CellType.Exit, symbol),
                    '#' => new Cell(CellType.Wall, symbol),
                    '.' => new Cell(CellType.Floor, symbol),
                    _ when char.IsLower(symbol) => new Cell(CellType.Switch, symbol),
                    _ => new Cell(CellType.Door, symbol)
                };
            }
        }
    }

    public void ReachExit() { }

    public void PrintMoves() {
        Console.WriteLine("U R W..."); // D | L | R | U | W
    }
}

readonly record struct Cell(CellType Type, char Symbol);

enum CellType {
    Start,
    Exit,
    Wall,
    Floor,
    Switch,
    Door
}
