using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace CodeVsZombies;

/// Save humans, destroy zombies!<br/>
/// https://www.codingame.com/multiplayer/optimization/code-vs-zombies
static class CodeVsZombies {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        var i = 0;
        while (true) {
            game.Loop();
            if (i++ == 0) {
                game.Save();
            }
        }
    }
}

public class Game {
    const int ASH_MOVE = 1000;
    const int GUN_RANGE = 2000;
    const int ZOMBIE_MOVE = 400;

    Point ash;
    Dictionary<int, Point> humans = new();
    Dictionary<int, Point> zombies = new();

    public void Loop() {
        ReadAsh();
        ReadHumans();
        ReadZombies();
        Console.WriteLine(ChooseTarget().target);
    }

    void ReadAsh() {
        string[] position = Console.ReadLine()!.Split(' ');
        ash = Point.From(position[0], position[1]);
    }

    void ReadHumans() {
        humans.Clear();
        int humanCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < humanCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int humanId = int.Parse(inputs[0]);
            var human = Point.From(inputs[1], inputs[2]);
            humans.Add(humanId, human);
        }
    }

    void ReadZombies() {
        zombies.Clear();
        int zombieCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < zombieCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int zombieId = int.Parse(inputs[0]);
            var zombie = Point.From(inputs[1], inputs[2]);
            var zombieNext = Point.From(inputs[3], inputs[4]);
            zombies.Add(zombieId, zombie);
        }
    }

    (Point target, int distance) ChooseTarget() {
        (var target, int distance) = CriticalSavableHuman();
        return target != null ? (target.Value, distance) : throw new ArgumentException("No target found");
    }

    (Point? target, int distance) CriticalSavableHuman() {
        (Point? human, int distance, int zombieTurns) critical = (null, int.MaxValue, int.MaxValue);
        foreach (var human in humans.Values) {
            var distance = (int)Point.Distance(human, ash);
            int ashTurns = (distance - GUN_RANGE) / ASH_MOVE;
            if (!IsSavable(human, ashTurns, out int zombieTurns)) {
                continue;
            }
            if (zombieTurns < critical.zombieTurns ||
                    zombieTurns == critical.zombieTurns && distance < critical.distance) {
                critical = (human, distance, zombieTurns);
            }
        }
        return (critical.human, critical.distance);
    }

    bool IsSavable(Point human, int ashTurns, out int zombieTurns) {
        zombieTurns = 0;
        (Point? zombie, int turns) closest = (null, int.MaxValue);
        foreach (var zombie in zombies.Values) {
            var distance = (int)Point.Distance(zombie, human);
            int turns = distance / ZOMBIE_MOVE;
            if (turns < ashTurns) {
                return false;
            }
            if (turns >= closest.turns) {
                continue;
            }
            closest = (zombie, turns);
        }
        zombieTurns = closest.turns;
        return true;
    }

    (Point? target, int distance) ClosestZombie() {
        (Point? zombie, int distance) closest = (null, int.MaxValue);
        foreach (var zombie in zombies.Values) {
            var distance = (int)Point.Distance(zombie, ash);
            if (distance >= closest.distance) {
                continue;
            }
            closest = (zombie, distance);
        }
        return closest;
    }

    #region Testing

    public void Save() {
        Console.Error.WriteLine(JsonSerializer.Serialize(new SaveData(ash, humans, zombies)));
    }

    public void Load(string json) {
        var data = JsonSerializer.Deserialize<SaveData>(json);
        ash = data.Ash;
        humans = data.Humans;
        zombies = data.Zombies;
    }

    public int Simulate() {
        var score = 0;
        while (zombies.Count > 0) {
            (var target, int distance) = ChooseTarget();
            MoveZombies();
            ash = MoveTo(ash, target, distance, ASH_MOVE);
            score += KillZombies();
            EatHumans();
            if (humans.Count == 0) {
                return 0;
            }
        }
        return score;
    }

    void MoveZombies() {
        foreach ((int id, var zombie) in zombies) {
            (var human, int distance) = ClosestHuman(zombie);
            zombies[id] = MoveTo(zombie, human, distance, ZOMBIE_MOVE);
        }
    }

    (Point human, int distance) ClosestHuman(Point zombie) {
        (Point human, int distance) closest = (ash, (int)Point.Distance(ash, zombie));
        foreach (var human in humans.Values) {
            var distance = (int)Point.Distance(human, zombie);
            if (distance < closest.distance) {
                closest = (human, distance);
            }
        }
        return closest;
    }

    static Point MoveTo(Point source, Point target, int distance, int step) {
        if (distance <= step) {
            return target;
        }
        var direction = (Vector)target - source;
        return source + direction.Normalized * step;
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    int KillZombies() {
        var score = 0;
        var zombieCount = 0;
        foreach (int id in zombies.Keys) {
            var zombie = zombies[id];
            var distance = (int)Point.Distance(zombie, ash);
            if (distance > GUN_RANGE) {
                continue;
            }
            zombies.Remove(id);
            score += humans.Count * humans.Count * 10 * Fibonacci(zombieCount++ + 2);
        }
        return score;
    }

    static int Fibonacci(int n) {
        return n < 2 ? n : Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    void EatHumans() {
        foreach (var zombie in zombies.Values) {
            foreach (int id in humans.Keys) {
                var human = humans[id];
                if (human == zombie) {
                    humans.Remove(id);
                }
            }
        }
    }

    readonly record struct SaveData(Point Ash, Dictionary<int, Point> Humans, Dictionary<int, Point> Zombies);

    #endregion
}

readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new(int.Parse(x), int.Parse(y));
    }

    public static double Distance(Point a, Point b) {
        int diffX = a.X - b.X;
        int diffY = a.Y - b.Y;
        return Math.Sqrt(diffX * diffX + diffY * diffY);
    }

    public static implicit operator Vector(Point value) => new(value.X, value.Y);

    public override string ToString() => $"{X} {Y}";
}

readonly record struct Vector(double X, double Y) {
    public Vector Normalized => this / Magnitude;

    double Magnitude => Math.Sqrt(X * X + Y * Y);

    public static implicit operator Point(Vector value) => new((int)Math.Floor(value.X), (int)Math.Floor(value.Y));

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector operator *(Vector a, double value) => new(a.X * value, a.Y * value);
    public static Vector operator /(Vector a, double value) => new(a.X / value, a.Y / value);
}
