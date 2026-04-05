using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MadPodRacing;

/// https://www.codingame.com/multiplayer/bot-programming/mad-pod-racing
static class MadPodRacing {
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
        }
        // ReSharper disable once FunctionNeverReturns
    }
}

class Game {
    const int PLAYERS = 2;
    const int RACER = 0;
    const int CHECKPOINT_RADIUS = 600;
    static readonly (int none, int max) THRUST = (0, 100);

    int turn = 1;
    readonly Pod[] players = new Pod[PLAYERS], opponents = new Pod[PLAYERS];
    Point[] checkpoints = null!;
    Point boostCheckpoint;
    bool haveBoost = true;

    public Game() {
        for (var i = 0; i < PLAYERS; i++) {
            players[i] = new Pod();
            opponents[i] = new Pod();
        }
    }

    public void Init() {
        int laps = int.Parse(Console.ReadLine()!);
        // 2 ≤ checkpointCount ≤ 8
        int checkpointCount = int.Parse(Console.ReadLine()!);
        checkpoints = new Point[checkpointCount];
        var maxDistance = double.MinValue;
        for (var i = 0; i < checkpointCount; i++) {
            string[] point = Console.ReadLine()!.Split(' ');
            var checkpoint = Point.From(point[0], point[1]);
            checkpoints[i] = checkpoint;
            if (i > 0) {
                FindBoostCheckpoint(checkpoints[i - 1], checkpoint);
            }
        }
        FindBoostCheckpoint(checkpoints[checkpointCount - 1], checkpoints[0]);
        return;

        void FindBoostCheckpoint(Point previous, Point current) {
            double distance = Vector.Distance(previous, current);
            if (distance <= maxDistance) {
                return;
            }
            // use boost on longest straight
            maxDistance = distance;
            boostCheckpoint = current;
        }
    }

    public void Loop() {
        ReadInput(out var leadingOpponent);
        for (var i = 0; i < players.Length; i++) {
            var player = players[i];
            string control = i == RACER
                    ? RacerControl(player)
                    : InterceptorControl(player, leadingOpponent);
            Console.WriteLine(control);
        }
        turn++;
    }

