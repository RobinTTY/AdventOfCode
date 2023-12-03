namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;
    private readonly char[][] _schematic;

    public Day03()
    {
        _input = File.ReadLines(InputFilePath).ToArray();
        _schematic = ParseSchematic();
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = 0;
        for (var row = 0; row < _schematic.Length; row++)
        for (var column = 0; column < _schematic[row].Length; column++)
        {
            if (!char.IsDigit(_schematic[row][column])) continue;
            if (TryGetPartNumber(row, ref column, out var partNumber)) sum += partNumber;
        }
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;
        
        for (var row = 0; row < _schematic.Length; row++)
        {
            if (!_schematic[row].Contains('*')) continue;
            for (var column = 0; column < _schematic[row].Length; column++)
            {
                var current = _schematic[row][column];
                if (current != '*') continue;

                var neighbours = GetNumberNeighbours(row, column);
                if (neighbours.Length != 2) continue;

                sum += int.Parse(neighbours[0]) * int.Parse(neighbours[1]);
            }
        }

        return new(sum.ToString());
    }

    private bool TryGetPartNumber(int row, ref int column, out int partNumber)
    {
        var number = _schematic[row][column].ToString();
        partNumber = 0;
        
        while (column++ < _schematic[row].Length - 1)
        {
            var current = _schematic[row][column];
            if (!char.IsDigit(current)) break;
            number += current;
        }

        return HasAdjacentSymbol(row, column - number.Length, number.Length) && int.TryParse(number, out partNumber);
    }
    
    private bool HasAdjacentSymbol(int row, int column, int length)
    {
        for (var rowMod = -1; rowMod <= 1; rowMod++)
            for (var columnMod = -1; columnMod <= length; columnMod++)
            {
                if (rowMod == 0 && columnMod >= 0 && columnMod < length) continue;
                var newRow = row + rowMod;
                var newColumn = column + columnMod;
                if (IsWithinBounds(newRow, newColumn) && IsSymbol(_schematic[newRow][newColumn])) return true;
            }

        return false;
    }
    
    private static bool IsSymbol(char character) => !char.IsDigit(character) && character != '.';

    private string[] GetNumberNeighbours(int row, int column)
    {
        var foundNumbers = new HashSet<string>();

        for (var rowMod = -1; rowMod <= 1; rowMod++)
            for (var columnMod = -1; columnMod <= 1; columnMod++)
            {
                if (rowMod == 0 && columnMod == 0) continue;
                var newRow = row + rowMod;
                var newColumn = column + columnMod;
                if (IsWithinBounds(newRow, newColumn) && char.IsDigit(_schematic[newRow][newColumn]))
                {
                    foundNumbers.Add(GetEntireNumber(newRow, newColumn));
                }
            }
        
        return foundNumbers.ToArray();
    }

    private string GetEntireNumber(int row, int column)
    {
        while (column != 0)
        {
            if (char.IsDigit(_schematic[row][column - 1])) column--;
            else break;
        }

        var number = "" + _schematic[row][column];
        while (column < _schematic[row].Length - 1)
        {
            if (char.IsDigit(_schematic[row][column + 1]))
            {
                column++;
                number += _schematic[row][column];
            }
            else break;
        }
        
        return number;
    }

    private bool IsWithinBounds(int row, int column) =>
        row >= 0 && row < _schematic.Length && column >= 0 && column < _schematic[row].Length;

    private char[][] ParseSchematic() => _input.Select(x => x.ToCharArray()).ToArray();
}
