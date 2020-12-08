using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _07
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");
            
            var wiring = new Dictionary<string, List<string>>();
            var cache = new Dictionary<string, ushort>();

            foreach (var instruction in input)
            {
                var parts = instruction.Split(' ').ToList();
                wiring.Add(parts.Last(), parts.GetRange(0, parts.Count - 2));
            }

            ushort solutionPart1 = wiring.GetSignal("a", cache);
            
            cache.Clear();
            wiring["b"] = new List<string> { "956" };
            var solutionPart2 = wiring.GetSignal("a", cache);

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }

        static ushort GetSignal(this Dictionary<string, List<string>> wiring, string wire, Dictionary<string, ushort> cache)
        {
            if (cache.ContainsKey(wire)) return cache[wire];
            
            ushort result = 0;

            if (ushort.TryParse(wire, out result))
            {
                return result;
            }

            var instructionParts = wiring[wire];
            var instructionPartCount = instructionParts.Count();

            if (instructionPartCount == 1)
            {
                result = wiring.GetSignal(instructionParts[0], cache);
            }
            else if (instructionPartCount == 2 && instructionParts[0] == "NOT")
            {
                result = (ushort)~wiring.GetSignal(instructionParts[1], cache);
            }
            else if (instructionPartCount == 3 && instructionParts[1] == "AND")
            {
                result = (ushort)(wiring.GetSignal(instructionParts[0], cache) & wiring.GetSignal(instructionParts[2], cache));
            }
            else if (instructionPartCount == 3 && instructionParts[1] == "OR")
            {
                result = (ushort)(wiring.GetSignal(instructionParts[0], cache) | wiring.GetSignal(instructionParts[2], cache));
            }
            else if (instructionPartCount == 3 && instructionParts[1] == "LSHIFT")
            {
                result = (ushort)(wiring.GetSignal(instructionParts[0], cache) << int.Parse(instructionParts[2]));
            }
            else if (instructionPartCount == 3 && instructionParts[1] == "RSHIFT")
            {
                result = (ushort)(wiring.GetSignal(instructionParts[0], cache) >> int.Parse(instructionParts[2]));
            }

            cache.Add(wire, result);
            return result;
        }
    }
}
