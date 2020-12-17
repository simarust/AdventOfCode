using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _17
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input");

            var _neighbourOffsets = GenerateNeighours().ToList();
            _neighbourOffsets.Remove((0, 0, 0, 0));
            Dictionary<(int x, int y, int z, int w), bool> _cubes =
                input.SelectMany((x, i) => x.Select((y, j) => (Coord: (j, i, 0, 0), Char: y)))
                    .ToDictionary(x => x.Coord, x => x.Char == '#' ? true : false);
            
            var initialState = _cubes;
            var solutionPart1 = Solve(false);
            Console.WriteLine($"Solution Part 1: {solutionPart1}");

            _cubes = initialState;
            var solutionPart2 = Solve(true);
            Console.WriteLine($"Solution Part 2: {solutionPart2}");

            int Solve(bool isPartTwo)
            {
                var result = 0;
                for (int i = 0; i < 6; i++)
                {
                    result = RunCycle(isPartTwo);
                }
                return result;
            }

            int RunCycle(bool isPartTwo)
            {
                var nextDict = new Dictionary<(int x, int y, int z, int w), bool>();

                Expand(isPartTwo);

                var keys = _cubes.Keys.ToList();
                foreach (var key in keys)
                {
                    var activeNeighbours = isPartTwo
                        ? _neighbourOffsets
                            .Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, key.w + x.w))
                            .Count(x => _cubes.ContainsKey(x) && _cubes[x] == true)
                        : _neighbourOffsets.Where(x => x.w == 0)
                            .Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, 0))
                            .Count(x => _cubes.ContainsKey(x) && _cubes[x] == true);

                    bool nextStatus;

                    if (_cubes[key] == true)
                        nextStatus = activeNeighbours == 2 || activeNeighbours == 3 ? true : false;
                    else
                        nextStatus = activeNeighbours == 3 ? true : false;

                    nextDict[key] = nextStatus;
                }
                _cubes = nextDict;
                return _cubes.Keys.Count(x => _cubes[x] == true);
            }

            void Expand(bool isPartTwo)
            {
                var keys = _cubes.Keys.ToList();
                foreach (var key in keys)
                {
                    var neighbours = isPartTwo
                        ? _neighbourOffsets.Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, key.w + x.w))
                        : _neighbourOffsets.Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, 0));

                    foreach (var neighbour in neighbours)
                    {
                        if (!_cubes.TryGetValue(neighbour, out var value))
                        {
                            _cubes[neighbour] = false;
                        }
                    }
                }
            }

            IEnumerable<(int x, int y, int z, int w)> GenerateNeighours()
            {
                for (int x = -1; x < 2; x++)
                    for (int y = -1; y < 2; y++)
                        for (int z = -1; z < 2; z++)
                            for (int w = -1; w < 2; w++)
                                yield return (x, y, z, w);
            }
        }
    }
}