using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace _05
{
    static class Program
    {

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");
            var niceStrings1 = new List<string>();
            var niceStrings2 = new List<string>();

            niceStrings1 = input.Where(s => s.HasThreeVowels() && s.ContainsDoubleLetter() && s.DoesNotContainBadStrings()).ToList();
            niceStrings2 = input.Where(s => s.ContainsPairOfLettersTwice() && s.SameLetterRepeatsWithOneLetterBetween()).ToList();

            Console.WriteLine($"Solution Part 1: {niceStrings1.Count()}");
            Console.WriteLine($"Solution Part 2: {niceStrings2.Count()}");
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
