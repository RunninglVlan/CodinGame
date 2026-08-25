using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

// https://www.codingame.com/training/medium/elliptic-curve-cryptography
var g = new Point(0x69d463ce83b758e, 0x287a120903f7ef5c);
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

/// Point on Elliptic-Curve
readonly record struct Point(long X, long Y) {
    const long A = 0;
    const long B = 7;
    const long P = 0x3fddbf07bb3bc551;

    /// L  = (Yd - Yc) / (Xd - Xc) mod P (when C != D) <br/>
    /// L  = (3*Xc² + A) / (2*Yc)  mod P (when C == D) <br/>
    /// Xs = L² - Xc - Xd          mod P <br/>
    /// Ys = L * (Xc - Xs) - Yc    mod P
    public Point Double() {
        long x = X;
        // Modular division: (a / b) % M => (a × b^-1 ) % M, where b^-1 is modular inverse of b modulo M
        long l = (3 * x * x + A) * MathUtils.ModularInverse(2 * Y, P) % P;
        long newX = (l * l - x - x) % P;
        return new(newX, (l * (x - newX) - Y) % P);
    }
}

static class MathUtils {
    /// Euclidean algorithm
    [SuppressMessage("ReSharper", "TailRecursiveCall")]
    public static long GreatestCommonDivisor(long a, long b) {
        return b == 0 ? a : GreatestCommonDivisor(b, a % b);
    }

    public static long ModularInverse(long a, long m) {
        // inverse exists only if gcd(a, m) = 1, i.e., a and m are coprime
        if (GreatestCommonDivisor(a, m) != 1) {
            throw new ArgumentException("No modular inverse exists: a and m are not coprime");
        }
        // x must be in the range [1, ... , m−1] (it cannot be 0 since a × 0 mod m is never 1)
        for (var x = 1; x < m; x++) {
            // modular inverse x must satisfy: a * x ≡ 1 (mod m)
            if (a * x % m == 1) {
                return x;
            }
        }
        throw new UnreachableException();
    }
}
