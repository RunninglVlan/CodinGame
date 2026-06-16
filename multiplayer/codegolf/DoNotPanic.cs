using System;

string[] inputs = Console.ReadLine()!.Split(' ');
int floors = int.Parse(inputs[0]);
var targets = new int[floors];
int exitFloor = int.Parse(inputs[3]);
int exitX = int.Parse(inputs[4]);
targets[exitFloor] = exitX;
int elevatorCount = int.Parse(inputs[7]);
for (var i = 0; i < elevatorCount; i++) {
    inputs = Console.ReadLine()!.Split(' ');
    int elevatorFloor = int.Parse(inputs[0]);
    int elevatorX = int.Parse(inputs[1]);
    targets[elevatorFloor] = elevatorX;
}
while (true) {
    inputs = Console.ReadLine()!.Split(' ');
    int floor = int.Parse(inputs[0]);
    int x = int.Parse(inputs[1]);
    string direction = inputs[2];
    bool noLeadingClone = floor < 0;
    Console.WriteLine(noLeadingClone ? "WAIT" : BlockIfMovingInWrongDirection(x, targets[floor], direction));
}

static string BlockIfMovingInWrongDirection(int x, int target, string direction) {
    if (target < x) {
        return direction == "RIGHT" ? "BLOCK" : "WAIT";
    }
    if (target > x) {
        return direction == "LEFT" ? "BLOCK" : "WAIT";
    }
    return "WAIT";
}
