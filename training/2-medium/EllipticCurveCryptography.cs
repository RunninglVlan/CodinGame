using System;
using System.Globalization;
using System.Linq;
using System.Numerics;

// https://www.codingame.com/training/medium/elliptic-curve-cryptography
var g = new Point(0x69d463ce83b758e, 0x287a120903f7ef5c);
int keys = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
for (var i = 0; i < keys; i++) {
    string[] line = Console.ReadLine()!.Split('x');
    long privateKey = long.Parse(line.Last(), NumberStyles.HexNumber);
    var publicKey = g;
    for (var j = 0; j < privateKey - 1; j++) {
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
        var x = (BigInteger)X;
        var y = (BigInteger)Y;
        // Modular division: (a / b) % M => (a × b^-1) % M, where b^-1 is modular inverse of b modulo M
        var l = MathUtils.Mod((3 * x * x + A) * MathUtils.ModularInverse(2 * y, P), P);
        var newX = MathUtils.Mod(l * l - x - x, P);
        return new((long)newX, (long)MathUtils.Mod(l * (x - newX) - y, P));
    }
}

static class MathUtils {
    /// Modulo operation that supports negative a
    public static BigInteger Mod(BigInteger a, BigInteger m) => (a % m + m) % m;

    /// Extended Euclidean algorithm
    public static BigInteger ModularInverse(BigInteger a, BigInteger n) {
        // r is remainder
        // t is one of Bézout coefficients
        BigInteger rPrevious = n, r = a;
        BigInteger tPrevious = 0, t = 1;
        // modular inverse t must satisfy: a * t ≡ 1 (mod n)
        while (r != 0) {
            var quotient = rPrevious / r;
            (rPrevious, r) = (r, rPrevious - quotient * r);
            (tPrevious, t) = (t, tPrevious - quotient * t);
        }
        // inverse exists only if gcd(a, n) = 1, i.e., a and n are coprime
        if (rPrevious != 1) {
            throw new ArgumentException("No modular inverse exists: a and n are not coprime");
        }
        // algorithm satisfies |t| < n
        return tPrevious < 0 ? tPrevious + n : tPrevious;
    }
}
