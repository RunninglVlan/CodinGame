using System;
using System.Collections.Generic;
using System.Diagnostics;

// https://www.codingame.com/training/easy/button-mash
int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 1_000_000);
Console.WriteLine(Display(number));
return;

int Display(int required) {
    var queue = new Queue<(int value, int operations)>();
    queue.Enqueue((0, 0));
    while (queue.TryDequeue(out var current)) {
        if (current.value == required) {
            return current.operations;
        }
        foreach (int next in Next(current.value)) {
            queue.Enqueue((next, current.operations + 1));
        }
    }
    throw new UnreachableException();

    IEnumerable<int> Next(int current) {
        yield return current + 1;
        yield return current - 1;
        yield return current * 2;
    }
}
