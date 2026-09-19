using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/organic-compounds
int compoundLines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 20);
Dictionary<Point, Element> compound = [];
for (var y = 0; y < compoundLines; y++) {
    string compoundLine = Console.ReadLine()!;
    for (var x = 0; x < compoundLine.Length; x += Element.LENGTH) {
        string elementChars = compoundLine.Substring(x, Element.LENGTH);
        var element = elementChars.First() switch {
            'C' => new Element(int.Parse(elementChars[2..3]), ElementType.Unit),
            _ => new Element(int.Parse(elementChars[1..2]), ElementType.Bond)
        };
        compound[new Point(x / Element.LENGTH, y)] = element;
    }
}
foreach (var (point, element) in compound) {
    if (element.Type != ElementType.Unit) {
        continue;
    }
    var bonds = Bonds(point);
    if (bonds.Sum(bond => bond.Count) != Element.MAX_BONDS - element.Count) {
        Console.WriteLine("INVALID");
        return;
    }
}
Console.WriteLine("VALID");
return;

IEnumerable<Element> Bonds(Point point) {
    foreach (var direction in Point.Directions4) {
        if (!compound.TryGetValue(point + direction, out var neighbor)) {
            continue;
        }
        if (neighbor.Type != ElementType.Bond) {
            continue;
        }
        yield return neighbor;
    }
}

record struct Element(int Count, ElementType Type) {
    public const int LENGTH = 3;
    public const int MAX_BONDS = 4;
}

enum ElementType {
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
