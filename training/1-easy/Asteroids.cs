using System;
using System.Collections.Generic;
using System.Numerics;

/// https://www.codingame.com/training/easy/asteroids
static class Asteroids {
    const char NOTHING = '.';
    static readonly Dictionary<char, Asteroid> asteroids = new();

    static void Main() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int width = int.Parse(inputs[0]), height = int.Parse(inputs[1]);
        int t1 = int.Parse(inputs[2]);
        int t2 = int.Parse(inputs[3]);
        int t3 = int.Parse(inputs[4]);
        for (var y = 0; y < height; y++) {
            string[] row = Console.ReadLine()!.Split(' ');
            for (var x = 0; x < width; x++) {
                for (var index = 0; index < 2; index++) {
                    string skyPictureRow = row[index];
                    char skySymbol = skyPictureRow[x];
                    int time = index == 0 ? t1 : t2;
                    if (skySymbol != NOTHING) {
                        AddAsteroid(skySymbol, time, new Vector2(x, y));
                    }
                }
            }
        }
        char[,] skyPictureAtT3 = SkyPicture(t3, width, height);
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                char value = skyPictureAtT3[x, y];
                Console.Write(value == 0 ? NOTHING : value);
            }
            Console.WriteLine();
        }
    }

    static void AddAsteroid(char name, int time, Vector2 position) {
        if (!asteroids.TryGetValue(name, out var asteroid)) {
            asteroids[name] = asteroid = new Asteroid();
        }
        asteroid.AddPositionAtTime(position, time);
    }

    static char[,] SkyPicture(int time, int width, int height) {
        var picture = new char[width, height];
        foreach ((char name, var asteroid) in asteroids) {
            var position = asteroid.PositionAtTime(time);
            int x = (int)Math.Floor(position.X), y = (int)Math.Floor(position.Y);
            if (x < 0 || x > picture.GetLength(0) - 1 || y < 0 || y > picture.GetLength(1) - 1) {
                continue;
            }
            char value = picture[x, y];
            if (value != 0 && value < name) {
                continue;
            }
            picture[x, y] = name;
        }
        return picture;
    }
}

class Asteroid {
    int minTime = int.MaxValue;
    int maxTime = int.MinValue;
    readonly Dictionary<int, Vector2> positions = new();

    public void AddPositionAtTime(Vector2 position, int time) {
        positions[time] = position;
        minTime = Math.Min(time, minTime);
        maxTime = Math.Max(time, maxTime);
    }

    public Vector2 PositionAtTime(int time) {
        Vector2 maxTimePosition = positions[maxTime], minTimePosition = positions[minTime];
        int timeDifference = maxTime - minTime;
        var motion = (maxTimePosition - minTimePosition) / timeDifference;
        if (motion == Vector2.Zero) {
            return minTimePosition;
        }
        int deltaTime = time - minTime;
        var deltaPosition = motion * deltaTime;
        var positionAtTime = minTimePosition + deltaPosition;
        return positionAtTime;
    }
}
