using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TrollFarm;

// TODO: First update with Wood 1 rules:
// $"PLANT {id} {type}" Make troll id plant a type on its current cell: PLUM, LEMON, APPLE or BANANA.
//  A troll can PLANT a tree on its current cell if the troll carries at least 1 fruit of that type.
//  If two trolls try to plant a tree of the same type on the same cell, both will lose a seed and the tree gets planted.
//  If they try to plant different types, nothing will happen.
// $"PICK {id} {type}" Make troll id pick one type from the shack: PLUM, LEMON, APPLE or BANANA.
//  A troll can PICK one item from its shack. Only 1 item can be picked at a time.

// TODO: Then update with Bronze rules:
// "MINE {id}" Make troll id mine a nearby IRON.
//  A troll adjacent to an IRON cell can MINE, gaining up to chopPower iron limited by free carryCapacity. Iron resources are infinite.
// "CHOP {id}" Make troll id chop on its current cell.
//  A troll can CHOP a tree on the same cell, reducing its health by chopPower.
//  When health reaches 0, the tree disappears and the troll collects wood equal to its size, capped by free carryCapacity.
//  Any wood that the troll can't carry will just vanish.
//  Similar to harvesting, both trolls get one wood at a time until they can't carry any more or the wood is completely split up.
//  The last wood can get duplicated.
// Training trolls:
//  chopPower requires IRON
//  Cost example: 0 chopPower: 2 + 0×0 = 2 IRONs
// Trees grow faster when next to water.
// Trees differ in cooldown and health as follows:
//                      PLUM  LEMON APPLE  BANANA
//  Cooldown            8     8     9      6
//  Cooldown near water 3     3     2      4
//  Health (size 1)     6     6     11     3
//  Health (size 2)     8     8     14     4
//  Health (size 3)     10    10    17     5
//  Health (size 4)     12    12    20     6
// When a damaged tree grows, it gains the health difference between sizes.
// The game turn order is:
//  Move trolls
//  Harvest
//  Plant
//  Chop
//  Pick
//  Train
//  Drop
//  Mine
//  Grow trees
// Actions of the same type occur simultaneously. If both teams harvest or chop the same tree at once, resources are shared equally.
//  Then both get one item at a time until they can't carry any more or the resource is exhausted. The last item can get duplicated.

/// https://www.codingame.com/contests/spring-challenge-2026-troll-farm
static class TrollFarm {
    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    static void Main() {
        var game = new Game();
        game.Init();
        while (true) {
            game.Loop();
        }
    }
}

class Game {
    readonly Grid grid = new();
    readonly Pathfinding pathfinding;
    readonly Dictionary<int, Troll> trolls = new();
    readonly Inventory[] inventories = new Inventory[Inventory.COUNT];
    Inventory MyInventory => inventories[Inventory.ME];

    public Game() => pathfinding = new(grid);

    public void Init() {
        grid.Init();
        for (var i = 0; i < Inventory.COUNT; i++) {
            inventories[i] = new Inventory();
        }
    }

    public void Loop() {
        ReadInput();
        Console.WriteLine(string.Join(';', Actions()));
    }

