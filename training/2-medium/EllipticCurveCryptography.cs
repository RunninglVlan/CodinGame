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
    var publicKey = PublicKey(privateKey, g);
    Console.WriteLine($"0x{publicKey.X:x}");
}
return;

Point PublicKey(long privateKey, Point point) {
    var publicKey = point;
    for (var j = 0; j < privateKey - 1; j++) {
        publicKey = publicKey.Double();
    }
    return publicKey;
}

/// Point on Elliptic-Curve
readonly record struct Point(BigInteger X, BigInteger Y) {
    const long A = 0;
    const long B = 7;
    const long P = 0x3fddbf07bb3bc551;

    /// L  = (Yd - Yc) / (Xd - Xc) mod P (when C != D) <br/>
    /// L  = (3*Xc² + A) / (2*Yc)  mod P (when C == D) <br/>
    /// Xs = L² - Xc - Xd          mod P <br/>
    /// Ys = L * (Xc - Xs) - Yc    mod P
    public Point Double() {
        var l = MathUtils.ModularDivide(3 * X * X + A, 2 * Y, P);
        var newX = MathUtils.Mod(l * l - X - X, P);
        return new(newX, MathUtils.Mod(l * (X - newX) - Y, P));
    }
}

static class MathUtils {
    /// Modulo operation that supports negative a
    public static BigInteger Mod(BigInteger a, BigInteger m) => (a % m + m) % m;

    /// (a / b) % M => (a × b^-1) % M, where b^-1 is modular inverse of b modulo M
    public static BigInteger ModularDivide(BigInteger a, BigInteger b, BigInteger m) {
        BigInteger aMod = Mod(a, m), bMod = Mod(b, m);
        return Mod(aMod * ModularInverse(bMod, m), m);
    }

    /// Extended Euclidean algorithm
    static BigInteger ModularInverse(BigInteger a, BigInteger n) {
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
