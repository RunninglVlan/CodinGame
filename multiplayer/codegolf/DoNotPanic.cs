using System;
var l = Console.ReadLine().Split(' ');
var t = new int[int.Parse(l[0])];
t[int.Parse(l[3])] = int.Parse(l[4]);
int e = int.Parse(l[7]);
for (var i = 0; i < e; i++) {
    l = Console.ReadLine().Split(' ');
    t[int.Parse(l[0])] = int.Parse(l[1]);
}
while (true) {
    l = Console.ReadLine().Split(' ');
    int f = int.Parse(l[0]);
    int x = int.Parse(l[1]);
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