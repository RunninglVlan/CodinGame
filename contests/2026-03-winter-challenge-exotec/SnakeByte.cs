using System;
using System.Text;
using System.Collections.Generic;

/// https://www.codingame.com/contests/winter-challenge-2026-exotec
static class SnakeByte {
    static readonly Grid grid = new();
    static readonly Dictionary<int, Snake> snakes = new();

    static void Main() {
        Init();

        while (true) {
            ClearPowerAndSnakes();
            UpdatePower();
            UpdateSnakes();
            Console.WriteLine(MoveSnakes());
        }
        // ReSharper disable once FunctionNeverReturns
    }

    static void ClearPowerAndSnakes() {
        grid.ClearPowerAndSnakes();
        foreach (var snake in snakes.Values) {
            snake.parts.Clear();
        }
    }

    static void Init() {
        // ReSharper disable once UnusedVariable
        int myId = int.Parse(Console.ReadLine()!);
        grid.Init();
        int snakesPerPlayer = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < snakesPerPlayer; i++) {
            int mySnakeId = int.Parse(Console.ReadLine()!);
            snakes.Add(mySnakeId, new Snake(grid, true));
        }
        for (var i = 0; i < snakesPerPlayer; i++) {
            int enemySnakeId = int.Parse(Console.ReadLine()!);
            snakes.Add(enemySnakeId, new Snake(grid, false));
        }
    }

    static void UpdatePower() {
        int powerSourceCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < powerSourceCount; i++) {
            string[] axes = Console.ReadLine()!.Split(' ');
            int x = int.Parse(axes[0]);
            int y = int.Parse(axes[1]);
            grid.Get(x, y).type = CellType.Power;
        }
    }

    static void UpdateSnakes() {
        int snakeCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < snakeCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int snakeId = int.Parse(inputs[0]);
            var snake = snakes[snakeId];
            string body = inputs[1];
            string[] parts = body.Split(':');
            foreach (string part in parts) {
                string[] axes = part.Split(',');
                int x = int.Parse(axes[0]);
                int y = int.Parse(axes[1]);
                snake.parts.Add(new Point(x, y));
                grid.Get(x, y).type = snake.isMine ? CellType.MySnake : CellType.EnemySnake;
            }
        }
    }

    static string MoveSnakes() {
        var result = new StringBuilder();
        foreach ((int id, var snake) in snakes) {
            if (snake is not { isMine: true, IsDead: false }) {
                continue;
            }
            result.Append($"{id} {snake.Move()};");
        }
        return result.ToString();
    }
}

class Grid {
    int width;
    int height;

    Cell[] cells = null!;
    public Cell Get(int x, int y) => cells[width * y + x];
    void Set(int x, int y, Cell cell) => cells[width * y + x] = cell;

    public void Init() {
        width = int.Parse(Console.ReadLine()!);
        height = int.Parse(Console.ReadLine()!);
        cells = new Cell[width * height];
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
            for (var x = 0; x < row.Length; x++) {
                char c = row[x];
                var cell = new Cell(x, y);
                Set(x, y, cell);
                cell.type = c == '#' ? CellType.Platform : CellType.Empty;
            }
        }
    }

    public Point? NearestPower(Point source) {
        (Point? point, float distance) nearest = (null, float.MaxValue);
        foreach (var cell in cells) {
            if (cell.type != CellType.Power) {
                continue;
            }
            float distance = Point.ManhattanDistance(source, cell.point);
            if (distance < nearest.distance) {
                nearest = (cell.point, distance);
            }
        }
        return nearest.point;
    }

    public void ClearPowerAndSnakes() {
        foreach (var cell in cells) {
            if (cell.type is not (CellType.MySnake or CellType.EnemySnake or CellType.Power)) {
                continue;
            }
            cell.type = CellType.Empty;
        }
    }
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
}

enum CellType {
    Platform,
    Empty,
    Power,
    MySnake,
    EnemySnake
}

class Snake(Grid grid, bool mine) {
    public readonly bool isMine = mine;

    public bool IsDead => parts.Count == 0;
    public readonly List<Point> parts = new();
    string? previousMove;

    public string Move() {
        var head = parts[0];
        var point = grid.NearestPower(head);
        if (point == null) {
            return Action.WAIT;
        }
        string move = MoveTo(head, point.Value);
        var nextCell = NextCell(head, move);
        if (nextCell.type == CellType.Platform) {
            move = Action.UP;
        }
        previousMove = move;
        return move;
    }

    string MoveTo(Point head, Point target) {
        if (head.x == target.x) {
            return Action.UP;
        }
        string move = target.x < head.x ? Action.LEFT : Action.RIGHT;
        // don't move backwards
        if ((move == Action.LEFT && previousMove == Action.RIGHT) ||
            (move == Action.RIGHT && previousMove == Action.LEFT)) {
            return Action.UP;
        }
        return move;
    }

    Cell NextCell(Point point, string move) {
        (int x, int y) = move switch {
            Action.LEFT => (point.x - 1, point.y),
            Action.RIGHT => (point.x + 1, point.y),
            Action.UP => (point.x, point.y - 1),
            _ => (point.x, point.y + 1)
        };
        return grid.Get(x, y);
    }
}

struct Point(int x, int y) {
    public readonly int x = x;
    public readonly int y = y;

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }
}

static class Action {
    public const string UP = "UP";
    public const string RIGHT = "RIGHT";
    public const string DOWN = "DOWN";
    public const string LEFT = "LEFT";
    public const string WAIT = "WAIT";
}
