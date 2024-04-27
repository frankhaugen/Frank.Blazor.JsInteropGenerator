using Frank.Blazor.JsInteropGenerator.Internals.Js;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public interface ICSharpCodeGenerator
{
    public CompilationUnitSyntax Generate(IEnumerable<JsFunctionDefinition> functionDefinitions, string className = "GeneratedInterop", string namespaceName = "YourNamespace");
}