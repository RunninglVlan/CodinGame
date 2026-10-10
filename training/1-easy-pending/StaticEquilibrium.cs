using System;

// https://www.codingame.com/contribute/view/134886a1467b44cd36bccace7cef8fc3771021
int forces = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 1000);
var balancingForce = Vector.ZERO;
for (var _ = 0; _ < forces; _++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    int forceMagnitude = Math.Clamp(int.Parse(inputs[0]), 1, 1000_000);
    string direction = inputs[1];
    if (direction is Directions.X_POSITIVE or Directions.X_NEGATIVE) {
        balancingForce.X += direction == Directions.X_POSITIVE ? forceMagnitude : -forceMagnitude;
    } else {
        balancingForce.Y += direction == Directions.Y_POSITIVE ? forceMagnitude : -forceMagnitude;
    }
}
balancingForce *= -1;
Console.WriteLine(balancingForce.Magnitude().ToString("F2"));
double forceAngle = balancingForce == Vector.ZERO ? 0 : Vector.Angle(balancingForce);
Console.WriteLine(forceAngle.ToString("F2"));

static class Directions {
    public const string X_POSITIVE = "RIGHT";
    public const string X_NEGATIVE = "LEFT";
    public const string Y_POSITIVE = "UP";
    public const string Y_NEGATIVE = "DOWN";
}

record struct Vector(double X, double Y) {
    public static readonly Vector ZERO = new(0, 0);

    public double Magnitude() => Math.Sqrt(X * X + Y * Y);

    /// Calculates the absolute heading of a vector. <br/>
    /// RIGHT is 0°, UP is 90°, LEFT is 180°, and DOWN is 270°
    /// <returns>The angle in degrees from 0 to 360.</returns>
    public static double Angle(Vector value) {
        double radians = Math.Atan2(value.Y, value.X);
        double degrees = double.RadiansToDegrees(radians);
        return degrees < 0 ? degrees + 360 : Math.Abs(degrees);
    }

    public static Vector operator *(Vector a, double value) => new(a.X * value, a.Y * value);
}
