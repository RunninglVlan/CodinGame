using System;
Func<string, int> P = int.Parse;
string[] S() => Console.ReadLine().Split(' ');
var l = S();
var t = new int[15];
t[P(l[3])] = P(l[4]);
int e = P(l[7]);
while (e-->0) {
    l = S();
    t[P(l[0])] = P(l[1]);
}
for (;;) {
    l = S();
    int f = P(l[0]);
    int x = P(l[1]);
    var d = l[2];
    Console.WriteLine(f < 0 ? "WAIT" : t[f] < x && d == "RIGHT" || t[f] > x && d == "LEFT" ? "BLOCK" : "WAIT");
}