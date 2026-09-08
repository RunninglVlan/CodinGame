using System;

// Visual
// Connect towns with your train tracks and disrupt the opponent's
// https://www.codingame.com/contests/summer-challenge-2026-back-track-king
string[] inputs;
int myId = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 1);
int width = Math.Clamp(int.Parse(Console.ReadLine()!), 21, 30); // map size
int height = Math.Clamp(int.Parse(Console.ReadLine()!), 14, 20);
for (var i = 0; i < height; i++) {
    for (var j = 0; j < width; j++) {
        inputs = Console.ReadLine()!.Split(' ');
        int regionId = int.Parse(inputs[0]);
        int type = int.Parse(inputs[1]); // 0 (PLAINS), 1 (RIVER), 2 (MOUNTAIN), 3 (POI)
    }
}
int townCount = Math.Clamp(int.Parse(Console.ReadLine()!), 4, 12);
for (var i = 0; i < townCount; i++) {
    inputs = Console.ReadLine()!.Split(' ');
    int townId = int.Parse(inputs[0]);
    int townX = int.Parse(inputs[1]);
    int townY = int.Parse(inputs[2]);
    string desiredConnections = inputs[3]; // comma-separated town ids e.g. 0,1,2,3
}

// game loop
while (true) {
    int myScore = int.Parse(Console.ReadLine()!);
    int foeScore = int.Parse(Console.ReadLine()!);
    for (var i = 0; i < height; i++) {
        for (var j = 0; j < width; j++) {
            inputs = Console.ReadLine()!.Split(' ');
            int tracksOwner = int.Parse(inputs[0]);
            int instability = int.Parse(inputs[1]); // region inked (destroyed) when this >= 3.
            bool inked = inputs[2] != "0"; // true if region is destroyed.
            string partOfActiveConnections = inputs[3]; // if this cell is part of one or more railway connections, this will be town ids (separated by -) in a list separated by commas. e.g. 0-1,1-2,1-3. "x" otherwise.
        }
    }

    // AUTOPLACE x1 y1 x2 | PLACE_TRACKS x y | DISRUPT regionId | MESSAGE text
    Console.WriteLine("WAIT");
}
