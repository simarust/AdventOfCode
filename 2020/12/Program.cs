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
            var input = File.ReadAllText("input");

            var regex = new Regex(@"([NSEWLRF])([\d]+)");
            var instructions = regex.Matches(input).Select(match => new {
                Instruction = match.Groups[1].Value,
                Value = Int32.Parse(match.Groups[2].Value)
            });

            var direction = 90;
            var northSouth = 0;
            var eastWest = 0;

            foreach (var instruction in instructions)
            {
                switch (instruction.Instruction)
                {
                    case "N": northSouth += instruction.Value; break;
                    case "S": northSouth -= instruction.Value; break;
                    case "E": eastWest   += instruction.Value; break;
                    case "W": eastWest   -= instruction.Value; break;
                    case "L": direction = (direction - instruction.Value + 360) % 360; break;
                    case "R": direction = (direction + instruction.Value) % 360; break;
                    case "F": 
                        if (direction == 0) northSouth += instruction.Value;
                        else if (direction == 90) eastWest += instruction.Value;
                        else if (direction == 180) northSouth -= instruction.Value;
                        else eastWest -= instruction.Value;
                        break;
                }
            }

            var shipNorthSouth = 0;
            var shipEastWest = 0;
            var wayPointNorthSouth = 1;
            var wayPointEastWest = 10;

            foreach (var instruction in instructions)
            {
                switch (instruction.Instruction)
                {
                    case "N": wayPointNorthSouth += instruction.Value; break;
                    case "S": wayPointNorthSouth -= instruction.Value; break;
                    case "E": wayPointEastWest   += instruction.Value; break;
                    case "W": wayPointEastWest   -= instruction.Value; break;
                    case "L":
                    case "R":
                        var oldNorthSouth = wayPointNorthSouth;
                        var oldEastWest = wayPointEastWest;
                        if (instruction.Instruction == "L" && instruction.Value == 270 || instruction.Instruction == "R" && instruction.Value == 90)
                        {
                            wayPointEastWest = oldNorthSouth;
                            wayPointNorthSouth = -oldEastWest;
                        }
                        else if (instruction.Value == 180)
                        {
                            wayPointEastWest = -oldEastWest;
                            wayPointNorthSouth = -oldNorthSouth;
                        }
                        else if (instruction.Instruction == "L" && instruction.Value == 90 || instruction.Instruction == "R" && instruction.Value == 270)
                        {
                            wayPointEastWest = -oldNorthSouth;
                            wayPointNorthSouth = oldEastWest;
                        }
                        break;
                    case "F":
                        shipNorthSouth += instruction.Value * wayPointNorthSouth;
                        shipEastWest += instruction.Value * wayPointEastWest;
                        break;
                }
            }

            var solutionPart1 = Math.Abs(northSouth) + Math.Abs(eastWest);
            var solutionPart2 = Math.Abs(shipNorthSouth) + Math.Abs(shipEastWest);

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
        }
    }
}
