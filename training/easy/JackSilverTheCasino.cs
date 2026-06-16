using System;

namespace JackSilverTheCasino;

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
        string[] splitPlay = value.Split(' ');
        string call = splitPlay[1];
        int number = call == Call.PLAIN
                ? Math.Clamp(int.Parse(splitPlay[2]), 0, MAX_NUMBER)
                : -1;
        // roulette table result
        int ball = Math.Clamp(int.Parse(splitPlay[0]), 0, MAX_NUMBER);
        return (new Call(call, number), ball);
    }

    public void ProcessRound(Call call, int ball) {
        int bet = Cash / 4;
        if (Cash % 4 != 0) {
            bet++;
        }
        Cash -= bet;
        Cash += Payout(ball, call, bet);
    }

    static int Payout(int ball, Call call, int bet) {
        bool isEven = ball % 2 == 0;
        if (call.Value == Call.EVEN && isEven && ball != 0
                || call.Value == Call.ODD && !isEven) {
            return bet * 2;
        }
        if (ball == call.Number) {
            return bet * 36;
        }
        return 0;
    }
}

record struct Call(string Value, int Number) {
    public const string EVEN = "EVEN";
    public const string ODD = "ODD";
    public const string PLAIN = "PLAIN";
}
