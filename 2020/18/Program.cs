using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _18
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");

            var parenthesesRegex = new Regex(@"\([\d+* ]+\)");
            var leftToRightRegex = new Regex(@"([\d]+|[+*])");
            var additionRegex = new Regex(@"(\d[\d +]+\d)");
            var numbersRegex = new Regex(@"([\d]+)");
            var resultsPart1 = new List<long>();
            var resultsPart2 = new List<long>();

            foreach (var line in input)
            {
                var resultPart1 = line;

                while (resultPart1.Contains('(') || resultPart1.Contains(')'))
                {
                    foreach (Match match in parenthesesRegex.Matches(resultPart1))
                    {
                        var pExpressionResultPart1 = Parentheses(match.Value);
                        resultPart1 = resultPart1.Replace(match.Value, pExpressionResultPart1);
                    }
                }

                resultsPart1.Add(LeftToRight(resultPart1));

                var resultPart2 = line;

                while (resultPart2.Contains('(') || resultPart2.Contains(')') || resultPart2.Contains('+'))
                {
                    foreach (Match match in additionRegex.Matches(resultPart2))
                    {
                        var sum = Add(match.Value);
                        resultPart2 = resultPart2.Replace(match.Value, sum.ToString());
                    }
                    foreach (Match match in parenthesesRegex.Matches(resultPart2))
                    {
                        var result = Parentheses(match.Value, advanced: true);
                        resultPart2 = resultPart2.Replace(match.Value, result);
                    }
                }

                resultsPart2.Add(Multiply(resultPart2));
            }

            var solutionPart1 = resultsPart1.Sum();
            var solutionPart2 = resultsPart2.Sum();

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");

            long Add(string input)
            {
                var sum = 0L;
                foreach (Match match in numbersRegex.Matches(input))
                {
                    sum += long.Parse(match.Value);
                }
                return sum;
            }

            string Parentheses(string input, bool advanced = false)
            {
                if (advanced)
                {
                    if (input.Contains('+')) return input;
                    else return Multiply(input).ToString();
                }
                else
                {
                    return LeftToRight(input.Trim(new char[] { '(', ')' })).ToString();
                }
            }

            long Multiply(string input)
            {
                var result = 1L;
                foreach (Match match in numbersRegex.Matches(input))
                {
                    result *= long.Parse(match.Value);
                }
                return result;
            }

            long LeftToRight(string input)
            {
                var result = 0L;
                var currentOperator = "";

                foreach (Match match in leftToRightRegex.Matches(input))
                {
                    if (result == 0L)
                    {
                        result = long.Parse(match.Value);
                        continue;
                    }
                    else if (match.Value == "*" || match.Value == "+")
                    {
                        currentOperator = match.Value;
                    }
                    else
                    {
                        var currentNumber = long.Parse(match.Value);
                        result = currentOperator == "*" ? result *= currentNumber : result += currentNumber;
                    }
                }

                return result;
            }
        }
    }
}