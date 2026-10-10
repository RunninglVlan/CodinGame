using System;

// https://www.codingame.com/contribute/view/1537621fc92617df1db4cda018100dceb3c47f
string mode = Console.ReadLine()!;
int players = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
for (var i = 0; i < players; i++) {
    string[] inputs = Console.ReadLine()!.Split(' ');
    string name = inputs[0];
    string hand = inputs[1];
}
int words = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 50);
for (var i = 0; i < words; i++) {
    string word = Console.ReadLine()!;
}
Console.WriteLine("winner_name winner_word");
