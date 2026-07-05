using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RobberyOptimisation;

/// https://www.codingame.com/training/medium/robbery-optimisation
static class RobberyOptimisation {
    static void Main() {
        var stopwatch = Stopwatch.StartNew();
        (string input, long expected) testCase7 = (
                """
                20
                2
                5
                10
                2
                4
                8
                5
                2
                6
                7
                7
                6
                11
                12
                5
                9
                6
                10
                7
                9
                """,
                75
        );
        (string input, long expected) testCase8 = (
                """
                50
                2
                5
                10
                2
                4
                8
                5
                2
                6
                7
                7
                6
                11
                12
                5
                9
                6
                10
                7
                9
                17
                63
                121
                12
                85
                95
                60
                100
                70
                91
                73
                26
                41
                28
                58
                19
                26
                17
                7
                9
                11
                23
                21
                42
                68
                65
                86
                101
                106
                1
                """,
                916
        );
        var testCase = testCase7;
        Console.SetIn(new StringReader(testCase.input));
        var robbery = new Robbery();
        robbery.ReadHouseValues();
        long actual = robbery.MaxMoney();
        Console.WriteLine(testCase.expected == actual ? $"✅ {actual}" : $"❌ {testCase.expected} != {actual}");
        Console.Error.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}

class Robbery {
    long[] houses = null!;

    public void ReadHouseValues() {
        long maxValue = (long)Math.Pow(10, 13) - 1;
        int count = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
        houses = new long[count];
        for (var i = 0; i < count; i++) {
            houses[i] = Math.Clamp(long.Parse(Console.ReadLine()!), long.MinValue, maxValue);
        }
        Console.Error.WriteLine($"House values: {string.Join(", ", houses)}");
    }

    public long MaxMoney() {
        long best = 0;
        foreach (var robbed in Rob(0, [], [])) {
            long money = robbed.Sum(j => houses[j]);
            if (money <= best) {
                continue;
            }
            best = money;
            Console.Error.WriteLine($"🆕 best: {best}");
            long total = 0;
            var lines = robbed.Select(r => $" Robbed {r}, +{houses[r]} => {total += houses[r]}");
            Console.Error.WriteLine(string.Join(Environment.NewLine, lines));
        }
        return best;
    }

    // DFS/Backtracking
    IEnumerable<List<int>> Rob(int current, List<int> currentRobbed, HashSet<int> currentChecked) {
        if (currentChecked.Count == houses.Length) {
            yield return currentRobbed;
            yield break;
        }
        for (int i = current; i < houses.Length; i++) {
            // ignore if already robbed, or it's next to the robbed house
            if (currentChecked.Contains(i)) {
                continue;
            }
            int next = i + 2;
            List<int> nextRobbed = [..currentRobbed, i];
            var nextChecked = new HashSet<int>(currentChecked) {
                i,
                Math.Clamp(i + 1, 0, houses.Length - 1),
                Math.Clamp(i - 1, 0, houses.Length - 1)
            };
            foreach (var result in Rob(next, nextRobbed, nextChecked)) {
                yield return result;
            }
        }
    }
}
