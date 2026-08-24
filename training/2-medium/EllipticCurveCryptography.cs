using System;
using System.Globalization;
using System.Linq;

// https://www.codingame.com/training/medium/elliptic-curve-cryptography
Point g = new(0x69d463ce83b758e, 0x287a120903f7ef5c);
int keys = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
for (var i = 0; i < keys; i++) {
    string[] line = Console.ReadLine()!.Split('x');
    long privateKey = long.Parse(line.Last(), NumberStyles.HexNumber);
    var publicKey = g;
    for (var j = 0; j < privateKey; j++) {
        publicKey = publicKey.Double();
    }
    Console.WriteLine($"0x{publicKey.X:x}");
}

readonly record struct Point(long X, long Y) {
    const long A = 0;
    const long B = 7;
    const long P = 0x3fddbf07bb3bc551;

    /// L  = (3*Xc² + A) / (2*Yc)  mod P <br/>
    /// Xs = L² - Xc - Xd          mod P <br/>
    /// Ys = L * (Xc - Xs) - Yc    mod P
    public Point Double() {
        long x = X;
        long l = (3 * x * x + A) / (2 * Y) % P;
        long newX = (l * l - x - x) % P;
        return new(newX, (l * (x - newX) - Y) % P);
    }
}
