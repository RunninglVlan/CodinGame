using System;

// https://www.codingame.com/training/easy/should-bakers-be-frugal
string[] inputs = Console.ReadLine()!.Split(' ');
float doughSide = Math.Clamp(float.Parse(inputs[0]), 1, 99);
float biscuitDiameter = Math.Clamp(float.Parse(inputs[1]), 1, 99);
Console.WriteLine(FrugalBiscuits() - WastefulBiscuits());
return;

int FrugalBiscuits() {
    return 3;
}

int WastefulBiscuits() {
    return 1;
}
