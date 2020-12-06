using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");
            var totalPaperArea = 0;
            var totalRibbonLength = 0;

            foreach (var present in input)
            {
                var match = Regex.Match(present, @"(\d+)x(\d+)x(\d+)");
                var l = Int32.Parse(match.Groups[1].Value);
                var w = Int32.Parse(match.Groups[2].Value);
                var h = Int32.Parse(match.Groups[3].Value);
                var lengths = new List<int>() { l, w, h }.OrderBy(l => l);
                var areas = new List<int>() { l * w, w * h, h * l }.OrderBy(s => s);

                var paperArea = 3 * areas.ElementAt(0) + 2 * areas.ElementAt(1) + 2 * areas.ElementAt(2);
                totalPaperArea += paperArea;
                var ribbonLength = 2 * lengths.ElementAt(0) + 2 * lengths.ElementAt(1) + lengths.Aggregate(1, (a, b) => a * b);
                totalRibbonLength += ribbonLength;
            }

            Console.WriteLine($"Solution Part 1: {totalPaperArea}");
            Console.WriteLine($"Solution Part 2: {totalRibbonLength}");
        }
    }
}
