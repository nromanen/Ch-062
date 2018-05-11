using Microsoft.CodeAnalysis;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Diagnostics;
using Model.Entity;
using BAL.Interfaces;
using System.IO;
using NUnit.Framework;
using System.Xml.Linq;

namespace BAL.Managers
{
    public class SandboxManager : ISandboxManager
    {
        public ExecutionResult Execute(string code, string entryPoint = "Test", object[] parameters = null)
        {
            var tree = SyntaxFactory.ParseSyntaxTree(code);

            var systemRefLocation = typeof(object).GetTypeInfo().Assembly.Location;
            var systemReference = MetadataReference.CreateFromFile(systemRefLocation);
            var nunitReference = MetadataReference.CreateFromFile(typeof(TestAttribute).GetTypeInfo().Assembly.Location);

            string fileName = "OnlineExam.dll";
            string resultFilename = "Result.xml";
            string pathTodll = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            string pathToxml = Path.Combine(Directory.GetCurrentDirectory(), resultFilename);
            string NUnit = "NUnit.Console-3.8.0";

            var compilation = CSharpCompilation
                .Create(fileName)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, checkOverflow: true))
                .AddReferences(systemReference, nunitReference)
                .AddSyntaxTrees(tree);

            var result = new ExecutionResult() { Success = false };
            EmitResult compilationResult = compilation.Emit(pathTodll);
            if (compilationResult.Success)
            {
                string strCmdText = $"NUNIT3-CONSOLE {pathTodll} --result={pathToxml}";
                var p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = true;
                p.Start();
                p.StandardInput.WriteLine($"cd {Path.Combine(Directory.GetCurrentDirectory(), NUnit)}");
                p.StandardInput.WriteLine(strCmdText);

                var xmlResult = XDocument.Load(pathToxml).Root;
                var tests = xmlResult.Document.Element("test-run");
                result.Result += string.Join("Total: ", (string)tests.Attribute("total"));
                result.Result += string.Join("Passed: ", (string)tests.Attribute("passed"));
                result.Result += string.Join("Failed: ", (string)tests.Attribute("failed"));
                result.Result += string.Join("Duration: ", (string)tests.Attribute("duration"));
                result.Success = true;
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