    void ReadInput() {
        grid.ClearOldData();
        for (var i = 0; i < Inventory.COUNT; i++) {
            inventories[i].ReadNewData();
        }
        int treesCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < treesCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            var point = Point.From(inputs[1], inputs[2]);
            grid.Get(point).tree = Tree.From(inputs);
        }
        int trollsCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < trollsCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            int id = int.Parse(inputs[0]);
            var point = Point.From(inputs[2], inputs[3]);
            if (!trolls.TryGetValue(id, out var troll)) {
                trolls[id] = troll = Troll.From(id, inputs);
            }
            troll.Update(point, inputs);
            grid.Get(point).troll = troll;
        }
    }

    IEnumerable<IAction> Actions() {
        if (CanTrain(out int movementSpeed, out int carryCapacity, out int harvestPower, out int chopPower)) {
            yield return new Train(movementSpeed, carryCapacity, harvestPower, chopPower);
        }
        foreach (var action in trolls.Values.Where(it => it.Player == Player.Me).Select(TrollAction)) {
            yield return action;
        }
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    bool CanTrain(out int movementSpeed, out int carryCapacity, out int harvestPower, out int chopPower) {
        movementSpeed = carryCapacity = harvestPower = 1;
        chopPower = 0;
        int trollCount = trolls.Values.Count(it => it.Player == Player.Me);
        if (MyInventory.plum < Required(movementSpeed)) {
            return false;
        }
        if (MyInventory.lemon < Required(carryCapacity)) {
            return false;
        }
        if (MyInventory.apple < Required(harvestPower)) {
            return false;
        }
        if (MyInventory.iron < Required(chopPower)) {
            return false;
        }
        return true;

        int Required(int count) => trollCount + count * count;
    }

    IAction TrollAction(Troll troll) {
        var cell = grid.Get(troll.Point);
        if (troll.CanCarry && cell.tree is { Fruits: > 0 }) {
            return troll.Harvest(cell.tree);
        }
        if (troll.IsCarrying) {
            if (grid.Neighbors(cell).Any(it => it == grid.myShack)) {
                return new Drop(troll.Id);
            }
            return new Move(troll.Id, grid.myShack.point);
        }
        var path = pathfinding.Path(troll.Point, it => it.tree is { Fruits: > 0 });
        return new Move(troll.Id, path![troll.MovementSpeed - 1]);
    }
}

class Grid {
    int width, height;
    Cell[,] cells = null!;
    public Cell myShack = null!;

    public void Init() {
        string[] dimensions = Console.ReadLine()!.Split(' ');
        height = Math.Clamp(int.Parse(dimensions[1]), 8, 11);
        width = 2 * height;
        cells = new Cell[width, height];
        for (var y = 0; y < height; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < line.Length; x++) {
                char c = line[x];
                var cell = new Cell(x, y);
                cells[x, y] = cell;
                cell.type = c switch {
                    '.' => CellType.Grass,
                    '~' => CellType.Water,
                    '#' => CellType.Rock,
                    '+' => CellType.Iron,
                    '0' => CellType.MyShack,
                    '1' => CellType.EnemyShack,
                    _ => throw new ArgumentException($"Unexpected cell type: {c}")
                };
                if (cell.type == CellType.MyShack) {
                    myShack = cell;
                }
            }
        }
    }

    public Cell Get(Point point) => cells[point.X, point.Y];

    bool TryGet(Point point, out Cell cell) {
        cell = null!;
        if (point.X < 0 || point.X >= width || point.Y < 0 || point.Y >= height) {
            return false;
        }
        cell = Get(point);
        return true;
    }

    public void ClearOldData() {
        foreach (var cell in cells) {
            cell.ClearOldData();
        }
    }

    public IEnumerable<Cell> Neighbors(Cell cell) {
        foreach (var point in Point.Directions4) {
            if (TryGet(cell.point + point, out var neighbor)) {
                yield return neighbor;
            }
        }
    }
}

class Cell(int x, int y) {
    public Point point = new(x, y);
    public CellType type;
    public Tree? tree;
    public Troll? troll;
    public bool IsWalkable => type == CellType.Grass;

    public void ClearOldData() {
        tree = null;
        troll = null;
    }
}

enum CellType {
    Grass,
    Water,
    Rock,
    Iron,
    MyShack,
    EnemyShack
}

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
record Tree(string Type, int Size, int Health, int Cooldown) {
    public int Fruits { get; set; }

    public static Tree From(string[] inputs) {
        string type = inputs[0];
        int size = int.Parse(inputs[3]);
        int health = int.Parse(inputs[4]);
        int fruits = int.Parse(inputs[5]);
        int cooldown = int.Parse(inputs[6]);
        return new Tree(type, size, health, cooldown) { Fruits = fruits };
    }
}

/// <param name="MovementSpeed">determines how many cells it can MOVE horizontally or vertically per turn</param>
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
record Troll(int Id, Player Player, int MovementSpeed, int CarryCapacity, int HarvestPower, int ChopPower) {
    public Point Point { get; private set; }
    public int CarryPlum { get; set; }
    public int CarryLemon { get; set; }
    public int CarryApple { get; set; }
    public int CarryBanana { get; set; }
    public int CarryIron { get; set; }
    public int CarryWood { get; set; }
    public bool IsCarrying => CarryTotal > 0;
    int CarryTotal => CarryPlum + CarryLemon + CarryApple + CarryBanana + CarryIron + CarryWood;
    public bool CanCarry => CarryTotal < CarryCapacity;

