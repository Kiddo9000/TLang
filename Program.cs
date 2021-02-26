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

            Console.WriteLine("TLang compiler revision 2.0.1\n");
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
                string Source = Generator.CleanSource(File.ReadAllText(FilePath));
                string Methods = Properties.Resources.Lib.Replace("//INSSCR", Generator.GenerateResult(Source));

                Console.WriteLine("Compiling...");
                bool Result = Compiler.CreateExecutable(Methods, CompilePath);

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
                Console.WriteLine("  ==  COMPILE FAILED  ==");
                Console.WriteLine("\nERROR: Source file path argument is missing or incorrect.\nThe path should be formatted as: \"C:\\Some Directory\\Some File.tl\"\n\nPress any key to quit.");
                Console.ReadKey();
            }
        }

        public static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            Console.WriteLine("  ==  COMPILE FAILED  ==");
            Console.WriteLine($"\nERROR: {ex.Message}\n\n{ex.StackTrace}\n\nPress any key to quit.");
            Console.ReadKey();

            Environment.Exit(1);            
        }
    }
}
