using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _06
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input");

            var groups = input.Split("\r\n\r\n");

            var totalNumberOfQuestions = 0;
            var totalNumberOfIntersectingQuestions = 0;

            foreach (var group in groups)
            {
                var persons = group.Split("\r\n");
                
                var numberOfDistinctQuestions = String.Concat(persons).Distinct().Count();
                totalNumberOfQuestions += numberOfDistinctQuestions;

                var intersection = persons
                    .Skip(1)
                    .Aggregate(
                        persons.First(),
                        (h, e) => { return String.Concat(h.Intersect(e)); }
                    );
                totalNumberOfIntersectingQuestions += intersection.Count();
            }

            Console.WriteLine($"Solution Part 1: {totalNumberOfQuestions}");
            Console.WriteLine($"Solution Part 2: {totalNumberOfIntersectingQuestions}");
        }
    }
}
