using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/decode-the-message
var maxEncoded = (long)Math.Pow(10, 16);
long encoded = Math.Clamp(long.Parse(Console.ReadLine()!), 1, maxEncoded - 1);
string alphabet = Console.ReadLine()!;
long currentEncoded = 0;
List<char> decoded = [alphabet.First()];
while (true) {
    foreach (char c in alphabet) {
        decoded[0] = c;
        if (currentEncoded++ == encoded) {
            Console.WriteLine(string.Join(string.Empty, decoded));
            return;
        }
    }
    for (var i = 0;;) {
        int index = alphabet.IndexOf(decoded[i]);
        index++;
        if (index >= alphabet.Length) {
            if (i >= decoded.Count - 1) {
                decoded.Add(alphabet.First());
                for (var j = 0; j < decoded.Count; j++) {
                    decoded[j] = alphabet.First();
                }
                break;
            }
            i++;
            continue;
        }
        for (var j = 0; j < i; j++) {
            decoded[j] = alphabet.First();
        }
        decoded[i] = alphabet[index];
        break;
    }
}
