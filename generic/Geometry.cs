using System;

readonly record struct Point(int X, int Y) {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point[] Directions8 { get; } = [
        RIGHT, LEFT, UP, DOWN,
        new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)
    ];

    public bool InBounds(int width, int? height = null) {
        return X >= 0 && X < width && Y >= 0 && Y < (height ?? width);
    }

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public static double Distance(Point a, Point b) {
        int diffX = a.X - b.X;
        int diffY = a.Y - b.Y;
        return Math.Sqrt(diffX * diffX + diffY * diffY);
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, Point b) => new(a.X * b.X, a.Y * b.Y);
    public static Point operator -(Point a, int value) => new(a.X - value, a.Y - value);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);
    public static Point operator /(Point a, int value) => new(a.X / value, a.Y / value);

    public static implicit operator Vector(Point value) => new(value.X, value.Y);

    public override string ToString() => $"({X}, {Y})";
}

readonly record struct Vector(double X, double Y) {
    public static readonly Vector ZERO = new(0, 0);

    public double Magnitude() => Math.Sqrt(X * X + Y * Y);

    public Vector Normalized() {
        const double epsilon = 0.00001;
        double magnitude = Magnitude();
        return magnitude > epsilon ? this / magnitude : ZERO;
    }

    public static double Distance(Vector a, Vector b) {
        double diffX = a.X - b.X;
        double diffY = a.Y - b.Y;
        return Math.Sqrt(diffX * diffX + diffY * diffY);
    }

    /// Calculates the absolute heading of a vector. <br/>
    /// RIGHT is 0°, UP is 90°, LEFT is 180°, and DOWN is 270°
    /// <returns>The angle in degrees from 0 to 360.</returns>
    public static double Angle(Vector value) {
        double radians = Math.Atan2(value.Y, value.X);
        double degrees = double.RadiansToDegrees(radians);
        return degrees < 0 ? degrees + 360 : Math.Abs(degrees);
    }

    public static double Angle(Vector from, Vector to) {
        double fromAngle = Math.Atan2(from.Y, from.X) * (180 / Math.PI);
        double toAngle = Math.Atan2(to.Y, to.X) * (180 / Math.PI);
        return Angle(fromAngle, toAngle);
    }

    /// Calculates the shortest delta between two absolute angles.
    /// <returns>The steering delta in degrees between -180 and 180 (negative means turn Left/CCW, positive means turn Right/CW).</returns>
    public static double Angle(double fromAngle, double toAngle) {
        double delta = toAngle - fromAngle;
        if (delta <= -180) {
            delta += 360;
        }
        if (delta > 180) {
            delta -= 360;
        }
        return delta;
    }

    /// Returns Z component of 3D cross product of two 2D vectors.
    /// Positive = <paramref name="b"/> is to the left of <paramref name="a"/>,
    /// negative = right, zero = collinear (parallel).
    public static double Cross(Vector a, Vector b) => a.X * b.Y - a.Y * b.X;

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector operator *(Vector a, double value) => new(a.X * value, a.Y * value);
    public static Vector operator /(Vector a, double value) => new(a.X / value, a.Y / value);

    public static implicit operator Point(Vector value) => new((int)Math.Floor(value.X), (int)Math.Floor(value.Y));

    public override string ToString() => $"({X:F1}, {Y:F1})";
}

static class Screen {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point ONE = new(1, 1);
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static char ToChar(Point direction) => direction switch {
        _ when direction == UP => '^',
        _ when direction == DOWN => 'v',
        _ when direction == LEFT => '<',
        _ when direction == RIGHT => '>',
        _ => throw new ArgumentException($"Unsupported direction: {direction}")
    };

    public static string ToString(Point direction) => direction switch {
        _ when direction == UP => nameof(UP),
        _ when direction == DOWN => nameof(DOWN),
        _ when direction == LEFT => nameof(LEFT),
        _ when direction == RIGHT => nameof(RIGHT),
        _ => throw new ArgumentException($"Unsupported direction: {direction}")
    };
}

readonly record struct Rect(Point Min, Point Max) {
    public Point Size => Max - Min;
    public int Area => Size.X * Size.Y;

    public static Rect Square(Point min, int size) {
        var max = min + Point.ONE * size;
        return new Rect(min, max);
    }

    public bool Contains(Point point) {
        (int x, int y) = point;
        return x >= Min.X && y >= Min.Y && x < Max.X && y < Max.Y;
    }

    public bool Contains(Rect other) => Contains(other.Min) && Contains(other.Max - Point.ONE);

    public Rect? Intersect(Rect other) {
        int xMin = Math.Max(Min.X, other.Min.X);
        int xMax = Math.Min(Max.X, other.Max.X);
        int yMin = Math.Max(Min.Y, other.Min.Y);
        int yMax = Math.Min(Max.Y, other.Max.Y);
        if (xMin < xMax && yMin < yMax) {
            return new Rect(new Point(xMin, yMin), new Point(xMax, yMax));
        }
        return null;
    }

    public bool Intersects(Rect other) => Intersect(other) is not null;
}
