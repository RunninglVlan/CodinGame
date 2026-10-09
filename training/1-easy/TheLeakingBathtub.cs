using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/the-leaking-bathtub
int surfaceArea = int.Parse(Console.ReadLine()!);
int height = int.Parse(Console.ReadLine()!);
int flow = int.Parse(Console.ReadLine()!);
long volume = (long)surfaceArea * height;
int leakCount = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1000);
List<Leak> leaks = [];
for (var i = 0; i < leakCount; i++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    int leakHeight = Math.Clamp(int.Parse(inputs[0]), 0, height - 1);
    int leakFlow = int.Parse(inputs[1]);
    leaks.Add(new Leak(leakHeight, leakFlow));
}
var orderedLeaks = leaks.OrderBy(it => it.Height).ToList();
double timeInSeconds = 0;
int currentFlow = flow;
for (double filled = 0; filled < volume;) {
    double currentHeight = filled / surfaceArea;
    if (orderedLeaks.Count > 0 && currentHeight >= orderedLeaks.First().Height) {
        var leak = orderedLeaks.First();
        currentFlow -= leak.Flow;
        orderedLeaks.RemoveAt(0);
    }
    double amount = currentFlow * 1000;
    amount /= 60;
    if (amount < 0) {
        Console.WriteLine($"Impossible, {(int)currentHeight} cm.");
        return;
    }
    timeInSeconds++;
    filled += amount;
}
Console.WriteLine(FormatTime(timeInSeconds));
return;

string FormatTime(double totalSeconds) {
    var hours = (int)(totalSeconds / 3600);
    var minutes = (int)(totalSeconds / 60 % 60);
    var seconds = (int)(totalSeconds % 60);
    return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
}

record struct Leak(int Height, int Flow);
