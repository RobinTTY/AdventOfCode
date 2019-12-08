using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            const string input = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,13,1,19,1,19,6,23,1,23,6,27,1,13,27,31,2,13,31,35,1,5,35,39,2,39,13,43,1,10,43,47,2,13,47,51,1,6,51,55,2,55,13,59,1,59,10,63,1,63,10,67,2,10,67,71,1,6,71,75,1,10,75,79,1,79,9,83,2,83,6,87,2,87,9,91,1,5,91,95,1,6,95,99,1,99,9,103,2,10,103,107,1,107,6,111,2,9,111,115,1,5,115,119,1,10,119,123,1,2,123,127,1,127,6,0,99,2,14,0,0";
            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var output = Compute(input, i, j);
                    if (output[0] != 19690720) continue;
                    var noun = output[1];
                    var verb = output[2];
                    return;
                }
            }
        }

        public static int[] Compute(string command, int parameter1, int parameter2)
        {
            var inputArr = Regex.Split(command, @"\D+").Select(int.Parse).ToArray();
            inputArr[1] = parameter1;
            inputArr[2] = parameter2;
            for (var i = 0; i < inputArr.Length; i += 4)
            {
                if (inputArr[i] == 1)
                    inputArr[inputArr[i + 3]] = inputArr[inputArr[i + 1]] + inputArr[inputArr[i + 2]];
                else if (inputArr[i] == 2)
                    inputArr[inputArr[i + 3]] = inputArr[inputArr[i + 1]] * inputArr[inputArr[i + 2]];
                else if (inputArr[i] == 99) return inputArr;
            }
            throw new Exception("Something went wrong");
        }
    }
}
