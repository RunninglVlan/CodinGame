using System;
using System.Collections.Generic;

namespace MiniCPUInstructionDecoder;

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

    /// <param name="instructions">space-separated hex bytes representing CPU instructions</param>
    public void Run(string instructions) {
    }
}
