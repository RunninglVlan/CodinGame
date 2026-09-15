using System;

// https://www.codingame.com/contribute/view/15475216bbf3fd5ab519bcb675b26f2cb7bd28
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
for (var _ = 0; _ < lines; _++) {
    string line = Console.ReadLine()!;
    Console.WriteLine(Sanitize(line));
}
return;

string Sanitize(string line) {
    return line;
}
