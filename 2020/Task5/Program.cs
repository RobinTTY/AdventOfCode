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
            var result = File.ReadAllLines("input.txt").ToList().Select(GetSeatId).ToList();
            Console.WriteLine($"Result for part 1: {result.Max()}");
            Console.WriteLine($"Result for part 1: {SearchMySeat(result)}");
        }

        private static int GetSeatId(string seatIdentifier) => Convert.ToInt32(seatIdentifier.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2);

        private static int SearchMySeat(IReadOnlyCollection<int> seatIds) => Enumerable.Range(seatIds.Min(), seatIds.Max() - seatIds.Min() + 1).Except(seatIds).Single();
    }
}
