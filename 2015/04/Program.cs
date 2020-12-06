using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "ckczppom";
            var hash = "";
            var solution1 = 0;
            var solution2 = 0;

            using (var md5 = MD5.Create())
            {
                while (!hash.StartsWith("00000"))
                {
                    solution1++;
                    hash = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes($"{input}{solution1}"))).Replace("-", string.Empty);
                }

                while (!hash.StartsWith("000000"))
                {
                    solution2++;
                    hash = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes($"{input}{solution2}"))).Replace("-", string.Empty);
                }
            }

            Console.WriteLine($"Solution Part 1: {solution1}");
            Console.WriteLine($"Solution Part 1: {solution2}");
        }
    }
}
