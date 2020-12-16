using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _16
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input");

            var sections = input.Split(System.Environment.NewLine + System.Environment.NewLine);

            var ruleRegex = new Regex(@"([a-z ]+): ([\d]+)-([\d]+) or ([\d]+)-([\d]+)");

            var rules = ruleRegex.Matches(sections[0]).Select(m => new {
                FieldName = m.Groups[1].Value,
                Range1 = new Tuple<int, int> (Int32.Parse(m.Groups[2].Value), Int32.Parse(m.Groups[3].Value)),
                Range2 = new Tuple<int, int> (Int32.Parse(m.Groups[4].Value), Int32.Parse(m.Groups[5].Value))
            }).ToList();
            var myTicket = sections[1].Split(System.Environment.NewLine).Skip(1).Select(l => {
                return l.Split(',').Select(n => Int32.Parse(n));
            }).First().ToList();
            var nearbyTickets = sections[2].Split(System.Environment.NewLine).Skip(1).Select(l => {
                return l.Split(',').Select(n => Int32.Parse(n)).ToList();
            });

            var solutionPart1 = nearbyTickets.Sum(t => t.Sum(n => n.Matches(rules) ? 0 : n));

            var validNearbyTickets = nearbyTickets.Where(t => t.All(n => n.Matches(rules)));

            var fields = new Dictionary<int, List<int>>();
            foreach (var ticket in validNearbyTickets)
            {
                for (int i = 0; i < ticket.Count(); i++)
                {
                    if (fields.ContainsKey(i)) fields[i].Add(ticket[i]);
                    else fields[i] = new List<int> { ticket[i] };
                }
            }

            var fieldNames = new Dictionary<int, string>();

            var fieldCount = fields.Count();

            while (fieldNames.Count() < fieldCount)
            {
                foreach(var field in fields)
                {
                    var ruleMatches = rules.Where(r => field.Value.Matches(r));
                    if (ruleMatches.Count() == 1)
                        fieldNames[field.Key] = ruleMatches.First().FieldName;
                }
                foreach (var fieldName in fieldNames)
                {
                    fields.Remove(fieldName.Key);
                    rules.RemoveAll(r => r.FieldName == fieldName.Value);
                }
            }

            var departureFields = fieldNames.Where(n => n.Value.StartsWith("departure"));
            var solutionPart2 = departureFields.Aggregate(1L, (a, n) => a * myTicket[n.Key]);

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }

        static bool Matches(this List<int> numbers, dynamic rule)
        {
            return numbers.All(n => rule.Range1.Item1 <= n && n <= rule.Range1.Item2 ||
                                    rule.Range2.Item1 <= n && n <= rule.Range2.Item2);
        }

        static bool Matches(this int number, IEnumerable<dynamic> rules)
        {
            return rules.Any(rule => rule.Range1.Item1 <= number && number <= rule.Range1.Item2 ||
                                     rule.Range2.Item1 <= number && number <= rule.Range2.Item2);
        }
    }
}
