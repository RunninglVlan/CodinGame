using System;

readonly record struct Point(int X, int Y) {
    public static readonly Point UP = new(0, 1);
    public static readonly Point DOWN = new(0, -1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point[] Directions8 { get; } = [
        RIGHT, LEFT, UP, DOWN,
        new(1, 1), new(-1, 1), new(1, -1), new(-1, -1)
    ];

    public bool InBounds(int width, int height) {
        return X >= 0 && X < width && Y >= 0 && Y < height;
    }

    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
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

    public static implicit operator Vector(Point value) => new(value.X, value.Y);

    public override string ToString() => $"({X}, {Y})";
}

readonly record struct Vector(double X, double Y) {
    public static readonly Vector ZERO = new(0, 0);

    public static Vector From(string x, string y) {
        return new Vector(int.Parse(x), int.Parse(y));
    }

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

    /// Calculates the absolute heading of a vector.<br/>
    /// RIGHT is 0°, UP is 90°, LEFT is 180° and DOWN is 270°
    /// <returns>The angle in degrees from 0 to 360.</returns>
    public static double Angle(Vector value) {
        double angle = Math.Atan2(value.Y, value.X) * (180 / Math.PI);
        return angle < 0 ? angle + 360 : angle;
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

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector operator *(Vector a, double value) => new(a.X * value, a.Y * value);
    public static Vector operator /(Vector a, double value) => new(a.X / value, a.Y / value);

    public static implicit operator Point(Vector value) => new((int)Math.Floor(value.X), (int)Math.Floor(value.Y));

    public override string ToString() => $"({X:F1}, {Y:F1})";
}

static class Screen {
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static string ToString(Point direction) => direction switch {
        (0, -1) => nameof(UP),
        (0, 1) => nameof(DOWN),
        (-1, 0) => nameof(LEFT),
        (1, 0) => nameof(RIGHT),
        _ => direction.ToString()
    };
}
