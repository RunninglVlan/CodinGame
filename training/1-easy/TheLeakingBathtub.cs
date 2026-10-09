using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/easy/the-leaking-bathtub
int surfaceArea = int.Parse(Console.ReadLine()!);
int height = int.Parse(Console.ReadLine()!);
int flow = int.Parse(Console.ReadLine()!);
int volume = surfaceArea * height;
int leakCount = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1000);
var leaks = new Leak[leakCount];
for (var i = 0; i < leakCount; i++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    int leakHeight = Math.Clamp(int.Parse(inputs[0]), 0, height - 1);
    int leakFlow = int.Parse(inputs[1]);
    leaks[i] = new Leak(leakHeight, leakFlow);
}
float seconds = 0;
for (float filled = 0; filled < volume;) {
    seconds++;
    float currentHeight = filled / surfaceArea;
    float volumePerSecond = VolumePerSecond(currentHeight);
    if (volumePerSecond <= 0) {
        Console.WriteLine($"Impossible, {(int)currentHeight} cm.");
        return;
    }
    filled += volumePerSecond;
}
Console.WriteLine(TimeSpan.FromSeconds(seconds));
return;

[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
float VolumePerSecond(float currentHeight) {
    int totalFlow = flow;
    foreach (var leak in leaks.Where(it => it.Height <= currentHeight)) {
        totalFlow -= leak.Flow;
    }
    return (float)totalFlow / 60 * 1000;
}

record struct Leak(int Height, int Flow);
