using System;
using System.Text.RegularExpressions;

namespace TLang
{
    public static class Generator
    {
        public static string CleanSource(string Input)
        {
            string Output = string.Empty;

            foreach (string Item in Input.Replace("\r\n", "\n").Split('\n'))
            {
                string Line = Item.Trim();

                if (string.IsNullOrEmpty(Line) || Line.StartsWith(";")) continue;

                Output += $"{Line}\n";
            }

            return (Output + "QUT\n").Trim();
        }

        public static string GenerateResult(string Input)
        {
            string Output = string.Empty;
            string CurrentSection = "COMP_ENTRYPOINT:";
            int Instructions = 0;
            int Statements = 0;

            foreach (string Item in Input.Split('\n'))
            {
                string Line = Item.Trim();

                // Replace sections with labels
                if (Line.EndsWith(":"))
                {
                    if (ContainsSpecialChars(Line.TrimEnd(':'))) throw new Exception($"Section name \"{Line}\" cannot contain special characters.");

                    Console.WriteLine($"Section {CurrentSection} {Instructions} instructions, {Statements} statements.");
                    CurrentSection = Line;
                    Instructions = 0;
                    Statements = 0;

                    Output += $"{Line}\n";
                    continue;
                }

                // Create goto commands
                if (Line.StartsWith("."))
                {
                    ++Statements;

                    if (ContainsSpecialChars(Line.TrimStart('.'))) throw new Exception($"Section name \"{Line}\" cannot contain special characters.");

                    Output += $"{Lib.Spacing}goto {Line.TrimStart('.')};\n";
                    continue;
                }

                // Create condition statements
                if (Line.StartsWith("$"))
                {
                    ++Statements;

                    string Variable = Line.TrimStart('$').Split(' ')[0];
                    string Section = Line.Split(' ')[1];

                    if (ContainsSpecialChars(Section)) throw new Exception($"Section name \"{Line}\" cannot contain special characters.");

                    Output += $"{Lib.Spacing}if ({Lib.ClassName}.ConvertToBool({Lib.ClassName}.Variables[\"{Variable}\"])) goto {Section};\n";
                    continue;
                }

                ++Instructions;

                // !!!! WARNING! THIS SECTION IS REALLY MESSY! TRY NOT TO GET LOST! !!!!

                // Get instruction data
                string Instruction = Line.Split(' ')[0];
                string[] InstParams = Line.TrimStart(Instruction.ToCharArray()).Trim().Split(new[] { "||" }, StringSplitOptions.None);

                int InstructionIndex = GetArrayItemIndex(Lib.InstructionTable, 0, Instruction);
                if (InstructionIndex == -1) throw new Exception($"Instruction \"{Instruction}\" does not exist.");

                string Function = Lib.InstructionTable[InstructionIndex, 1];
                string[] Parameters = Lib.InstructionTable[InstructionIndex, 2].Split(',');

                if (Parameters.Length != InstParams.Length) throw new Exception($"Instruction \"{Instruction}\" takes {Parameters.Length} parameters. (Got {InstParams.Length})");

                // Create function
                string NewFunction = $"{Lib.Spacing}{Lib.ClassName}.{Function}(";

                for (int i = 0; i < Parameters.Length; i++)
                {
                    string Start = string.Empty;
                    if (i != 0) Start = ", ";

                    if (Parameters[i] == "string") NewFunction += Start + CreateString(InstParams[i]);
                    else if (Parameters[i] == "bool") NewFunction += Start + CreateBool(InstParams[i]);
                    else if (Parameters[i] == "void") continue;
                    else throw new Exception($"Unexpected parameter type {Parameters[i]}.");
                }

                Output += NewFunction + ");\n";
            }

            Console.WriteLine($"Section {CurrentSection} {Instructions} instructions, {Statements} statements.");

            return Output;
        }

        // -- HELPER FUNCTIONS --

        private static int GetArrayItemIndex(string[,] Array, int Dimension, string Value)
        {
            int ArraySize = (Array.Length / 3) - 1;

            for (int Count = 0; Count <= ArraySize; Count++)
            {
                if (Array[Count, Dimension] == Value) return Count;
            }

            return -1;
        }

        private static string CreateString(string input)
        {
            input = input.Replace("\"", "\\\"");
            return $"\"{input}\"";
        }

        private static string CreateBool(string input)
        {
            if (input == "TRUE") return "true";
            else if (input == "FALSE") return "false";
            else throw new Exception("Value is not a boolean.");
        }

        private static bool ContainsSpecialChars(string input)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
            return !regex.IsMatch(input);
        }
    }
}
