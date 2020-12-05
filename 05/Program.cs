using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            var seatIds = new List<int>();
            var boardingPassesScan = File.ReadAllLines("./input");
            var mySeatId = 0;

            foreach (var boardingPass in boardingPassesScan)
            {
                var row = Convert.ToInt32(boardingPass.Substring(0, 7).Replace("F", "0").Replace("B", "1"), 2);
                var column = Convert.ToInt32(boardingPass.Substring(7, 3).Replace("R", "1").Replace("L", "0"), 2);
                var seatId = row * 8 + column;
                seatIds.Add(seatId);
            }

            foreach (var seat in seatIds.OrderBy(s => s))
            {
                if (!seatIds.Contains(seat + 1) && seatIds.Contains(seat + 2)) {
                    mySeatId = seat + 1;
                    break;
                }
            }

            var maxSeatId = seatIds.Max();
            Console.WriteLine($"Solution Part 1: {maxSeatId}");
            Console.WriteLine($"Solution Part 2: {mySeatId}");
        }
    }
}
