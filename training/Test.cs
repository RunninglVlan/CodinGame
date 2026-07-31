using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

/// https://www.codingame.com/contribute/view/149373d871f27bdcdc97a723791d28b1fdfe5e
static class SimpleMakefiles {
    static void Main() {
        const string testCase = """
                                9
                                hdr.h 100
                                base.c 400
                                data.cfg 100
                                base.o 300
                                shared.h 500
                                cache.o 500
                                lib.a 200
                                util.o 150
                                junk.bak 50
                                app tests
                                38
                                # KitchenSink: all-features build
                                  #  whitespace comment
                                 
                                
                                tests app : lib.a util.o shared.dep # all outputs
                                	# Link everything together
                                	link $^ -o $@
                                	done
                                app: config.o # extra dep
                                	setup $<
                                # Note here that lib.a appears in multiple rules
                                # including a rule with multiple targets
                                lib.a libb.a: base.o cache.o
                                	ar rcs $@ $^
                                lib.a: util.o
                                	stamp $^
                                base.o: base.c hdr.h
                                	cc -c $< -o $@
                                cache.o: shared.h hdr.h # compile
                                	  cc -c $< -o $@ # compile
                                
                                # Case with no space after colon
                                util.o:data.cfg hdr.h
                                	cc -c $< -o $@
                                config.o: hdr.h # extra module
                                	  cc -c $< -o $@   
                                config.o: hdr.h hdr.h
                                	redo $<
                                data.cfg:
                                	  init data.cfg   
                                init:
                                	echo $^
                                shared.dep: init
                                cache.o cache.o: hdr.h
                                	touch $@
                                tag: # release tag
                                garbage: junk.bak
                                	delete $<
                                """;
        Console.SetIn(new StringReader(testCase));
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
            PrintRuleActions(rules.First());
        } else {
            var requiredRules = requiredTargets
                    .SelectMany(target => rules.Where(it => it.Target == target))
                    .OrderBy(it => it.Target);
            foreach (var rule in requiredRules) {
                PrintRuleActions(rule);
            }
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

    void PrintRuleActions(Rule targetRule) {
        if (!targetRule.HasActions || IsUpToDate(targetRule)) {
            return;
        }
        var prerequisiteRules = rules
                .Where(it => targetRule.Prerequisites.Contains(it.Target))
                .OrderBy(it => it.Target);
        foreach (var rule in prerequisiteRules) {
            PrintRuleActions(rule);
        }
        targetRule.PrintActions();
        targetRule.WasExecuted = true;
        if (!rules.Where(it => it.Target == targetRule.Target).All(it => it.WasExecuted)) {
            return;
        }
        int maxTime = existingFileTimes.Count > 0 ? existingFileTimes.Values.Max() : 0;
        existingFileTimes[targetRule.Target] = maxTime + 1;
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
    public bool WasExecuted { get; set; }

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
