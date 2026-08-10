using System;
using System.Collections.Generic;

// https://www.codingame.com/training/easy/decode-the-message
var maxEncoded = (long)Math.Pow(10, 16);
long encoded = Math.Clamp(long.Parse(Console.ReadLine()!), 1, maxEncoded - 1);
string alphabet = Console.ReadLine()!;
long currentEncoded = 0;
List<char> decoded = [alphabet[0]];
while (true) {
    foreach (char c in alphabet) {
        decoded[0] = c;
        if (currentEncoded++ == encoded) {
            Console.WriteLine(string.Join(string.Empty, decoded));
            return;
        }
    }
    for (var level = 0;;) {
        int i = alphabet.IndexOf(decoded[level]) + 1;
        if (i >= alphabet.Length && level < decoded.Count - 1) {
            level++;
            continue;
        }
        ResetPrevious(level);
        if (i >= alphabet.Length && level >= decoded.Count - 1) {
            decoded.Add(alphabet[0]);
            break;
        }
        decoded[level] = alphabet[i];
        break;
    }
}

void ResetPrevious(int limit) {
    for (var i = 0; i <= limit; i++) {
        decoded[i] = alphabet[0];
    }
}
