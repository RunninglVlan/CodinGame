using System;
using System.Collections.Generic;

// https://www.codingame.com/training/medium/cgfunge-interpreter
int lines = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
var interpreter = new Interpreter();
interpreter.ReadSource(lines);
interpreter.InterpretSource();

class Interpreter {
    const char END = 'E';
    const char STRING_MODE = '\"';

    readonly Dictionary<Point, char> source = [];
    Point direction = Screen.RIGHT;
    int step = 1;
    readonly Stack<byte> memory = new();
    readonly Dictionary<char, Action> operations = [];
    bool stringMode;

    public Interpreter() {
        operations['+'] = Add;
        operations['-'] = Subtract;
        operations['*'] = Multiply;
        operations['I'] = PrintInt;
        operations['C'] = PrintChar;
        operations[STRING_MODE] = ToggleStringMode;
        operations['>'] = GoRight;
        operations['<'] = GoLeft;
        operations['^'] = GoUp;
        operations['v'] = GoDown;
        operations[' '] = () => { };
        operations['S'] = SkipNext;
        operations['_'] = IfHorizontal;
        operations['|'] = IfVertical;
        operations['P'] = () => memory.Pop();
        operations['X'] = Switch;
        operations['D'] = Duplicate;
    }

    public void ReadSource(int lines) {
        for (var y = 0; y < lines; y++) {
            string line = Debug.ReadLine()!;
            for (var x = 0; x < line.Length; x++) {
                source[new Point(x, y)] = line[x];
            }
        }
    }

    public void InterpretSource() {
        var cursor = Screen.ZERO;
        while (source.TryGetValue(cursor, out char character) && character != END) {
            Console.Error.WriteLine($"character: '{character}', memory: '{(memory.Count > 0 ? memory.Peek() : "")}', {memory.Count}");
            if (stringMode) {
                if (character == STRING_MODE) {
                    ToggleStringMode();
                } else {
                    memory.Push((byte)character);
                }
            } else if (byte.TryParse($"{character}", out byte digit)) {
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

    void SkipNext() => step = 2;

    void Add() {
        byte a = memory.Pop();
        byte b = memory.Pop();
        memory.Push((byte)(a + b));
    }

    void Subtract() {
        byte a = memory.Pop();
        byte b = memory.Pop();
        memory.Push((byte)(b - a));
    }

    void Multiply() {
        byte a = memory.Pop();
        byte b = memory.Pop();
        memory.Push((byte)(a * b));
    }

    void PrintInt() {
        byte value = memory.Pop();
        Console.Write(value);
    }

    void PrintChar() {
        var value = (char)memory.Pop();
        Console.Write(value);
    }

    void ToggleStringMode() => stringMode = !stringMode;

    void GoRight() => direction = Screen.RIGHT;
    void GoLeft() => direction = Screen.LEFT;
    void GoUp() => direction = Screen.UP;
    void GoDown() => direction = Screen.DOWN;

    void IfHorizontal() {
        bool condition = memory.Pop() == 0;
        if (condition) {
            GoRight();
        } else {
            GoLeft();
        }
    }

    void IfVertical() {
        bool condition = memory.Pop() == 0;
        if (condition) {
            GoDown();
        } else {
            GoUp();
        }
    }

    void Switch() {
        byte a = memory.Pop();
        byte b = memory.Pop();
        memory.Push(a);
        memory.Push(b);
    }

    void Duplicate() {
        byte value = memory.Peek();
        memory.Push(value);
    }
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
