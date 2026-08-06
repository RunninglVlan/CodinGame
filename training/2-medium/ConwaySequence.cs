using System;
using System.Collections.Generic;

/// https://www.codingame.com/training/medium/conway-sequence
static class ConwaySequence {
    static void Main() {
        int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 99);
        int line = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 25);
        Console.WriteLine(GenerateSequence(number, line));
    }

    static string GenerateSequence(int number, int times) {
        List<int> current = [number];
        for (var _ = 1; _ < times; _++) {
            current = LookAndSay(current);
        }
        return string.Join(" ", current);
    }

    static List<int> LookAndSay(List<int> current) {
        List<int> next = [];
        var count = 1;
        for (var i = 1; i <= current.Count; i++) {
            if (i < current.Count && current[i] == current[i - 1]) {
                count++;
            } else {
                next.Add(count);
                next.Add(current[i - 1]);
                count = 1;
            }
        }
        return next;
    }
}
