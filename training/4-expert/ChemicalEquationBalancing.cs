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
        var matrix = BuildMatrix(compounds, elements);
        SolveMatrix(matrix, compounds.Length, elements.Length);
        SetCoefficients(matrix, compounds, elements.Length);
    }

    Fraction[,] BuildMatrix(Compound[] compounds, string[] elements) {
        int[] compoundSigns = [.. LeftCompounds.Select(_ => 1), .. RightCompounds.Select(_ => -1)];
        var matrix = new Fraction[compounds.Length, elements.Length];
        for (var y = 0; y < elements.Length; y++) {
            for (var x = 0; x < compounds.Length; x++) {
                matrix[x, y] = compounds[x].ElementCount(elements[y]) * compoundSigns[x];
            }
        }
        return matrix;
    }

    /// Solve the system of linear equations using Gaussian elimination
    static void SolveMatrix(Fraction[,] matrix, int width, int height) {
        for (int x = 0, y = 0; x < width && y < height; x++, y++) {
            if (matrix[x, y] == Fraction.ZERO) {
                Swap2Rows(x, y);
            }
            for (int yBelow = y + 1; yBelow < height; yBelow++) {
                if (matrix[x, yBelow] != Fraction.ZERO) {
                    AddMultipleOfRow(x, yBelow);
                }
            }
        }
        return;

        void Swap2Rows(int pivotX, int pivotY) {
            if (!TryFindRow(pivotX, pivotY, +1, out int y)) {
                return;
            }
            for (var x = 0; x < width; x++) {
                (matrix[x, pivotY], matrix[x, y]) = (matrix[x, y], matrix[x, pivotY]);
            }
        }

        void AddMultipleOfRow(int pivotX, int cellY) {
            TryFindRow(pivotX, cellY, -1, out int y);
            var multiple = matrix[pivotX, cellY] / matrix[pivotX, y];
            for (var x = 0; x < width; x++) {
                matrix[x, cellY] -= matrix[x, y] * multiple;
            }
        }

        bool TryFindRow(int x, int y, int direction, out int row) {
            row = y + direction;
            while (row < height && matrix[x, row] == Fraction.ZERO) {
                row += direction;
            }
            return row < height;
        }
    }

    static void SetCoefficients(Fraction[,] matrix, Compound[] compounds, int elements) {
        int? fractionalDenominator = null;
        for (int x = elements - 1, y = elements - 1; x >= 0; x--, y--) {
            if (x >= compounds.Length || matrix[x, y] == Fraction.ZERO) {
                continue;
            }
            var rightSum = Fraction.ZERO;
            for (int xRight = x + 1; xRight < compounds.Length; xRight++) {
                var right = matrix[xRight, y] * compounds[xRight].Coefficient;
                rightSum += right;
            }
            compounds[x].Coefficient = rightSum * -1 / matrix[x, y];
            if (fractionalDenominator == null && compounds[x].Coefficient.Denominator != 1) {
                fractionalDenominator = compounds[x].Coefficient.Denominator;
            }
        }
        if (fractionalDenominator == null) {
            return;
        }
        foreach (var compound in compounds) {
            compound.Coefficient *= fractionalDenominator.Value;
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
