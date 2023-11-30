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
                    .Select(inputString => int.TryParse(inputString, out var number) ? number : (int?)null)
                    .Where(number => number.HasValue)
                    .Select(number => number.Value)
                    .ToList();

            var (item1, item2) = FindPairWithSum2020(parsedInput);
            Console.WriteLine($"Pair multiplied: {item1 * item2}");
            var ((item3, item4), item5) = FindTripletWithSum2020(parsedInput);
            Console.WriteLine($"Triplet multiplied: {item3 * item4 * item5}");
        }

        private static Tuple<int, int> FindPairWithSum2020(IReadOnlyCollection<int> input) => input.SelectMany(x => input, Tuple.Create).First(tuple => tuple.Item1 + tuple.Item2 == 2020);

        private static Tuple<Tuple<int, int>, int> FindTripletWithSum2020(IReadOnlyCollection<int> input) =>
            input.SelectMany(x => input, Tuple.Create)
                .SelectMany(x => input, Tuple.Create)
                .First(tuple => tuple.Item1.Item1 + tuple.Item1.Item2 + tuple.Item2 == 2020);
    }
}
