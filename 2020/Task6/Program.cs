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
            var parsedInput = string.Join("--", File.ReadLines("input.txt").Select(str => str == "" ? ":" : str));
            Console.WriteLine($"Uniques yes answers: {CountUniqueYesAnswers(parsedInput.Replace("--", ""))}");
            Console.WriteLine($"Mutual yes answers: {CountMutualYesAnswers(parsedInput)}");
        }

        public static int CountUniqueYesAnswers(string input) => input.Split(":").Select(str => str.GroupBy(c => c)).Sum(c => c.Count());

        public static int CountMutualYesAnswers(string input) =>
            input.Split(":").Select(str => str.Split("--").Where(str => str != "").Select(person => person.ToArray())
                .Aggregate<IEnumerable<char>>((last, next) => last.Intersect(next)).ToList()).Sum(cha => cha.Count);
    }
}
