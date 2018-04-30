using Microsoft.CodeAnalysis;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Diagnostics;
using Model.Entity;
using BAL.Interfaces;
using System;
using System.IO;

namespace BAL.Managers
{
    public class SandboxManager : ISandboxManager
    {
        public ExecutionResult Execute(string code, string entryPoint = "Test", object[] parameters = null)
        {
            var tree = SyntaxFactory.ParseSyntaxTree(code);
            string fileName = "OnlineExam.dll";
            string targetClass = "OnlineExam.Program";
            // Detect the file location for the library that defines the object type
            var systemRefLocation = typeof(object).GetTypeInfo().Assembly.Location;
            // Create a reference to the library
            var systemReference = MetadataReference.CreateFromFile(systemRefLocation);
            // A single, immutable invocation to the compiler
            // to produce a library
            var compilation = CSharpCompilation
                .Create(fileName)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, checkOverflow: true))
                .AddReferences(systemReference)
                .AddSyntaxTrees(tree);
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var fileStream = File.Open(path, FileMode.OpenOrCreate);
            var timer = new Stopwatch();
            timer.Start();
            EmitResult compilationResult = compilation.Emit(fileStream);
            timer.Stop();
            fileStream.Close();
            var result = new ExecutionResult() { Success = false };
            if (compilationResult.Success)
            {
                result.CompileTime = timer.Elapsed;
                // Load the assembly
                //Assembly asm = AssemblyLoadContext.Default.LoadFromStream(fileStream);
                Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                // Invoke the method passing an argument
                try
                {
                    timer.Reset();
                    timer.Start();
                    object temp = asm.GetType(targetClass).GetMethod(entryPoint).Invoke(null, parameters);
                    timer.Stop();
                    result.Success = true;
                    result.Result = temp.ToString();
                }
                catch (Exception ex)
                {
                    timer.Stop();
                    result.RunTimeExceptions.Add(ex.InnerException.Message);
                }
                result.ExecutionTime = timer.Elapsed;
            }
            else
            {
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    string issue = codeIssue.GetMessage();
                    result.CompileTimeExceptions.Add(issue);
                }
            }
            return result;
        }
    }
}
