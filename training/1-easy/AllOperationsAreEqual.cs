using System;

// https://www.codingame.com/training/medium/all-operations-are-equal
int integers = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var _ = 0; _ < integers; _++) {
    int integer = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100_000);
    Console.WriteLine(IsGood(integer) ? "YES" : "NO");
}
return;

bool IsGood(int integer) {
    for (var i = 2; i < integer; i++) {
        long square = (long)i * i;
        if (integer % square == 0 && integer >= square * 2) {
            return true;
        }
    }
    return false;
}
