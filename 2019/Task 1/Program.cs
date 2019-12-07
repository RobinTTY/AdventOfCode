using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_T1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var input = File.ReadAllLines("Input.txt").Select(int.Parse).ToArray();
            var output = CalcFuelRequirementSpacecraft(input);
        }

        private static int CalcFuelRequirementSpacecraft(IEnumerable<int> massArr) => massArr.Sum(CalcFuelRequirementModule);

        private static int CalcFuelRequirementModule(int mass) => (int)Math.Floor((double)mass / 3) - 2;
    }
}
