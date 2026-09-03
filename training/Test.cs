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
        player = Debug.ReadLine()!.First();
        enemy = player == Square.PLAYER_O ? Square.PLAYER_X : Square.PLAYER_O;
        for (var y = 0; y < SIZE; y++) {
            string line = Debug.ReadLine()!;
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
        if (false) {
            var games = SimulateGames(new GameState([])).ToArray();
            (float win, float lose) chances = (Chances(games, GameResult.PlayerWon), Chances(games, GameResult.EnemyWon));
            Debug.Log($"initial win: {chances.win}, lose: {chances.lose}");
            // i know original winning chances
            // let's try any next move and compare
            (Point? move, int minWin, int maxLose, float deltaWin, float deltaLose) best = (null, 0, 0, 0, 0);
            foreach (var playerMove in Squares(IsEmpty)) {
                var nextGames = SimulateGames(new GameState([]), playerMove).ToArray();
                if (nextGames.Length == 0) {
                    continue;
                }
                (float win, float lose) nextChances = (Chances(nextGames, GameResult.PlayerWon), Chances(nextGames, GameResult.EnemyWon));
                float deltaWin = nextChances.win - chances.win;
                float deltaLose = nextChances.lose - chances.lose;
                var wonGames = nextGames.Where(it => it.Result == GameResult.PlayerWon).ToArray();
                int minWin = wonGames.Length > 0 ? wonGames.Min(it => it.Turns) : int.MaxValue;
                var lostGames = nextGames.Where(it => it.Result == GameResult.EnemyWon).ToArray();
                int maxLose = lostGames.Length > 0 ? lostGames.Max(it => it.Turns) : int.MinValue;
                Debug.Log($"next for {playerMove} minWin: {minWin}, win: {nextChances.win} ({deltaWin}), lose: {nextChances.lose} ({deltaLose}), maxLose: {maxLose}", 1, ConsoleColor.Green);
                foreach (var turnGroup in wonGames.GroupBy(it => it.Turns)) {
                    Debug.Log($"won Turns: {turnGroup.Key}, Count: {turnGroup.Count()}", 3, ConsoleColor.Cyan);
                    // if (turnGroup.Key <= 1) {
                    //     continue;
                    // }
                    // foreach (var movesGroup in turnGroup.GroupBy(it => (it.Moves[0], it.Moves[1]))) {
                    //     Debug.Log($"Moves: {movesGroup.Key}, Count: {movesGroup.Count()}", 4, ConsoleColor.Magenta);
                    // }
                    // if (resultGroup.Key != GameResult.PlayerWon) {
                    //     continue;
                    // }
                    // foreach (var gameState in turnGroup) {
                    //     PrintGame(gameState);
                    // }
                }
                foreach (var turnGroup in lostGames.GroupBy(it => it.Turns)) {
                    Debug.Log($"lost Turns: {turnGroup.Key}, Count: {turnGroup.Count()}", 3, ConsoleColor.Cyan);
                }
                if (best.move == null
                        || deltaWin > best.deltaWin && nextChances.win > nextChances.lose
                        || deltaWin > 0 && minWin < best.minWin
                        // || deltaLose < best.deltaLose
                        || Mathf.Approximately(deltaLose, best.deltaLose) && maxLose > best.maxLose) {
                    Debug.Log($"deltaWin > best.deltaWin && nextChances.win > nextChances.lose: {deltaWin > best.deltaWin && nextChances.win > nextChances.lose}", 2, deltaWin > best.deltaWin && nextChances.win > nextChances.lose ? ConsoleColor.Red : ConsoleColor.Blue);
                    Debug.Log($"deltaWin > 0 && minWin < best.minWin: {deltaWin > 0 && minWin < best.minWin}", 2, deltaWin > 0 && minWin < best.minWin ? ConsoleColor.Red : ConsoleColor.Blue);
                    // Debug.Log($"deltaLose < best.deltaLose: {deltaLose < best.deltaLose}", 2, deltaLose < best.deltaLose ? ConsoleColor.Red : ConsoleColor.Blue);
                    Debug.Log($"Mathf.Approximately(deltaLose, best.deltaLose) && maxLose > best.maxLose: {Mathf.Approximately(deltaLose, best.deltaLose) && maxLose > best.maxLose}", 2, Mathf.Approximately(deltaLose, best.deltaLose) && maxLose > best.maxLose ? ConsoleColor.Red : ConsoleColor.Blue);
                    best = (playerMove, minWin, maxLose, deltaWin, deltaLose);
                    Debug.Log($"next best: {best}", 2, ConsoleColor.Blue);
                }
            }
            Set(best.move!.Value, player);
            return;

            float Chances(GameState[] states, GameResult result) {
                var all = states.GroupBy(it => it.Result).ToArray();
                return all
                        .Where(it => it.Key == result)
                        .Select(won => won.Count() / (float)all/*.Where(it => it.Key != GameResult.Draw)*/.Select(it => it.Count()).Sum())
                        .FirstOrDefault();
            }
        }
        {
            var games = SimulateGames(new GameState([])).ToArray();
            Debug.Log($"Total games: {games.Length}");
            var firstGroups = games.GroupBy(it => it.First).ToArray();
            foreach (var square in SQUARE_PRIORITIES) {
                var filtered = firstGroups.Where(group => group.Key == square).ToArray();
                if (filtered.Length == 0) {
                    continue;
                }
                var firstGroup = filtered.First();
                // if (firstGroup.Key != Screen.ONE && firstGroup.Key != Screen.ONE * 2) {
                //     continue;
                // }
                var resultGroups = firstGroup.GroupBy(it => it.Result).ToDictionary(it => it.Key, it => it);
                Debug.Log($"First: {firstGroup.Key}, Count: {firstGroup.Count()}", 1, ConsoleColor.Green);
                foreach (var resultGroup in resultGroups.OrderBy(it => it.Key)) {
                    float chance = resultGroup.Value.Count() / (float)resultGroups.Select(it => it.Value.Count()).Sum();
                    Debug.Log($"Result: {resultGroup.Key}, Count: {resultGroup.Value.Count()}, chance: {chance}", 2, ConsoleColor.Blue);
                    foreach (var turnGroup in resultGroup.Value.GroupBy(it => it.Turns)) {
                        Debug.Log($"Turns: {turnGroup.Key}, Count: {turnGroup.Count()}", 3, ConsoleColor.Cyan);
                        // if (turnGroup.Key <= 1) {
                        //     continue;
                        // }
                        // foreach (var movesGroup in turnGroup.GroupBy(it => (it.Moves[0], it.Moves[1]))) {
                        //     Debug.Log($"Moves: {movesGroup.Key}, Count: {movesGroup.Count()}", 4, ConsoleColor.Magenta);
                        // }
                        // if (resultGroup.Key != GameResult.PlayerWon) {
                        //     continue;
                        // }
                        // foreach (var gameState in turnGroup) {
                        //     PrintGame(gameState);
                        // }
                    }
                }
            }
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
            var winningGames = gamesWithChances
                    .Where(it => {
                        return it.ContainsKey(GameResult.PlayerWon);
                        float wonChance = it.GetValueOrDefault(GameResult.PlayerWon).chance;
                        float lostChance = it.GetValueOrDefault(GameResult.EnemyWon).chance;
                        return wonChance >= lostChance;
                    })
                    .ToArray();
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
            var bestWinningMove = winningGames
                    .SelectMany(it => it)
                    .OrderByDescending(it => it.Value.chance)
                    .SelectMany(it => it.Value.states)
                    .OrderBy(it => it.Turns)
                    .Select(it => it.First)
                    .First();
            Set(bestWinningMove, player);
        }
        return;
        {
            List<Point>[] moves = [.. FindWinningMoves([])];
            if (moves.Length == 0) {
                var blockingMoves = Squares(EnemyCanWin).ToArray();
                if (blockingMoves.Length > 0) {
                    Set(blockingMoves.First(), player);
                    return;
                }
                moves = [.. FindDrawMoves([])];
                foreach (var pointse in moves.GroupBy(it => it.Count)) {
                    Debug.Log($"Draw moves: {pointse.Key}");
                    foreach (var grouping in pointse.GroupBy(it => it.First())) {
                        Set(grouping.Key, player);
                        LogBoard();
                        Set(grouping.Key, Square.EMPTY);
                        Debug.Log();
                    }
                }
                return;
            }
            var movesWithLeastTurns = moves.GroupBy(it => it.Count).First().ToArray();
            foreach (var square in SQUARE_PRIORITIES) {
                if (!movesWithLeastTurns.Select(it => it.First()).Contains(square)) {
                    continue;
                }
                Set(square, player);
                return;
            }
            /*
            var movesWithLeastTurns = moves.GroupBy(it => it.Turns).First().ToArray();
            foreach (var square in SQUARE_PRIORITIES) {
                var filtered = movesWithLeastTurns.Where(it => it.First == square).ToArray();
                if (filtered.Length == 0 || filtered.Select(it => it.Result).Distinct().Count() > 1) {
                    continue;
                }
                // if (filtered.First().Result != GameResult.PlayerWon) {
                //     continue;
                // }
                // foreach (var moves1 in filtered) {
                //     Debug.Log(moves1, moves1.Turns, Color(moves1.Turns));
                // }
                // if (!movesWithLeastTurns.Select(it => it.First).Contains(square)) {
                //     continue;
                // }
                Set(square, player);
                return;
            }
            */
        }
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
    IEnumerable<List<Point>> FindWinningMoves(List<Point> path) {
        var winningMoves = Squares(PlayerCanWin).ToArray();
        if (winningMoves.Length > 0) {
            yield return [.. path, winningMoves.First()];
            yield break;
        }
        foreach (var playerMove in Squares(IsEmpty)) {
            Set(playerMove, player);
            if (Squares(EnemyCanWin).Any()) {
                Set(playerMove, Square.EMPTY);
                continue;
            }
            foreach (var enemyMove in Squares(PlayerCanWin)) {
                Set(enemyMove, enemy);
                foreach (var moves in FindWinningMoves([.. path, playerMove])) {
                    yield return moves;
                }
                Set(enemyMove, Square.EMPTY);
            }
            Set(playerMove, Square.EMPTY);
        }
    }

    /// Backtracking
    IEnumerable<GameState> SimulateGames(GameState state, Point? firstMove = null) {
        // i guess i need to improve strategies
        // foreach (var playerMove in Squares(it => CanPotentiallyWin(it, player, enemy) || CanPotentiallyWin(it, enemy, player))) { // win or block
        foreach (var playerMove in Squares(it => CanPotentiallyWin(it, player, enemy))) { // win
        // foreach (var playerMove in Squares(it => IsEmpty(it))) {
        // foreach (var playerMove in Squares(it => PlayerCanWin(it) || EnemyCanWin(it))) {
            if (firstMove != null && playerMove != firstMove) {
                continue;
            }
            Set(playerMove, player);
            var gameResult = GameOver();
            var nextWithPlayer = new GameState([.. state.Moves, (player, playerMove)]);
            if (gameResult is not null) {
                Set(playerMove, Square.EMPTY);
                yield return nextWithPlayer with { Result = gameResult.Value };
                continue;
            }
            // maybe something is missing here
            // foreach (var enemyMove in Squares(it => PlayerCanWin(it) || PlayerCanWinNext(it) || EnemyCanWin(it) || EnemyCanWinNext(it))) { // block or win
            // foreach (var enemyMove in Squares(it => PlayerCanWin(it) || EnemyCanWin(it))) { // block or win - limits moves
            // foreach (var enemyMove in Squares(it => CanPotentiallyWin(it, player, enemy) || CanPotentiallyWin(it, enemy, player))) { // block or win
            foreach (var enemyMove in Squares(it => PlayerCanWin(it) || EnemyCanWin(it))) { // block
            // foreach (var enemyMove in Squares(it => IsEmpty(it))) {
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
    }

    /// Backtracking
    IEnumerable<List<Point>> FindDrawMoves(List<Point> path) {
        if (!Squares(IsEmpty).Any()) {
            yield return path;
            yield break;
        }
        foreach (var playerMove in Squares(IsEmpty)) {
            Set(playerMove, player);
            // if all next moves end in draw, we got it
            if (Squares(EnemyCanWinNext).Any()) {
                Set(playerMove, Square.EMPTY);
                continue;
            }
            foreach (var enemyMove in Squares(IsEmpty)) {
                Set(enemyMove, enemy);
                foreach (var moves in FindDrawMoves([.. path, playerMove])) {
                    yield return moves;
                }
                Set(enemyMove, Square.EMPTY);
            }
            Set(playerMove, Square.EMPTY);
        }
    }

    bool IsEmpty(Point point) => Get(point) == Square.EMPTY;
    bool PlayerCanWin(Point point) => IsEmpty(point) && HasLineWithNumberOfSquares(point, player, 2);
    bool EnemyCanWin(Point point) => IsEmpty(point) && HasLineWithNumberOfSquares(point, enemy, 2);
    bool PlayerWon(Point point) => HasLineWithNumberOfSquares(point, player, 3);
    bool EnemyWon(Point point) => HasLineWithNumberOfSquares(point, enemy, 3);

    bool CanPotentiallyWin(Point point, char playerSquare, char enemySquare) {
        if (!IsEmpty(point)) {
            return false;
        }
        return LINES
                .Where(it => it.Contains(point))
                .Where(line => line.Any(it => squares[it.X, it.Y] == playerSquare))
                .Any(line => line.Any(it => squares[it.X, it.Y] != enemySquare));
    }

    bool PlayerCanWinNext(Point point) {
        if (!IsEmpty(point)) {
            return false;
        }
        var nextLines = LINES.Where(it => it.Contains(point))
                .Where(line => line.Any(it => squares[it.X, it.Y] == player))
                .Where(line => line.Any(it => squares[it.X, it.Y] == Square.EMPTY));
        return nextLines.Count() >= 2;
    }

    bool EnemyCanWinNext(Point point) {
        if (!IsEmpty(point)) {
            return false;
        }
        var nextLines = LINES.Where(it => it.Contains(point))
                .Where(line => line.Any(it => squares[it.X, it.Y] == enemy))
                .Where(line => line.Any(it => squares[it.X, it.Y] == Square.EMPTY));
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

    public static void ReadCorrect() {
        Debug.Log("Correct:");
        foreach (int _ in Enumerable.Range(0, SIZE)) {
            Debug.ReadLine();
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

static class Mathf {
    public static bool Approximately(double a, double b) => Math.Abs(a - b) < 0.001;
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
