const string testCase = """
                        8
                        Winning Move
                        X
                        XOX
                        OOX
                        ...
                        XOX
                        OOX
                        ..X
                        Blocking the Win
                        O
                        ..X
                        XXO
                        .O.
                        ..X
                        XXO
                        OO.
                        Decisive Move
                        X
                        .O.
                        XO.
                        .X.
                        .O.
                        XO.
                        XX.
                        Drawn
                        O
                        XXO
                        OOX
                        XOX
                        XXO
                        OOX
                        XOX
                        Optimal Win
                        X
                        ...
                        ..X
                        O..
                        ...
                        ..X
                        O.X
                        Winning Position
                        X
                        X..
                        ...
                        ..O
                        X.X
                        ...
                        ..O
                        Lost
                        O
                        ..X
                        O.X
                        O.X
                        ..X
                        O.X
                        O.X
                        Hold the Draw
                        O
                        .O.
                        ..X
                        .X.
                        .O.
                        ..X
                        OX.
                        """;
Console.SetIn(new StringReader(testCase));

// https://www.codingame.com/training/medium/tic-tac-toe-engine
var tests = int.Parse(Console.ReadLine()!);
for (var i = 0; i < tests; i++) {
    Debug.Log();
    Debug.ReadLine();
    var board = new Board();
    board.Read();
    board.PlayBestMove();
    board.Compare();
    // board.Print();
}

class Board {
    const int SIZE = 3;

    static readonly Point[][] LINES = [
        // rows
        [Screen.ZERO, Screen.RIGHT, Screen.RIGHT * 2],
        [Screen.DOWN, Screen.ONE, Screen.RIGHT * 2 + Screen.DOWN],
        [Screen.DOWN * 2, Screen.DOWN * 2 + Screen.RIGHT, Screen.ONE * 2],
        // columns
        [Screen.ZERO, Screen.DOWN, Screen.DOWN * 2],
        [Screen.RIGHT, Screen.ONE, Screen.DOWN * 2 + Screen.RIGHT],
        [Screen.RIGHT * 2, Screen.RIGHT * 2 + Screen.DOWN, Screen.ONE * 2],
        // diagonals
        [Screen.ZERO, Screen.ONE, Screen.ONE * 2],
        [Screen.RIGHT * 2, Screen.ONE, Screen.DOWN * 2]
    ];

    /// <code>
    /// 8 | 4 | 7 
    ///---+---+---
    /// 3 | 9 | 2 
    ///---+---+---
    /// 6 | 1 | 5 </code>
    static readonly Point[] SQUARE_PRIORITIES = [
        Screen.ONE,
        Screen.ZERO,
        Screen.RIGHT * 2,
        Screen.DOWN * 2,
        Screen.DOWN * 2 + Screen.RIGHT * 2,
        Screen.RIGHT,
        Screen.DOWN,
        Screen.DOWN + Screen.RIGHT * 2,
        Screen.DOWN * 2 + Screen.RIGHT
    ];

    char player, enemy;
    readonly char[,] squares = new char[SIZE, SIZE];

