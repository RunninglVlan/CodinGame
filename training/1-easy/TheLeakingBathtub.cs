using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/easy/the-leaking-bathtub
int surfaceArea = int.Parse(Console.ReadLine()!);
int height = int.Parse(Console.ReadLine()!);
int flow = int.Parse(Console.ReadLine()!);
long volume = (long)surfaceArea * height;
int leakCount = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1000);
var leaks = new Leak[leakCount];
for (var i = 0; i < leakCount; i++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    int leakHeight = Math.Clamp(int.Parse(inputs[0]), 0, height - 1);
    int leakFlow = int.Parse(inputs[1]);
    leaks[i] = new Leak(leakHeight, leakFlow);
}
float timeInSeconds = 0;
for (float filled = 0; filled < volume;) {
    float currentHeight = filled / surfaceArea;
    float amount = Flow(currentHeight) * 1000;
    if (amount < 0) {
        Console.WriteLine($"Impossible, {(int)currentHeight} cm.");
        return;
    }
    timeInSeconds += 60;
    filled += amount;
}
Console.WriteLine(FormatTime(timeInSeconds));
return;

[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
int Flow(float currentHeight) {
    int result = flow;
    foreach (var leak in leaks.Where(it => it.Height <= currentHeight)) {
        result -= leak.Flow;
    }
    return result;
}

string FormatTime(float totalSeconds) {
    var hours = (int)(totalSeconds / 3600);
    var minutes = (int)(totalSeconds / 60 % 60);
    var seconds = (int)(totalSeconds % 60);
    return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
}

record struct Leak(int Height, int Flow);
