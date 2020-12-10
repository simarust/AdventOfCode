using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _09
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input")
                .Select(line => Int32.Parse(line))
                .ToList<int>();
            input.Add(0);
            input = input.OrderBy(a => a).ToList();

            var oneJolt = 0;
            var threeJolts = 1;
            var outlet = 0;

            for (int i = 0; i < input.Count(); i++)
            {
                var currentJoltage = input.ElementAt(i);
                if (currentJoltage - outlet == 1)
                {
                    oneJolt++;
                }
                else if(currentJoltage - outlet == 3)
                {
                    threeJolts++;
                }
                
                outlet = currentJoltage;
            }

            var leafs = new Dictionary<int, long>();
            leafs[0] = 1L;

            foreach (var element in input)
            {
                if (input.Contains(element + 1))
                {
                    if (!leafs.ContainsKey(element + 1)) leafs[element + 1] = 0;
                    leafs[element + 1] += leafs[element];
                }
                if (input.Contains(element + 2))
                {
                    if (!leafs.ContainsKey(element + 2)) leafs[element + 2] = 0;
                    leafs[element + 2] += leafs[element];
                }
                if (input.Contains(element + 3)){
                    if (!leafs.ContainsKey(element + 3)) leafs[element + 3] = 0;
                    leafs[element + 3] += leafs[element];
                }
            }

            var solutionPart1 = oneJolt * threeJolts;
            var solutionPart2 = leafs[input.Last()];

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }
    }
}
