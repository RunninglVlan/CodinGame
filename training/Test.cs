const string testCase = """
                        H2 + O2 -> H2O
                        2H2 + O2 -> 2H2O
                        CO2 + H2O -> C6H12O6 + O2
                        6CO2 + 6H2O -> C6H12O6 + 6O2
                        NaOH + H2CO3 -> Na2CO3 + H2O
                        2NaOH + H2CO3 -> Na2CO3 + 2H2O
                        C2H2 + O2 -> CO2 + H2O
                        2C2H2 + 5O2 -> 4CO2 + 2H2O
                        Rb + HNO3 -> RbNO3 + H2
                        2Rb + 2HNO3 -> 2RbNO3 + H2
                        Al + O2 -> Al2O3
                        4Al + 3O2 -> 2Al2O3
                        KClO3 -> KCl + O2
                        2KClO3 -> 2KCl + 3O2
                        C4H10 + O2 -> CO2 + H2O
                        2C4H10 + 13O2 -> 8CO2 + 10H2O
                        V + O2 -> V2O5
                        4V + 5O2 -> 2V2O5
                        Mg + O2 -> MgO
                        2Mg + O2 -> 2MgO
                        C6H12O6 + O2 -> CO2 + H2O
                        C6H12O6 + 6O2 -> 6CO2 + 6H2O
                        H2O2 -> H2O + O2
                        2H2O2 -> 2H2O + O2
                        """;
Console.SetIn(new StringReader(testCase));
// https://www.codingame.com/training/expert/chemical-equation-balancing
for (string? line = Debug.ReadLine(); line != null; line = Debug.ReadLine()) {
    string[] unbalanced = line.Split(Separators.SIDE);
    var equation = new Equation(ParseCompounds(unbalanced.First()), ParseCompounds(unbalanced.Last()));
    equation.Balance();
    Assert.ActualEqualsExpected(equation.ToString(), Console.ReadLine()!);
    Debug.Log();
}
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
        var count = 0;
        for (var unbalanced = UnbalancedElements(); unbalanced.Count > 0; unbalanced = UnbalancedElements()) {
            if (count++ > 10) {
                break;
            }
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
            int leftCount = ElementCount(left, element);
            int rightCount = ElementCount(right, element);
            var compounds = leftCount < rightCount ? left : right;
            var compound = compounds.OrderBy(it => it.Elements.Count).First();
            Debug.Log($"balancing {element} from {compound}", 1);
            int max = Math.Max(leftCount, rightCount);
            int min = Math.Min(leftCount, rightCount);
            Debug.Log($"max: {max}, min: {min}", 1);
            if (compounds.Count > 1) {
                Debug.Log("compounds.Count > 1", 2);
                /*
Pt + H-N-O3 + 6H-Cl -> H2-Pt-Cl6 + N-O2 + H2-O
  unbalanced: [H (7mm->4mm): H-N-O3, 6H-Cl -> H2-Pt-Cl6, H2-O]
  balancing H
  max: 7, min: 4
    compounds.Count > 1
      compound.Elements.Count > 1
      chose H2-O
                 */
                if (compound.Elements.Count > 1) {
                    Debug.Log("compound.Elements.Count > 1", 3);
                    compound = compounds.OrderBy(it => it.ElementCount(element)).ThenBy(it => it.Elements.Count).First();
                    Debug.Log($"chose {compound}", 3);
                    // need to do something with this, just ++ doesn't work anymore
                    // *= 2 is not enough
                    // this is still hardcode
                    compound.Coefficient *= 2;
                    continue;
                }
                Debug.Log("compound.Elements.Count == 1", 3);
                int notSingleCount = compounds.First(it => it.Elements.Count != 1).ElementCount(element);
                max -= notSingleCount;
                min -= notSingleCount;
            }
            if (max % min != 0) {
                Debug.Log("max % min != 0", 2);
                compound.Coefficient = max;
                continue;
            }
            Debug.Log("max % min == 0", 2);
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

static class Assert {
    public static void ActualEqualsExpected(string actual, string expected) {
        Console.WriteLine(Equals(actual, expected) ? $"✅ {actual}" : $"❌ found {actual}, but expected {expected}");
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
