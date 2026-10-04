using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/easy/equivalent-resistance-circuit-building
static class EquivalentResistanceCircuitBuilding {
    static void Main() {
        var resistors = new Dictionary<string, int>();
        int n = int.Parse(Console.ReadLine()!);
        for (var _ = 0; _ < n; _++) {
            string[] inputs = Console.ReadLine()!.Split(CircuitPart.SPACE);
            string name = inputs[0];
            int resistance = int.Parse(inputs[1]);
            resistors.Add(name, resistance);
        }
        var circuit = new CircuitPart(Console.ReadLine()!);
        Console.WriteLine($"{circuit.EquivalentResistance(resistors):F1}");
    }
}

class CircuitPart {
    public const char SPACE = ' ';
    const char PARALLEL_START = '[', PARALLEL_END = ']';
    const char SERIES_START = '(', SERIES_END = ')';
    readonly string input;
    readonly CircuitPartType type;
    readonly List<CircuitPart> parts = [];

    public CircuitPart(string input) {
        this.input = input;
        type = PartType(input[0]);
        if (type == CircuitPartType.Value) {
            return;
        }
        const int border = 2;
        string partsInput = input.Substring(border, input.Length - border * 2);
        var partType = PartType(partsInput[0]);
        while (partsInput.Length > 0) {
            if (partType == CircuitPartType.Value) {
                int partEndIndex = partsInput.IndexOf(SPACE);
                int index = partEndIndex > 0 ? partEndIndex : partsInput.Length;
                string partInput = partsInput[..index];
                parts.Add(new CircuitPart(partInput));
                partsInput = partsInput[index..];
            } else {
                int endIndex = EndIndex(partsInput, partType);
                string partInput = partsInput[..(endIndex + 1)];
                parts.Add(new CircuitPart(partInput));
                partsInput = partsInput[partInput.Length..];
            }
            if (partsInput.Length == 0) {
                continue;
            }
            if (partsInput[0] == SPACE) {
                partsInput = partsInput[1..];
            }
            partType = PartType(partsInput[0]);
        }
    }

    static CircuitPartType PartType(char value) => value switch {
        PARALLEL_START => CircuitPartType.Parallel,
        SERIES_START => CircuitPartType.Series,
        _ => CircuitPartType.Value
    };

    static int EndIndex(string value, CircuitPartType partType) {
        char start = partType == CircuitPartType.Series ? SERIES_START : PARALLEL_START;
        char end = partType == CircuitPartType.Series ? SERIES_END : PARALLEL_END;
        var level = 0;
        for (var index = 0; index < value.Length; index++) {
            char character = value[index];
            if (character == start) {
                level++;
            } else if (character == end) {
                level--;
            }
            if (level == 0) {
                return index;
            }
        }
        return -1;
    }

    public float EquivalentResistance(Dictionary<string, int> resistors) {
        if (type == CircuitPartType.Value) {
            return resistors[input];
        }
        var resistance = .0f;
        if (type == CircuitPartType.Series) {
            resistance += parts.Sum(part => part.EquivalentResistance(resistors));
        } else {
            resistance += parts.Sum(part => 1 / part.EquivalentResistance(resistors));
            resistance = 1 / resistance;
        }
        return resistance;
    }
}

enum CircuitPartType {
    Series,
    Parallel,
    Value
}
