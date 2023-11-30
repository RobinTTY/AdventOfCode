using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Program
    {
        static void Main(string[] args)
        {
            var parsedInput = File.ReadAllLines("input.txt").Where(line => line.Length > 0).Select(long.Parse).ToList();
            var preamble = parsedInput.Take(25).ToList();
            parsedInput.RemoveRange(0, 25);
            
            // create all possible permutations and results
            var permutations = CreatePermutations(preamble);
            var possibleResults = permutations.Select(pair => pair.Item1 + pair.Item2).ToList();

            while (parsedInput.Any())
            {
                var currentCheckValue = parsedInput.ElementAt(0);
                if (!possibleResults.Contains(currentCheckValue))
                {
                    Console.WriteLine($"Part 1 first non conforming number: {currentCheckValue}");
                    break;
                }
                
                // remove no longer needed values
                permutations.RemoveRange(0,9);
                parsedInput.RemoveAt(0);
                preamble.RemoveAt(0);
                
                // update permutations based on current value
                preamble.Add(currentCheckValue);
                permutations = CreatePermutations(preamble);
                
                Console.WriteLine($"Preamble length: {preamble.Count}");
                Console.WriteLine($"permutations length: {permutations.Count}");
            }
        }

        private static List<Tuple<long, long>> CreatePermutations(IReadOnlyCollection<long> input) =>
            input.SelectMany(_ => input, Tuple.Create).Where(pair => pair.Item1 != pair.Item2).ToList();
    }
}
