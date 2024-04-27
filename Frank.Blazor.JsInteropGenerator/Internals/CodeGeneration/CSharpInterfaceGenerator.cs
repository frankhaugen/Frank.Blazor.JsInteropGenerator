using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public class CSharpInterfaceGenerator : ICSharpInterfaceGenerator
{
    public InterfaceDeclarationSyntax Generate(ClassDeclarationSyntax classDeclaration)
    {
        return SyntaxFactory.InterfaceDeclaration(classDeclaration.Identifier);
    }
}