using System;

/// https://www.codingame.com/training/easy/jack-silver-the-casino
static class JackSilverTheCasino {
    static void Main() {
        var game = new Game(
                int.Parse(Console.ReadLine()!),
                int.Parse(Console.ReadLine()!));
        for (var i = 0; i < game.Rounds; i++) {
            (var call, int ball) = Game.ParsePlay(Console.ReadLine()!);
            game.ProcessRound(call, ball);
        }
        Console.WriteLine(game.Cash);
    }
}

class Game(int rounds, int cash) {
    const int MAX_NUMBER = 36;

    public int Rounds { get; } = Math.Clamp(rounds, 1, 100);
    public int Cash { get; private set; } = Math.Clamp(cash, 50_000, 100_000);

    public static (Call call, int ball) ParsePlay(string value) {
        (string call, int number, int ball) = value.Split(' ') switch {
            [var b, Call.PLAIN, var n] => (Call.PLAIN, int.Parse(n), int.Parse(b)),
            [var b, var c] => (c, -1, int.Parse(b)),
            _ => throw new ArgumentException($"Unrecognized casino play format: {value}")
        };
        // roulette table result
        ball = Math.Clamp(ball, 0, MAX_NUMBER);
        return (new Call(call, Math.Clamp(number, -1, MAX_NUMBER)), ball);
    }

    public void ProcessRound(Call call, int ball) {
        // round up if bet is fractional
        var bet = (int)Math.Ceiling(Cash / 4.0);
        Cash -= bet;
        Cash += Payout(ball, call, bet);
    }

    static int Payout(int ball, Call call, int bet) {
        bool isEven = ball % 2 == 0;
        return (call.Value, isEven) switch {
            (Call.EVEN, true) when ball != 0 => bet * 2,
            (Call.ODD, false) => bet * 2,
            (Call.PLAIN, _) when ball == call.Number => bet * 36,
            _ => 0
        };
    }
}

record struct Call(string Value, int Number) {
    public const string EVEN = "EVEN";
    public const string ODD = "ODD";
    public const string PLAIN = "PLAIN";
}
