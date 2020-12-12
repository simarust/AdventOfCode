using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _12
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input");

            var numbers = new Regex(@"[-\d]+");
            
            var solutionPart1 = numbers.Matches(input).Sum(match => Int32.Parse(match.Value));

            var accounting = JArray.Parse(input);

            while (true)
            {
                var red = accounting.Descendants().FirstOrDefault(d => d.GetType() == typeof(JObject) && ((JObject)d).PropertyValues().Contains("red"));
                if (red == null) break;
                red.Replace("");
            }

            var solutionPart2 = numbers.Matches(accounting.ToString()).Sum(match => Int32.Parse(match.Value));

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }
    }
}
