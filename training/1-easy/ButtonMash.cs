using System;
using System.Collections.Generic;
using System.Diagnostics;

// https://www.codingame.com/training/easy/button-mash
int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 1_000_000);
Console.WriteLine(Display(number));
return;

int Display(int required) {
    var queue = new Queue<(int value, int operations)>();
    var start = (0, 0);
    queue.Enqueue(start);
    HashSet<(int, int)> visited = [start];
    while (queue.TryDequeue(out var current)) {
        if (current.value == required) {
            return current.operations;
        }
        foreach (int nextValue in Next(current.value)) {
            var next = (nextValue, current.operations + 1);
            if (!visited.Add(next)) {
                continue;
            }
            queue.Enqueue(next);
        }
    }
    throw new UnreachableException();

    IEnumerable<int> Next(int current) {
        yield return current + 1;
        if (current > 0) {
            yield return current - 1;
            yield return current * 2;
        }
    }
}
