using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _08
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input");
            var regex = new Regex(@"(nop|acc|jmp) ([+-]\d+)");
            ValueTuple<string, int, bool>[] instructions = regex.Matches(input)
                .Select(match => new ValueTuple<string, int, bool>(match.Groups[1].Value, Int32.Parse(match.Groups[2].Value), false))
                .ToArray();

            _ = IsInfiniteLoop((ValueTuple<string, int, bool>[])instructions.Clone(), out var accumulator1);
            var solutionPart1 = accumulator1;

            var solutionPart2 = 0;

            for (int i = 0; i < instructions.Length; i++)
            {
                var instructionsClone = (ValueTuple<string, int, bool>[])instructions.Clone();
                if (instructionsClone[i].Item1 == "nop")
                {
                    instructionsClone[i].Item1 = "jmp";
                }
                else if (instructionsClone[i].Item1 == "jmp")
                {
                    instructionsClone[i].Item1 = "nop";
                }
                else continue;

                if (!IsInfiniteLoop(instructionsClone, out var accumulator2))
                {
                    solutionPart2 = accumulator2;
                    break;
                }
            }

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }
        
        private static bool IsInfiniteLoop(ValueTuple<string, int, bool>[] instructions, out int accumulator)
        {
            var index = 0;
            accumulator = 0;

            while (true)
            {
                if (instructions[index].Item3) return true;
                instructions[index].Item3 = true;
                var instruction = instructions[index];
                switch (instruction.Item1)
                {
                    case "nop": index++; break;
                    case "acc": index++; accumulator += instruction.Item2; break;
                    case "jmp": index += instruction.Item2; break;
                }
                if (index == instructions.Length) return false;
            }
        }
    }
}
