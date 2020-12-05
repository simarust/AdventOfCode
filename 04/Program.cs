using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            var batchFile = File.ReadAllLines("./input");
            var regex = @"(\w{3}):(\S+)";
            var currentPassport = new Passport();
            var passports = new List<Passport>();

            foreach (var line in batchFile)
            {
                var matches = Regex.Matches(line, regex);
                foreach (Match match in matches)
                {
                    var key = match.Groups[1].Value;
                    var value = match.Groups[2].Value;

                    switch (key)
                    {
                        case "byr": currentPassport.BirthYear = value; break;
                        case "iyr": currentPassport.IssueYear = value; break;
                        case "eyr": currentPassport.ExpirationYear = value; break;
                        case "hgt": currentPassport.Height = value; break;
                        case "hcl": currentPassport.HairColor = value; break;
                        case "ecl": currentPassport.EyeColor = value; break;
                        case "pid": currentPassport.PassportId = value; break;
                        case "cid": currentPassport.CountryId = value; break;
                    }
                }
                
                if (String.IsNullOrEmpty(line))
                {
                    passports.Add(currentPassport);
                    currentPassport = new Passport();
                }
            }

            var numberOfValidPassports1 = passports.Count(p => p.IsValid1());
            var numberOfValidPassports2 = passports.Count(p => p.IsValid2());
            
            Console.WriteLine($"Solution Part 1: {numberOfValidPassports1}");
            Console.WriteLine($"Solution Part 2: {numberOfValidPassports2}");
        }

        private class Passport {
            public string BirthYear { get; set; }
            public string IssueYear { get; set; }
            public string ExpirationYear { get; set; }
            public string Height { get; set; }
            public string HairColor { get; set; }
            public string EyeColor { get; set; }
            public string PassportId { get; set; }
            public string CountryId { get; set; }

            public bool IsValid1()
            {
                return BirthYear != default && IssueYear != default
                    && ExpirationYear != default && Height != default
                    && HairColor != default && EyeColor != default
                    && PassportId != default;
            }

            public bool IsValid2()
            {
                return BirthYear != default && Int32.TryParse(BirthYear, out int birthYear) && birthYear >= 1920 && birthYear <= 2002
                    && IssueYear != default && Int32.TryParse(IssueYear, out int issueYear) && issueYear >= 2010 && issueYear <= 2020
                    && ExpirationYear != default && Int32.TryParse(ExpirationYear, out int expirationYear) && expirationYear >= 2020 && expirationYear <= 2030
                    && Height != default && IsValidHeightString(Height)
                    && HairColor != default && Regex.Match(HairColor, @"#[0-9a-f]{6}").Success
                    && EyeColor != default && Regex.Match(EyeColor, @"(amb|blu|brn|gry|grn|hzl|oth)").Success
                    && PassportId != default && Int32.TryParse(PassportId, out _) && PassportId.Count() == 9;
            }

            private static bool IsValidHeightString(string heightString)
            {
                var match = Regex.Match(heightString, @"(\d+)(cm|in)");
                return match.Groups[2].Value == "cm"
                    ? Int32.TryParse(match.Groups[1].Value, out int heightCm) && heightCm >= 150 && heightCm <= 193
                    : match.Groups[2].Value == "in"
                        ? Int32.TryParse(match.Groups[1].Value, out int heightIn) && heightIn >= 59 && heightIn <= 76
                        : false;
            }
        }
    }
}
