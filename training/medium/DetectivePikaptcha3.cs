using System;

namespace DetectivePikaptcha3;

/// https://www.codingame.com/training/medium/detective-pikaptcha-ep3
static class DetectivePikaptcha3 {
    static void Main() {
        var game = new Game();
        var sideToFollow = Console.ReadLine()! == "R" ? Side.Right : Side.Left;
        game.MoveThroughMaze(sideToFollow);
        game.PrintMaze();
    }
}

class Game {
    readonly Grid maze = new();
    readonly Player pikaptcha;

    public Game() => maze.Init(out pikaptcha);

    public void MoveThroughMaze(Side sideToFollow) {
        var start = pikaptcha.Cell.point;
        while (pikaptcha.Cell.point != start || pikaptcha.Cell.stepped <= 0) {
            if (!IsWallOnThe(sideToFollow)) {
                pikaptcha.Turn(sideToFollow);
            }
            Cell next;
            var originalDirection = pikaptcha.Direction;
            while (!TryMoveForward(out next)) {
                var oppositeSide = sideToFollow == Side.Right ? Side.Left : Side.Right;
                pikaptcha.Turn(oppositeSide);
                if (pikaptcha.Direction != originalDirection) {
                    continue;
                }
                Console.Error.WriteLine("Trapped - turned all the way around and found nowhere to go!");
                return;
            }
            pikaptcha.Move(next);
        }
    }

    bool IsWallOnThe(Side side) {
        var wallDirection = Screen.Turn(pikaptcha.Direction, side);
        var wallToCheck = pikaptcha.Cell.point + wallDirection;
        return !maze.TryGet(wallToCheck, out var cell) || cell.type == CellType.Wall;
    }

    bool TryMoveForward(out Cell nextCell) {
        var nextPoint = pikaptcha.Cell.point + pikaptcha.Direction;
        return maze.TryGet(nextPoint, out nextCell) && nextCell.type != CellType.Wall;
    }

    public void PrintMaze() => maze.Print();
}

class Grid {
    int width, height;
    Cell[,] cells = null!;

    public bool TryGet(Point point, out Cell cell) {
        cell = null!;
        if (point.X < 0 || point.X >= width || point.Y < 0 || point.Y >= height) {
            return false;
        }
        cell = cells[point.X, point.Y];
        return true;
    }

    public void Init(out Player pikaptcha) {
        pikaptcha = null!;
        string[] dimensions = Console.ReadLine()!.Split(' ');
        width = Math.Clamp(int.Parse(dimensions[0]), 1, 200);
        height = Math.Clamp(int.Parse(dimensions[1]), 1, 200);
        cells = new Cell[width, height];
        for (var y = 0; y < height; y++) {
            string characters = Console.ReadLine()!;
            Console.Error.WriteLine(characters);
            for (var x = 0; x < width; x++) {
                char c = characters[x];
                var cell = new Cell(x, y);
                cells[x, y] = cell;
                cell.type = c == '#' ? CellType.Wall : CellType.Passage;
                Point? direction = c switch {
                    '^' => Screen.UP,
                    'v' => Screen.DOWN,
                    '<' => Screen.LEFT,
                    '>' => Screen.RIGHT,
                    '0' or '#' => null,
                    _ => throw new ArgumentException($"Unexpected cell type: {c}")
                };
                if (direction != null) {
                    pikaptcha = new(cell, direction.Value);
                }
            }
        }
    }

    public void Print() {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                var cell = cells[x, y];
                Console.Write(cell.type == CellType.Wall ? "#" : cell.stepped);
            }
            Console.WriteLine();
        }
    }
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
    public int stepped;
}

enum CellType {
    Passage,
    Wall
}

class Player(Cell cell, Point direction) {
    public Cell Cell { get; private set; } = cell;
    public Point Direction { get; private set; } = direction;

    public void Turn(Side side) => Direction = Screen.Turn(Direction, side);

    public void Move(Cell cell) {
        Cell = cell;
        cell.stepped++;
    }
}

enum Side {
    Right,
    Left
}

readonly record struct Point(int X, int Y) {
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"({X}, {Y})";
}

static class Screen {
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point Turn(Point direction, Side side) {
        if (direction == UP) {
            return side == Side.Right ? RIGHT : LEFT;
        }
        if (direction == DOWN) {
            return side == Side.Right ? LEFT : RIGHT;
        }
        if (direction == LEFT) {
            return side == Side.Right ? UP : DOWN;
        }
        if (direction == RIGHT) {
            return side == Side.Right ? DOWN : UP;
        }
        throw new ArgumentException($"Unknown screen direction: {direction}");
    }
}
