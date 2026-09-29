using System;
using System.Collections.Generic;
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
    public void Balance() {
        string[] elements = [.. LeftCompounds.SelectMany(it => it.Elements).Select(it => it.Symbol).Distinct()];
        var compounds = LeftCompounds.Concat(RightCompounds).ToArray();
        // building matrix
        int[] compoundSigns = [.. LeftCompounds.Select(_ => 1), .. RightCompounds.Select(_ => -1)];
        var matrix = new Fraction[compounds.Length, elements.Length];
        for (var y = 0; y < elements.Length; y++) {
            for (var x = 0; x < compounds.Length; x++) {
                matrix[x, y] = compounds[x].ElementCount(elements[y]) * compoundSigns[x];
            }
        }
        Log("built matrix");
        // doing gaussian elimination
        for (int pivotX = 0, pivotY = 0; pivotY < elements.Length; pivotX++, pivotY++) {
            if (matrix[pivotX, pivotY] == Fraction.ZERO && pivotY < elements.Length - 1) {
                // 1. swapping two rows
                if (matrix[pivotX, pivotY + 1] == Fraction.ZERO) {
                    Debug.Log($"TODO: cell below {(pivotX, pivotY)} is zero", 1);
                }
                for (var x = 0; x < compounds.Length; x++) {
                    (matrix[x, pivotY], matrix[x, pivotY + 1]) = (matrix[x, pivotY + 1], matrix[x, pivotY]);
                }
                (elements[pivotY], elements[pivotY + 1]) = (elements[pivotY + 1], elements[pivotY]);
            }
            for (int cellY = pivotY + 1; cellY < elements.Length; cellY++) {
                // cells under pivot need to be zero
                if (matrix[pivotX, cellY] == Fraction.ZERO) {
                    continue;
                }
                // 3. adding multiple of one row to another row
                int y;
                for (y = cellY - 1; matrix[pivotX, y] == Fraction.ZERO; y--) { }
                var multiple = matrix[pivotX, cellY] / matrix[pivotX, y];
                Debug.Log($"multiple: {multiple}", 2);
                for (var x = 0; x < compounds.Length; x++) {
                    matrix[x, cellY] -= matrix[x, y] * multiple;
                }
            }
            Log($"processed pivot {(pivotX, pivotY)}");
        }
        // setting coefficients from matrix variables
        Debug.Log($"coefficient for {compounds.Last()} is 1", 1);
        for (int x = elements.Length - 1, y = elements.Length - 1; x >= 0; x--, y--) {
            if (matrix[x, y] == Fraction.ZERO) {
                continue;
            }
            var coefficient = Fraction.ZERO;
            Debug.Log($"setting coefficient for {compounds[x]}", 1);
            for (int xRight = x + 1; xRight < compounds.Length; xRight++) {
                var right = matrix[xRight, y] * compoundSigns[xRight] * compounds[xRight].Coefficient;
                coefficient += right;
                Debug.Log($"plus {right}: {coefficient}", 2);
            }
            compounds[x].Coefficient = coefficient / matrix[x, y];
            Debug.Log($"divide by itself {matrix[x, y]}: {compounds[x].Coefficient}", 1);
        }
        Debug.Log(this);
        // if fractional coefficient exists, multiply all coefficients by its denominator
        int? fractionalDenominator = null;
        foreach (var compound in compounds) {
            if (compound.Coefficient.Denominator != 1) {
                fractionalDenominator = compound.Coefficient.Denominator;
                break;
            }
        }
        if (fractionalDenominator != null) {
            foreach (var compound in compounds) {
                compound.Coefficient *= fractionalDenominator.Value;
            }
        }
        return;

        void Log(string message) {
            Console.Error.WriteLine($"-------- {message}");
            for (var y = 0; y < elements.Length; y++) {
                Console.Error.Write($"{elements[y]}:\t");
                for (var x = 0; x < compounds.Length; x++) {
                    Console.Error.Write($"{matrix[x, y]}\t");
                }
                Console.Error.WriteLine();
            }
        }
    }

    (string element, List<Compound> left, List<Compound> right, Fraction leftCount, Fraction rightCount)[]
            UnbalancedElements() {
        return LeftCompounds
                .SelectMany(it => it.Elements.Select(element => element.Symbol))
                .Distinct()
                .Select(symbol => {
                    var left = ElementCompounds(LeftCompounds, symbol);
                    var right = ElementCompounds(RightCompounds, symbol);
                    var leftCount = ElementCount(left, symbol);
                    var rightCount = ElementCount(right, symbol);
                    return (symbol, left, right, leftCount, rightCount);
                })
                .Where(it => it.leftCount != it.rightCount)
                .ToArray();
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
    public List<Element> Elements { get; private init; } = [];

    public void AddElement(string symbol, string count) {
        Elements.Add(new Element(symbol, count != string.Empty ? int.Parse(count) : 1));
    }

    public Fraction ElementCount(string symbol) {
        if (Elements.All(it => it.Symbol != symbol)) {
            return Fraction.ZERO;
        }
        return Coefficient * Elements.First(it => it.Symbol == symbol).Count;
    }

    public Compound Clone(int multiplier) {
        return new Compound {
            Coefficient = Coefficient * multiplier,
            Elements = [.. Elements]
        };
    }

    public override string ToString() {
        string result = string.Join(null, Elements);
        // string result = string.Join('-', Elements);
        if (Coefficient != Fraction.ONE) {
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

    public int Numerator { get; }
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
