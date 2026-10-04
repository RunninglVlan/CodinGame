using System;
using System.Linq;

/// https://www.codingame.com/training/easy/ghost-legs
static class GhostLegs {
    const int LEG_COEFFICIENT = 3;
    const char HORIZONTAL_LINE = '-';

    static string[] topLabels = [];
    static int[] bottomIndices = [];
    static string[] bottomLabels = [];

    static void Main() {
        int height = int.Parse(Console.ReadLine()!.Split(' ')[1]);

        topLabels = Console.ReadLine()!.Split("  ");
        bottomIndices = [.. Enumerable.Range(0, topLabels.Length)];
        for (var i = 1; i < height - 1; i++) {
            DetermineIfPathTurns(Console.ReadLine()!);
        }
        bottomLabels = Console.ReadLine()!.Split("  ");

        ListConnectedPairs();
    }

    static void DetermineIfPathTurns(string line) {
        for (var i = 0; i < bottomIndices.Length; i++) {
            int legIndex = bottomIndices[i] * LEG_COEFFICIENT;
            bool pathTurnsLeft = legIndex - 1 >= 0 && line[legIndex - 1] == HORIZONTAL_LINE;
            bool pathTurnsRight = legIndex + 1 < line.Length && line[legIndex + 1] == HORIZONTAL_LINE;
            if (pathTurnsLeft) {
                bottomIndices[i]--;
            } else if (pathTurnsRight) {
                bottomIndices[i]++;
            }
        }
    }

    static void ListConnectedPairs() {
        for (var i = 0; i < bottomIndices.Length; i++) {
            Console.WriteLine(topLabels[i] + bottomLabels[bottomIndices[i]]);
        }
    }
}
