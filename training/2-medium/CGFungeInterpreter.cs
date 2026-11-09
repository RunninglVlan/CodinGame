using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/cgfunge-interpreter
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
var interpreter = new Interpreter();
interpreter.ReadSource(lines);
interpreter.InterpretSource();

class Interpreter {
    readonly Dictionary<Point, string> source = [];
    Point direction = Screen.RIGHT;
    int step = 1;
    readonly Stack<object> memory = new();
    readonly Dictionary<string, Action> operations = [];
    bool stringMode;

    public Interpreter() {
        operations[Commands.ADD] = Add;
        operations[Commands.MULTIPLY] = Multiply;
        operations[Commands.PRINT_INT] = PrintTop;
        operations[Commands.PRINT_CHAR] = PrintTop;
        operations[Commands.STRING_MODE] = ToggleStringMode;
        operations[Commands.GO_RIGHT] = GoRight;
        operations[Commands.GO_LEFT] = GoLeft;
        operations[Commands.GO_UP] = GoUp;
        operations[Commands.GO_DOWN] = GoDown;
        operations[Commands.SPACE] = () => { };
        operations[Commands.SKIP_NEXT] = () => step = 2;
        operations[Commands.IF_HORIZONTAL] = IfHorizontal;
        operations[Commands.IF_VERTICAL] = IfVertical;
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
        var cursor = Screen.ZERO;
        while (source.TryGetValue(cursor, out string? character) && character != Commands.END) {
            Console.Error.WriteLine(character);
            if (stringMode) {
                if (character == Commands.STRING_MODE) {
                    ToggleStringMode();
                } else {
                    memory.Push(character);
                }
            } else if (byte.TryParse(character, out byte digit)) {
                memory.Push(digit);
            } else {
                if (!operations.TryGetValue(character, out var operation)) {
                    throw new ArgumentException($"Unknown command: {character}");
                }
                operation();
            }
            cursor += direction * step;
            step = 1;
        }
    }

    void Add() {
        var a = (byte)memory.Pop();
        var b = (byte)memory.Pop();
        memory.Push((byte)(a + b));
    }

    void Multiply() {
        var a = (byte)memory.Pop();
        var b = (byte)memory.Pop();
        memory.Push((byte)(a * b));
    }

    void PrintTop() => Console.Write(memory.Pop());
    void ToggleStringMode() => stringMode = !stringMode;

    void GoRight() => direction = Screen.RIGHT;
    void GoLeft() => direction = Screen.LEFT;
    void GoUp() => direction = Screen.UP;
    void GoDown() => direction = Screen.DOWN;

    void IfHorizontal() {
        bool condition = (byte)memory.Pop() == 0;
        if (condition) {
            GoRight();
        } else {
            GoLeft();
        }
    }

    void IfVertical() {
        bool condition = (byte)memory.Pop() == 0;
        if (condition) {
            GoDown();
        } else {
            GoUp();
        }
    }
}

static class Commands {
    public const string END = "E";
    public const string ADD = "+";
    public const string MULTIPLY = "*";
    public const string PRINT_INT = "I";
    public const string PRINT_CHAR = "C";
    public const string STRING_MODE = "\"";
    public const string GO_RIGHT = ">";
    public const string GO_LEFT = "<";
    public const string GO_UP = "^";
    public const string GO_DOWN = "v";
    public const string SPACE = " ";
    public const string SKIP_NEXT = "S";
    public const string IF_HORIZONTAL = "_";
    public const string IF_VERTICAL = "|";
}

readonly record struct Point(int X, int Y) {
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator *(Point a, int value) => new(a.X * value, a.Y * value);

    public override string ToString() => $"({X}, {Y})";
}

static class Screen {
    public static readonly Point ZERO = new(0, 0);
    public static readonly Point UP = new(0, -1);
    public static readonly Point DOWN = new(0, 1);
    public static readonly Point LEFT = new(-1, 0);
    public static readonly Point RIGHT = new(1, 0);
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
