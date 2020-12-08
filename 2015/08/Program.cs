using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _08
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");

            var sumOfOriginalLineLengths = input.Sum(line => line.Length);
            var sumOfRealLineLengths = input.Sum(line => Regex.Unescape(line.Substring(1, line.Length - 2)).Length);
            var sumOfEscapedLineLengths = input.Sum(line => $"\"{Escape(line)}\"".Length);

            var solutionPart1 = sumOfOriginalLineLengths - sumOfRealLineLengths;
            var solutionPart2 = sumOfEscapedLineLengths - sumOfOriginalLineLengths;

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");

            string Escape(string input)
            {
                return input.Replace("\\", "\\\\").Replace("\"", "\\\"");
            }
        }
    }
}
