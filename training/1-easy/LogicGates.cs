using System;

namespace LogicGates;

/// https://www.codingame.com/training/easy/logic-gates
static class LogicGates {
    static void Main() {
        int inputSignals = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 4);
        int outputSignals = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 16);
        for (var i = 0; i < inputSignals; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            string inputName = inputs[0];
            string inputSignal = inputs[1];
        }
        for (var i = 0; i < outputSignals; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            string outputName = inputs[0];
            string type = inputs[1];
            string inputName1 = inputs[2];
            string inputName2 = inputs[3];
        }
        for (var i = 0; i < outputSignals; i++) {
            Console.WriteLine("output name and signal");
        }
    }
}
