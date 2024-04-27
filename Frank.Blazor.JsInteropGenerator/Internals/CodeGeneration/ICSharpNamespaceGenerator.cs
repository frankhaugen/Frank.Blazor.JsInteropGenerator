using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public interface ICSharpNamespaceGenerator
{
    public NamespaceDeclarationSyntax Generate(string namespaceName, ClassDeclarationSyntax classDeclaration);
}