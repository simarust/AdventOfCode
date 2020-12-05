using System;
using System.IO;
using System.Linq;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(".\\input");

            var up = input.Count(c => c == '(');
            var down = input.Count(c => c == ')');

            var destination = up - down;

            var currentFloor = 0;
            var currentPosition = 0;
            foreach (var c in input)
            {
                currentPosition++;
                currentFloor = c == '(' ? currentFloor + 1 : currentFloor - 1;
                if (currentFloor == -1) break;
            }

            Console.WriteLine($"Solution Part1: {destination}");
            Console.WriteLine($"Solution Part 2: {currentPosition}");
        }
    }
}
