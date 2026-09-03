using System;

// https://www.codingame.com/training/easy/button-mash
int number = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 1_000_000);
Console.WriteLine(Display(number));
return;

int Display(int required, int current = 0, int operations = 0) {
    if (current == required || operations == 10) {
        return operations;
    }
    int plus1 = Display(number, current + 1, operations + 1);
    int minus1 = Display(number, current - 1, operations + 1);
    int multiplyBy2 = Display(number, current * 2, operations + 1);
    return Math.Min(Math.Min(plus1, minus1), multiplyBy2);
}
