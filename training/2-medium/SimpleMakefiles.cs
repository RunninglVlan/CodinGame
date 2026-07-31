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
        string[] lastRuleTargets = [];
        for (var _ = 0; _ < lines; _++) {
            string makefileLine = Debug.ReadLine();
            bool isAction = makefileLine.StartsWith(ACTION_CHAR);
            makefileLine = StripComments(makefileLine);
            if (string.IsNullOrEmpty(makefileLine)) {
                continue;
            }
            if (!isAction) {
                string[] tokens = makefileLine.Split(':');
                lastRuleTargets = Split(tokens.First());
                string[] prerequisites = Split(tokens.Last());
                foreach (string target in lastRuleTargets) {
                    rules.Add(new Rule(target, prerequisites));
                }
            } else {
                var lastRules = lastRuleTargets.Select(target => rules.Last(it => it.Target == target));
                foreach (var lastRule in lastRules) {
                    lastRule.Add(makefileLine);
                }
            }
        }
        return;

        string[] Split(string token) => [.. token.Split(SPACE, StringSplitOptions.RemoveEmptyEntries).Distinct()];
    }

    static string StripComments(string line) {
        line = line.Trim();
        int commentIndex = line.IndexOf('#');
        if (commentIndex == 0) {
            return string.Empty;
        }
        return commentIndex == -1 ? line : line[..commentIndex].TrimEnd();
    }

    public void PrintActions() {
        if (CheckCircularDependency()) {
            Console.WriteLine("[Circular dependencies detected]");
            return;
        }
        if (requiredTargets.Length == 0) {
            PrintRuleActions([rules.First()]);
        } else {
            PrintRuleActions(requiredTargets);
        }
        Console.WriteLine("[Build complete]");
    }

    bool CheckCircularDependency() {
        return rules.Any(it => Check(it, []));

        bool Check(Rule current, HashSet<Rule> currentRules) {
            if (currentRules.Contains(current)) {
                return true;
            }
            return rules
                    .Where(it => current.Prerequisites.Contains(it.Target))
                    .Any(it => Check(it, [.. currentRules, current]));
        }
    }

    void PrintRuleActions(IEnumerable<string> required) {
        var ruleGroups = required
                .SelectMany(target => rules.Where(it => it.Target == target))
                .OrderBy(it => it.Target)
                .GroupBy(it => it.Target);
        foreach (var group in ruleGroups) {
            PrintRuleActions([.. group]);
        }
    }

    void PrintRuleActions(List<Rule> targetRules) {
        if (!targetRules.All(it => it.HasActions) || targetRules.All(IsUpToDate)) {
            return;
        }
        PrintRuleActions(targetRules.SelectMany(it => it.Prerequisites));
        targetRules.ForEach(it => it.PrintActions());
        int maxTime = existingFileTimes.Count > 0 ? existingFileTimes.Values.Max() : 0;
        existingFileTimes[targetRules.First().Target] = maxTime + 1;
    }

    bool IsUpToDate(Rule rule) {
        if (!existingFileTimes.TryGetValue(rule.Target, out int targetTime)) {
            return false;
        }
        foreach (string prerequisite in rule.Prerequisites) {
            if (!existingFileTimes.TryGetValue(prerequisite, out int prerequisiteTime)) {
                return false;
            }
            if (targetTime <= prerequisiteTime) {
                return false;
            }
            if (!rules.Where(it => it.Target == prerequisite).All(IsUpToDate)) {
                return false;
            }
        }
        return true;
    }
}

record Rule(string Target, string[] Prerequisites) {
    readonly List<string> actions = [];
    public bool HasActions => actions.Count > 0;

    public void Add(string action) => actions.Add(action);

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
