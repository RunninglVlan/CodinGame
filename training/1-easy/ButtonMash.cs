using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/button-mash
int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 1_000_000);
Console.WriteLine(Display(number));
return;

int Display(int required, int current = 0, int operations = 0) {
    if (current == required || operations == 10) {
        return operations;
    }
    return Next().Select(it => Display(required, it, operations + 1)).Min();

    IEnumerable<int> Next() {
        yield return current + 1;
        yield return current - 1;
        yield return current * 2;
    }
}
