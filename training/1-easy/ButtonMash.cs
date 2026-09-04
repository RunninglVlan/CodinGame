using System;
using System.Collections.Generic;
using System.Diagnostics;

// https://www.codingame.com/training/easy/button-mash
int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 1_000_000);
Console.WriteLine(Display(number));
return;

// BFS
int Display(int required) {
    var queue = new Queue<(int value, int operations)>();
    queue.Enqueue((0, 0));
    HashSet<int> visited = [];
    while (queue.TryDequeue(out var current)) {
        if (current.value == required) {
            return current.operations;
        }
        foreach (int nextValue in Next(current.value)) {
            if (!visited.Add(nextValue)) {
                continue;
            }
            queue.Enqueue((nextValue, current.operations + 1));
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
