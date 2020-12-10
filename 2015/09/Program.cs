using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Combinatorics.Collections;

namespace _09
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");
            var regex = new Regex(@"([\w]+) to ([\w]+) = ([\d]+)");
            var routes = input.Select(line => {
                var match = regex.Match(line);
                return new Route
                {
                    Destinations = new List<string> { match.Groups[1].Value, match.Groups[2].Value },
                    Distance = Int32.Parse(match.Groups[3].Value)
                };
            }).ToList();

            var allDestinations = routes.SelectMany(route => route.Destinations).Distinct().ToList();
            var destinationCount = allDestinations.Count();
            var permutations = new Permutations<string>(allDestinations);
            var totalDistances = permutations.Select(permutation => {
                var totalDistance = 0;
                for (int i = 0; i < permutation.Count() - 1; i++)
                {
                    totalDistance += routes
                        .Single(route => route.Destinations.Contains(permutation[i]) && route.Destinations.Contains(permutation[i + 1]))
                        .Distance;
                }
                return totalDistance;
            }).ToList();

            var solutionPart1 = totalDistances.Min();
            var solutionPart2 = totalDistances.Max();

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }
    }

    public class Route {
        public List<string> Destinations { get; set; }
        public int Distance { get; set; }
    }
}
