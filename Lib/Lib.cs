using System;
using System.Collections.Generic;
using System.Threading;

namespace TLang
{
    public static class Lib
    {
        // -- SCRIPT STUFF --

        public static string ClassName = "Lib";
        public static string Spacing = "            ";

        public static Dictionary<string, string> Variables = new Dictionary<string, string>();

        public static string[,] InstructionTable = new string[,] {
            // Console
            { "CLS", "ClearConsole", "void" },
            { "WRO", "WriteToConsole", "string,bool" },
            { "WRV", "WriteVarToConsole", "string,bool" },
            { "WRN", "WriteNewlineToConsole", "void" },
            { "WRI", "ReadFromConsole", "string" },
            { "WFK", "WaitForKeyboard", "void" },

            // Variables
            { "DEF", "CreateVariable", "string,string" },
            { "SET", "SetVariable", "string,string" },
            { "DES", "DestroyVariable", "string" },
                
            // Conditional Functions
            { "VEQ", "VariableEquals", "string,string,string" },
            { "VCT", "VariableContains", "string,string,string" },

            // Functions
            { "SLP", "Sleep", "string" },
            { "QUT", "Quit", "void" },
        };

        public static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            Console.WriteLine("\n\nRUNTIME ERROR: " + ex.Message + "\n\n" + ex.StackTrace + "\n\nPress any key to quit.");
            Console.ReadKey();

            Environment.Exit(1);
        }

        public static bool ConvertToBool(string input)
        {
            if (input == "TRUE") return true;
            else if (input == "FALSE") return false;
            else throw new Exception(input + " is not a boolean.");
        }

        // -- CONSOLE --

        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static void WriteToConsole(string input, bool newline)
        {
            if (newline) Console.WriteLine(input);
            else Console.Write(input);
        }

        public static void WriteVarToConsole(string var, bool newline)
        {
            if (!Variables.ContainsKey(var)) throw new Exception("Variable " + var + " is not defined.");

            if (newline) Console.WriteLine(Variables[var]);
            else Console.Write(Variables[var]);
        }

        public static void WriteNewlineToConsole()
        {
            Console.WriteLine();
        }

        public static void ReadFromConsole(string var)
        {
            if (!Variables.ContainsKey(var)) throw new Exception("Variable " + var + " is not defined.");
            Variables[var] = Console.ReadLine();
        }

        public static void WaitForKeyboard()
        {
            Console.ReadKey();
        }

        // -- VARIABLES --

        public static void CreateVariable(string name, string value)
        {
            if (Variables.ContainsKey(name)) throw new Exception("Variable " + name + " is already defined.");
            Variables[name] = value;
        }

        public static void SetVariable(string name, string value)
        {
            if (!Variables.ContainsKey(name)) throw new Exception("Variable " + name + " is not defined.");
            Variables[name] = value;
        }

        public static void DestroyVariable(string name)
        {
            if (!Variables.ContainsKey(name)) throw new Exception("Variable " + name + " could not be destroyed since it is not defined.");
            Variables.Remove(name);
        }

        // -- CONDITION FUNCTIONS --

        public static void VariableEquals(string invar, string condition, string outvar)
        {
            if (!Variables.ContainsKey(invar)) throw new Exception("Variable " + invar + " is not defined.");
            if (!Variables.ContainsKey(outvar)) throw new Exception("Variable " + outvar + " is not defined.");

            if (Variables[invar] == condition) Variables[outvar] = "TRUE";
            else Variables[outvar] = "FALSE";
        }

        public static void VariableContains(string invar, string condition, string outvar)
        {
            if (!Variables.ContainsKey(invar)) throw new Exception("Variable " + invar + " is not defined.");
            if (!Variables.ContainsKey(outvar)) throw new Exception("Variable " + outvar + " is not defined.");

            if (Variables[invar].Contains(condition)) Variables[outvar] = "TRUE";
            else Variables[outvar] = "FALSE";
        }

        // -- FUNCTIONS --

        public static void Sleep(string input)
        {
            int Time = Int32.Parse(input);

            Thread.Sleep(Time);
        }

        public static void Quit()
        {
            Environment.Exit(0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Lib.HandleException);

//INSSCR
        }
    }
}
