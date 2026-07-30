using System;

/// https://www.codingame.com/contribute/view/149373d871f27bdcdc97a723791d28b1fdfe5e
static class SimpleMakefiles {
    static void Main() {
        int files = int.Parse(Console.ReadLine()!);
        for (var _ = 0; _ < files; _++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            string preexistingFile = inputs[0];
            int fileTime = int.Parse(inputs[1]);
        }
        string cmdArgs = Console.ReadLine()!;
        int lines = int.Parse(Console.ReadLine()!);
        for (var _ = 0; _ < lines; _++) {
            string makefileLine = Console.ReadLine()!;
        }
        Console.WriteLine("[Build complete]");
    }
}
