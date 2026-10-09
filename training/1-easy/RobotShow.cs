using System;
using System.Collections.Generic;
using System.Linq;

// https://www.codingame.com/training/easy/robot-show
int ductLength = Math.Clamp(int.Parse(Console.ReadLine()!), 2, 1000);
Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100); // botCount
int[] botPositions = [.. Console.ReadLine()!.Split(' ').Select(int.Parse).OrderBy(it => it)];
List<Bot> bots = [];
for (var i = 0; i < botPositions.Length; i++) {
    // only first looks right;
    // not optimal when there's single bot, and it's closer to the right
    bots.Add(new Bot(botPositions[i], i == 0 ? 1 : -1));
}
Console.WriteLine(SimulateBotMovement());
return;

int SimulateBotMovement() {
    var time = 0;
    for (; bots.Count > 0; time++) {
        // move and exit
        for (int i = bots.Count - 1; i >= 0; i--) {
            var bot = bots[i];
            bot.Position += bot.Direction;
            if (bot.Position == 0 || bot.Position == ductLength) {
                bots.RemoveAt(i);
            }
        }
        // turn around bots that collide
        foreach (var group in bots.GroupBy(it => it.Position)) {
            if (group.Count() == 1) {
                continue;
            }
            foreach (var bot in group) {
                bot.Direction *= -1;
            }
        }
    }
    return time;
}

class Bot(int position, int direction) {
    public int Position { get; set; } = position;
    public int Direction { get; set; } = direction;
}
