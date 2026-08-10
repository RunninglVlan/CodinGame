using System;

// https://www.codingame.com/training/easy/decode-the-message
var maxEncoded = (long)Math.Pow(10, 16);
long encoded = Math.Clamp(long.Parse(Console.ReadLine()!), 1, maxEncoded - 1);
string alphabet = Console.ReadLine()!;
for (var i = 0; i < alphabet.Length; i++) {
    var repeated = string.Empty;
    int times = i + 1;
    for (var _ = 0; _ < times; _++) {
        repeated += alphabet[0];
    }
    for (var j = 0; j < alphabet.Length; j++) {
        long currentEncoded = i * alphabet.Length + j;
        string decoded = alphabet[j] + repeated[1..];
        if (currentEncoded == encoded) {
            Console.WriteLine(decoded);
            break;
        }
    }
}
