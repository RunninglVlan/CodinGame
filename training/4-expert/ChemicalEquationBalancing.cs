using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/expert/chemical-equation-balancing
string[] unbalanced = Console.ReadLine()!.Split(Separators.SIDE);
var equation = new Equation(ParseCompounds(unbalanced.First()), ParseCompounds(unbalanced.Last()));
Console.WriteLine(equation);
return;

List<Compound> ParseCompounds(string side) {
    List<Compound> compounds = [];
    foreach (string compoundChars in side.Split(Separators.COMPOUND)) {
        var compound = new Compound();
        char? symbol = null;
        var count = string.Empty;
        foreach (char c in compoundChars) {
            if (symbol == null) {
                symbol = c;
            } else if (char.IsDigit(c)) {
                count += c;
            } else {
                compound.AddElement(symbol.Value, count);
                symbol = c;
                count = string.Empty;
            }
        }
        compound.AddElement(symbol!.Value, count);
        compounds.Add(compound);
    }
    return compounds;
}

record Equation(List<Compound> LeftCompounds, List<Compound> RightCompounds) {
    public override string ToString() {
        return string.Join(Separators.COMPOUND, LeftCompounds)
                + Separators.SIDE
                + string.Join(Separators.COMPOUND, RightCompounds);
    }
}

class Compound {
    List<Element> Elements { get; } = [];

    public void AddElement(char symbol, string count) {
        Elements.Add(new Element(symbol, count != string.Empty ? int.Parse(count) : 1));
    }

    public override string ToString() {
        return string.Join(null, Elements);
    }
}

readonly record struct Element(char Symbol, int Count) {
    public override string ToString() {
        var result = $"{Symbol}";
        if (Count > 1) {
            result += Count;
        }
        return result;
    }
}

static class Separators {
    public const string SIDE = " -> ";
    public const string COMPOUND = " + ";
}
