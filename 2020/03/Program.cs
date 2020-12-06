using System;
using System.IO;
using System.Linq;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(".\\input");
            
            var slope1 = TraverseMap(input, 1, 1);
            Console.WriteLine($"Slope 1: {slope1} trees.");
            var slope2 = TraverseMap(input, 3, 1);
            Console.WriteLine($"Slope 2: {slope2} trees.");
            var slope3 = TraverseMap(input, 5, 1);
            Console.WriteLine($"Slope 3: {slope3} trees.");
            var slope4 = TraverseMap(input, 7, 1);
            Console.WriteLine($"Slope 4: {slope4} trees.");
            var slope5 = TraverseMap(input, 1, 2);
            Console.WriteLine($"Slope 5: {slope5} trees.");
            long product = slope1 * slope2 * slope3 * slope4 * slope5;

            Console.WriteLine($"Solution Part1: {slope2}");
            Console.WriteLine($"Solution Part2: {product}");
        }

        private static long TraverseMap(string[] map, int xStep, int yStep)
        {
            var mapWidth = map[0].Count();
            var x = 0;
            var y = 0;
            long numberOfTrees = 0;
            while (y < map.Count() - 1)
            {
                x += xStep; x = x % mapWidth;
                y += yStep;
                if (map[y][x] == '#') numberOfTrees++;
            }
            return numberOfTrees;
        }
    }
}
