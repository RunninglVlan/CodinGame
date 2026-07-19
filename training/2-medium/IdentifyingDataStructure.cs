using System;
using System.Collections.Generic;

namespace IdentifyingDataStructure;

/// https://www.codingame.com/training/medium/identifying-data-structure
static class IdentifyingDataStructure {
    const char INPUT = 'i';
    const char OUTPUT = 'o';

    static void Main() {
        int operationSequences = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
        for (var _ = 0; _ < operationSequences; _++) {
            List<IDataStructure<int>> structures = [new Queue(), new Stack(), new PriorityQueue()];
            string[] operations = Console.ReadLine()!.Split(' ');
            foreach (string operation in operations) {
                (char op, int value) = (operation[0], int.Parse(operation[1..]));
                switch (op) {
                    case INPUT:
                        structures.ForEach(it => it.Put(value));
                        break;
                    case OUTPUT:
                        for (int i = structures.Count - 1; i >= 0; i--) {
                            var structure = structures[i];
                            if (!structure.TryTake(out int actual) || actual != value) {
                                structures.Remove(structure);
                            }
                        }
                        break;
                }
            }
            string result = structures.Count switch {
                0 => "mystery",
                > 1 => "unsure",
                _ => structures[0].Name
            };
            Console.WriteLine(result);
        }
    }
}

interface IDataStructure<T> {
    string Name { get; }
    void Put(T item);
    bool TryTake(out T item);
}

class Queue : Queue<int>, IDataStructure<int> {
    public string Name => "queue";
    public void Put(int item) => Enqueue(item);
    public bool TryTake(out int item) => TryDequeue(out item);
}

class Stack : Stack<int>, IDataStructure<int> {
    public string Name => "stack";
    public void Put(int item) => Push(item);
    public bool TryTake(out int item) => TryPop(out item);
}

/// On dequeue, the item with the highest priority value is removed
class PriorityQueue : PriorityQueue<int, int>, IDataStructure<int> {
    public string Name => "priority queue";
    public void Put(int item) => Enqueue(item, int.MaxValue - item);
    public bool TryTake(out int item) => TryDequeue(out item, out _);
}
