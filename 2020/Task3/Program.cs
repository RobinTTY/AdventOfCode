using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsedInput = File.ReadAllText("input.txt")
                .Split("\n").Where(line => line.Length > 0).ToList();

            Console.WriteLine($"Number of trees that were hit for Part 1: {GetNumberOfTreesHit(parsedInput, 1, 3)}");
            var result = GetNumberOfTreesHit(parsedInput, 1, 1) * GetNumberOfTreesHit(parsedInput, 1, 3) *
                         GetNumberOfTreesHit(parsedInput, 1, 5) * GetNumberOfTreesHit(parsedInput, 1, 7) *
                         GetNumberOfTreesHit(parsedInput, 2, 1);
            Console.WriteLine($"Number of trees that were hit for Part 2: {result}");
        }

        public static long GetNumberOfTreesHit(List<string> input, int rowSpeed, int colSpeed) => input.Where((line, iterator) => line[iterator / rowSpeed * colSpeed % line.Length] == '#' && iterator % rowSpeed == 0).Count();
    }
}