    public void Read() {
        player = Console.ReadLine()!.First();
        enemy = player == Square.PLAYER_O ? Square.PLAYER_X : Square.PLAYER_O;
        for (var y = 0; y < SIZE; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < SIZE; x++) {
                squares[x, y] = line[x];
            }
        }
    }

    char Get(Point point) => squares[point.X, point.Y];
    void Set(Point point, char square) => squares[point.X, point.Y] = square;

    public void PlayBestMove() {
        if (GameOver() is not null) {
            return;
        }
        var games = SimulateGames(new GameState([])).ToArray();
        var gamesWithChances = games
                .GroupBy(it => it.First)
                .Select(firstGroup => {
                    var result = new Dictionary<GameResult, (GameState[] states, float chance)>();
                    var resultGroups = firstGroup.GroupBy(it => it.Result).ToArray();
                    foreach (var resultGroup in resultGroups) {
                        float chance = resultGroup.Count() / (float)resultGroups.Select(it => it.Count()).Sum();
                        result.Add(resultGroup.Key!.Value, ([.. resultGroup], chance));
                    }
                    return result;
                }).ToArray();
        var winningGames = WinningGames(gamesWithChances);
        if (winningGames.Length == 0) {
            Debug.Log("bestLosingMove");
            var bestLosingMove = gamesWithChances
                    .SelectMany(it => it)
                    .OrderByDescending(it => it.Value.chance)
                    .SelectMany(it => it.Value.states)
                    .OrderByDescending(it => it.Turns)
                    .Select(it => it.First)
                    .First();
            Set(bestLosingMove, player);
            return;
        }
        Debug.Log("bestWinningMove");
        var firstGroups = winningGames
                .SelectMany(it => it)
                .SelectMany(it => it.Value.states)
                .GroupBy(it => it.First)
                .ToArray();
        foreach (var square in SQUARE_PRIORITIES) {
            var filtered = firstGroups.Where(group => group.Key == square).ToArray();
            if (filtered.Length == 0) {
                continue;
            }
            var firstGroup = filtered.First();
            if (false) {
                var resultGroups = firstGroup.GroupBy(it => it.Result).ToDictionary(it => it.Key, it => it);
                Debug.Log($"First: {firstGroup.Key}, Count: {firstGroup.Count()}", 1, ConsoleColor.Green);
                foreach (var resultGroup in resultGroups.OrderBy(it => it.Key)) {
                    float chance = resultGroup.Value.Count()
                            / (float)resultGroups.Select(it => it.Value.Count()).Sum();
                    Debug.Log($"Result: {resultGroup.Key}, Count: {resultGroup.Value.Count()}, chance: {chance}", 2,
                            ConsoleColor.Blue);
                    foreach (var turnGroup in resultGroup.Value.GroupBy(it => it.Turns)) {
                        Debug.Log($"Turns: {turnGroup.Key}, Count: {turnGroup.Count()}", 3, ConsoleColor.Cyan);
                    }
                }
            }
            Set(firstGroup.Key, player);
            return;
        }
    }

    static Dictionary<GameResult, (GameState[] states, float chance)>[] WinningGames(
            Dictionary<GameResult, (GameState[] states, float chance)>[] games) {
        var result = games
                .Where(it => it.Count == 1 && it.ContainsKey(GameResult.PlayerWon))
                .ToArray();
        if (result.Length > 0) {
            return result;
        }
        result = games
                .Where(it => it.TryGetValue(GameResult.PlayerWon, out var won) && won.chance >= 0.5f)
                .ToArray();
        if (result.Length > 0) {
            return result;
        }
        result = games
                .Where(it => it.Count == 1 && it.ContainsKey(GameResult.Draw))
                .ToArray();
        return result.Length > 0 ? result : games;
    }

    void PrintGame(GameState state) {
        var turns = 0;
        foreach ((char playerSquare, var point) in state.Moves) {
            Set(point, playerSquare);
            LogBoard(turns, Color(turns));
            turns++;
        }
        foreach (var (_, point) in state.Moves) {
            Set(point, Square.EMPTY);
        }
    }

    /// Backtracking
    IEnumerable<GameState> SimulateGames(GameState state) {
        foreach (var playerMove in NextMoves(player, enemy)) {
            Set(playerMove, player);
            var gameResult = GameOver();
            var nextWithPlayer = new GameState([.. state.Moves, (player, playerMove)]);
            if (gameResult is not null) {
                Set(playerMove, Square.EMPTY);
                yield return nextWithPlayer with { Result = gameResult.Value };
                continue;
            }
            foreach (var enemyMove in NextMoves(enemy, player)) {
                Set(enemyMove, enemy);
                gameResult = GameOver();
                var nextWithEnemy = new GameState([.. nextWithPlayer.Moves, (enemy, enemyMove)]);
                if (gameResult is not null) {
                    Set(enemyMove, Square.EMPTY);
                    yield return nextWithEnemy with { Result = gameResult.Value };
                    continue;
                }
                foreach (var nextMoves in SimulateGames(nextWithEnemy)) {
                    yield return nextMoves;
                }
                Set(enemyMove, Square.EMPTY);
            }
            Set(playerMove, Square.EMPTY);
        }
    }

    GameResult? GameOver() {
        if (Squares(PlayerWon).Any()) {
            return GameResult.PlayerWon;
        }
        if (Squares(EnemyWon).Any()) {
            return GameResult.EnemyWon;
        }
        if (!Squares(IsEmpty).Any()) {
            return GameResult.Draw;
        }
        return null;

        bool PlayerWon(Point point) => HasLineWithNumberOfSquares(point, player, 3);
        bool EnemyWon(Point point) => HasLineWithNumberOfSquares(point, enemy, 3);
    }

    IEnumerable<Point> NextMoves(char playerSquare, char enemySquare) {
        Point[] winningMoves = [.. Squares(it => CanWin(it, playerSquare))];
        if (winningMoves.Length > 0) {
            foreach (var move in winningMoves) {
                yield return move;
            }
            yield break;
        }
        Point[] blockingMoves = [.. Squares(it => CanWin(it, enemySquare))];
        if (blockingMoves.Length > 0) {
            foreach (var move in blockingMoves) {
                yield return move;
            }
            yield break;
        }
        Point[] nextWinningMoves = [.. Squares(it => CanWinNext(it, playerSquare, enemySquare))];
        if (nextWinningMoves.Length > 0) {
            foreach (var move in nextWinningMoves) {
                yield return move;
            }
            yield break;
        }
        Point[] nextBlockingMoves = [.. Squares(it => CanWinNext(it, enemySquare, playerSquare))];
        if (nextBlockingMoves.Length > 0) {
            foreach (var nextMove in nextBlockingMoves) {
                Set(nextMove, enemySquare);
                blockingMoves = [.. Squares(it => CanWin(it, enemySquare))];
                Set(nextMove, Square.EMPTY);
                if (blockingMoves.Length > 0) {
                    foreach (var move in blockingMoves) {
                        yield return move;
                    }
                    yield break;
                }
            }
        }
        foreach (var move in Squares(IsEmpty)) {
            yield return move;
        }
    }

    bool IsEmpty(Point point) => Get(point) == Square.EMPTY;
    bool CanWin(Point point, char playerSquare) => IsEmpty(point) && HasLineWithNumberOfSquares(point, playerSquare, 2);

    bool CanWinNext(Point point, char playerSquare, char enemySquare) {
        if (!IsEmpty(point)) {
            return false;
        }
        var nextLines = LINES.Where(line => {
            if (!line.Contains(point)) {
                return false;
            }
            char[] lineSquares = [.. line.Select(it => squares[it.X, it.Y])];
            return lineSquares.Contains(playerSquare) && !lineSquares.Contains(enemySquare);
        });
        return nextLines.Count() >= 2;
    }

    static IEnumerable<Point> Squares(Predicate<Point> predicate) {
        for (var y = 0; y < SIZE; y++) {
            for (var x = 0; x < SIZE; x++) {
                var point = new Point(x, y);
                if (predicate(point)) {
                    yield return point;
                }
            }
        }
    }

    bool HasLineWithNumberOfSquares(Point point, char playerSquare, int number) {
        return LINES.Where(it => it.Contains(point))
                .Select(line => line.Count(it => Get(it) == playerSquare))
                .Any(count => count == number);
    }

    public void Print() {
        foreach (int y in Enumerable.Range(0, SIZE)) {
            var line = Enumerable.Range(0, SIZE).Select(x => squares[x, y]);
            Console.WriteLine(string.Concat(line));
        }
    }

    static ConsoleColor Color(int turns) => turns switch {
        0 => ConsoleColor.Red,
        1 => ConsoleColor.Green,
        2 => ConsoleColor.Blue,
        3 => ConsoleColor.Cyan,
        4 => ConsoleColor.Magenta,
        _ => ConsoleColor.Yellow
    };

    void LogBoard(int turns = 0, ConsoleColor? color = null) {
        foreach (int y in Enumerable.Range(0, SIZE)) {
            var line = Enumerable.Range(0, SIZE).Select(x => squares[x, y]);
            Debug.Log($"{string.Concat(line)}", turns, color);
        }
    }

    public void Compare() {
        foreach (int y in Enumerable.Range(0, SIZE)) {
            string? expected = Console.ReadLine();
            var line = Enumerable.Range(0, SIZE).Select(x => squares[x, y]);
            string actual = string.Concat(line);
            Console.WriteLine(expected == actual ? $"✅ {actual} found, and expected {expected}" : $"❌ {actual} found, but expected {expected}");
        }
    }
}

static class Square {
    public const char EMPTY = '.';
    public const char PLAYER_X = 'X';
    public const char PLAYER_O = 'O';
}

readonly record struct Point(int X, int Y) {
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}

readonly record struct GameState(List<(char square, Point point)> Moves, GameResult? Result = null) {
    public int Turns => Moves.Count;
    public Point First => Moves.First().point;

    public override string ToString() => $"Moves: {string.Join(", ", Moves)}, Result: {Result}";
}

enum GameResult {
    PlayerWon,
    EnemyWon,
    Draw
}

static class Screen {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point RIGHT = new(1, 0);
}

static class Debug {
    const int TAB_LENGTH = 2;

    /// Reads line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
