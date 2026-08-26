using System;

// https://www.codingame.com/training/easy/blending-colors
string[] inputs = Console.ReadLine()!.Split(' ');
int shapes = Math.Clamp(int.Parse(inputs[0]), 1, 50);
int points = Math.Clamp(int.Parse(inputs[1]), 1, 50);
for (var _ = 0; _ < shapes; _++) {
    string[] line = Console.ReadLine()!.Split(' ');
    string name = line[0];
    var point = ParsePoint(line[1], line[2]);
    int length = Math.Clamp(int.Parse(line[3]), 1, 500);
    var color = Color.Parse(line[4], line[5], line[6]);
}
for (var _ = 0; _ < points; _++) {
    string[] coordinates = Console.ReadLine()!.Split(' ');
    var point = ParsePoint(coordinates[0], coordinates[1]);
    Console.WriteLine(Color.WHITE);
}
return;

Point ParsePoint(string x, string y) => new(Math.Clamp(int.Parse(x), 0, 1000), Math.Clamp(int.Parse(y), 0, 1000));

readonly record struct Point(int X, int Y);

readonly record struct Color(int R, int G, int B) {
    public static readonly Color WHITE = new(255, 255, 255);

    public static Color Parse(string r, string g, string b) => new(
            Math.Clamp(int.Parse(r), 0, 255),
            Math.Clamp(int.Parse(g), 0, 255),
            Math.Clamp(int.Parse(b), 0, 255)
    );

    public override string ToString() => $"({R}, {G}, {B})";
}
