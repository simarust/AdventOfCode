using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(".\\input");
            var regex = @"(\d+)-(\d+) (.): (.+)";
            var count1 = 0;
            var count2 = 0;
            foreach (var line in input) {
                var match = Regex.Match(line, regex);
                var parsed = new {
                    FirstNum = Convert.ToInt32(match.Groups[1].Value),
                    SecondNum = Convert.ToInt32(match.Groups[2].Value),
                    Character = match.Groups[3].Value,
                    Password = match.Groups[4].Value
                };
                if (ValidPassword1(parsed)) count1++;
                if (ValidPassword2(parsed)) count2++;
            }
            Console.WriteLine($"Solution 1: {count1}");
            Console.WriteLine($"Solution 2: {count2}");
        }

        public static bool ValidPassword1(dynamic policies)
        {
            string password = policies.Password.ToString();
            string character = policies.Character.ToString();
            var minimum = policies.FirstNum;
            var maximum = policies.SecondNum;
            var characterCount = password.Count(c => c.ToString() == character);
            return characterCount >= minimum && characterCount <= maximum;
        }

        public static bool ValidPassword2(dynamic policies)
        {
            string password = policies.Password.ToString();
            string character = policies.Character.ToString();
            var position1 = policies.FirstNum - 1;
            var position2 = policies.SecondNum - 1;

            return password[position1].ToString() == character ^ password[position2].ToString() == character;
        }
     }
}
