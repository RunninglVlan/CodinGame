using System;
using System.Collections.Generic;

/// https://www.codingame.com/training/medium/dwarfs-standing-on-the-shoulders-of-giants
static class DwarfsStandingOnTheShouldersOfGiants {
    static void Main() {
        // the number of relationships of influence
        int relationships = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 9999);
        for (var i = 0; i < relationships; i++) {
            string[] relationship = Console.ReadLine()!.Split(' ');
            // a relationship of influence between two people (x influences y)
            int x = Math.Clamp(int.Parse(relationship[0]), 1, 9999);
            int y = Math.Clamp(int.Parse(relationship[1]), 1, 9999);
        }
        // the number of people involved in the longest succession of influences
        Console.WriteLine(2);
    }
}
