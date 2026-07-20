using System;
using System.Linq;

namespace LogicGates;

/// https://www.codingame.com/training/easy/logic-gates
static class LogicGates {
    static void Main() {
        int inputSignalCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 4);
        int outputSignalCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 16);
        var inputSignals = Enumerable.Range(0, inputSignalCount)
                .Select(_ => {
                    string[] inputs = Console.ReadLine()!.Split(' ');
                    return (name: inputs[0], signal: inputs[1]);
                })
                .ToDictionary(it => it.name, it => it.signal);
        for (var _ = 0; _ < outputSignalCount; _++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            string outputName = inputs[0];
            string gate = inputs[1];
            string inputSignal1 = inputSignals[inputs[2]];
            string inputSignal2 = inputSignals[inputs[3]];
            Console.Write($"{outputName} ");
            for (var i = 0; i < inputSignal1.Length; i++) {
                Console.Write(Evaluate(gate, inputSignal1[i], inputSignal2[i]));
            }
            Console.WriteLine();
        }
    }

    static char Evaluate(string gate, char level1, char level2) {
        bool a = level1 == Levels.HIGH;
        bool b = level2 == Levels.HIGH;
        bool result = gate switch {
            Gates.AND => a & b,
            Gates.OR => a | b,
            Gates.XOR => a ^ b,
            _ => throw new ArgumentException($"Unexpected logic gate: {gate}")
        };
        return result ? Levels.HIGH : Levels.LOW;
    }
}

static class Levels {
    public const char LOW = '_';
    public const char HIGH = '-';
}

static class Gates {
    public const string AND = "AND";
    public const string OR = "OR";
    public const string XOR = "XOR";
}
