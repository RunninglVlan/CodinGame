using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/organic-compounds
int compoundLines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
var compound = new Compound();
for (var y = 0; y < compoundLines; y++) {
    string compoundLine = Console.ReadLine()!;
    for (var x = 0; x < compoundLine.Length; x += Part.LENGTH) {
        string part = compoundLine.Substring(x, Part.LENGTH);
        var point = new Point(x / Part.LENGTH, y);
        compound.AddPart(point, part.First() switch {
            'C' => new Part(int.Parse(part.Substring(Part.UNIT_INDEX, 1)), PartType.Unit),
            _ => new Part(int.Parse(part.Substring(Part.BOND_INDEX, 1)), PartType.Bond)
        });
    }
}
Console.WriteLine(compound.IsValid() ? "VALID" : "INVALID");

class Compound {
    const int MAX_BONDS = 4;

    readonly Dictionary<Point, Part> parts = [];

    public void AddPart(Point point, Part part) {
        parts.Add(point, part);
    }

    public bool IsValid() {
        foreach (var (point, part) in parts) {
            if (part.Type != PartType.Unit) {
                continue;
            }
            var bonds = Bonds(point);
            if (bonds.Sum(bond => bond.Count) != MAX_BONDS - part.Count) {
                return false;
            }
        }
        return true;
    }

    IEnumerable<Part> Bonds(Point point) {
        foreach (var direction in Point.Directions4) {
            if (!parts.TryGetValue(point + direction, out var neighbor)) {
                continue;
            }
            if (neighbor.Type != PartType.Bond) {
                continue;
            }
            yield return neighbor;
        }
    }
}

record struct Part(int Count, PartType Type) {
    public const int LENGTH = 3;
    public const int BOND_INDEX = 1;
    public const int UNIT_INDEX = 2;
}

enum PartType {
    Unit,
    Bond
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
}
