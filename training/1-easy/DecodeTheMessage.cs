using System;

// https://www.codingame.com/training/easy/decode-the-message
var maxEncoded = (long)Math.Pow(10, 16);
long encoded = Math.Clamp(long.Parse(Console.ReadLine()!), 1, maxEncoded - 1);
string alphabet = Console.ReadLine()!;
var decoded = string.Empty;
// Bijective base-N (like Excel columns: A..Z, AA..AZ, ...) - no zero digit,
// so shift into 1-indexed counting, then re-apply the shift each iteration.
long remaining = encoded + 1;
while (remaining > 0) {
    remaining--;
    long letter = remaining % alphabet.Length;
    decoded += alphabet[(int)letter];
    remaining /= alphabet.Length;
}
Console.WriteLine(decoded);
