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
            char[][] input = File.ReadAllLines("input")
                .Select(line => line.ToCharArray())
                .ToArray<char[]>();

            var seatsPart1 = input.Select(a => (char[])a.Clone()).ToArray();
            var seatsPart2 = input.Select(a => (char[])a.Clone()).ToArray();

            while(ApplyRulesPart1(seatsPart1)) {}
            while(ApplyRulesPart2(seatsPart2)) {}

            var solutionPart1 = seatsPart1.Sum(row => row.Count(c => c == '#'));
            var solutionPart2 = seatsPart2.Sum(row => row.Count(c => c == '#'));

            Console.WriteLine($"Solution Part 1: {solutionPart1}");
            Console.WriteLine($"Solution Part 2: {solutionPart2}");
            
            bool ApplyRulesPart1(char[][] seats)
            {                
                var stateChanged = false;
                var seatClone = seats.Select(a => (char[])a.Clone()).ToArray();
                for (int x = 0; x < seats.Length; x++)
                {
                    for (int y = 0; y < seats[x].Length; y++)
                    {
                        var occupiedAdjacentSeats = seatClone.OccupiedAdjacentSeats(x, y);
                        if (seatClone[x][y] == 'L' && occupiedAdjacentSeats == 0) 
                        {
                            seats[x][y] = '#';
                            stateChanged = true;
                        }
                        else if (seatClone[x][y] == '#' && occupiedAdjacentSeats >= 4)
                        {
                            seats[x][y] = 'L';
                            stateChanged = true;
                        }
                    }
                }

                return stateChanged;
            }

            bool ApplyRulesPart2(char[][] seats)
            {
                var stateChanged = false;
                var seatClone = seats.Select(a => (char[])a.Clone()).ToArray();
                for (int x = 0; x < seats.Length; x++)
                {
                    for (int y = 0; y < seats[x].Length; y++)
                    {
                        var occupiedAdjacentSeats = seatClone.OccupiedDirectionalSeats(x, y);
                        if (seatClone[x][y] == 'L' && occupiedAdjacentSeats == 0) 
                        {
                            seats[x][y] = '#';
                            stateChanged = true;
                        }
                        else if (seatClone[x][y] == '#' && occupiedAdjacentSeats >= 5)
                        {
                            seats[x][y] = 'L';
                            stateChanged = true;
                        }
                    }
                }

                return stateChanged;
            }
        }

        static int OccupiedAdjacentSeats(this char[][] seats, int ownX, int ownY)
        {
            var adjacentSeats = 0;
            if (ownX != 0                && ownY != 0                       && seats[ownX - 1][ownY - 1] == '#') adjacentSeats++;
            if (ownX != 0                                                   && seats[ownX - 1][ownY]     == '#') adjacentSeats++;
            if (ownX != 0                && ownY != seats[ownX].Length - 1  && seats[ownX - 1][ownY + 1] == '#') adjacentSeats++;
            if (                            ownY != 0                       && seats[ownX]    [ownY - 1] == '#') adjacentSeats++;
            if (                            ownY != seats[ownX].Length - 1  && seats[ownX]    [ownY + 1] == '#') adjacentSeats++;
            if (ownX != seats.Length - 1 && ownY != 0                       && seats[ownX + 1][ownY - 1] == '#') adjacentSeats++;
            if (ownX != seats.Length - 1                                    && seats[ownX + 1][ownY]     == '#') adjacentSeats++;
            if (ownX != seats.Length - 1 && ownY != seats[ownX].Length - 1  && seats[ownX + 1][ownY + 1] == '#') adjacentSeats++;
            return adjacentSeats;
        }

        static int OccupiedDirectionalSeats(this char[][] seats, int ownX, int ownY)
        {
            var directionalSeats = 0;
            if (seats.LookDirection(-1, -1, ownX, ownY) == '#') directionalSeats++;
            if (seats.LookDirection(-1,  0, ownX, ownY) == '#') directionalSeats++;
            if (seats.LookDirection(-1,  1, ownX, ownY) == '#') directionalSeats++;
            if (seats.LookDirection( 0, -1, ownX, ownY) == '#') directionalSeats++;
            if (seats.LookDirection( 0,  1, ownX, ownY) == '#') directionalSeats++;
            if (seats.LookDirection( 1, -1, ownX, ownY) == '#') directionalSeats++;
            if (seats.LookDirection( 1,  0, ownX, ownY) == '#') directionalSeats++;
            if (seats.LookDirection( 1,  1, ownX, ownY) == '#') directionalSeats++;
            return directionalSeats;
        }

        static char LookDirection(this char[][] seats, int xDir, int yDir, int ownX, int ownY)
        {
            int i = 1;
            while (true)
            {
                var x = ownX + i * xDir;
                var y = ownY + i * yDir;
                if (x < 0 || x > seats.Length - 1 || y < 0 || y > seats[x].Length - 1) return '.';
                var seat = seats[ownX + i * xDir][ownY + i * yDir];
                if (seat != '.') return seat;
                i++;
            }
        }
    }
}
