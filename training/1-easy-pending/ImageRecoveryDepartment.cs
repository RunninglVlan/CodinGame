using System;
using System.Linq;
using System.Text;

// https://www.codingame.com/contribute/view/1508259d38553ca4273153ae30ab92e5b04982
string[] inputs = Console.ReadLine()!.Split(' ');
int height = Math.Clamp(int.Parse(inputs[0]), 1, 50);
int width = Math.Clamp(int.Parse(inputs[1]), 1, 50);
Math.Clamp(int.Parse(Console.ReadLine()!), 1, 150); // logLength
var transformations = Console.ReadLine()!.Split("  ")
        .Select(it => it.Split(' '))
        .Select(it => {
            int radius = int.Parse(it[3]);
            int x = Math.Clamp(int.Parse(it[1]), radius, width - radius - 1);
            int y = Math.Clamp(int.Parse(it[0]), radius, height - radius - 1);
            int turns = Math.Clamp(int.Parse(it[2]), -10_000, 10_000);
            return new Transformation(new Point(x, y), turns, radius);
        })
        .ToArray();
var image = new Image(width, height);
image.Read();
image.Undo(transformations);
Console.WriteLine(image);

/// Transformation square
/// <param name="Turns">90° rotations, positive - CCW, negative - CW</param>
readonly record struct Transformation(Point Center, int Turns, int Radius);

class Image(int width, int height) {
    readonly char[,] cells = new char[width, height];

    public void Read() {
        for (var y = 0; y < height; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < width; x++) {
                cells[x, y] = line[x];
            }
        }
    }

    public void Undo(Transformation[] transformations) {
        Array.Reverse(transformations);
        foreach (var transformation in transformations) {
            var min = transformation.Center - transformation.Radius;
            var region = new SquareRegion(min, transformation.Radius);
            region.Crop(cells);
            int rightTurns = (-transformation.Turns % 4 + 4) % 4;
            for (var _ = 0; _ < rightTurns; _++) {
                region.TurnRight();
            }
            region.Paste(cells);
        }
    }

    public override string ToString() {
        var lines = new StringBuilder();
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                lines.Append(cells[x, y]);
            }
            if (y < height - 1) {
                lines.AppendLine();
            }
        }
        return lines.ToString();
    }
}

class SquareRegion(Point min, int radius) {
    char[,] cells = new char[2 * radius + 1, 2 * radius + 1];

    int Side => cells.GetLength(0);

    public void Crop(char[,] image) {
        for (int y = min.Y; y < min.Y + Side; y++) {
            for (int x = min.X; x < min.X + Side; x++) {
                cells[x - min.X, y - min.Y] = image[x, y];
            }
        }
    }

    public void Paste(char[,] image) {
        for (int y = min.Y; y < min.Y + Side; y++) {
            for (int x = min.X; x < min.X + Side; x++) {
                image[x, y] = cells[x - min.X, y - min.Y];
            }
        }
    }

    /// Turns CW
    public void TurnRight() {
        var target = new char[Side, Side];
        target[radius, radius] = cells[radius, radius];
        for (var r = 1; r <= radius; r++) {
            int side = 2 * r + 1;
            for (int x = 0, y = 0; x < side - 1; x++) {
                char value = SourceAt(x, y, r);
                SetTargetAt(side - 1, x, r, value);
            }
            for (int x = side - 1, y = 0; y < side - 1; y++) {
                char value = SourceAt(x, y, r);
                SetTargetAt(side - 1 - y, x, r, value);
            }
            for (int x = side - 1, y = side - 1; x > 0; x--) {
                char value = SourceAt(x, y, r);
                SetTargetAt(0, x, r, value);
            }
            for (int x = 0, y = side - 1; y > 0; y--) {
                char value = SourceAt(x, y, r);
                SetTargetAt(side - 1 - y, 0, r, value);
            }
        }
        cells = target;
        return;

        char SourceAt(int x, int y, int r) => cells[x + radius - r, y + radius - r];
        void SetTargetAt(int x, int y, int r, char value) => target[x + radius - r, y + radius - r] = value;
    }
}

readonly record struct Point(int X, int Y) {
    public static Point operator -(Point a, int value) => new(a.X - value, a.Y - value);
}
