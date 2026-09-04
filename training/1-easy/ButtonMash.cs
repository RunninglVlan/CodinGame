using System;
using System.Collections.Generic;
using System.Diagnostics;

// https://www.codingame.com/training/easy/button-mash
int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 1_000_000);
Console.WriteLine(Display(number));
return;

int Display(int required) {
    var queue = new PriorityQueue<int, int>();
    queue.Enqueue(0, 0);
    while (queue.TryDequeue(out int current, out int operations)) {
        if (current == required) {
            return operations;
        }
        foreach (int next in Next(current)) {
            queue.Enqueue(next, operations + 1);
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
