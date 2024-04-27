using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator;

public interface IJsInteropGenerator
{
    public CompilationUnitSyntax Generate(string javascript, string className = "GeneratedInterop", string namespaceName = "YourNamespace");
}