using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/contribute/view/149373d871f27bdcdc97a723791d28b1fdfe5e
static class SimpleMakefiles {
    static void Main() {
        const string testCase4 = """
                                 2
                                 main.c 100
                                 util.c 100
                                 
                                 6
                                 all: util.o main.o
                                 	gcc -o $@ $^
                                 util.o: util.c
                                 	gcc -c $< -o $@
                                 main.o: main.c
                                 	gcc -c $< -o $@
                                 """;
        string testCase = testCase4;
        Console.SetIn(new StringReader(testCase));
        var make = new Make();
        make.ReadFiles();
        make.ReadRequiredTargets();
        make.ReadMakefile();
        make.PrintActions();
    }
}

class Make {
    const string TAB = "\t";

    File[] preexistingFiles = null!;
    string[] requiredTargets = null!;
    readonly List<Rule> rules = [];

    public void ReadFiles() {
        int files = Math.Clamp(int.Parse(Debug.ReadLine()), 0, 20);
        preexistingFiles = new File[files];
        for (var i = 0; i < files; i++) {
            string[] inputs = Debug.ReadLine().Split();
            string preexistingFile = inputs.First();
            int fileTime = Math.Clamp(int.Parse(inputs.Last()), 0, 1000);
            preexistingFiles[i] = new File(preexistingFile, fileTime);
        }
    }

    public void ReadRequiredTargets() {
        requiredTargets = Debug.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    public void ReadMakefile() {
        int lines = Math.Clamp(int.Parse(Debug.ReadLine()), 1, 100);
        for (var _ = 0; _ < lines; _++) {
            string makefileLine = Debug.ReadLine();
            if (!makefileLine.StartsWith(TAB)) {
                string[] tokens = makefileLine.Split(':');
                string target = tokens.First();
                string[] prerequisites = tokens.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                rules.Add(new Rule(target, prerequisites));
            } else {
                var lastRule = rules.Last();
                lastRule.Actions.Add(makefileLine.Replace(TAB, string.Empty));
            }
        }
    }

    public void PrintActions() {
        var targetRule = requiredTargets.Length == 0
                ? rules.First()
                : rules.First(it => it.Target == requiredTargets.First());
        foreach (string prerequisite in targetRule.Prerequisites) {
            var prerequisiteRule = rules.First(it => it.Target == prerequisite);
            prerequisiteRule.Actions.ForEach(Console.WriteLine);
        }
        targetRule.Actions.ForEach(Console.WriteLine);
        Console.WriteLine("[Build complete]");
    }
}

record struct File(string Name, int Time);

record Rule(string Target, string[] Prerequisites) {
    public List<string> Actions { get; } = [];
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string ReadLine() {
        string line = Console.ReadLine()!;
        Console.Error.WriteLine(line);
        return line;
    }
}
