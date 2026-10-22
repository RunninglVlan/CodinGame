using System;

// https://www.codingame.com/contribute/view/1508259d38553ca4273153ae30ab92e5b04982
string[] inputs = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(inputs[0]), 1, 50);
int width = Math.Clamp(int.Parse(inputs[1]), 1, 50);
int transformationEntries = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 150);
string diagnosticLog = Console.ReadLine()!;
for (var y = 0; y < height; y++) {
    string imageLine = Console.ReadLine()!;
}
Console.WriteLine("Reconstructed Image.");
