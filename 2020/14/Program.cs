using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Combinatorics.Collections;

namespace _14
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");

            var maskRegex = new Regex(@"mask = ([10X]+)");
            var memoryRegex = new Regex(@"mem\[([\d]+)\] = ([\d]+)");

            var currentOnMask = 0UL;
            var currentOffMask = ulong.MaxValue;

            var memoryPart1 = new Dictionary<int, ulong>();

            foreach (var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    var mask = maskRegex.Match(line).Groups[1].Value;
                    currentOnMask = Convert.ToUInt64($"{new string('0', 28)}{new string(mask.Select(c => c == '1' ? '1' : '0').ToArray())}", 2);
                    currentOffMask = Convert.ToUInt64($"{new string('1', 28)}{new string(mask.Select(c => c == '0' ? '0' : '1').ToArray())}", 2);
                }
                else
                {
                    var match = memoryRegex.Match(line);
                    var address = Int32.Parse(match.Groups[1].Value);
                    var value = ulong.Parse(match.Groups[2].Value);
                    value = value | currentOnMask;
                    value = value & currentOffMask;
                    memoryPart1[address] = value;
                }
            }

            var sum = 0UL;

            foreach (var m in memoryPart1)
            {
                sum += m.Value;
            }

            ulong solutionPart1 = sum;

            Console.WriteLine($"Solution Part 1: {solutionPart1}");

            var memoryPart2 = new Dictionary<string, ulong>();

            string currentMask = "";
            IEnumerable<IEnumerable<int>> allPossibleSubsets = new List<List<int>>();

            foreach (var line in input)
            {
                if (line.StartsWith("mask"))
                {
                    currentMask = maskRegex.Match(line).Groups[1].Value;
                    var currentXIndices = currentMask.Select((c, i) => new { i, c }).Where(x => x.c == 'X').Select(x => x.i);
                    allPossibleSubsets = SubSetsOf(currentXIndices);
                }
                else
                {
                    var match = memoryRegex.Match(line);
                    var baseAddress = match.Groups[1].Value;
                    var value = ulong.Parse(match.Groups[2].Value);
                    var addresses = allPossibleSubsets.Select(bitIndices => {
                        var binaryAddress = Convert.ToString(int.Parse(baseAddress), 2);
                        var address = new StringBuilder($"{new string('0', 36 - binaryAddress.Length)}{binaryAddress}");
                        for (int i = 0; i < currentMask.Length; i++)
                        {
                            if (currentMask[i] == 'X')
                            {
                                address[i] = bitIndices.Contains(i) ? '1' : '0';
                            } else if (currentMask[i] == '1') {
                                address[i] = '1';
                            }
                        }
                        return address.ToString();
                    });

                    foreach (var address in addresses)
                    {
                        memoryPart2[address] = value;
                    }
                }
            }

            sum = 0UL;

            foreach (var m in memoryPart2)
            {
                sum += m.Value;
            }

            var solutionPart2 = sum;

            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }
        
        public static IEnumerable<IEnumerable<T>> SubSetsOf<T>(IEnumerable<T> source)
        {
            if (!source.Any())
                return Enumerable.Repeat(Enumerable.Empty<T>(), 1);

            var element = source.Take(1);

            var haveNots = SubSetsOf(source.Skip(1));
            var haves = haveNots.Select(set => element.Concat(set));

            return haves.Concat(haveNots);
        }
    }
}
