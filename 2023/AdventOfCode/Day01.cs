using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day01 : BaseDay
{
    private readonly IEnumerable<string> _input;

    public Day01()
    {
        _input = File.ReadLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = _input.Sum(line => int.Parse(string.Concat(line.First(char.IsDigit), line.Last(char.IsDigit))));
        return new (sum.ToString(CultureInfo.InvariantCulture));
    }

    public override ValueTask<string> Solve_2()
    {
        var rx = MyRegex(); 
        return new (_input.Select(line => rx.Matches(line))
            .Select(matches => (matches, outputLocal: MapValue(matches.First().Groups.Values.Last().Value) * 10))
            .Select(t => t.outputLocal + MapValue(t.matches.Last().Groups.Values.Last().Value)).Sum().ToString());
    }

    private static int MapValue(string input)
    {
        return input switch
        {
            "zero" => 0,
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => int.Parse(input)
        };
    }

    [GeneratedRegex(@"(?=(one|two|three|four|five|six|seven|eight|nine|[1-9]))", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}
