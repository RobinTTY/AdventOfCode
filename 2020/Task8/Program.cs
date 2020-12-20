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
            var parsedInput = File.ReadAllLines("input.txt");
            var instructions = new List<Instruction>();
            parsedInput.ToList().ForEach(instruction => instructions.Add(new Instruction(instruction)));

            var executionUnit = new ExecutionUnit();
            Console.WriteLine("Executing until loop is found on next instruction:");
            executionUnit.ExecuteProgram(instructions.ToArray(), true);
            
            Console.WriteLine("\nExecuting until the program works:");
            ExecuteUntilWorkingProgramFound(executionUnit, instructions);
        }

        public static InstructionType FlipInstructionType(Instruction instruction) => instruction.Type == InstructionType.Jmp ? InstructionType.Nop : InstructionType.Jmp;

        public static void ExecuteUntilWorkingProgramFound(ExecutionUnit executionUnit,List<Instruction> instructions)
        {
            var successfulExecution = false;
            var manipulatedInstructionIndex = 0;
            while (!successfulExecution)
            {
                var instructionToManipulate = instructions[manipulatedInstructionIndex];
                if (instructionToManipulate.Type != InstructionType.Acc)
                    instructionToManipulate.Type = FlipInstructionType(instructionToManipulate);
                else
                {
                    manipulatedInstructionIndex++;
                    continue;
                }
                successfulExecution = executionUnit.ExecuteProgram(instructions.ToArray(), true);
                instructionToManipulate.Type = FlipInstructionType(instructionToManipulate);
                manipulatedInstructionIndex++;
            }
        }
    }

    public class ExecutionUnit
    {
        private int _accumulator;
        private int _instructionPointer;
        private Dictionary<Instruction, bool> _instructionTracker;

        public bool ExecuteProgram(Instruction[] instructions, bool checkForProgramLoops = false)
        {
            ResetExecutionUnit();
            if (checkForProgramLoops)
                instructions.ToList().ForEach(instruction => _instructionTracker.Add(instruction, false));
            
            while (instructions.Length > _instructionPointer)
            {
                var nextInstruction = instructions[_instructionPointer];
                if (checkForProgramLoops)
                {
                    if (_instructionTracker[nextInstruction])
                    {
                        Console.WriteLine("The next instruction was already executed.");
                        Console.WriteLine($"Accumulator value: {_accumulator}");
                        return false;
                    }

                    _instructionTracker[nextInstruction] = true;
                }
                ExecuteInstruction(nextInstruction);
            }
            
            Console.WriteLine("Program successfully executed.");
            Console.WriteLine($"Number of instructions: {instructions.Length}");
            Console.WriteLine($"Instruction pointer at: {_instructionPointer}");
            Console.WriteLine($"Accumulator at: {_accumulator}");
            return true;
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            switch (instruction.Type)
            {
                case InstructionType.Acc:
                    _accumulator += instruction.Argument;
                    _instructionPointer++;
                    break;
                case InstructionType.Jmp:
                    _instructionPointer += instruction.Argument;
                    break;
                case InstructionType.Nop:
                    _instructionPointer++;
                    break;
                default:
                    throw new Exception("Execution engine encountered an unknown instruction.");
            }
        }

        private void ResetExecutionUnit()
        {
            _instructionTracker = new Dictionary<Instruction, bool>();
            _accumulator = 0;
            _instructionPointer = 0;
        }
    }

    public class Instruction
    {
        public InstructionType Type { get; set; }
        public int Argument { get; set; }
        
        public Instruction(string instructionString)
        {
            var parts = instructionString.Split(" ");
            Type = parts[0] switch
            {
                "acc" => InstructionType.Acc,
                "jmp" => InstructionType.Jmp,
                "nop" => InstructionType.Nop,
                _ => Type
            };
            Argument = int.Parse(parts[1]);
        }
    }

    public enum InstructionType
    {
        Acc,
        Jmp,
        Nop
    }
}
