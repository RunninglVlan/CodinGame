using System;

// https://www.codingame.com/training/easy/should-bakers-be-frugal
string[] inputs = Console.ReadLine()!.Split(' ');
float doughSide = Math.Clamp(float.Parse(inputs[0]), 1, 99);
float biscuitDiameter = Math.Clamp(float.Parse(inputs[1]), 1, 99);
Console.WriteLine(FrugalBiscuits(doughSide) - WastefulBiscuits(doughSide));
return;

int FrugalBiscuits(double side) {
    var count = 0;
    float biscuitSquareArea = biscuitDiameter * biscuitDiameter;
    for (double doughArea = side * side; doughArea >= biscuitSquareArea;) {
        int biscuits = WastefulBiscuits(side);
        count += biscuits;
        doughArea -= biscuits * (Math.PI * biscuitSquareArea / 4);
        side = Math.Sqrt(doughArea);
    }
    return count;
}

int WastefulBiscuits(double side) {
    var biscuitsPerSide = (int)(side / biscuitDiameter);
    return biscuitsPerSide * biscuitsPerSide;
}
