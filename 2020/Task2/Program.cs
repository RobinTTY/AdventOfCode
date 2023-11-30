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
                .Split("\n")
                .Select(line => line.Split(" "))
                .Select(line => line.Select(str => str.Replace(":", "")).ToList())
                .Where(line => line.Count == 3).ToList();

            Console.WriteLine($"The result for part 1 is {CountValidPasswordsPart1(parsedInput)}");
            Console.WriteLine($"The result for part 2 is {CountValidPasswordsPart2(parsedInput)}");

        }

        private static int CountValidPasswordsPart1(IEnumerable<List<string>> input) =>
            input.Count(elements => elements[2].Count(c => c == char.Parse(elements[1])) <= int.Parse(elements[0].Split("-")[1])
                                    && elements[2].Count(c => c == char.Parse(elements[1])) >= int.Parse(elements[0].Split("-")[0]));

        private static int CountValidPasswordsPart2(IEnumerable<List<string>> input) =>
            input.Count(elements => elements[2][int.Parse(elements[0].Split("-")[0]) - 1] == char.Parse(elements[1]) ^
                                    elements[2][int.Parse(elements[0].Split("-")[1]) - 1] == char.Parse(elements[1]));
    }
}
