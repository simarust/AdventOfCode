using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _10
{
    static class Program
    {
        static void Main(string[] args)
        {
            var iterationsPart1 = 40;
            var iterationsPart2 = 50;

            var solutionPart1 = "3113322113";
            var solutionPart2 = "3113322113";

            for (int i = 0; i < iterationsPart1; i++)
            {
                solutionPart1 = String.Concat(solutionPart1
                    .GetSequences()
                    .Select(t => $"{t.Item1}{t.Item2}"));
            }

            for (int i = 0; i < iterationsPart2; i++)
            {
                solutionPart2 = String.Concat(solutionPart2
                    .GetSequences()
                    .Select(t => $"{t.Item1}{t.Item2}"));
            }

            Console.WriteLine($"Solution Part 1: {solutionPart1.Length}");
            Console.WriteLine($"Solution Part 2: {solutionPart2.Length}");
        }

        static List<Tuple<int, char>> GetSequences(this string input)
        {
            var sequences = new List<Tuple<int, char>>();
            var lastC = ' ';
            Tuple<int, char> currentTuple = null;
            foreach (var c in input)
            {
                if (c != lastC)
                {
                    if (currentTuple != null) sequences.Add(currentTuple);
                    currentTuple = new Tuple<int, char> (1, c);
                    lastC = c;
                }
                else
                {
                    currentTuple = new Tuple<int, char>(currentTuple.Item1 + 1, c);
                }
            }
            sequences.Add(currentTuple);
            return sequences;
        }
    }
}
