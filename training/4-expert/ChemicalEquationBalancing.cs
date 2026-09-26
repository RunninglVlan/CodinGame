using System;
using System.Collections.Generic;
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
    public void Balance() {
        for (var unbalanced = UnbalancedElements(); unbalanced.Count > 0; unbalanced = UnbalancedElements()) {
            Debug.Log(this);
            var unbalancedFormatted = unbalanced.Select(it => {
                (string e, var (l, r)) = it;
                int lCount = ElementCount(l, e);
                int rCount = ElementCount(r, e);
                string lType = l.Count == 1 ? l[0].Elements.Count == 1 ? "s" : "m" : "mm";
                string rType = r.Count == 1 ? r[0].Elements.Count == 1 ? "s" : "m" : "mm";
                return $"[{e} ({lCount}{lType}->{rCount}{rType}): {string.Join(", ", l)} -> {string.Join(", ", r)}]";
            });
            Debug.Log($"unbalanced: {string.Join(", ", unbalancedFormatted)}", 1);
            (string element, var (left, right)) = unbalanced
                    .OrderBy(it => Math.Min(ElementCount(it.Value.left, it.Key), ElementCount(it.Value.right, it.Key)))
                    .First();
            Debug.Log($"balancing {element}", 1);
            int leftCount = ElementCount(left, element);
            int rightCount = ElementCount(right, element);
            var compounds = leftCount < rightCount ? left : right;
            var compound = compounds.OrderBy(it => it.Elements.Count).First();
            int max = Math.Max(leftCount, rightCount);
            int min = Math.Min(leftCount, rightCount);
            if (compounds.Count > 1) {
                if (compound.Elements.Count > 1) {
                    compound = compounds.OrderBy(it => it.ElementCount(element)).First();
                    compound.Coefficient++;
                    continue;
                }
                int notSingleCount = compounds.First(it => it.Elements.Count != 1).ElementCount(element);
                max -= notSingleCount;
                min -= notSingleCount;
            }
            if (max % min != 0) {
                compound.Coefficient = max;
                continue;
            }
            compound.Coefficient *= max / min;
        }
    }

    Dictionary<string, (List<Compound> left, List<Compound> right)> UnbalancedElements() {
        return LeftCompounds
                .SelectMany(it => it.Elements.Select(element => element.Symbol))
                .Distinct()
                .Select(symbol => (
                        symbol,
                        left: ElementCompounds(LeftCompounds, symbol),
                        right: ElementCompounds(RightCompounds, symbol))
                )
                .Where(it => ElementCount(it.left, it.symbol) != ElementCount(it.right, it.symbol))
                .ToDictionary(it => it.symbol, it => (it.left, it.right));
    }

    static int ElementCount(List<Compound> compounds, string symbol) {
        return compounds.Sum(it => it.ElementCount(symbol));
    }

    static List<Compound> ElementCompounds(List<Compound> compounds, string symbol) {
        var elementCompounds = compounds.Where(it => it.Elements.Any(element => element.Symbol == symbol));
        return [.. elementCompounds];
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

    public int ElementCount(string symbol) => Coefficient * Elements.First(it => it.Symbol == symbol).Count;

    public override string ToString() {
        string result = string.Join(null, Elements);
        // string result = string.Join('-', Elements);
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