    void ReadInput(out Pod leadingOpponent) {
        for (var i = 0; i < PLAYERS; i++) {
            UpdatePod(players[i]);
        }
        (Pod? pod, double distance) best = (null, double.MaxValue);
        for (var i = 0; i < PLAYERS; i++) {
            var opponent = opponents[i];
            UpdatePod(opponent);
            double distance = Vector.Distance(opponent.Point, opponent.Next);
            if (best.pod == null
                    || opponent.Progress > best.pod.Progress
                    || opponent.Progress == best.pod.Progress && distance < best.distance) {
                best = (opponent, distance);
            }
        }
        leadingOpponent = best.pod!;
        return;

        void UpdatePod(Pod pod) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            pod.Point = Point.From(inputs[0], inputs[1]);
            pod.Velocity = Vector.From(inputs[2], inputs[3]);
            pod.Angle = int.Parse(inputs[4]);
            int nextCheckPoint = int.Parse(inputs[5]);
            pod.Next = checkpoints[nextCheckPoint];
        }
    }

    string RacerControl(Pod player) {
        var control = $"{player.Target.X} {player.Target.Y} ";
        if (player.Speed > 300 && turn - player.ShieldTurn > 3 && HasCollisionRisk(player)) {
            control += Ability.SHIELD;
            player.ShieldTurn = turn;
        } else if (haveBoost && player.Target == boostCheckpoint && Math.Abs(player.AngleToTarget) < 5) {
            control += Ability.BOOST;
            haveBoost = false;
        } else {
            control += Thrust(player);
        }
        return control;
    }

    string InterceptorControl(Pod player, Pod opponent) {
        var ambush = AmbushPoint(player, opponent);
        bool movingToAmbush = false, waiting = false;
        if (opponent.Next == ambush.point) {
            player.Target = opponent.Point + opponent.Velocity;
        } else if (ambush.distance > CHECKPOINT_RADIUS) {
            player.Target = ambush.point;
            movingToAmbush = true;
        } else {
            player.Target = opponent.Point;
            waiting = true;
        }
        var control = $"{player.Target.X} {player.Target.Y} ";
        if (turn - player.ShieldTurn > 3 && HasCollisionRisk(player)) {
            control += Ability.SHIELD;
            player.ShieldTurn = turn;
        } else if (waiting) {
            control += THRUST.none;
        } else {
            control += Thrust(player, movingToAmbush);
        }
        return control;
    }

    (Point point, double distance) AmbushPoint(Pod player, Pod opponent) {
        var current = opponent.Next;
        int currentI = Array.IndexOf(checkpoints, opponent.Next);
        var nearest = checkpoints
                .Select(it => (point: it, distance: Vector.Distance(it, player.Point)))
                .OrderBy(it => it.distance)
                .First();
        if (nearest.point == current) {
            return nearest;
        }
        double opponentDistance = Vector.Distance(opponent.Point, current);
        for (var i = 0; i < checkpoints.Length; i++, currentI++) {
            int iNext = (currentI + 1) % checkpoints.Length;
            var next = checkpoints[iNext];
            double playerDistance = Vector.Distance(player.Point, next);
            if (playerDistance < opponentDistance) {
                return (next, playerDistance);
            }
            opponentDistance += Vector.Distance(current, next);
            current = next;
        }
        return nearest;
    }

    bool HasCollisionRisk(Pod player) {
        foreach (var opponent in opponents) {
            var (pNew, _, _) = Next(player.Point, player.Velocity, player.Angle, player.Target, THRUST.max);
            var (oNew, _, _) = Next(opponent.Point, opponent.Velocity, opponent.Angle, opponent.Target, THRUST.max);
            if (Vector.Distance(pNew, oNew) < Pod.DIAMETER) {
                return true;
            }
        }
        return false;
    }

    int Thrust(Pod player, bool stopAtTarget = false) {
        if (turn == 1) {
            return THRUST.max;
        }
        if (Math.Abs(player.AngleToTarget) > 90) {
            return THRUST.none;
        }
        (double score, int thrust) best = (int.MaxValue, THRUST.max);
        for (int thrust = THRUST.none; thrust <= THRUST.max; thrust += 10) {
            var (p, v) = (player.Point, player.Velocity);
            (p, v, int? turns) = SimulateNext(p, v, player.Angle, player.Target, thrust);
            double score = Score(p, v, player.Target, turns, stopAtTarget);
            if (score >= best.score) {
                continue;
            }
            best = (score, thrust);
        }
        return best.thrust;
    }

    static (Vector, Vector, int?) SimulateNext(Vector p, Vector v, double angle, Vector target, int thrust) {
        const int maxTurns = 20;
        for (var turns = 1; turns < maxTurns; turns++) {
            (p, v, angle) = Next(p, v, angle, target, thrust);
            if (Vector.Distance(p, target) < CHECKPOINT_RADIUS) {
                return (p, v, turns);
            }
        }
        return (p, v, null);
    }

    static (Vector, Vector, double) Next(Vector p, Vector v, double angle, Vector target, int thrust) {
        const int turnAngle = 18;
        var direction = target - p;
        double targetAngle = Vector.Angle(direction);
        double angleToNext = Vector.Angle(angle, targetAngle);
        angle += Math.Clamp(angleToNext, -turnAngle, turnAngle);
        double radians = angle * Math.PI / 180;
        v += new Vector(thrust * Math.Cos(radians), thrust * Math.Sin(radians));
        p += v;
        v *= Pod.FRICTION;
        v.X = (int)v.X;
        v.Y = (int)v.Y;
        p.X = Math.Round(p.X);
        p.Y = Math.Round(p.Y);
        return (p, v, Math.Round(angle));
    }

    double Score(Vector pNew, Vector vNew, Point target, int? turnsToHit, bool stopAtTarget) {
        if (turnsToHit == null) {
            return 1_000_000 + Vector.Distance(pNew, target);
        }
        // prioritizing thrust that leads to reaching checkpoint in less turns
        double score = turnsToHit.Value * 1_000;
        if (stopAtTarget) {
            return score + vNew.Magnitude() * 20;
        }
        // prioritizing thrust that shortens distance to next checkpoint
        int i = Array.IndexOf(checkpoints, target);
        if (i == -1) {
            return score + Vector.Distance(pNew, target);
        }
        int iNext = (i + 1) % checkpoints.Length;
        var nextCheckpoint = checkpoints[iNext];
        return score + Vector.Distance(pNew, nextCheckpoint);
    }
}

