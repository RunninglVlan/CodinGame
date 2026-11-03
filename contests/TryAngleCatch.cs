using System;

// Visual
// https://www.codingame.com/contests/fall-challenge-2026
var game = new Game();
game.Init();
while (true) {
    game.Loop();
}

class Game {
    int houseCount;

    public void Init() {
        houseCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
        for (var i = 0; i < houseCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int houseId = int.Parse(inputs[0]);
            int houseX = Math.Clamp(int.Parse(inputs[1]), 0, 1920 - 1);
            int houseY = Math.Clamp(int.Parse(inputs[2]), 0, 1080 - 1);
        }
    }

    public void Loop() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        int myScore = int.Parse(inputs[0]);
        int opponentScore = int.Parse(inputs[1]);
        for (var i = 0; i < houseCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int houseId = int.Parse(inputs[0]);
            int myUnits = int.Parse(inputs[1]);
            int opponentUnits = int.Parse(inputs[2]);
        }
        int pathCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < pathCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(inputs[0]);
            int house2 = int.Parse(inputs[1]);
        }
        int triangleCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < triangleCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(inputs[0]);
            int house2 = int.Parse(inputs[1]);
            int house3 = int.Parse(inputs[2]);
            int owner = int.Parse(inputs[3]);
            bool meCanCapture = inputs[4] != "0";
            bool opponentCanCapture = inputs[5] != "0";
        }
        int linkableCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < linkableCount; i++) {
            inputs = Console.ReadLine()!.Split(' ');
            int house1 = int.Parse(inputs[0]);
            int house2 = int.Parse(inputs[1]);
        }
        Console.WriteLine("MOVE 3 17 1");
    }
}
