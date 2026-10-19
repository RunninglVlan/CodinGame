using System;
using System.Linq;

Debug.Enabled = true;
// https://www.codingame.com/multiplayer/optimization/codingame-sponsored-contest
var game = new Game();
game.Init();
while (true) {
    game.ReadTurn();
    Console.WriteLine(game.Action());
}

class Game {
    const string WALL = "#";
    const int ENEMY = 0, PLAYER = 4;

    int turn;
    readonly WalkableDirection[] walkableDirections = new WalkableDirection[4];
    Point[] characters = null!;

    public void Init() {
        int height = int.Parse(Console.ReadLine()!);
        int width = int.Parse(Console.ReadLine()!);
        int characterCount = int.Parse(Console.ReadLine()!);
        characters = new Point[characterCount];
        Debug.Log($"{width}x{height}, {characterCount}");
    }

    public void ReadTurn() {
        string gridViewUp = Console.ReadLine()!;
        string gridViewRight = Console.ReadLine()!;
        string gridViewDown = Console.ReadLine()!;
        string gridViewLeft = Console.ReadLine()!;
        walkableDirections[0] = new WalkableDirection(Screen.UP, gridViewUp != WALL);
        walkableDirections[1] = new WalkableDirection(Screen.RIGHT, gridViewRight != WALL);
        walkableDirections[2] = new WalkableDirection(Screen.DOWN, gridViewDown != WALL);
        walkableDirections[3] = new WalkableDirection(Screen.LEFT, gridViewLeft != WALL);
        for (var i = 0; i < characters.Length; i++) {
            string[] coordinates = Console.ReadLine()!.Split(' ');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);
            characters[i] = new Point(x, y);
        }
        if (turn++ > 0) {
            Debug.Log();
        }
        Debug.Log(gridViewUp);
        Debug.Log(gridViewRight);
        Debug.Log(gridViewDown);
        Debug.Log(gridViewLeft);
        foreach (var character in characters) {
            Debug.Log(character);
        }
    }

    public char Action() => walkableDirections
            .Where(it => it.Walkable)
            .Select(it => {
                var next = characters[PLAYER] + it.Direction;
                int distanceToEnemy = Point.ManhattanDistance(next, characters[ENEMY]);
                return (it.Direction, distanceToEnemy);
            })
            .OrderByDescending(it => it.distanceToEnemy)
            .Select(it => Screen.ToChar(it.Direction))
            .FirstOrDefault();
}

record struct WalkableDirection(Point Direction, bool Walkable);

readonly record struct Point(int X, int Y) {
    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"{X} {Y}";
}

static class Screen {
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static char ToChar(Point direction) => direction switch {
        _ when direction == UP => 'C',
        _ when direction == DOWN => 'D',
        _ when direction == LEFT => 'E',
        _ when direction == RIGHT => 'A',
        _ => 'B'
    };
}

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; }

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        if (!Enabled) {
            return;
        }
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
