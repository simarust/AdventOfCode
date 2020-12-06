using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            var housesPart1 = new HashSet<string> { "0-0" };
            var housesPart2 = new HashSet<string> { "0-0" };
            var santaPart1 = new Santa();
            var santaPart2 = new Santa();
            var roboSantaPart2 = new Santa();
            var santasTurn = true;

            var input = File.ReadAllText(".\\input");

            foreach (var instruction in input)
            {
                switch (instruction)
                {
                    case '<':
                        santaPart1.X--;
                        if (santasTurn) santaPart2.X--;
                        else roboSantaPart2.X--;
                        break;
                    case '>':
                        santaPart1.X++;
                        if (santasTurn) santaPart2.X++;
                        else roboSantaPart2.X++;
                        break;
                    case '^':
                        santaPart1.Y--;
                        if (santasTurn) santaPart2.Y--;
                        else roboSantaPart2.Y--;
                        break;
                    case 'v':
                        santaPart1.Y++;
                        if (santasTurn) santaPart2.Y++;
                        else roboSantaPart2.Y++;
                        break;
                }

                santasTurn = !santasTurn;

                housesPart1.Add($"{santaPart1.X}-{santaPart1.Y}");
                housesPart2.Add($"{santaPart2.X}-{santaPart2.Y}");
                housesPart2.Add($"{roboSantaPart2.X}-{roboSantaPart2.Y}");
            }

            Console.WriteLine($"Solution Part1: {housesPart1.Count()}");
            Console.WriteLine($"Solution Part2: {housesPart2.Count()}");
        }

        private class Santa
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
