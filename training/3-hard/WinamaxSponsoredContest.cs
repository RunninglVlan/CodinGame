namespace System;

/// https://www.codingame.com/training/hard/winamax-sponsored-contest
static class WinamaxSponsoredContest {
    static void Main() {
        string[] dimensions = Console.ReadLine()!.Split(' ');
        int width = Math.Clamp(int.Parse(dimensions[0]), 1, 1000);
        int height = Math.Clamp(int.Parse(dimensions[1]), 1, 1000);
        for (var y = 0; y < height; y++) {
            string row = Console.ReadLine()!;
        }
    }
}
