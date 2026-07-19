using System;
using System.Collections.Generic;

namespace IdentifyingDataStructure;

/// https://www.codingame.com/training/medium/identifying-data-structure
static class IdentifyingDataStructure {
    const char INPUT = 'i';
    const char OUTPUT = 'o';

    static void Main() {
        int operationSequences = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
        for (var i = 0; i < operationSequences; i++) {
            var queue = new Queue<int>();
            var stack = new Stack<int>();
            var priorityQueue = new PriorityQueue<int, int>();
            List<object> structures = [queue, stack, priorityQueue];
            string[] operations = Console.ReadLine()!.Split(' ');
            foreach (string operation in operations) {
                (char op, int value) = (operation[0], int.Parse(operation[1..]));
                switch (op) {
                    case INPUT:
                        queue.Enqueue(value);
                        stack.Push(value);
                        priorityQueue.Enqueue(value, 10 - value);
                        break;
                    case OUTPUT:
                        if (queue.Dequeue() != value) {
                            structures.Remove(queue);
                        }
                        if (stack.Pop() != value) {
                            structures.Remove(stack);
                        }
                        if (priorityQueue.Dequeue() != value) {
                            structures.Remove(priorityQueue);
                        }
                        break;
                }
            }
            Console.WriteLine(Determine(structures));
        }
    }

    static string Determine(List<object> structures) => structures.Count switch {
        0 => "mystery",
        > 1 => "unsure",
        _ => structures[0] switch {
            Queue<int> => "queue",
            Stack<int> => "stack",
            PriorityQueue<int, int> => "priority queue",
            _ => throw new ArgumentException($"Unknown structure type: {structures[0].GetType().Name}")
        }
    };
}
