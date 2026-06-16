using System;
Func<string, int> P = int.Parse;
string[] S() => Console.ReadLine().Split(' ');
var l = S();
var t = new int[15];
t[P(l[3])] = P(l[4]);
int e = P(l[7]),f,x;
while (e-->0) {
    l = S();
    t[P(l[0])] = P(l[1]);
}
for (;;Console.WriteLine(f < 0 ? "WAIT" : t[f] < x && l[2][0] == 'R' || t[f] > x && l[2][0] == 'L' ? "BLOCK" : "WAIT")) {
    l = S();
    f = P(l[0]);
    x = P(l[1]);
}