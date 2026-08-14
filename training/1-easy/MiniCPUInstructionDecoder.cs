using System;
using System.Collections.Generic;
using System.Linq;

/// Decode and execute the MiniCPU bytecode program <br/>
/// https://www.codingame.com/training/easy/minicpu-instruction-decoder
static class MiniCPUInstructionDecoder {
    static void Main() {
        string program = Console.ReadLine()!;
        var cpu = new MiniCPU();
        cpu.Run(program);
        foreach (byte register in cpu.Registers) {
            Console.WriteLine(register);
        }
    }
}

class MiniCPU {
    public byte[] Registers { get; } = new byte[4];

    readonly Dictionary<byte, Instruction> instructions = new() {
        { 0x1, new Move() },
        { 0x2, new Add() },
        { 0x3, new Subtract() },
        { 0x4, new Multiply() },
        { 0x5, new Increment() },
        { 0x6, new Decrement() },
        { 0xFF, new Halt() }
    };

    /// <param name="program">space-separated hex bytes representing CPU instructions</param>
    public void Run(string program) {
        byte[] bytes = program.Split(' ').Select(it => Convert.ToByte(it, 16)).ToArray();
        for (byte pc = 0; pc < bytes.Length;) {
            byte opcode = bytes[pc];
            var instruction = instructions[opcode];
            instruction.Execute(ref pc, bytes, Registers);
        }
    }
}

abstract record Instruction {
    public abstract void Execute(ref byte pc, byte[] bytes, byte[] registers);

    protected static byte ReadNext(ref byte pc, byte[] bytes) {
        Advance(ref pc);
        return bytes[pc];
    }

    protected static void Advance(ref byte pc) => pc += 1;
}

/// 01 X V: MOV - Load value V into register RX
record Move : Instruction {
    public override void Execute(ref byte pc, byte[] bytes, byte[] registers) {
        byte i = ReadNext(ref pc, bytes);
        registers[i] = ReadNext(ref pc, bytes);
        Advance(ref pc);
    }
}

// 02 X Y: ADD - Add register RY to register RX
record Add : Instruction {
    public override void Execute(ref byte pc, byte[] bytes, byte[] registers) {
        byte iX = ReadNext(ref pc, bytes);
        byte iY = ReadNext(ref pc, bytes);
        registers[iX] += registers[iY];
        Advance(ref pc);
    }
}

// 03 X Y: SUB - Subtract register RY from register RX
record Subtract : Instruction {
    public override void Execute(ref byte pc, byte[] bytes, byte[] registers) {
        byte iX = ReadNext(ref pc, bytes);
        byte iY = ReadNext(ref pc, bytes);
        registers[iX] -= registers[iY];
        Advance(ref pc);
    }
}

// 04 X Y: MUL - Multiply register RX by register RY
record Multiply : Instruction {
    public override void Execute(ref byte pc, byte[] bytes, byte[] registers) {
        byte iX = ReadNext(ref pc, bytes);
        byte iY = ReadNext(ref pc, bytes);
        registers[iX] *= registers[iY];
        Advance(ref pc);
    }
}

// 05 X: INC - Increment register RX by 1
record Increment : Instruction {
    public override void Execute(ref byte pc, byte[] bytes, byte[] registers) {
        byte i = ReadNext(ref pc, bytes);
        registers[i]++;
        Advance(ref pc);
    }
}

// 06 X: DEC - Decrement register RX by 1
record Decrement : Instruction {
    public override void Execute(ref byte pc, byte[] bytes, byte[] registers) {
        byte i = ReadNext(ref pc, bytes);
        registers[i]--;
        Advance(ref pc);
    }
}

// FF: HLT - Halt execution
record Halt : Instruction {
    public override void Execute(ref byte pc, byte[] bytes, byte[] registers) {
        Advance(ref pc);
    }
}
