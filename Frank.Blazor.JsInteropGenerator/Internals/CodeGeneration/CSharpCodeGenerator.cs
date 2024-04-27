using Frank.Blazor.JsInteropGenerator.Internals.Js;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public class CSharpCodeGenerator : ICSharpCodeGenerator
{
    private readonly ICSharpClassGenerator _cSharpClassGenerator;
    private readonly ICSharpNamespaceGenerator _cSharpNamespaceGenerator;

    public CSharpCodeGenerator(ICSharpClassGenerator cSharpClassGenerator, ICSharpNamespaceGenerator cSharpNamespaceGenerator)
    {
        _cSharpClassGenerator = cSharpClassGenerator;
        _cSharpNamespaceGenerator = cSharpNamespaceGenerator;
    }

    public CompilationUnitSyntax Generate(IEnumerable<JsFunctionDefinition> functionDefinitions, string className = "GeneratedInterop", string namespaceName = "YourNamespace")
    {
        var classDeclaration = _cSharpClassGenerator.Generate(functionDefinitions, className);
        var namespaceDeclaration = _cSharpNamespaceGenerator.Generate(namespaceName, classDeclaration);
        return SyntaxFactory.CompilationUnit()
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Threading.Tasks")),
                SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("Microsoft.JSInterop")))
            .AddMembers(namespaceDeclaration)
            .NormalizeWhitespace();
    }
}