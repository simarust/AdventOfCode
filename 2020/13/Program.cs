using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _13
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");

            var busIdRegex = new Regex(@"[\d]+");

            var earliestTimestamp = Int64.Parse(input[0]);
            var busIds = busIdRegex.Matches(input[1]).Select(match => Int32.Parse(match.Value));

            var nextBusDeparture = earliestTimestamp;
            int possibleBusId;

            while (true)
            {
                possibleBusId = busIds.FirstOrDefault(busId => nextBusDeparture % busId == 0);
                if (possibleBusId != 0) break;
                nextBusDeparture++;
            }

            var solutionPart1 = possibleBusId * (nextBusDeparture - earliestTimestamp);
            Console.WriteLine($"Solution Part 1: {solutionPart1}");

            busIdRegex = new Regex(@"[x\d]+");
            var maxBusId = busIds.Max();
            var buses = busIdRegex
                .Matches(input[1])
                .Select(match => Int32.TryParse(match.Value, out int id) ? id : 0)
                .Select((id, i) => new { id, i })
                .Where(bus => bus.id != 0)
                .ToList();

            var timestamp = 0L;
            var stepSize = 1L;

            for (int i = 0; i < buses.Count(); i++)
            {
                stepSize = leastCommonMultiple(stepSize, buses[i].id);
                
                while (true)
                {
                    if (buses.Take(i + 2).All(bus => (timestamp + bus.i) % bus.id == 0)) break;
                    timestamp += stepSize;
                }
            }

            var solutionPart2 = timestamp;
            Console.WriteLine($"Solution Part 1: {solutionPart2}");

            long leastCommonMultiple(long a, long b)
            {
                long x = a > b ? a : b;
                long y = a > b ? b : a;

                for (long i = 1; i < y; i++)
                {
                    long mult = x * i;
                    if (mult % y == 0) return mult;
                }
                return x * y;
            }
        }
    }
}
