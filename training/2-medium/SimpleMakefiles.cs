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
        Console.Error.WriteLine();
        make.PrintActions();
    }
}

class Make {
    const char ACTION_CHAR = '\t';
    public const char SPACE = ' ';
    const char COMMENT = '#';

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
            if (makefileLine.StartsWith(COMMENT)) {
                continue;
            }
            if (!makefileLine.StartsWith(ACTION_CHAR)) {
                DuplicateLastRuleIfNeeded();
                string[] tokens = makefileLine.Split(':');
                string target = tokens.First();
                string[] prerequisites = tokens.Last().Split(SPACE, StringSplitOptions.RemoveEmptyEntries);
                rules.Add(new Rule(target, prerequisites));
            } else {
                var lastRule = rules.Last();
                int actionStart = makefileLine.IndexOf(ACTION_CHAR) + 1;
                int actionEnd = makefileLine.IndexOf(COMMENT);
                actionEnd = actionEnd == -1 ? makefileLine.Length : actionEnd;
                string action = makefileLine[actionStart..actionEnd].TrimEnd();
                lastRule.Add(action);
            }
        }
        DuplicateLastRuleIfNeeded();
    }

    void DuplicateLastRuleIfNeeded() {
        if (rules.Count == 0) {
            return;
        }
        var lastRule = rules.Last();
        if (!lastRule.Target.Contains(SPACE)) {
            return;
        }
        rules.Remove(lastRule);
        foreach (string target in lastRule.Target.Split(SPACE)) {
            rules.Add(lastRule with { Target = target });
        }
    }

    public void PrintActions() {
        if (requiredTargets.Length == 0) {
            PrintRuleActions(rules.First());
        } else {
            var requiredRules = requiredTargets
                    .Select(target => rules.First(it => it.Target == target))
                    .OrderBy(it => it.Target);
            foreach (var rule in requiredRules) {
                PrintRuleActions(rule);
            }
        }
        Console.WriteLine("[Build complete]");
    }

    void PrintRuleActions(Rule rule) {
        if (!rule.HasActions || rule.IsUpToDate(existingFileTimes)) {
            return;
        }
        foreach (string prerequisite in rule.Prerequisites.OrderBy(it => it)) {
            var prerequisiteRule = rules.FirstOrDefault(it => it.Target == prerequisite);
            if (prerequisiteRule == null || prerequisiteRule.IsUpToDate(existingFileTimes)) {
                continue;
            }
            prerequisiteRule.PrintActions();
        }
        rule.PrintActions();
    }
}

record Rule(string Target, string[] Prerequisites) {
    readonly List<string> actions = [];
    public bool HasActions => actions.Count > 0;

    public void Add(string action) => actions.Add(action);

    public bool IsUpToDate(Dictionary<string, int> fileTimes) {
        if (!fileTimes.TryGetValue(Target, out int targetTime)) {
            return false;
        }
        foreach (string prerequisite in Prerequisites) {
            if (!fileTimes.TryGetValue(prerequisite, out int prerequisiteTime)) {
                return false;
            }
            if (targetTime <= prerequisiteTime) {
                return false;
            }
        }
        return true;
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public void PrintActions() {
        foreach (string action in actions) {
            string replaced = action.Replace(Macros.CURRENT_TARGET, Target);
            if (Prerequisites.Length > 0) {
                replaced = replaced
                        .Replace(Macros.FIRST_PREREQUISITE, Prerequisites.First())
                        .Replace(Macros.ALL_PREREQUISITES, string.Join(Make.SPACE, Prerequisites));
            }
            Console.WriteLine(replaced);
        }
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
