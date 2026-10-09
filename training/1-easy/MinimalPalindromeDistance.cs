using System;

// https://www.codingame.com/training/easy/minimal-palindrome-distance
int length = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 10_000 + 1);
string notPalindrome = Console.ReadLine()!;
var count = 0;
for (var i = 0; i < length; i++) {
    if (notPalindrome[i] == notPalindrome[length - 1]) {
        break;
    }
    count++;
}
Console.WriteLine(count);
