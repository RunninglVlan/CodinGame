using System;

// https://www.codingame.com/training/easy/decode-the-message
var maxEncoded = (long)Math.Pow(10, 16);
long encoded = Math.Clamp(long.Parse(Console.ReadLine()!), 1, maxEncoded - 1);
string alphabet = Console.ReadLine()!;
var decoded = string.Empty;
long letter = encoded % alphabet.Length;
decoded += alphabet[(int)letter];
long times = encoded / alphabet.Length;
while (times > 0) {
    letter = (times - 1) % alphabet.Length;
    decoded += alphabet[(int)letter];
    times /= alphabet.Length;
}
Console.WriteLine(decoded);
