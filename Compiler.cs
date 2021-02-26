using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;

namespace TLang
{
    public static class Compiler
    {
        public static bool CreateExecutable(string Input, string OutPath)
        {
            bool Compile = true;

            CSharpCodeProvider CompileProvider = new CSharpCodeProvider();
            CompilerParameters CompileParams = new CompilerParameters
            {
                GenerateInMemory = false,
                GenerateExecutable = true,
                OutputAssembly = OutPath
            };

            CompilerResults CompileResults = CompileProvider.CompileAssemblyFromSource(CompileParams, Input);

            foreach (CompilerError Error in CompileResults.Errors)
            {
                Console.WriteLine($"ERROR: ({Error.ErrorNumber}) {Error.ErrorText}");
                Compile = false;
            }

            return Compile;
        }
    }
}
