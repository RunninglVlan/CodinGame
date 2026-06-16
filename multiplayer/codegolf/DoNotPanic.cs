using System;
Func<string, int> P = int.Parse;
string[] S() => Console.ReadLine().Split(' ');
var l = S();
var t = new int[P(l[0])];
t[P(l[3])] = P(l[4]);
int e = P(l[7]);
for (var i = 0; i < e; i++) {
    l = S();
    t[P(l[0])] = P(l[1]);
}
while (true) {
    l = S();
    int f = P(l[0]);
    int x = P(l[1]);
    Console.WriteLine(f < 0 ? "WAIT" : B(x, t[f], l[2]));
}
static string B(int x, int t, string d) {
    if (t < x) {
        return d == "RIGHT" ? "BLOCK" : "WAIT";
    }
    if (t > x) {
        return d == "LEFT" ? "BLOCK" : "WAIT";
    }
    return "WAIT";
}