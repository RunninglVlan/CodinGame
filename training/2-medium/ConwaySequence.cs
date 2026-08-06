using System;

/// https://www.codingame.com/training/medium/conway-sequence
static class ConwaySequence {
    static void Main() {
        int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 99);
        int line = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 25);
        Console.WriteLine("answer");
    }
}
