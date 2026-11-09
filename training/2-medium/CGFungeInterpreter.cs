using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/cgfunge-interpreter
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
var interpreter = new Interpreter();
interpreter.ReadSource(lines);
interpreter.InterpretSource();

class Interpreter {
    readonly Dictionary<Point, string> source = [];
    readonly Stack<byte> memory = new();
    readonly Dictionary<string, Action> operations = [];

    public Interpreter() {
        operations[Commands.ADD] = Add;
        operations[Commands.MULTIPLY] = Multiply;
        operations[Commands.PRINT_TOP] = PrintTop;
    }

    public void ReadSource(int lines) {
        for (var y = 0; y < lines; y++) {
            string line = Debug.ReadLine()!;
            for (var x = 0; x < line.Length; x++) {
                source[new Point(x, y)] = line[x..(x + 1)];
            }
        }
    }

    public void InterpretSource() {
        var cursor = Point.ZERO;
        while (source.TryGetValue(cursor, out string? command) && command != Commands.END) {
            if (byte.TryParse(command, out byte digit)) {
                memory.Push(digit);
            } else {
                if (!operations.TryGetValue(command, out var operation)) {
                    throw new ArgumentException($"Unknown command: {command}");
                }
                operation();
            }
            cursor += Point.RIGHT;
        }
    }

    void Add() {
        byte a = memory.Pop();
        byte b = memory.Pop();
        memory.Push((byte)(a + b));
    }

    void Multiply() {
        byte a = memory.Pop();
        byte b = memory.Pop();
        memory.Push((byte)(a * b));
    }

    void PrintTop() {
        Console.Write(memory.Pop());
    }
}

static class Commands {
    public const string END = "E";
    public const string ADD = "+";
    public const string MULTIPLY = "*";
    public const string PRINT_TOP = "I";
}

readonly record struct Point(int X, int Y) {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point RIGHT = new(1, 0);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public override string ToString() => $"({X}, {Y})";
}

static class Debug {
    const int TAB_LENGTH = 2;

    public static bool Enabled { get; set; } = true;

    /// Reads the line from Console and also writes it to Console.Error
    public static string? ReadLine() {
        string? line = Console.ReadLine();
        Console.Error.WriteLine(line);
        return line;
    }

    public static void Log(object? line = null, int tab = 0, ConsoleColor? color = null) {
        if (!Enabled) {
            return;
        }
        Console.ForegroundColor = color ?? ConsoleColor.Red;
        var padding = new string(' ', tab * TAB_LENGTH);
        Console.Error.WriteLine($"{padding}{line}");
        Console.ResetColor();
    }
}
