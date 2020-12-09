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
                .Select(line => Int64.Parse(line));

            var preambleLength = 25;

            var solutionPart1 = 0L;
            var solutionPart2 = 0L;

            for (int i = preambleLength; i < input.Count(); i++)
            {
                if (input.GetPreambleSums(i, preambleLength).Contains(input.ElementAt(i))) continue;
                else solutionPart1 = input.ElementAt(i);
            }

            for (int i = 0; i < input.Count(); i++)
            {
                var j = i;
                var sum = 0L;
                while (sum < solutionPart1)
                {
                    sum += input.ElementAt(j);
                    j++;
                }

                if (sum == solutionPart1)
                {
                    var range = input.ToList().GetRange(i, j - i);
                    solutionPart2 = range.Min() + range.Max();
                    break;
                }
            }

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }

        static IEnumerable<long> GetPreambleSums(this IEnumerable<long> input, int i, int preambleLength)
        {
            var preamble = input.ToList().GetRange(i - preambleLength, preambleLength);
            return preamble.SelectMany(value => preamble.Where(x => x != value).Select(x => x + value));
        }
    }
}
