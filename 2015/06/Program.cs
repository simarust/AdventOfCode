using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace _06
{
    static class Program
    {

        static void Main(string[] args)
        {
            const int GridSize = 1000;
            var input = File.ReadAllLines("input");

            var lightsPart1 = new bool[GridSize * GridSize];
            var lightsPart2 = new int[GridSize * GridSize];

            var instructionRegex = new Regex(@"(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)");

            foreach (var instruction in input)
            {
                var instructionMatch = instructionRegex.Match(instruction);
                var switchInstruction = instructionMatch.Groups[1].Value;
                var startX = Int32.Parse(instructionMatch.Groups[2].Value);
                var startY = Int32.Parse(instructionMatch.Groups[3].Value);
                var stopX = Int32.Parse(instructionMatch.Groups[4].Value);
                var stopY = Int32.Parse(instructionMatch.Groups[5].Value);

                for (int x = startX; x <= stopX; x++)
                {
                    for (int y = startY; y <= stopY; y++)
                    {
                        switch (switchInstruction)
                        {
                            case "turn on":
                                lightsPart1[x * GridSize + y] = true;
                                lightsPart2[x * GridSize + y]++;
                                break;
                            case "turn off":
                                lightsPart1[x * GridSize + y] = false;
                                lightsPart2[x * GridSize + y] = Math.Max(0, lightsPart2[x * GridSize + y] - 1);
                                break;
                            case "toggle":
                                lightsPart1[x * GridSize + y] = !lightsPart1[x * GridSize + y];
                                lightsPart2[x * GridSize + y] = lightsPart2[x * GridSize + y] + 2;
                                break;
                        }
                    }
                }
            }

            var numberOfLitLights = lightsPart1.Count(l => l == true);
            var totalBrightness = lightsPart2.Sum();

            Console.WriteLine($"Solution Part 1: {numberOfLitLights}");
            Console.WriteLine($"Solution Part 2: {totalBrightness}");
        }

        static bool HasThreeVowels(this string input)
        {
            var threeVowels = new Regex(@"([aeiou])");
            return threeVowels.Matches(input).Count() >= 3;
        }

        static bool ContainsDoubleLetter(this string input)
        {
            var doubleLetter = new Regex(@"(\w)\1+");
            return doubleLetter.Matches(input).Count() >= 1;
        }

        static bool DoesNotContainBadStrings(this string input)
        {
            var noBadStrings = new Regex(@"(ab|cd|pq|xy)");
            return noBadStrings.Matches(input).Count() == 0;
        }

        static bool ContainsPairOfLettersTwice(this string input)
        {
            var letterPairTwice = new Regex(@"(\w\w)\w*\1+");
            return letterPairTwice.Matches(input).Count() >= 1;
        }

        static bool SameLetterRepeatsWithOneLetterBetween(this string input)
        {
            var sameletterWithOneBetween = new Regex(@"(\w)\w\1+");
            return sameletterWithOneBetween.Matches(input).Count >= 1;
        }
    }
}