// 0 ≤ x < 16000, 0 ≤ y < 9000
readonly record struct Point(int X, int Y) {
    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public static implicit operator Vector(Point value) => new(value.X, value.Y);

    public override string ToString() => $"({X}, {Y})";
}

record struct Vector(double X, double Y) {
    public static Vector From(string x, string y) {
        return new Vector(int.Parse(x), int.Parse(y));
    }

    public double Magnitude() => Math.Sqrt(X * X + Y * Y);

    public static double Distance(Vector a, Vector b) {
        double diffX = a.X - b.X;
        double diffY = a.Y - b.Y;
        return Math.Sqrt(diffX * diffX + diffY * diffY);
    }

    public static double Angle(Vector value) {
        double angle = Math.Atan2(value.Y, value.X) * (180 / Math.PI);
        return angle < 0 ? angle + 360 : angle;
    }

    public static double Angle(Vector from, Vector to) {
        double fromAngle = Math.Atan2(from.Y, from.X) * (180 / Math.PI);
        double toAngle = Math.Atan2(to.Y, to.X) * (180 / Math.PI);
        return Angle(fromAngle, toAngle);
    }

    public static double Angle(double fromAngle, double toAngle) {
        double delta = toAngle - fromAngle;
        if (delta <= -180) {
            delta += 360;
        }
        if (delta > 180) {
            delta -= 360;
        }
        return delta;
    }

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector operator *(Vector a, double value) => new(a.X * value, a.Y * value);
    public static Vector operator /(Vector a, double value) => new(a.X / value, a.Y / value);

    public static implicit operator Point(Vector value) => new((int)value.X, (int)value.Y);

    public override string ToString() => $"({X:F0}, {Y:F0})";
}

[SuppressMessage("ReSharper", "ReplaceWithFieldKeyword")]
class Pod {
    public const double FRICTION = 0.85;
    public const int DIAMETER = 800;

    Vector velocity;
    Point next = new(-1, -1), target;

    public Point Point { get; set; }

    public Vector Velocity {
        get => velocity;
        set {
            velocity = value;
            Speed = velocity.Magnitude();
        }
    }

    /// Angle is absolute. 0° means facing EAST while 90° means facing SOUTH.
    public int Angle { get; set; }

    public Point Next {
        get => next;
        set {
            if (next != value) {
                next = value;
                Progress++;
            }
            Target = next;
        }
    }

    public Point Target {
        get => target;
        set {
            target = value;
            var direction = target - Point;
            double toAngle = Vector.Angle(direction);
            AngleToTarget = Vector.Angle(Angle, toAngle);
        }
    }

    public double Speed { get; private set; }
    public double AngleToTarget { get; private set; }
    public int Progress { get; private set; }
    public int ShieldTurn { get; set; } = -3;

    public override string ToString() {
        var direction = Target - Point;
        double vAngle = Vector.Angle(Velocity, direction);
        double distance = Vector.Distance(Point, Target);
        return $"p: {Point}, v: {Velocity}, a: {Angle}, vA: {vAngle:F0}, aToT: {AngleToTarget:F0}, d: {distance:F0}";
    }
}

static class Ability {
    public const string BOOST = "BOOST";
    public const string SHIELD = "SHIELD";
}
