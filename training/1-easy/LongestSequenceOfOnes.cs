using System;

/// https://www.codingame.com/training/easy/create-the-longest-sequence-of-1s
static class LongestSequenceOfOnes {
    static void Main() {
        string bits = Console.ReadLine()!;
        var longest = 0;
        for (var i = 0; i < bits.Length; i++) {
            if (bits[i] != '0') {
                continue;
            }
            char[] changed = bits.ToCharArray();
            changed[i] = '1';
            int current = LongestSequence(changed);
            if (current > longest) {
                longest = current;
            }
        }
        Console.WriteLine(longest);
    }

    static int LongestSequence(char[] bits) {
        int longest = 0, current = 0;
        foreach (char bit in bits) {
            if (bit == '0') {
                current = 0;
                continue;
            }
            current++;
            if (current > longest) {
                longest = current;
            }
        }
        return longest;
    }
}
