using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _18
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input");

            var ruleRegex = new Regex(@"(\d+): ([\d\w"" |]+)");
            var numberRegex = new Regex(@"\d+");
            var messageRegex = new Regex(@"[ab]{2,}");

            var rulesDictionary = ruleRegex.Matches(input)
                .Select(m => new {
                    Index = Int32.Parse(m.Groups[1].Value),
                    Value = m.Groups[2].Value })
                .ToDictionary(x => x.Index, x => $"{x.Value.Trim('"')}");
            
            var messages = messageRegex.Matches(input).Select(m => m.Value);

            var rulesPart1 = new Regex($"^{ReplaceNumbers(rulesDictionary[0]).Replace(" ", "")}$");

            rulesDictionary[8] = "(42+)";
            rulesDictionary[11] = "(31+)";

            Regex rulesPart2 = new Regex($"^{ReplaceNumbers(rulesDictionary[0]).Replace(" ", "")}$");

            var solutionPart1 = messages.Count(m => rulesPart1.IsMatch(m));
            var solutionPart2 = messages.Count(m => rulesPart2.IsMatch(m)
                && rulesPart2.Match(m).Groups[1].Value.Length > rulesPart2.Match(m).Groups[2].Value.Length);

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");

            string ReplaceNumbers(string input)
            {
                return Regex.Replace(input, @"\d+", m => {
                    var replacement = rulesDictionary[Int32.Parse(m.Value)];
                    var replacementString = replacement.Contains('|') ? $"(?:{replacement})" : replacement;
                    return ReplaceNumbers(replacementString);
                });
            }
        }
    }
}
