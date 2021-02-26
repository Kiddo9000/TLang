using System;
using System.IO;

namespace TLang
{
    class Program
    {        
        public static void Main(string[] args)
        {
            string FilePath = null;
            string CompilePath = null;

            // Subscribe to unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleException);

            Console.WriteLine($"TLang compiler revision {Properties.Resources.Version}\n");
            Console.WriteLine("  ==  COMPILE STARTED  ==");

            foreach (string Arg in args)
            {
                if (File.Exists(Arg))
                {
                    FilePath = Arg;
                    CompilePath = Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileNameWithoutExtension(FilePath) + ".exe";

                    if (Path.GetExtension(FilePath).ToLower() != ".tl") Console.WriteLine("WARNING: Source file extension is not .tl! Make sure you are not using another file with the same name!");
                }
            }

            if (!string.IsNullOrEmpty(FilePath))
            {
                Console.WriteLine("Generating high-level code...");
                string Source = Properties.Resources.Lib.Replace(
                    Properties.Resources.Insert, 
                    Generator.GenerateResult(
                        Generator.CleanSource(File.ReadAllText(FilePath))
                    )
                );

                Console.WriteLine("Compiling...");
                bool Result = Compiler.CreateExecutable(Source, CompilePath);

                if (Result)
                {
                    Console.WriteLine("  ==  COMPILE FINISHED  ==");
                    Console.WriteLine($"\nTarget file: {FilePath}");
                    Console.WriteLine($"Output file: {CompilePath}");
                }
                else
                {
                    Console.WriteLine("  ==  COMPILE FAILED  ==");
                }
            }
            else
            {
                Console.WriteLine("ERROR: Source file path argument is missing or incorrect. The path should be formatted as: \"C:\\Some Directory\\Some File.tl\"");
                Console.WriteLine("  ==  COMPILE FAILED  ==");
            }
        }

        public static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            Console.WriteLine($"ERROR: {ex.Message}\n{ex.StackTrace}");
            Console.WriteLine("  ==  COMPILE FAILED  ==");

            Environment.Exit(1);            
        }
    }
}
