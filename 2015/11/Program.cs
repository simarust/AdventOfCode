using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _11
{
    static class Program
    {

        static void Main(string[] args)
        {
            var password = "cqjxjnds".ToCharArray();

            Regex rule1 = new Regex(@"(abc|bcd|cde|def|efg|fgh|ghi|hij|ijk|jkl|klm|lmn|mno|nop|opq|pqr|qrs|rst|stu|tuv|uvw|vwx|wxy|xyz)");
            Regex rule2 = new Regex(@"[iol]");
            Regex rule3 = new Regex(@"(aa|bb|cc|dd|ee|ff|gg|hh|ii|jj|kk|ll|mm|nn|oo|pp|qq|rr|ss|tt|uu|vv|ww|xx|yy|zz)");

            while (!IsValid(password))
            {
                Increment(password);
            }

            var solutionPart1 = new string(password);

            Increment(password);
            
            while (!IsValid(password))
            {
                Increment(password);
            }

            var solutionPart2 = new string(password);

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
            

            bool IsValid(char[] password)
            {
                var testee = new string(password);
                return rule1.IsMatch(testee) && !rule2.IsMatch(testee) && rule3.Matches(testee).Count() >= 2;
            }

            char[] Increment(char[] password, int position = 7)
            {
                if (password[position] == 'z')
                {
                    password[position] = 'a';
                    Increment(password, position - 1);
                }
                else
                {
                    password[position]++;
                }
                return password;
            }
        }
    }
}
