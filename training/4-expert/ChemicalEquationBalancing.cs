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
        for (var unbalanced = UnbalancedElements(); unbalanced.Count > 0; unbalanced = UnbalancedElements()) {
            foreach (var compound in LeftCompounds) {
                if (compound.Elements.Count == unbalanced.Count
                        && compound.Elements.All(it => unbalanced.Contains(it.Symbol))) {
                    continue;
                }
                compound.Coefficient++;
            }
            foreach (var compound in RightCompounds) {
                if (compound.Elements.Count == unbalanced.Count
                        && compound.Elements.All(it => unbalanced.Contains(it.Symbol))) {
                    continue;
                }
                compound.Coefficient++;
            }
        }
    }

    HashSet<char> UnbalancedElements() {
        var leftElements = Elements(LeftCompounds);
        var rightElements = Elements(RightCompounds);
        HashSet<char> unbalancedElements = [];
        foreach ((char symbol, int leftCount) in leftElements) {
            if (leftCount == rightElements[symbol]) {
                continue;
            }
            unbalancedElements.Add(symbol);
        }
        return unbalancedElements;

        Dictionary<char, int> Elements(List<Compound> compounds) {
            return compounds
                    .SelectMany(compound => {
                        return compound.Elements.Select(it => (it.Symbol, count: compound.Coefficient * it.Count));
                    })
                    .GroupBy(it => it.Symbol)
                    .ToDictionary(it => it.Key, it => it.Sum(element => element.count));
        }
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
