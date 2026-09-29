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
        // doing gaussian elimination
        for (int pivotX = 0, pivotY = 0; pivotX < compounds.Length && pivotY < elements.Length; pivotX++, pivotY++) {
            if (matrix[pivotX, pivotY] == Fraction.ZERO) {
                // 1. swapping two rows
                int y = pivotY + 1;
                while (y < elements.Length && matrix[pivotX, y] == Fraction.ZERO) {
                    y++;
                }
                if (y < elements.Length) {
                    for (var x = 0; x < compounds.Length; x++) {
                        (matrix[x, pivotY], matrix[x, y]) = (matrix[x, y], matrix[x, pivotY]);
                    }
                    (elements[pivotY], elements[y]) = (elements[y], elements[pivotY]);
                }
            }
            for (int cellY = pivotY + 1; cellY < elements.Length; cellY++) {
                // cells under pivot need to be zero
                if (matrix[pivotX, cellY] == Fraction.ZERO) {
                    continue;
                }
                // 3. adding multiple of one row to another row
                int y = cellY - 1;
                while (matrix[pivotX, y] == Fraction.ZERO) {
                    y--;
                }
                var multiple = matrix[pivotX, cellY] / matrix[pivotX, y];
                for (var x = 0; x < compounds.Length; x++) {
                    matrix[x, cellY] -= matrix[x, y] * multiple;
                }
            }
        }
        // setting coefficients from matrix variables
        for (int x = elements.Length - 1, y = elements.Length - 1; x >= 0; x--, y--) {
            if (x >= compounds.Length || matrix[x, y] == Fraction.ZERO) {
                continue;
            }
            var rightSum = Fraction.ZERO;
            for (int xRight = x + 1; xRight < compounds.Length; xRight++) {
                var right = matrix[xRight, y] * compounds[xRight].Coefficient;
                rightSum += right;
            }
            compounds[x].Coefficient = rightSum * -1 / matrix[x, y];
        }
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

    public Fraction ElementCount(string symbol) {
        if (Elements.All(it => it.Symbol != symbol)) {
            return Fraction.ZERO;
        }
        return Coefficient * Elements.First(it => it.Symbol == symbol).Count;
    }

    public override string ToString() {
        string result = string.Join(null, Elements);
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

readonly record struct Fraction {
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

    int CompareTo(Fraction other) {
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
