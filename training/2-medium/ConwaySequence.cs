using System;
using System.Collections.Generic;

/// https://www.codingame.com/training/medium/conway-sequence
static class ConwaySequence {
    static void Main() {
        int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 99);
        int line = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 25);
        Console.WriteLine(GenerateSequence(number, line));
    }

    static string GenerateSequence(int number, int line) {
        List<int> current = [number];
        for (var _ = 1; _ < line; _++) {
            List<int> next = [];
            var count = 1;
            int last = current[0];
            for (var i = 1; i < current.Count; i++) {
                int currentNumber = current[i];
                if (currentNumber == last) {
                    count++;
                } else {
                    next.Add(count);
                    next.Add(last);
                    last = currentNumber;
                    count = 1;
                }
            }
            next.Add(count);
            next.Add(last);
            current = next;
        }
        return string.Join(" ", current);
    }
}
