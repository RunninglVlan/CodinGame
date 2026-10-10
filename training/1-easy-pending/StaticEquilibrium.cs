using System;

// https://www.codingame.com/contribute/view/134886a1467b44cd36bccace7cef8fc3771021
int forces = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 1000);
for (var _ = 0; _ < forces; _++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    int forceMagnitude = Math.Clamp(int.Parse(inputs[0]), 1, 1000_000);
    string direction = inputs[1];
}
Console.WriteLine("answer");
