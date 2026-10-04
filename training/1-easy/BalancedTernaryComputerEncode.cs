using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/easy/balanced-ternary-computer-encode
static class BalancedTernaryComputerEncode {
    static void Main() {
        int n = int.Parse(Console.ReadLine()!);
        Console.WriteLine(BalancedTernary(n));
    }

    static string BalancedTernary(int input) => FastRemainderSolution(input);

    static string FastRemainderSolution(int input) {
        if (input == 0) {
            return "0";
        }

        var ternary = string.Empty;
        while (input != 0) {
            int remainder = input % 3;
            ternary = remainder switch {
                2 => "T" + ternary,
                1 => "1" + ternary,
                0 => "0" + ternary,
                -1 => "T" + ternary,
                -2 => "1" + ternary,
                _ => ternary
            };

            if (Math.Abs(remainder) == 2) {
                input += Math.Sign(remainder);
            }

            input /= 3;
        }

        return ternary;
    }

    static string SlowCounterSolution(int input) {
        var ternarySymbols = new List<TernarySymbol> { new(0) };
        var counter = 0;
        if (input != 0) {
            int sign = input > 0 ? 1 : -1;
            while (counter != input) {
                counter += sign;
                if (ternarySymbols.All(it => it.value == sign)) {
                    ternarySymbols.Add(new TernarySymbol(sign) { count = -1 });
                }

                for (var index = 0; index < ternarySymbols.Count; index++) {
                    var ternarySymbol = ternarySymbols[index];
                    if (++ternarySymbol.count == (int)Math.Pow(3, index)) {
                        ternarySymbol.Change(sign);
                    }

                    ternarySymbols[index] = ternarySymbol;
                }
            }
        }

        ternarySymbols.Reverse();
        return string.Join("", ternarySymbols.Select(it => it.ToString()));
    }

    struct TernarySymbol(int value) {
        public int value = value;
        public int count = 0;

        public void Change(int sign) {
            value += sign;
            value = value switch {
                2 => -1,
                -2 => 1,
                _ => value
            };
            count = 0;
        }

        public override string ToString() {
            return value == -1 ? "T" : value.ToString();
        }
    }
}
