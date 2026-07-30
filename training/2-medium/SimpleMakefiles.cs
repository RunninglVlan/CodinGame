using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

/// https://www.codingame.com/contribute/view/149373d871f27bdcdc97a723791d28b1fdfe5e
static class SimpleMakefiles {
    static void Main() {
        var make = new Make();
        make.ReadFiles();
        make.ReadRequiredTargets();
        make.ReadMakefile();
        make.PrintActions();
    }
}

class Make {
    const string ACTION_CHAR = "\t";
    public const char SPACE = ' ';

    readonly Dictionary<string, int> existingFileTimes = [];
    string[] requiredTargets = null!;
    readonly List<Rule> rules = [];

    public void ReadFiles() {
        int files = Math.Clamp(int.Parse(Debug.ReadLine()), 0, 20);
        for (var i = 0; i < files; i++) {
            string[] inputs = Debug.ReadLine().Split();
            string preexistingFile = inputs.First();
            int fileTime = Math.Clamp(int.Parse(inputs.Last()), 0, 1000);
            existingFileTimes.Add(preexistingFile, fileTime);
        }
    }

    public void ReadRequiredTargets() {
        requiredTargets = Debug.ReadLine().Split(SPACE, StringSplitOptions.RemoveEmptyEntries);
    }

    public void ReadMakefile() {
        int lines = Math.Clamp(int.Parse(Debug.ReadLine()), 1, 100);
        for (var _ = 0; _ < lines; _++) {
            string makefileLine = Debug.ReadLine();
            if (!makefileLine.StartsWith(ACTION_CHAR)) {
                string[] tokens = makefileLine.Split(':');
                string target = tokens.First();
                string[] prerequisites = tokens.Last().Split(SPACE, StringSplitOptions.RemoveEmptyEntries);
                rules.Add(new Rule(target, prerequisites));
            } else {
                var lastRule = rules.Last();
                lastRule.Add(makefileLine.Replace(ACTION_CHAR, string.Empty));
            }
        }
    }

    public void PrintActions() {
        var targetRule = requiredTargets.Length == 0
                ? rules.First()
                : rules.First(it => it.Target == requiredTargets.First());
        if (targetRule.HasActions && !targetRule.IsUpToDate(existingFileTimes)) {
            foreach (string prerequisite in targetRule.Prerequisites.OrderBy(it => it)) {
                var prerequisiteRule = rules.First(it => it.Target == prerequisite);
                if (prerequisiteRule.IsUpToDate(existingFileTimes)) {
                    continue;
                }
                prerequisiteRule.PrintActions();
            }
            targetRule.PrintActions();
        }
        Console.WriteLine("[Build complete]");
    }
}

record Rule(string Target, string[] Prerequisites) {
    readonly List<string> actions = [];
    public bool HasActions => actions.Count > 0;

    public void Add(string action) => actions.Add(action);

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public void PrintActions() {
        foreach (string action in actions) {
            string replaced = action
                    .Replace(Macros.CURRENT_TARGET, Target)
                    .Replace(Macros.FIRST_PREREQUISITE, Prerequisites.First())
                    .Replace(Macros.ALL_PREREQUISITES, string.Join(Make.SPACE, Prerequisites));
            Console.WriteLine(replaced);
        }
    }

    public bool IsUpToDate(Dictionary<string, int> fileTimes) {
        if (!fileTimes.TryGetValue(Target, out int targetTime)) {
            return false;
        }
        foreach (string prerequisite in Prerequisites) {
            if (!fileTimes.TryGetValue(prerequisite, out int prerequisiteTime)) {
                return false;
            }
            if (targetTime < prerequisiteTime) {
                return false;
            }
        }
        return true;
    }
}

static class Macros {
    public const string CURRENT_TARGET = "$@";
    public const string FIRST_PREREQUISITE = "$<";
    public const string ALL_PREREQUISITES = "$^";
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string ReadLine() {
        string line = Console.ReadLine()!;
        Console.Error.WriteLine(line);
        return line;
    }
}
