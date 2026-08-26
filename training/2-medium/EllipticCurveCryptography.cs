using System;
using System.Globalization;
using System.Linq;
using System.Numerics;

// https://www.codingame.com/training/medium/elliptic-curve-cryptography
// b = 7 is unused
var curve = new EllipticCurve(0, 0x3fddbf07bb3bc551);
var g = new Point(0x69d463ce83b758e, 0x287a120903f7ef5c);
int keys = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
for (var i = 0; i < keys; i++) {
    string[] line = Console.ReadLine()!.Split('x');
    long privateKey = long.Parse(line.Last(), NumberStyles.HexNumber);
    var publicKey = curve.PublicKey(privateKey, g);
    Console.WriteLine($"0x{(long)publicKey.X:x}");
}

/// Y² = X³ + A*X + B mod P
class EllipticCurve(long a, long p) {
    public Point PublicKey(long privateKey, Point point) {
        var sum = Double(point);
        for (var i = 2; i < privateKey; i++) {
            sum = Add(sum, point);
        }
        return sum;
    }

    /// L = (3*Xc² + A) / (2*Yc) mod P
    Point Double(Point c) {
        var l = MathUtils.ModularDivide(3 * c.X * c.X + a, 2 * c.Y, p);
        return Add(l, c, c);
    }

    /// L = (Yd - Yc) / (Xd - Xc) mod P
    Point Add(Point c, Point d) {
        var l = MathUtils.ModularDivide(d.Y - c.Y, d.X - c.X, p);
        return Add(l, c, d);
    }

    /// Xs = L² - Xc - Xd       mod P <br/>
    /// Ys = L * (Xc - Xs) - Yc mod P
    Point Add(BigInteger l, Point c, Point d) {
        var newX = MathUtils.Mod(l * l - c.X - d.X, p);
        return new(newX, MathUtils.Mod(l * (c.X - newX) - c.Y, p));
    }
}

readonly record struct Point(BigInteger X, BigInteger Y);

static class MathUtils {
    /// Modulo operation that supports negative a
    public static BigInteger Mod(BigInteger a, BigInteger m) => (a % m + m) % m;

    /// (a / b) % M => (a × b⁻¹) % M, where b⁻¹ is modular inverse of b modulo M
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
