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
    readonly List<Tree> trees = new();
    readonly HashSet<Cell> myTrees = new();
    readonly List<Cell> enemyTrees = new();
    readonly Inventory[] inventories = new Inventory[Inventory.COUNT];
    Inventory MyInventory => inventories[Inventory.ME];
    Cell[] cellsNearWater = null!;

    public Game() => pathfinding = new(grid);

    public void Init() {
        grid.Init();
        cellsNearWater = grid.CellsNearWater()
                .Select(cell => {
                    int distance = pathfinding.Path(grid.myShack.point, it => it == cell)?.Count ?? int.MaxValue;
                    return (cell, distance);
                })
                .OrderBy(it => it.distance)
                .Select(it => it.cell)
                .ToArray();
        for (var i = 0; i < Inventory.COUNT; i++) {
            inventories[i] = new Inventory();
        }
    }

    public void Loop() {
        ReadInput();
        var actions = Actions().ToList();
        actions.Add(new Message($"Trolls: my {trolls.Values.Count(it => it.Player == Player.Me)} "
                + $"vs {trolls.Values.Count(it => it.Player == Player.Enemy)}"));
        Console.WriteLine(string.Join(';', actions));
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    void ReadInput() {
        var previousTrees = trees.Select(it => it.Point).ToHashSet();
        grid.ClearOldData();
        trees.Clear();
        for (var i = 0; i < Inventory.COUNT; i++) {
            inventories[i].ReadNewData();
        }
        ReadTrees();
        ReadTrolls();
        myTrees.RemoveWhere(it => it.tree == null);
        enemyTrees.RemoveAll(it => it.tree == null);
        if (previousTrees.Count == 0) {
            return;
        }
        // new enemyTrees = current trees - previousTress - myTrees
        foreach (var tree in trees) {
            var cell = grid.Get(tree.Point);
            if (previousTrees.Contains(tree.Point) || myTrees.Contains(cell)) {
                continue;
            }
            enemyTrees.Add(cell);
        }
    }

    void ReadTrees() {
        int treesCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < treesCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            var point = Point.From(inputs[1], inputs[2]);
            var tree = Tree.From(point, inputs);
            grid.Get(point).tree = tree;
            trees.Add(tree);
        }
    }

    void ReadTrolls() {
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
        const int movementSpeed = 2;
        var myTrolls = trolls.Values.Where(it => it.Player == Player.Me).ToArray();
        if (!myTrolls.Any(it => it.IsHarvester)) {
            if (CanTrain(movementSpeed, Troll.HARVESTER_CAPACITY, Troll.HARVESTER_POWER)) {
                yield return new Train(movementSpeed, Troll.HARVESTER_CAPACITY, Troll.HARVESTER_POWER);
            }
        } else if (!myTrolls.Any(it => it.IsChopper)) {
            for (int chopPower = Troll.CHOPPER_POWER + 1; chopPower >= Troll.CHOPPER_POWER; chopPower--) {
                if (!CanTrain(movementSpeed, Troll.CHOPPER_CAPACITY, chopPower: chopPower)) {
                    continue;
                }
                yield return new Train(movementSpeed, Troll.CHOPPER_CAPACITY, chopPower: chopPower);
                break;
            }
        } else if (CanTrain(movementSpeed, Troll.HARVESTER_CAPACITY, Troll.HARVESTER_POWER)) {
            yield return new Train(movementSpeed, Troll.HARVESTER_CAPACITY, Troll.HARVESTER_POWER);
        }
        foreach (var troll in myTrolls) {
            if (troll.IsDefault) {
                yield return DefaultAction(troll);
            } else if (troll.IsHarvester) {
                yield return HarvesterAction(troll);
            } else if (troll.IsChopper) {
                yield return ChopperAction(troll);
            }
        }
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    bool CanTrain(int movementSpeed, int carryCapacity, int harvestPower = 0, int chopPower = 0) {
        if (MyInventory.plum < RequiredToTrain(movementSpeed)) {
            return false;
        }
        if (MyInventory.lemon < RequiredToTrain(carryCapacity)) {
            return false;
        }
        if (MyInventory.apple < RequiredToTrain(harvestPower)) {
            return false;
        }
        if (MyInventory.iron < RequiredToTrain(chopPower)) {
            return false;
        }
        return true;
    }

    int RequiredToTrain(int count = 0) => trolls.Values.Count(it => it.Player == Player.Me) + count * count;

    IAction DefaultAction(Troll troll) {
        bool haveHarvester = trolls.Values.Any(it => it is { Player: Player.Me, IsHarvester: true });
        if (!haveHarvester) {
            return HarvestTree(troll);
        }
        if (MyInventory.iron < RequiredToTrain(Troll.CHOPPER_POWER)) {
            return troll.IsCarrying ? CarryResource(troll) : MineIron(troll);
        }
        // harass
        if (!TryFindEnemyTree(out var target)) {
            return new Move(troll.Id, grid.enemyShack.point);
        }
        return target == troll.Point ? new Chop(troll.Id) : new Move(troll.Id, target);
    }

    IAction HarvesterAction(Troll troll) {
        if (troll.Planting != null) {
            var plantingTarget = troll.Planting.Value.point;
            return plantingTarget == troll.Point ? PlantTree(troll) : new Move(troll.Id, plantingTarget);
        }
        var cell = grid.Get(troll.Point);
        bool nearShack = grid.Neighbors(cell).Any(it => it == grid.myShack);
        bool planting = trolls.Values.Any(it => it is { Planting: not null, IsHarvester: true });
        if (nearShack && !planting && !troll.IsCarrying && NeedToPlant(out var target)) {
            return troll.PickToPlant(target);
        }
        return HarvestTree(troll);
    }

    Plant PlantTree(Troll troll) {
        var action = new Plant(troll.Id, troll.Planting!.Value.type);
        troll.Planting = null;
        myTrees.Add(grid.Get(troll.Point));
        return action;
    }

    IAction CarryResource(Troll troll) {
        var cell = grid.Get(troll.Point);
        return grid.Neighbors(cell).Any(it => it == grid.myShack)
                ? new Drop(troll.Id)
                : new Move(troll.Id, grid.myShack.point);
    }

    IAction ChopperAction(Troll troll) {
        if (troll.CarryWood > 0) {
            return CarryResource(troll);
        }
        var choppingTree = trees
                .Where(it => it.Size >= Troll.CHOPPER_CAPACITY)
                .OrderBy(it => {
                    var trollPath = pathfinding.Path(troll.Point, it.Point);
                    var shackPath = pathfinding.Path(grid.myShack.point, it.Point);
                    if (trollPath == null || shackPath == null) {
                        return int.MaxValue;
                    }
                    return trollPath.Count + shackPath.Count + it.Health;
                })
                .FirstOrDefault();
        if (choppingTree != null) {
            return choppingTree.Point == troll.Point ? new Chop(troll.Id) : new Move(troll.Id, choppingTree.Point);
        }
        if (troll.Planting != null) {
            var target = troll.Planting.Value.point;
            return target == troll.Point ? PlantTree(troll) : new Move(troll.Id, target);
        }
        var cell = grid.Get(troll.Point);
        return cell == grid.myShack || grid.Neighbors(cell).Any(it => it == grid.myShack)
                ? troll.PickToPlant((FruitType.Banana, TreePlaceNearWater()))
                : new Move(troll.Id, grid.myShack.point);
    }

    IAction MineIron(Troll troll) {
        var cell = grid.Get(troll.Point);
        if (grid.Neighbors(cell).Any(IronCell)) {
            return new Mine(troll.Id);
        }
        var ironTarget = pathfinding.Path(troll.Point, it => grid.Neighbors(it).Any(IronCell))!.Last();
        return new Move(troll.Id, ironTarget);

        bool IronCell(Cell it) => it.type == CellType.Iron;
    }

    bool TryFindEnemyTree(out Point target) {
        target = default;
        if (enemyTrees.Count > 0) {
            target = enemyTrees.First().point;
            return true;
        }
        var targets = trees
                .Select(tree => (tree, enemyPath: pathfinding.Path(grid.enemyShack.point, tree.Point)))
                .Where(it => it.enemyPath != null)
                .Select(it => (it.tree, enemyDistance: it.enemyPath!.Count))
                .OrderBy(it => it.enemyDistance)
                .Where(it => {
                    var myPath = pathfinding.Path(grid.myShack.point, it.tree.Point);
                    if (myPath == null) {
                        return false;
                    }
                    return it.enemyDistance < myPath.Count;
                })
                .Select(it => it.tree.Point)
                .ToArray();
        if (targets.Length == 0) {
            return false;
        }
        target = targets.First();
        return true;
    }

    Point TreePlaceNearWater() => cellsNearWater.Where(it => it.tree == null).Select(it => it.point).First();

    bool NeedToPlant(out (FruitType type, Point point) target) {
        var shack = grid.myShack.point;
        var nearShack = pathfinding.Path(shack, it => it is { type: CellType.Grass, tree: null })!.Last();
        if (TryGetNeededFruit(nearShack, type => PathDistance(it => it.tree?.Type == type), out var fruitType)) {
            target = (fruitType, nearShack);
            return true;
        }
        var nearWater = TreePlaceNearWater();
        if (TryGetNeededFruit(nearWater, type => {
                    var tree = cellsNearWater.FirstOrDefault(it => it.tree?.Type == type);
                    return tree != null ? PathDistance(it => it.point == tree.point) : int.MaxValue;
                }, out fruitType)) {
            target = (fruitType, nearWater);
            return true;
        }
        target = default;
        return false;

        bool TryGetNeededFruit(Point targetPoint, Func<FruitType, int> findDistance, out FruitType fruit) {
            fruit = default;
            var treeTuples = Enum.GetValues<FruitType>()
                    .Select(type => (type, distance: findDistance(type)))
                    .OrderByDescending(it => it.distance);
            int plantingDistance = PathDistance(it => it.point == targetPoint);
            foreach ((var type, int distance) in treeTuples) {
                if (!MyInventory.Has(type) || distance <= plantingDistance) {
                    continue;
                }
                fruit = type;
                return true;
            }
            return false;
        }

        int PathDistance(Predicate<Cell> predicate) => pathfinding.Path(shack, predicate)?.Count ?? int.MaxValue;
    }

    IAction HarvestTree(Troll troll) {
        var cell = grid.Get(troll.Point);
        var harvestingTree = HarvestingTree();
        if (troll.CanCarry && cell.tree?.Fruits > 0 && cell.tree.Type == harvestingTree.Type) {
            return troll.Harvest(cell.tree);
        }
        return troll.IsCarrying ? CarryResource(troll) : new Move(troll.Id, harvestingTree.Point);

        Tree HarvestingTree() {
            int maxFruits = Enum.GetValues<FruitType>().Max(it => MyInventory.Count(it));
            return trees
                    .Where(it => it.Fruits > 0)
                    .OrderByDescending(it => {
                        var path = pathfinding.Path(troll.Point, it.Point);
                        if (path == null) {
                            return int.MinValue;
                        }
                        int fruits = MyInventory.Count(it.Type);
                        double scarcity = maxFruits - fruits + 1;
                        return scarcity / (path.Count + 0.5);
                    })
                    .First();
        }
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
record Tree(Point Point, FruitType Type, int Size, int Health, int Cooldown) {
    public int Fruits { get; set; }

    public static Tree From(Point point, string[] inputs) {
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
        return new Tree(point, type, size, health, cooldown) { Fruits = fruits };
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
    const int DEFAULT_POWER = 1;
    public const int HARVESTER_CAPACITY = 2;
    public const int HARVESTER_POWER = 2;
    public const int CHOPPER_CAPACITY = 3;
    public const int CHOPPER_POWER = 2;

    public Point Point { get; private set; }
    int CarryPlum { get; set; }
    int CarryLemon { get; set; }
    int CarryApple { get; set; }
    int CarryBanana { get; set; }
    int CarryIron { get; set; }
    public int CarryWood { get; private set; }
    public bool IsCarrying => CarryTotal > 0;
    int CarryTotal => CarryPlum + CarryLemon + CarryApple + CarryBanana + CarryIron + CarryWood;
    public bool CanCarry => CarryTotal < CarryCapacity;
    public (FruitType type, Point point)? Planting { get; set; }
    public bool IsDefault => ChopPower == DEFAULT_POWER;
    public bool IsHarvester => HarvestPower == HARVESTER_POWER;
    public bool IsChopper => ChopPower >= CHOPPER_POWER;

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

    public IAction PickToPlant((FruitType type, Point point) planting) {
        Planting = planting;
        return new Pick(Id, planting.type);
    }
}

enum Player {
    Me = 0,
    Enemy = 1
}

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
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
                if (!cell.IsWalkable) {
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
