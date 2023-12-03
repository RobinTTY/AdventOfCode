using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        return new (_input.Split("\n")
            .Select(line => new {line, game = ParseGame(line)})
            .Where(t => t.game.red <= 12 && t.game.green <= 13 && t.game.blue <= 14)
            .Select(t => t.game.id).Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new ((_input.Split("\n")
            .Select(line => new {line, game = ParseGame(line)})
            .Select(t => t.game.red * t.game.green * t.game.blue)).Sum().ToString());
    }

    private static Game ParseGame(string line) => 
        new(
            ParseInts(line, @"Game (\d+)").First(), 
            ParseInts(line, @"(\d+) red").Max(),
            ParseInts(line, @"(\d+) green").Max(),
            ParseInts(line, @"(\d+) blue").Max()
        );

    private static IEnumerable<int> ParseInts(string st, string rx) => Regex.Matches(st, rx).Select(m => int.Parse(m.Groups[1].Value));
}

record Game(int id, int red, int green, int blue);
