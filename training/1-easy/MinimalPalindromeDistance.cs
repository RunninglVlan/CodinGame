using System;

// https://www.codingame.com/training/easy/minimal-palindrome-distance
int length = Math.Clamp(int.Parse(Console.ReadLine()!), 0, 10_000 - 1);
string notPalindrome = Console.ReadLine()!;
var count = 0;
for (var i = 0; i < length; i++) {
    if (notPalindrome[i] == notPalindrome[length - 1] && IsPalindrome(notPalindrome[i..])) {
        break;
    }
    count++;
}
Console.WriteLine(count);
return;

bool IsPalindrome(string candidate) {
    for (var i = 0; i < candidate.Length / 2; i++) {
        if (candidate[i] != candidate[candidate.Length - 1 - i]) {
            return false;
        }
    }
    return true;
}
