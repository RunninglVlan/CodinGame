using System;
using System.Collections.Generic;

/// Decode and execute the MiniCPU bytecode program<br/>
/// https://www.codingame.com/training/easy/minicpu-instruction-decoder
static class MiniCPUInstructionDecoder {
    static void Main() {
        string program = Console.ReadLine()!;
        var cpu = new MiniCPU();
        cpu.Run(program);
        cpu.Registers.ForEach(Console.WriteLine);
    }
}

class MiniCPU {
    public List<int> Registers { get; } = [0, 0, 0, 0];

    readonly Dictionary<string, Instruction> instructions = new() {
        { "01", new Move() },
        { "02", new Add() },
        { "03", new Subtract() },
        { "04", new Multiply() },
        { "FF", new Halt() }
    };

    /// <param name="program">space-separated hex bytes representing CPU instructions</param>
    public void Run(string program) {
        string[] bytes = program.Split(' ');
        for (var pc = 0; pc < bytes.Length;) {
            string opcode = bytes[pc];
            var instruction = instructions[opcode];
            instruction.Execute(ref pc, bytes, Registers);
        }
    }
}

abstract record Instruction {
    public abstract void Execute(ref int pc, string[] bytes, List<int> registers);

    protected static int ReadNext(ref int pc, string[] bytes) {
        Advance(ref pc);
        return Convert.ToByte(bytes[pc], 16);
    }

    protected static void Advance(ref int pc) => pc += 1;
}

/// 01 X V: MOV - Load value V into register RX
record Move : Instruction {
    public override void Execute(ref int pc, string[] bytes, List<int> registers) {
        int i = ReadNext(ref pc, bytes);
        registers[i] = ReadNext(ref pc, bytes);
        Advance(ref pc);
    }
}

// 02 X Y: ADD - Add register RY to register RX
record Add : Instruction {
    public override void Execute(ref int pc, string[] bytes, List<int> registers) {
        int iX = ReadNext(ref pc, bytes);
        int iY = ReadNext(ref pc, bytes);
        registers[iX] += registers[iY];
        Advance(ref pc);
    }
}

// 03 X Y: SUB - Subtract register RY from register RX
record Subtract : Instruction {
    public override void Execute(ref int pc, string[] bytes, List<int> registers) {
        int iX = ReadNext(ref pc, bytes);
        int iY = ReadNext(ref pc, bytes);
        registers[iX] -= registers[iY];
        Advance(ref pc);
    }
}

// 04 X Y: MUL - Multiply register RX by register RY
record Multiply : Instruction {
    public override void Execute(ref int pc, string[] bytes, List<int> registers) {
        int iX = ReadNext(ref pc, bytes);
        int iY = ReadNext(ref pc, bytes);
        registers[iX] *= registers[iY];
        Advance(ref pc);
    }
}

// FF: HLT - Halt execution
record Halt : Instruction {
    public override void Execute(ref int pc, string[] bytes, List<int> registers) {
        Advance(ref pc);
    }
}
