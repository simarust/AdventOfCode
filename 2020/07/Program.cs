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
            var regex = new Regex(@"([\d])*[ ]*([\w]+ [\w]+) bag[s]*");
            var bags = new Dictionary<string, List<Tuple<string, int>>>();

            foreach (var line in input)
            {
                var matches = regex.Matches(line);
                var bagName = matches.First().Groups[2].Value;
                var innerBags = matches
                    .Where(match => match.Groups[1].Value != "")
                    .Select(match => new Tuple<string, int>(match.Groups[2].Value, Int32.Parse(match.Groups[1].Value)))
                    .ToList();
                bags.Add(bagName, innerBags);
            }

            var outerBags = bags.GetOuterBags("shiny gold");
            var innerBagCount = bags.CountInnerBags("shiny gold");

            var solutionPart1 = outerBags.Distinct().Count();
            var solutionPart2 = innerBagCount;

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }

        private static List<string> GetOuterBags(this Dictionary<string, List<Tuple<string, int>>> bags, string innerBag)
        {
            var result = new List<string>();
            var directOuterBags = bags.Where(bag => bag.Value.Exists(t => t.Item1 == innerBag)).Select(d => d.Key).ToList();
            result.AddRange(directOuterBags);
            foreach (var directOuterBag in directOuterBags)
            {
                if (bags.ContainsKey(directOuterBag))
                {
                    result.AddRange(bags.GetOuterBags(directOuterBag));
                }
            }
            return result;
        }

        private static int CountInnerBags(this Dictionary<string, List<Tuple<string, int>>> bags, string outerBag)
        {
            var result = 0;
            var directInnerBags = bags[outerBag].Sum(t => t.Item2);
            result += directInnerBags;
            foreach (var innerBag in bags[outerBag])
            {
                result += bags.CountInnerBags(innerBag.Item1) * innerBag.Item2;
            }
            return result;
        }
    }
}
