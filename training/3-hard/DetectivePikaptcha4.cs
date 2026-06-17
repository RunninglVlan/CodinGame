using System;

namespace DetectivePikaptcha4;

/// https://www.codingame.com/training/hard/detective-pikaptcha-ep4
static class DetectivePikaptcha4 {
    static void Main() {
        var game = new Game();
        game.Init();
        var sideToFollow = Console.ReadLine()! == "R" ? Side.Right : Side.Left;
        game.MoveThroughMaze(sideToFollow);
        game.PrintMaze();
    }
}

class Game {
    readonly Cube maze = new();
    Player pikaptcha = null!;

    public void Init() => maze.Init(out pikaptcha);

    public void MoveThroughMaze(Side sideToFollow) {
        var start = pikaptcha.Cell.Point;
        while (pikaptcha.Cell.Point != start || pikaptcha.Cell.Stepped <= 0) {
            if (!IsWallOn(sideToFollow)) {
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

    bool IsWallOn(Side side) {
        var wallDirection = Screen.Turn(pikaptcha.Direction, side);
        var wallToCheck = pikaptcha.Cell.Point + wallDirection;
        return maze.Get(pikaptcha.Cell.Face, wallToCheck).Type == CellType.Wall;
    }

    bool TryMoveForward(out Cell nextCell) {
        var nextPoint = pikaptcha.Cell.Point + pikaptcha.Direction;
        nextCell = maze.Get(pikaptcha.Cell.Face, nextPoint);
        return nextCell.Type != CellType.Wall;
    }

    public void PrintMaze() => maze.Print();
}

class Cube {
    const int FACES = 6;

    int size;
    Cell[][,] faces = null!;

    public void Init(out Player pikaptcha) {
        pikaptcha = null!;
        size = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
        faces = new Cell[FACES][,];
        for (var f = 0; f < FACES; f++) {
            faces[f] = new Cell[size, size];
            for (var y = 0; y < size; y++) {
                string characters = Console.ReadLine()!;
                Console.Error.WriteLine(characters);
                for (var x = 0; x < size; x++) {
                    char c = characters[x];
                    var type = c == '#' ? CellType.Wall : CellType.Passage;
                    var cell = new Cell(f, new Point(x, y), type);
                    faces[f][x, y] = cell;
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
    }

    public Cell Get(int f, Point point) {
        if (point.X >= 0 && point.X < size && point.Y >= 0 && point.Y < size) {
            return faces[f][point.X, point.Y];
        }
        return f switch {
            0 when point.X < 0 => faces[1][point.Y, 0],
            0 when point.X >= size => faces[3][size - 1 - point.Y, 0],
            0 when point.Y < 0 => faces[5][point.X, size - 1],
            0 when point.Y >= size => faces[2][point.X, 0],

            1 when point.X < 0 => faces[5][size - 1 - point.Y, 0],
            1 when point.X >= size => faces[2][0, point.Y],
            1 when point.Y < 0 => faces[0][0, point.X],
            1 when point.Y >= size => faces[4][0, size - 1 - point.X],

            2 when point.X < 0 => faces[1][size - 1, point.Y],
            2 when point.X >= size => faces[2][0, point.Y],
            2 when point.Y < 0 => faces[0][point.X, size - 1],
            2 when point.Y >= size => faces[4][point.X, 0],

            3 when point.X < 0 => faces[2][size - 1, point.Y],
            3 when point.X >= size => faces[5][size - 1, size - 1 - point.Y],
            3 when point.Y < 0 => faces[0][size - 1, size - 1 - point.X],
            3 when point.Y >= size => faces[4][size - 1, point.X],

            4 when point.X < 0 => faces[1][size - 1 - point.Y, size - 1],
            4 when point.X >= size => faces[3][point.Y, size - 1],
            4 when point.Y < 0 => faces[2][point.X, size - 1],
            4 when point.Y >= size => faces[5][point.X, 0],

            5 when point.X < 0 => faces[1][0, size - 1 - point.Y],
            5 when point.X >= size => faces[3][size - 1, size - 1 - point.Y],
            5 when point.Y < 0 => faces[4][point.X, size - 1],
            5 when point.Y >= size => faces[0][point.X, 0],
            _ => throw new ArgumentException($"Unexpected face: {f} and point: {point}")
        };
    }

    public void Print() {
        for (var f = 0; f < FACES; f++) {
            var face = faces[f];
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    var cell = face[x, y];
                    Console.Write(cell.Type == CellType.Wall ? "#" : cell.Stepped);
                }
                Console.WriteLine();
            }
        }
    }
}

record Cell(int Face, Point Point, CellType Type) {
    public int Stepped { get; set; }
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
        cell.Stepped++;
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
        return side == Side.Right
                ? new Point(-direction.Y, direction.X)
                : new Point(direction.Y, -direction.X);
    }
}
