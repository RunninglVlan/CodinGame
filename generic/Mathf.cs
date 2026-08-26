static class Mathf {
    public static bool Approximately(double a, double b) => Math.Abs(a - b) < 0.01;

    /// Euclidean algorithm
    public static long GreatestCommonDivisor(long a, long b) {
        // r is remainder
        long rPrevious = a, r = b;
        while (r != 0) {
            (rPrevious, r) = (r, rPrevious % r);
        }
        return rPrevious;
    }
}
