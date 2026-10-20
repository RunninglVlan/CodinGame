using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

Debug.Enabled = true;
// This is pacman, test names probably mean maze types and difficulty levels (e.g., how fast ghosts start moving)
// todo: character count can really be different from 5, so account for that too - e.g., use last index, not hardcoded 5
// todo: i should get more points when i do that
// todo: maximize points - explore all cells
// todo: - i guess test what else this pacman has (like can it eat ghosts), check other pacman strategies
// todo: i still don't understand how i get points
// todo: - maybe it's not even about exploration, maybe i just need to maximize turns instead
// todo: - if so, then when there are portals, just move back and forth
// todo: does game really end when player is caught?
// todo: check if more cell types are shown on the radar
// https://www.codingame.com/multiplayer/optimization/codingame-sponsored-contest
var game = new Game();
game.Init();
while (true) {
    game.ReadTurn();
    game.LogMaze();
    Console.WriteLine(game.Action());
}

class Game {
    const int PLAYER = 4;

    Maze maze = null!;
    int turn;
    readonly WalkableDirection[] walkableDirections = new WalkableDirection[4];
    Point[] characters = null!;
    readonly HashSet<Point> explored = [];
    Point previous;

    public void Init() {
        int height = int.Parse(Console.ReadLine()!);
        int width = int.Parse(Console.ReadLine()!);
        maze = new Maze(width + 2, height + 2);
        int characterCount = int.Parse(Console.ReadLine()!);
        characters = new Point[characterCount];
        Debug.Log($"{width}x{height}, {characterCount}");
    }

    public void ReadTurn() {
        string gridViewUp = Console.ReadLine()!;
        string gridViewRight = Console.ReadLine()!;
        string gridViewDown = Console.ReadLine()!;
        string gridViewLeft = Console.ReadLine()!;
        walkableDirections[0] = new WalkableDirection(Screen.UP, gridViewUp != Cells.WALL);
        walkableDirections[1] = new WalkableDirection(Screen.RIGHT, gridViewRight != Cells.WALL);
        walkableDirections[2] = new WalkableDirection(Screen.DOWN, gridViewDown != Cells.WALL);
        walkableDirections[3] = new WalkableDirection(Screen.LEFT, gridViewLeft != Cells.WALL);
        for (var i = 0; i < characters.Length; i++) {
            string[] coordinates = Console.ReadLine()!.Split(' ');
            int x = int.Parse(coordinates[0]) + 1;
            int y = int.Parse(coordinates[1]) + 1;
            var cell = new Point(x, y);
            characters[i] = cell;
            maze.SetWalkable(x, y);
            if (i == PLAYER) {
                SetPlayer(cell);
            }
        }
        if (turn++ > 0) {
            Debug.Log();
        }
        foreach ((var direction, bool walkable) in walkableDirections) {
            var player = characters[PLAYER];
            var neighbor = player + direction;
            maze.SetWalkable(neighbor.X, neighbor.Y, walkable);
        }
    }

    void SetPlayer(Point cell) {
        explored.Add(cell);
        // player can teleport to the other side
        if (cell.X == 1 && previous.X == maze.Width - 2 || cell.X == maze.Width - 2 && previous.X == 1) {
            explored.Add(cell with { X = 0 });
            explored.Add(cell with { X = maze.Width - 1 });
        } else if (cell.Y == 1 && previous.Y == maze.Height - 2 || cell.Y == maze.Height - 2 && previous.Y == 1) {
            explored.Add(cell with { Y = 0 });
            explored.Add(cell with { Y = maze.Height - 1 });
        }
        previous = cell;
    }

    public void LogMaze() {
        Debug.Log(maze.ToString(characters, explored));
    }

    public char Action() {
        var closestGhost = characters
                .Where((_, i) => i != PLAYER)
                .Select(it => (point: it, distance: Point.ManhattanDistance(characters[PLAYER], it)))
                .OrderBy(it => it.distance)
                .Select(it => it.point)
                .First();
        return walkableDirections
                .Where(it => it.Walkable)
                .Select(it => {
                    var next = characters[PLAYER] + it.Direction;
                    int nextDistance = Point.ManhattanDistance(next, closestGhost);
                    return (next, it.Direction, nextDistance);
                })
                .OrderByDescending(it => !explored.Contains(it.next) ? 1 : 0)
                .ThenByDescending(it => it.nextDistance)
                .Select(it => Screen.ToChar(it.Direction))
                .FirstOrDefault();
    }
}

record Maze(int Width, int Height) {
    readonly bool?[,] walkableCells = new bool?[Width, Height];

    public void SetWalkable(int x, int y, bool value = true) {
        walkableCells[x, y] = value;
    }

    public string ToString(Point[] characters, HashSet<Point> explored) {
        var lines = new StringBuilder();
        for (var y = 0; y < Height; y++) {
            for (var x = 0; x < Width; x++) {
                bool? cell = walkableCells[x, y];
                string value = cell == null ? Cells.UNKNOWN : cell.Value ? Cells.EMPTY : Cells.WALL;
                if (value == Cells.EMPTY) {
                    value = Empty(x, y, value);
                }
                lines.Append(value);
            }
            lines.AppendLine();
        }
        return lines.ToString();

        string Empty(int x, int y, string value) {
            var point = new Point(x, y);
            for (var i = 0; i < characters.Length; i++) {
                if (point == characters[i]) {
                    return (i + 1).ToString();
                }
            }
            return !explored.Contains(point) ? Cells.EMPTY_UNEXPLORED : value;
        }
    }
}

static class Cells {
    public const string WALL = "#";
    public const string EMPTY = " ";
    public const string EMPTY_UNEXPLORED = "*";
    public const string UNKNOWN = ":";
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
