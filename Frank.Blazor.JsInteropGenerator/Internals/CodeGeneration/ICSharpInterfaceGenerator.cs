using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public interface ICSharpInterfaceGenerator
{
    public InterfaceDeclarationSyntax Generate(ClassDeclarationSyntax classDeclaration);
}