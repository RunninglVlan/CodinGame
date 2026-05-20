using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TrollFarm;

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
    (Cell cell, int score)[] plantingPlaces = null!;

    public Game() => pathfinding = new(grid);

    public void Init() {
        grid.Init();
        plantingPlaces = grid.CellsNearWater()
                .Select(it => (it, score: Score(it)))
                .Where(it => it.score > int.MinValue)
                .OrderByDescending(it => it.score)
                .ToArray();
        for (var i = 0; i < Inventory.COUNT; i++) {
            inventories[i] = new Inventory();
        }
        return;

        int Score(Cell cell) {
            var enemyPath = pathfinding.Path(grid.enemyShack.point, it => it == cell)?.ToArray();
            var myPath = pathfinding.Path(grid.myShack.point, it => it == cell)?.ToArray();
            if (enemyPath == null || myPath == null) {
                return int.MinValue;
            }
            return enemyPath.Length - myPath.Length;
        }
    }

    public void Loop() {
        ReadInput();
        var actions = Actions().ToList();
        actions.Add(new Message($"Trolls: my {trolls.Values.Count(it => it.Player == Player.Me)} "
                + $"vs {trolls.Values.Count(it => it.Player == Player.Enemy)}"));
        Console.WriteLine(string.Join(';', actions));
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
        const int movementSpeed = 2, carryCapacity = 2, harvestPower = 2;
        bool haveChopper = trolls.Values.Any(it => it is { Player: Player.Me, IsChopper: true });
        if (!haveChopper && CanTrain(movementSpeed, carryCapacity, chopPower: Troll.CHOPPER_POWER)) {
            yield return new Train(movementSpeed, carryCapacity, chopPower: Troll.CHOPPER_POWER);
        } else if (CanTrain(movementSpeed, carryCapacity, harvestPower)) {
            yield return new Train(movementSpeed, carryCapacity, harvestPower);
        }
        foreach (var action in trolls.Values.Where(it => it.Player == Player.Me).Select(TrollAction)) {
            yield return action;
        }
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    bool CanTrain(int movementSpeed, int carryCapacity, int harvestPower = 0, int chopPower = 0) {
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
        if (troll.IsChopper) {
            return ChopperAction(troll);
        }
        // planting
        if (troll.PlantingType != null) {
            var target = PlantingTarget();
            return target == troll.Point ? troll.Plant() : new Move(troll.Id, target);
        }
        bool nearShack = grid.Neighbors(cell).Any(it => it == grid.myShack);
        bool planting = trolls.Values.Any(it => it.PlantingType != null);
        if (nearShack && !planting && !troll.IsCarrying && NeedToPlant(out var fruit)) {
            return troll.PickToPlant(fruit);
        }
        // harvesting
        if (troll.CanCarry && cell.tree is { Fruits: > 0 }) {
            return troll.Harvest(cell.tree);
        }
        if (troll.IsCarrying) {
            return nearShack ? new Drop(troll.Id) : new Move(troll.Id, grid.myShack.point);
        }
        return new Move(troll.Id, HarvestingTree(troll));
    }

    IAction ChopperAction(Troll troll) {
        var cell = grid.Get(troll.Point);
        bool nearShack = grid.Neighbors(cell).Any(it => it == grid.myShack);
        if (troll.IsCarrying) {
            return nearShack ? new Drop(troll.Id) : new Move(troll.Id, grid.myShack.point);
        }
        if (TryFindChopperTarget(out var target)) {
            return target == troll.Point ? new Chop(troll.Id) : new Move(troll.Id, target);
        }
        if (grid.Neighbors(cell).Any(it => it.type == CellType.Iron)) {
            return new Mine(troll.Id);
        }
        var ironTarget = pathfinding.Path(troll.Point, it => grid.Neighbors(it).Any(IronCell))!.Last();
        return new Move(troll.Id, ironTarget);

        bool IronCell(Cell it) => it.type == CellType.Iron;
    }

    /// Prioritizing Banana trees as they have less health
    bool TryFindChopperTarget(out Point target) {
        target = default;
        foreach (var type in new[] { FruitType.Banana, FruitType.Plum, FruitType.Lemon, FruitType.Apple }) {
            var enemyPath = pathfinding.Path(grid.enemyShack.point, it => it.tree?.Type == type);
            if (enemyPath == null) {
                continue;
            }
            int enemyDistance = enemyPath.Count;
            var tree = enemyPath.Last();
            var myPath = pathfinding.Path(grid.myShack.point, tree);
            if (enemyDistance >= myPath!.Count) {
                continue;
            }
            target = tree;
            return true;
        }
        return false;
    }

    Point PlantingTarget() => plantingPlaces.Where(it => it.cell.tree == null).Select(it => it.cell.point).First();

    bool NeedToPlant(out FruitType fruitType) {
        fruitType = default;
        var shack = grid.myShack.point;
        var treeTuples = Enum.GetValues<FruitType>()
                .Select(type => (type, distance: PathDistance(it => it.tree?.Type == type)))
                .OrderByDescending(it => it.distance);
        var plantingTarget = PlantingTarget();
        int plantingDistance = PathDistance(it => it.point == plantingTarget);
        foreach ((var type, int distance) in treeTuples) {
            if (!MyInventory.Has(type) || distance < plantingDistance) {
                continue;
            }
            fruitType = type;
            return true;
        }
        return false;

        int PathDistance(Predicate<Cell> predicate) => pathfinding.Path(shack, predicate)?.Count ?? int.MaxValue;
    }

    Point HarvestingTree(Troll troll) {
        var harvestingPath = Enum.GetValues<FruitType>().OrderBy(it => MyInventory.Count(it))
                .Select(type => pathfinding.Path(troll.Point, it => it.tree?.Fruits > 0 && it.tree.Type == type))
                .OfType<List<Point>>()
                .FirstOrDefault();
        var path = harvestingPath ?? pathfinding.Path(troll.Point, it => it.tree != null)!;
        return path.Count == 0 ? troll.Point : path.Last();
    }
}

