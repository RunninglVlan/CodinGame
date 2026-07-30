using System;

/// https://www.codingame.com/contribute/view/149373d871f27bdcdc97a723791d28b1fdfe5e
static class SimpleMakefiles {
    static void Main() {
        var make = new Make();
        make.ReadFiles();
        make.ReadCmd();
        make.ReadMakefile();
        make.PrintActions();
    }
}

class Make {
    public void ReadFiles() {
        int files = int.Parse(Debug.ReadLine());
        for (var _ = 0; _ < files; _++) {
            string[] inputs = Debug.ReadLine().Split(' ');
            string preexistingFile = inputs[0];
            int fileTime = int.Parse(inputs[1]);
        }
    }

    public void ReadCmd() {
        string cmdArgs = Debug.ReadLine();
    }

    public void ReadMakefile() {
        int lines = int.Parse(Debug.ReadLine());
        for (var _ = 0; _ < lines; _++) {
            string makefileLine = Debug.ReadLine();
        }
    }

    public void PrintActions() {
        Console.WriteLine("[Build complete]");
    }
}

static class Debug {
    /// Reads line from Console and also writes it to Console.Error
    public static string ReadLine() {
        string line = Console.ReadLine()!;
        Console.Error.WriteLine(line);
        return line;
    }
}
