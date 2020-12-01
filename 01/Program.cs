using System;
using System.IO;
using System.Linq;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(".\\input").Select(el => Convert.ToInt32(el));

            foreach (var number1 in input) {
                foreach (var number2 in input) {
                    if (number1 == number2) continue;
                    foreach (var number3 in input) {
                        if (number2 == number3 || number1 == number3) continue;
                        
                        if (number1 + number2 + number3 == 2020) {
                            Console.WriteLine($"Solution: {number1} * {number2} * {number3} = {number1 * number2 * number3}");
                            System.Environment.Exit(0);
                        }
                    }
                }
            }

            Console.WriteLine("No solution found! 😢");
        }
    }
}
