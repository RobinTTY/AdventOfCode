using System.Globalization;

namespace AdventOfCode;

public class Day01 : BaseDay
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
        string[] validNumbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
        var cleanInput = new List<string>();
        
        _input.ToList().ForEach(line =>
        {
            var index = 0;
            var indexOfNumbersInText = new int?[9];
            
            foreach (var validNumber in validNumbers)
            {
                var result = line.IndexOf(validNumber, StringComparison.Ordinal);
                indexOfNumbersInText[index] = result == -1 ? null : result;
                index++;
            }

            var newLine = line;
            var min = Array.FindIndex(indexOfNumbersInText, i => i == indexOfNumbersInText.Min());
            var max = Array.FindIndex(indexOfNumbersInText, i => i == indexOfNumbersInText.Max());
            newLine = newLine.Replace(validNumbers[min], (min + 1).ToString());
            newLine = newLine.Replace(validNumbers[max], (max + 1).ToString());
            cleanInput.Add(newLine);
        });
        
        var sum = cleanInput.Sum(line => int.Parse(string.Concat(line.First(char.IsDigit), line.Last(char.IsDigit))));
        return new (sum.ToString(CultureInfo.InvariantCulture));
    }
}
