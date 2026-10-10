using System;
using System.Collections.Generic;

// https://www.codingame.com/training/easy/the-leaking-bathtub
int surfaceArea = int.Parse(Console.ReadLine()!);
int height = int.Parse(Console.ReadLine()!);
float flow = Flow(int.Parse(Console.ReadLine()!));
long volume = (long)surfaceArea * height;
int leakCount = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1000);
PriorityQueue<float, int> leaks = new();
for (var i = 0; i < leakCount; i++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    int leakHeight = Math.Clamp(int.Parse(inputs[0]), 0, height - 1);
    float leakFlow = Flow(int.Parse(inputs[1]));
    leaks.Enqueue(leakFlow, leakHeight);
}
const int stepMilliseconds = 40;
var timeInMilliseconds = 0;
double currentFlow = flow;
for (double filled = 0; filled < volume;) {
    double currentHeight = filled / surfaceArea;
    if (leaks.TryPeek(out _, out int flowHeight) && currentHeight >= flowHeight) {
        currentFlow -= leaks.Dequeue();
    }
    double amount = currentFlow * stepMilliseconds;
    if (amount < 0) {
        Console.WriteLine($"Impossible, {(int)currentHeight} cm.");
        return;
    }
    timeInMilliseconds += stepMilliseconds;
    filled += amount;
}
Console.WriteLine(FormatTime(timeInMilliseconds));
return;

// convert l/minute to cm3/millisecond
float Flow(int amount) => amount / 60f;

string FormatTime(int totalMilliseconds) {
    double totalSeconds = totalMilliseconds / 1000f;
    var hours = (int)(totalSeconds / 3600);
    var minutes = (int)(totalSeconds / 60 % 60);
    var seconds = (int)(totalSeconds % 60);
    return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
}
