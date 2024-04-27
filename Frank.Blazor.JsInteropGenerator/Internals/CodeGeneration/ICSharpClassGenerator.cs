using Frank.Blazor.JsInteropGenerator.Internals.Js;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public interface ICSharpClassGenerator
{
    public ClassDeclarationSyntax Generate(IEnumerable<JsFunctionDefinition> functionDefinitions, string className = "GeneratedInterop");
}