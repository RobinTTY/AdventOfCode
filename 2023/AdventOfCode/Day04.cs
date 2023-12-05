using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly IEnumerable<string> _input;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var allNumbers = _input.Select(x => x.Split(':', '|').Skip(1).ToArray().Where(s => !string.IsNullOrEmpty(s))).ToArray();
        var output = allNumbers
            .Select(numbers => numbers.Select(number => number.Split(' ').Where(num => !string.IsNullOrEmpty(num)).Select(int.Parse).ToList()).ToList())
            .Select(currentGame => currentGame.ElementAt(0).Intersect(currentGame.ElementAt(1)).Count())
            .Select(currentWins => currentWins == 0 ? 0 : 1 << (currentWins - 1)).Sum();
        return new ValueTask<string>(output.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new (_input.Reverse()
            .Select(line => line.Nums())                                                
            .Select(nums =>
            {
                var enumerable = nums as int[] ?? nums.ToArray();
                return (enumerable.Skip(1).Take(10), enumerable.Skip(11));
            })                    
            .Select((pair, idx) => (cnt: pair.Item1.Intersect(pair.Item2).Count(), idx))     
            .Aggregate(new List<int>(), (l, pair) => l.Append(l.Skip(pair.idx - pair.cnt).Take(pair.cnt).Sum() + 1).ToList())
            .Sum().ToString());
    }
}

public static class Extensions
{
    public static IEnumerable<int> Nums(this string str)
    {
        return Regex.Matches(str, @"-?\d+").Select(x => int.Parse(x.Value));
    }
}
