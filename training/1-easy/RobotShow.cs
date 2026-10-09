using System;

// https://www.codingame.com/training/easy/robot-show
int ductLength = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 1000);
int botCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
string[] input = Console.ReadLine()!.Split(' ');
// collisions don't matter;
// when bots collide and move in opposite directions, it's the same as if they passed through each other;
// so max time is just the max time a bot would need to reach either exit
var maxTime = 0;
for (var i = 0; i < botCount; i++) {
    int bot = int.Parse(input[i]);
    int time = Math.Max(bot, ductLength - bot);
    maxTime = Math.Max(maxTime, time);
}
Console.WriteLine(maxTime);
