using System;
using System.Linq;

// https://www.codingame.com/contribute/view/154026f4c56967fa2dfb15ca7bbd572c9fe863
Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100); // cartLines
var carts = Console.ReadLine()!.Split(' ')
        .Select((it, i) => new Cart(i) { Count = Math.Clamp(int.Parse(it), 0, 250) })
        .ToList();
string actions = Console.ReadLine()!;
foreach (char action in actions) {
    if (action == Actions.TAKE) {
        var cart = carts.OrderByDescending(it => it.Count).ThenBy(it => it.Index).First();
        cart.Count--;
    } else {
        var cart = carts.OrderBy(it => it.Count).ThenByDescending(it => it.Index).First();
        cart.Count++;
    }
}
Console.WriteLine(string.Join(' ', carts.Select(it => it.Count)));

record Cart(int Index) {
    public int Count { get; set; }
}

static class Actions {
    public const char TAKE = 'T';
    public const char RETURN = 'R';
}
