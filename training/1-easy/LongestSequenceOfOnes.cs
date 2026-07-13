using System;

namespace LongestSequenceOf1s;

/// https://www.codingame.com/training/easy/create-the-longest-sequence-of-1s
static class LongestSequenceOfOnes {
    static void Main() {
        string bits = Console.ReadLine()!;
        (string bits, int sequence) longest = (string.Empty, 0);
        for (var i = 0; i < bits.Length; i++) {
            if (bits[i] != '0') {
                continue;
            }
            string currentBits = bits[..i] + '1' + bits[(i + 1)..];
            int currentSequence = LongestSequence(currentBits);
            if (currentSequence > longest.sequence) {
                longest = (currentBits, currentSequence);
            }
        }
        Console.WriteLine(longest.sequence);
    }

    static int LongestSequence(string bits) {
        int max = 0, current = 0;
        foreach (char bit in bits) {
            if (bit == '0') {
                current = 0;
                continue;
            }
            current++;
            if (current > max) {
                max = current;
            }
        }
        return max;
    }
}
