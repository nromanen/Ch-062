using Microsoft.CodeAnalysis;
using System.Reflection;
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
            string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".dll");
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var timer = new Stopwatch();
            timer.Start();
            EmitResult compilationResult = compilation.Emit(path);
            timer.Stop();
            File.Copy(path, tempPath);
            var result = new ExecutionResult() { Success = false };
            if (compilationResult.Success)
            {
                result.CompileTime = timer.Elapsed;
                // Load the assembly
                //Assembly asm = AssemblyLoadContext.Default.LoadFromStream(fileStream);
                //Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(tempPath);
                var assembly = Assembly.LoadFile(tempPath);
                try
                {
                    timer.Reset();
                    timer.Start();
                    // Invoke the method passing arguments
                    object tempres = assembly.GetType(targetClass).GetMethod(entryPoint).Invoke(null, parameters);
                    result.Success = true;
                    result.Result = tempres.ToString();
                }
                catch (Exception ex)
                {
                    timer.Stop();
                    result.RunTimeExceptions.Add($"Error: {ex.InnerException.Message}, Location: {ex.InnerException.StackTrace}");
                }
                result.ExecutionTime = timer.Elapsed;
            }
            else
            {
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    string issue = $"Error: {codeIssue.Id}, {codeIssue.GetMessage()}, Location {codeIssue.Location.GetLineSpan().StartLinePosition}";
                    result.CompileTimeExceptions.Add(issue);
                }
            }
            return result;
        }
    }
}
