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
    readonly List<Tree> trees = new();
    readonly List<Troll> myTrolls = new();
    readonly Inventory[] inventories = new Inventory[Inventory.COUNT];

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
        trees.Clear();
        myTrolls.Clear();
        for (var i = 0; i < Inventory.COUNT; i++) {
            inventories[i].ReadNewData();
        }
        int treesCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < treesCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            var point = Point.From(inputs[1], inputs[2]);
            var tree = Tree.From(point, inputs);
            grid.Get(point).tree = tree;
            trees.Add(tree);
        }
        int trollsCount = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < trollsCount; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            var point = Point.From(inputs[2], inputs[3]);
            var troll = Troll.From(point, inputs);
            grid.Get(point).troll = troll;
            if (troll.Player == Player.Me) {
                myTrolls.Add(troll);
            }
        }
    }

    IEnumerable<IAction> Actions() {
        foreach (var troll in myTrolls) {
            var cell = grid.Get(troll.Point);
            if (troll.CanCarry && cell.tree is { Fruits: > 0 }) {
                yield return new Harvest(troll.Id);
                continue;
            }
            if (troll.IsCarrying) {
                if (grid.Neighbors(cell).Any(it => it.type == CellType.MyShack)) {
                    yield return new Drop(troll.Id);
                } else {
                    yield return new Move(troll.Id, grid.myShack);
                }
            } else {
                (Cell cell, int distance) bestTree = (null!, int.MaxValue);
                foreach (var tree in trees) {
                    if (tree.Fruits == 0) {
                        continue;
                    }
                    int distance = Point.ManhattanDistance(troll.Point, tree.Point);
                    if (distance < bestTree.distance) {
                        bestTree = (grid.Get(tree.Point), distance);
                    }
                }
                yield return new Move(troll.Id, bestTree.cell);
            }
        }
    }
}

class Grid {
    int width, height;
    Cell[,] cells = null!;
    public Cell myShack = null!;

    public void Init() {
        string[] dimensions = Console.ReadLine()!.Split(' ');
        width = int.Parse(dimensions[0]);
        height = int.Parse(dimensions[1]);
        cells = new Cell[width, height];
        for (var y = 0; y < height; y++) {
            string line = Console.ReadLine()!;
            for (var x = 0; x < line.Length; x++) {
                char c = line[x];
                var cell = new Cell(x, y);
                cells[x, y] = cell;
                cell.type = c switch {
                    '0' => CellType.MyShack,
                    '1' => CellType.EnemyShack,
                    _ => CellType.Grass
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

    public void ClearOldData() {
        tree = null;
        troll = null;
    }
}

enum CellType {
    Grass,
    MyShack,
    EnemyShack
}

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
readonly record struct Tree(Point Point, string Type, int Size, int Health, int Fruits, int Cooldown) {
    public static Tree From(Point point, string[] inputs) {
        string type = inputs[0];
        int size = int.Parse(inputs[3]);
        int health = int.Parse(inputs[4]);
        int fruits = int.Parse(inputs[5]);
        int cooldown = int.Parse(inputs[6]);
        return new Tree(point, type, size, health, fruits, cooldown);
    }
}

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
readonly record struct Troll(int Id, Point Point, Player Player,
        int MovementSpeed, int CarryCapacity, int HarvestPower, int ChopPower,
        int CarryPlum, int CarryLemon, int CarryApple, int CarryBanana, int CarryIron, int CarryWood) {
    public bool IsCarrying => CarryTotal > 0;
    int CarryTotal => CarryPlum + CarryLemon + CarryApple + CarryBanana + CarryIron + CarryWood;
    public bool CanCarry => CarryTotal < CarryCapacity;

    public static Troll From(Point point, string[] inputs) {
        int id = int.Parse(inputs[0]);
        var player = (Player)int.Parse(inputs[1]);
        int movementSpeed = int.Parse(inputs[4]);
        int carryCapacity = int.Parse(inputs[5]);
        int harvestPower = int.Parse(inputs[6]);
        int chopPower = int.Parse(inputs[7]);
        int carryPlum = int.Parse(inputs[8]);
        int carryLemon = int.Parse(inputs[9]);
        int carryApple = int.Parse(inputs[10]);
        int carryBanana = int.Parse(inputs[11]);
        int carryIron = int.Parse(inputs[12]);
        int carryWood = int.Parse(inputs[13]);
        return new Troll(id, point, player,
                movementSpeed, carryCapacity, harvestPower, chopPower,
                carryPlum, carryLemon, carryApple, carryBanana, carryIron, carryWood);
    }
}

enum Player {
    Me = 0,
    Enemy = 1
}

class Inventory {
    public const int COUNT = 2;

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

readonly struct Move(int trollId, Cell cell) : IAction {
    public override string ToString() => $"MOVE {trollId} {cell.point.X} {cell.point.Y}";
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
