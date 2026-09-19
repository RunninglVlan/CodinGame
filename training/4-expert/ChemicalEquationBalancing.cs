using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/expert/chemical-equation-balancing
string[] unbalanced = Console.ReadLine()!.Split(Separators.SIDE);
var equation = new Equation(ParseCompounds(unbalanced.First()), ParseCompounds(unbalanced.Last()));
equation.Balance();
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
    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public void Balance() {
        var leftElements = Elements(LeftCompounds);
        var rightElements = Elements(RightCompounds);
        var unbalancedSymbol = '\0';
        var leftHasMore = false;
        foreach ((char symbol, int leftCount) in leftElements) {
            if (leftCount == rightElements[symbol]) {
                continue;
            }
            unbalancedSymbol = symbol;
            leftHasMore = leftCount > rightElements[symbol];
            Console.Error.WriteLine($"unbalanced element: {symbol}{leftCount} -> {symbol}{rightElements[symbol]}");
        }
        foreach (var compound in LeftCompounds) {
            if (compound.Elements.Any(it => it.Symbol == unbalancedSymbol) && leftHasMore) {
                continue;
            }
            compound.Coefficient++;
        }
        foreach (var compound in RightCompounds) {
            if (compound.Elements.Any(it => it.Symbol == unbalancedSymbol) && !leftHasMore) {
                continue;
            }
            compound.Coefficient++;
        }
    }

    static Dictionary<char, int> Elements(List<Compound> compounds) {
        return compounds
                .SelectMany(it => it.Elements)
                .GroupBy(it => it.Symbol)
                .ToDictionary(it => it.Key, it => it.Sum(element => element.Count));
    }

    public override string ToString() {
        return string.Join(Separators.COMPOUND, LeftCompounds)
                + Separators.SIDE
                + string.Join(Separators.COMPOUND, RightCompounds);
    }
}

class Compound {
    public int Coefficient { get; set; } = 1;
    public List<Element> Elements { get; } = [];

    public void AddElement(char symbol, string count) {
        Elements.Add(new Element(symbol, count != string.Empty ? int.Parse(count) : 1));
    }

    public override string ToString() {
        string result = string.Join(null, Elements);
        if (Coefficient > 1) {
            result = Coefficient + result;
        }
        return result;
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
