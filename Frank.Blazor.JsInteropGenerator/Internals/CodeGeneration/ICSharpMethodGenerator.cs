using Frank.Blazor.JsInteropGenerator.Internals.Js;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public interface ICSharpMethodGenerator
{
    public MethodDeclarationSyntax Generate(JsFunctionDefinition functionDefinition);
}