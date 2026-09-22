using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// https://www.codingame.com/training/expert/chemical-equation-balancing
string[] unbalanced = Debug.ReadLine()!.Split(Separators.SIDE);
var equation = new Equation(ParseCompounds(unbalanced.First()), ParseCompounds(unbalanced.Last()));
equation.Balance();
Console.WriteLine(equation);
return;

List<Compound> ParseCompounds(string side) {
    List<Compound> compounds = [];
    foreach (string compoundChars in side.Split(Separators.COMPOUND)) {
        var compound = new Compound();
        var symbol = string.Empty;
        var count = string.Empty;
        foreach (char c in compoundChars) {
            if (string.IsNullOrEmpty(symbol)) {
                symbol += c;
            } else if (char.IsDigit(c)) {
                count += c;
            } else if (char.IsLower(c)) {
                symbol += c;
            } else {
                compound.AddElement(symbol, count);
                symbol = $"{c}";
                count = string.Empty;
            }
        }
        compound.AddElement(symbol, count);
        compounds.Add(compound);
    }
    return compounds;
}

record Equation(List<Compound> LeftCompounds, List<Compound> RightCompounds) {
    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public void Balance() {
        Dictionary<string, (int left, int right)>? lastUnbalanced = null;
        for (var unbalanced = UnbalancedElements(); unbalanced.Count > 0; unbalanced = UnbalancedElements()) {
            if (lastUnbalanced == null) {
                Debug.Log($"Unbalanced: {string.Join(", ", unbalanced)}", 1);
            } else if (unbalanced.Count == lastUnbalanced.Count) {
                var allMatch = true;
                foreach ((string symbol, (int left, int right)) in unbalanced) {
                    if (!lastUnbalanced.TryGetValue(symbol, out var value)
                            || left != value.left && right != value.right) {
                        allMatch = false;
                    }
                }
                if (allMatch) {
                    Debug.Log("Couldn't balance", 1);
                    return;
                }
            }
            foreach (var compound in LeftCompounds) {
                if (compound.Elements.Count == unbalanced.Count
                        && compound.Elements.All(it => unbalanced.ContainsKey(it.Symbol))) {
                    continue;
                }
                compound.Coefficient++;
            }
            foreach (var compound in RightCompounds) {
                if (compound.Elements.Count == unbalanced.Count
                        && compound.Elements.All(it => unbalanced.ContainsKey(it.Symbol))) {
                    continue;
                }
                compound.Coefficient++;
            }
            lastUnbalanced = unbalanced;
        }
    }

    Dictionary<string, (int left, int right)> UnbalancedElements() {
        var leftElements = Elements(LeftCompounds);
        var rightElements = Elements(RightCompounds);
        Dictionary<string, (int, int)> unbalancedElements = [];
        foreach ((string symbol, int leftCount) in leftElements) {
            if (leftCount == rightElements[symbol]) {
                continue;
            }
            unbalancedElements.Add(symbol, (leftCount, rightElements[symbol]));
        }
        return unbalancedElements;

        Dictionary<string, int> Elements(List<Compound> compounds) {
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

    public void AddElement(string symbol, string count) {
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

readonly record struct Element(string Symbol, int Count) {
    public override string ToString() {
        string result = Symbol;
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

static class Debug {
    const int TAB_LENGTH = 2;

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
