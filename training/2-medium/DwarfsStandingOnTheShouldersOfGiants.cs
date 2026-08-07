using System;

/// https://www.codingame.com/training/medium/dwarfs-standing-on-the-shoulders-of-giants
static class DwarfsStandingOnTheShouldersOfGiants {
    static void Main() {
        int n = int.Parse(Console.ReadLine()!); // the number of relationships of influence
        for (var i = 0; i < n; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int x = int.Parse(inputs[0]); // a relationship of influence between two people (x influences y)
            int y = int.Parse(inputs[1]);
        }
        // the number of people involved in the longest succession of influences
        Console.WriteLine(2);
    }
}