class Grid {
    int width, height;
    Cell[,] cells = null!;
    public Cell myShack = null!, enemyShack = null!;

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
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
                } else if (cell.type == CellType.EnemyShack) {
                    enemyShack = cell;
                }
            }
        }
    }

    public HashSet<Cell> CellsNearWater() {
        HashSet<Cell> cellsNearWater = new();
        foreach (var cell in cells) {
            if (cell.type != CellType.Water) {
                continue;
            }
            foreach (var neighbor in Neighbors(cell)) {
                if (neighbor.type != CellType.Grass) {
                    continue;
                }
                cellsNearWater.Add(neighbor);
            }
        }
        return cellsNearWater;
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
record Tree(FruitType Type, int Size, int Health, int Cooldown) {
    public int Fruits { get; set; }

    public static Tree From(string[] inputs) {
        string treeType = inputs[0];
        var type = treeType switch {
            "PLUM" => FruitType.Plum,
            "LEMON" => FruitType.Lemon,
            "APPLE" => FruitType.Apple,
            "BANANA" => FruitType.Banana,
            _ => throw new ArgumentException($"Unexpected tree type: {treeType}")
        };
        int size = int.Parse(inputs[3]);
        int health = int.Parse(inputs[4]);
        int fruits = int.Parse(inputs[5]);
        int cooldown = int.Parse(inputs[6]);
        return new Tree(type, size, health, cooldown) { Fruits = fruits };
    }
}

enum FruitType {
    Plum,
    Lemon,
    Apple,
    Banana
}

/// <param name="MovementSpeed">determines how many cells it can MOVE horizontally or vertically per turn</param>
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
record Troll(int Id, Player Player, int MovementSpeed, int CarryCapacity, int HarvestPower, int ChopPower) {
    public const int CHOPPER_POWER = 2;

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
    public FruitType? PlantingType { get; private set; }
    public bool IsChopper => ChopPower == CHOPPER_POWER;

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

    public IAction PickToPlant(FruitType fruitType) {
        PlantingType = fruitType;
        return new Pick(Id, fruitType);
    }

    public IAction Plant() {
        var action = new Plant(Id, PlantingType!.Value);
        PlantingType = null;
        return action;
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

    public bool Has(FruitType type) => Count(type) > 0;

    public int Count(FruitType type) => type switch {
        FruitType.Plum => plum,
        FruitType.Lemon => lemon,
        FruitType.Apple => apple,
        FruitType.Banana => banana,
        _ => throw new ArgumentException($"Unexpected fruit type: {type}")
    };
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

readonly struct Train(int moveSpeed, int carryCapacity, int harvestPower = 0, int chopPower = 0) : IAction {
    public override string ToString() => $"TRAIN {moveSpeed} {carryCapacity} {harvestPower} {chopPower}";
}

readonly struct Pick(int trollId, FruitType type) : IAction {
    public override string ToString() => $"PICK {trollId} {type.ToString().ToUpper()}";
}

readonly struct Plant(int trollId, FruitType type) : IAction {
    public override string ToString() => $"PLANT {trollId} {type.ToString().ToUpper()}";
}

readonly struct Chop(int trollId) : IAction {
    public override string ToString() => $"CHOP {trollId}";
}

readonly struct Mine(int trollId) : IAction {
    public override string ToString() => $"MINE {trollId}";
}

readonly struct Message(string value) : IAction {
    public override string ToString() => $"MSG {value}";
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
