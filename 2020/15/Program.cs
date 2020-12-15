using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _15
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = new List<int> { 9, 6, 0, 10, 18, 2, 1 };
            var solutionPart1 = input.CalculateNthNumber(2020);
            Console.WriteLine($"Solution Part 1: {solutionPart1}");

            var solutionPart2 = input.CalculateNthNumber(30000000);
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }

        static int CalculateNthNumber(this List<int> input, int n)
        {
            var numbers = new Dictionary<int, Tuple<int, int>>();

            for (int i = 0; i < input.Count(); i++)
            {
                numbers[input[i]] = new Tuple<int, int>(0, i + 1);
            }

            var lastValue = input.Last();

            for (int i = input.Count() + 1; i <= n; i++)
            {
                var lastOccurences = numbers[lastValue];
                var newValue = 0;
                if (lastOccurences.Item1 != 0)
                {
                    newValue = lastOccurences.Item2 - lastOccurences.Item1;
                }

                if (numbers.ContainsKey(newValue))
                    numbers[newValue] = new Tuple<int, int>(numbers[newValue].Item2, i);
                else
                    numbers[newValue] = new Tuple<int, int>(0, i);

                lastValue = newValue;
            }

            return lastValue;
        }
    }
}