    public static Troll From(int id, string[] inputs) {
        var player = (Player)int.Parse(inputs[1]);
        int movementSpeed = int.Parse(inputs[4]);
        int carryCapacity = int.Parse(inputs[5]);
        int harvestPower = int.Parse(inputs[6]);
        int chopPower = int.Parse(inputs[7]);
        return new Troll(id, player, movementSpeed, carryCapacity, harvestPower, chopPower);
    }

    public void Update(Point point, string[] inputs) {
        Point = point;
        CarryPlum = int.Parse(inputs[8]);
        CarryLemon = int.Parse(inputs[9]);
        CarryApple = int.Parse(inputs[10]);
        CarryBanana = int.Parse(inputs[11]);
        CarryIron = int.Parse(inputs[12]);
        CarryWood = int.Parse(inputs[13]);
    }

    public IAction Harvest(Tree tree) {
        int taken = Math.Min(HarvestPower, CarryCapacity - CarryTotal);
        tree.Fruits = Math.Max(0, tree.Fruits - taken);
        return new Harvest(Id);
    }
}

enum Player {
    Me = 0,
    Enemy = 1
}

class Inventory {
    public const int COUNT = 2;
    public const int ME = 0;

    public int plum;
    public int lemon;
    public int apple;
    public int banana;
    public int iron;
    public int wood;

    public void ReadNewData() {
        string[] inputs = Console.ReadLine()!.Split(' ');
        plum = int.Parse(inputs[0]);
        lemon = int.Parse(inputs[1]);
        apple = int.Parse(inputs[2]);
        banana = int.Parse(inputs[3]);
        iron = int.Parse(inputs[4]);
        wood = int.Parse(inputs[5]);
    }
}

interface IAction;

readonly struct Harvest(int trollId) : IAction {
    public override string ToString() => $"HARVEST {trollId}";
}

readonly struct Drop(int trollId) : IAction {
    public override string ToString() => $"DROP {trollId}";
}

readonly struct Move(int trollId, Point point) : IAction {
    public override string ToString() => $"MOVE {trollId} {point.X} {point.Y}";
}

readonly struct Train(int moveSpeed, int carryCapacity, int harvestPower, int chopPower) : IAction {
    public override string ToString() => $"TRAIN {moveSpeed} {carryCapacity} {harvestPower} {chopPower}";
}

readonly record struct Point(int X, int Y) {
    static readonly Point UP = new(0, 1);
    static readonly Point DOWN = new(0, -1);
    static readonly Point LEFT = new(-1, 0);
    static readonly Point RIGHT = new(1, 0);

    public static Point[] Directions4 { get; } = [RIGHT, LEFT, UP, DOWN];

    public static Point From(string x, string y) {
        return new Point(int.Parse(x), int.Parse(y));
    }

    public static int ManhattanDistance(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public override string ToString() => $"({X}, {Y})";
}

class Pathfinding(Grid grid) {
    readonly List<Point> path = new();
    readonly Queue<Node> queue = new();
    readonly Dictionary<Point, Node> visited = new();

    /// BFS to end
    public List<Point>? Path(Point start, Point end) {
        return Path(start, cell => cell.point == end);
    }

    /// BFS to endPredicate
    public List<Point>? Path(Point start, Predicate<Cell> endPredicate) {
        queue.Clear();
        visited.Clear();
        var startNode = new Node(grid.Get(start));
        queue.Enqueue(startNode);
        visited.Add(start, startNode);
        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (endPredicate(current.cell)) {
                return Reconstruct(startNode, current);
            }
            foreach (var cell in grid.Neighbors(current.cell)) {
                if (!cell.IsWalkable && !endPredicate(cell)) {
                    continue;
                }
                if (visited.ContainsKey(cell.point)) {
                    continue;
                }
                var neighbor = new Node(cell) { parent = current };
                queue.Enqueue(neighbor);
                visited.Add(cell.point, neighbor);
            }
        }
        return null;
    }

    List<Point> Reconstruct(Node start, Node end) {
        path.Clear();
        var current = end;
        while (current != start) {
            path.Add(current!.cell.point);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    class Node(Cell cell) {
        public readonly Cell cell = cell;
        public Node? parent;
    }
}
