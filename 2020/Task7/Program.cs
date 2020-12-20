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
            var parsedInput = File.ReadAllLines("input.txt").ToList();
            var hierarchy = new Dictionary<string, List<Bag>>();
            
            // create initial hierarchy
            parsedInput.ForEach(line =>
            {
                var splitLine = line.Replace("bags", "").Replace("bag", "").Replace(".", "").Split("contain").Select(part => part.Trim()).ToArray();
                var containedBags = new List<Bag>();

                if (splitLine[1] == "no other")
                {
                    hierarchy.Add(splitLine[0], new List<Bag>());
                    return;
                }

                splitLine[1].Split(",").Select(line => line.Trim()).ToList().ForEach(line => containedBags.Add(new Bag(line.Substring(2), int.Parse(line.Take(1).ToArray()))));
                hierarchy.Add(splitLine[0], containedBags);
            });


            Console.WriteLine($"Part 1 count: {CountDistinctColors(hierarchy)}");
            Console.WriteLine($"Part 2 count: {CountIndividualBags(hierarchy)}");
        }

        public static int CountDistinctColors(Dictionary<string, List<Bag>> hierarchy)
        {
            List<string> bagsContainingGoldenBag;
            var resultSet = bagsContainingGoldenBag = hierarchy
                .Where(item => item.Value.Select(bag => bag.Color).Contains("shiny gold")).Select(item => item.Key)
                .ToList();
            while (bagsContainingGoldenBag.Any())
            {
                var kvpList = hierarchy.Where(kvp =>
                    kvp.Value.Select(bag => bag.Color).Intersect(bagsContainingGoldenBag).Any()).ToList();
                bagsContainingGoldenBag = kvpList.Select(item => item.Key).ToList();
                resultSet = resultSet.Concat(bagsContainingGoldenBag).ToList();
            }

            return resultSet.Distinct().Count();
        }

        public static int CountIndividualBags(Dictionary<string, List<Bag>> hierarchy, string color = "shiny gold") => StepThroughHierarchy(hierarchy, hierarchy[color]);

        public static int StepThroughHierarchy(Dictionary<string, List<Bag>> hierarchy, List<Bag> bags) => bags.Sum(bag => bag.Count) + bags.Sum(bag => StepThroughHierarchy(hierarchy, hierarchy[bag.Color]) * bag.Count);
    }

    public class Bag
    {
        public string Color { get; set; }
        public int Count { get; set; }

        public Bag(string color, int count)
        {
            Color = color;
            Count = count;
        }
    }
}
