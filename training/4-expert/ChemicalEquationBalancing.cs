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
                var lCount = ElementCount(l, e);
                var rCount = ElementCount(r, e);
                string lType = l.Count == 1 ? l[0].Elements.Count == 1 ? "s" : "m" : "mm";
                string rType = r.Count == 1 ? r[0].Elements.Count == 1 ? "s" : "m" : "mm";
                return $"[{e} ({lCount}{lType}->{rCount}{rType}): {string.Join(", ", l)} -> {string.Join(", ", r)}]";
            });
            Debug.Log($"unbalanced: {string.Join(", ", unbalancedFormatted)}", 1);
            (string element, var (left, right)) = unbalanced
                    .OrderBy(it => Mathf.Min(ElementCount(it.Value.left, it.Key), ElementCount(it.Value.right, it.Key)))
                    .First();
            var leftCount = ElementCount(left, element);
            var rightCount = ElementCount(right, element);
            var compounds = leftCount < rightCount ? left : right;
            var compound = compounds.OrderBy(it => it.Elements.Count).First();
            Debug.Log($"balancing {element} from {compound}", 1);
            var max = Mathf.Max(leftCount, rightCount);
            var min = Mathf.Min(leftCount, rightCount);
            if (compounds.Count > 1) {
                if (compound.Elements.Count > 1) {
                    compound = compounds.OrderBy(it => it.ElementCount(element)).First();
                    compound.Coefficient += Fraction.ONE;
                    continue;
                }
                var notSingleCount = compounds.First(it => it.Elements.Count != 1).ElementCount(element);
                max -= notSingleCount;
                min -= notSingleCount;
            }
            compound.Coefficient = max / min;
            int denominator = compound.Coefficient.Denominator;
            if (denominator == 1) {
                continue;
            }
            LeftCompounds.ForEach(it => it.Coefficient *= denominator);
            RightCompounds.ForEach(it => it.Coefficient *= denominator);
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

    static Fraction ElementCount(List<Compound> compounds, string symbol) {
        return compounds.Aggregate(Fraction.ZERO, (accumulator, it) => accumulator + it.ElementCount(symbol));
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
    public Fraction Coefficient { get; set; } = Fraction.ONE;
    public List<Element> Elements { get; } = [];

    public void AddElement(string symbol, string count) {
        Elements.Add(new Element(symbol, count != string.Empty ? int.Parse(count) : 1));
    }

    public Fraction ElementCount(string symbol) => Coefficient * Elements.First(it => it.Symbol == symbol).Count;

    public override string ToString() {
        string result = string.Join(null, Elements);
        // string result = string.Join('-', Elements);
        if (Coefficient > Fraction.ONE) {
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

readonly record struct Fraction : IComparable<Fraction> {
    public static readonly Fraction ZERO = new(0, 1);
    public static readonly Fraction ONE = new(1, 1);

    int Numerator { get; }
    public int Denominator { get; }

    Fraction(int numerator, int denominator) {
        int gcd = Mathf.GreatestCommonDivisor(numerator, denominator);
        if (gcd != 0) {
            numerator /= gcd;
            denominator /= gcd;
        }
        Numerator = numerator;
        Denominator = denominator;
    }

    public int CompareTo(Fraction other) {
        int left = Numerator * other.Denominator;
        int right = other.Numerator * Denominator;
        return left.CompareTo(right);
    }

    public static bool operator >(Fraction a, Fraction b) => a.CompareTo(b) > 0;
    public static bool operator <(Fraction a, Fraction b) => a.CompareTo(b) < 0;
    public static bool operator >=(Fraction a, Fraction b) => a.CompareTo(b) >= 0;
    public static bool operator <=(Fraction a, Fraction b) => a.CompareTo(b) <= 0;

    public static Fraction operator +(Fraction a, Fraction b) {
        int lcm = Mathf.LeastCommonMultiple(a.Denominator, b.Denominator);
        return new Fraction(a.Numerator * lcm / a.Denominator + b.Numerator * lcm / b.Denominator, lcm);
    }

    public static Fraction operator -(Fraction a, Fraction b) {
        int lcm = Mathf.LeastCommonMultiple(a.Denominator, b.Denominator);
        return new Fraction(a.Numerator * lcm / a.Denominator - b.Numerator * lcm / b.Denominator, lcm);
    }

    public static Fraction operator *(Fraction a, Fraction b) {
        return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
    }

    public static Fraction operator *(Fraction a, int value) => new(a.Numerator * value, a.Denominator);

    public static Fraction operator /(Fraction a, Fraction b) {
        return new Fraction(a.Numerator * b.Denominator, b.Numerator * a.Denominator);
    }

    public override string ToString() {
        return Denominator == 1 ? Numerator.ToString() : $"{Numerator}/{Denominator}";
    }
}

static class Mathf {
    public static Fraction Max(Fraction a, Fraction b) => a >= b ? a : b;
    public static Fraction Min(Fraction a, Fraction b) => a <= b ? a : b;

    /// Euclidean algorithm
    public static int GreatestCommonDivisor(int a, int b) {
        // r is a remainder
        int rPrevious = a, r = b;
        while (r != 0) {
            (rPrevious, r) = (r, rPrevious % r);
        }
        return rPrevious;
    }

    public static int LeastCommonMultiple(int a, int b) {
        int gcd = GreatestCommonDivisor(a, b);
        return Math.Abs(a) / gcd * Math.Abs(b);
    }
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
