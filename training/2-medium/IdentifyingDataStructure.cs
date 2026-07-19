using System;

namespace IdentifyingDataStructure;

/// https://www.codingame.com/training/medium/identifying-data-structure
static class IdentifyingDataStructure {
    static void Main() {
        int operationSequences = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
        for (var i = 0; i < operationSequences; i++) {
            string line = Console.ReadLine()!;
        }
        Console.WriteLine("queue");
    }
}
