using System;

/// https://www.codingame.com/training/easy/1d-spreadsheet
static class OneDSpreadsheet {
    static void Main() {
        int n = int.Parse(Console.ReadLine()!);
        var cells = new Cell[n];
        for (var i = 0; i < n; i++) {
            string[] inputs = Console.ReadLine()!.Split(' ');
            string operation = inputs[0];
            string arg1 = inputs[1];
            string arg2 = inputs[2];
            cells[i] = new Cell(operation, arg1, arg2);
        }
        for (var index = 0; index < n; index++) {
            Console.WriteLine(cells[index].Value(cells));
        }
    }
}

class Cell(string operation, string arg1, string arg2) {
    readonly Operation operation = Enum.Parse<Operation>(operation, ignoreCase: true);
    readonly Argument arg1 = new(arg1);
    readonly Argument arg2 = new(arg2);
    int? resolvedValue;

    public int Value(Cell[] cells) {
        if (resolvedValue.HasValue) {
            return resolvedValue.Value;
        }
        var value = 0;
        if (operation == Operation.Value) {
            value = arg1.Value(cells);
        } else {
            int arg1Value = arg1.Value(cells);
            int arg2Value = arg2.Value(cells);
            value = operation switch {
                Operation.Add => arg1Value + arg2Value,
                Operation.Sub => arg1Value - arg2Value,
                Operation.Mult => arg1Value * arg2Value,
                _ => value
            };
        }
        resolvedValue = value;
        return value;
    }
}

class Argument {
    const char REFERENCE = '$';
    readonly string value;
    int? resolvedValue;

    public Argument(string value) {
        this.value = value;
        if (value != "_" && !value.StartsWith(REFERENCE)) {
            resolvedValue = int.Parse(value);
        }
    }

    public int Value(Cell[] cells) {
        if (resolvedValue.HasValue) {
            return resolvedValue.Value;
        }
        if (!value.StartsWith(REFERENCE)) {
            return 0;
        }
        int index = int.Parse(value[1..]);
        resolvedValue = cells[index].Value(cells);
        return resolvedValue.Value;
    }
}

enum Operation {
    Value,
    Add,
    Sub,
    Mult
}
