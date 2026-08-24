using System;
using System.Globalization;
using System.Linq;

// https://www.codingame.com/training/medium/elliptic-curve-cryptography
const long a = 0;
const long b = 7;
const long p = 0x3fddbf07bb3bc551;
(long x, long y) g = (0x69d463ce83b758e, 0x287a120903f7ef5c);
int keys = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
for (var i = 0; i < keys; i++) {
    string[] line = Console.ReadLine()!.Split('x');
    long privateKey = long.Parse(line.Last(), NumberStyles.HexNumber);
    (long x, long y) publicKey = (0x0, 0x1);
    Console.WriteLine($"0x{publicKey.x:x}");
}
