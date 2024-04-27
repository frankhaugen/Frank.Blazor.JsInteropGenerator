using Frank.Blazor.JsInteropGenerator.Internals.Js;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public class CSharpCodeGenerator : ICSharpCodeGenerator
{
    private readonly ICSharpNamespaceGenerator _cSharpNamespaceGenerator;
    private readonly ICSharpClassGenerator _cSharpClassGenerator;
    private readonly ICSharpInterfaceGenerator _cSharpInterfaceGenerator;

    public CSharpCodeGenerator(ICSharpClassGenerator cSharpClassGenerator, ICSharpNamespaceGenerator cSharpNamespaceGenerator, ICSharpInterfaceGenerator cSharpInterfaceGenerator)
    {
        _cSharpClassGenerator = cSharpClassGenerator;
        _cSharpNamespaceGenerator = cSharpNamespaceGenerator;
        _cSharpInterfaceGenerator = cSharpInterfaceGenerator;
    }

    public CompilationUnitSyntax Generate(IEnumerable<JsFunctionDefinition> functionDefinitions, string className = "GeneratedInterop", string namespaceName = "YourNamespace")
    {
        var classDeclaration = _cSharpClassGenerator.Generate(functionDefinitions, className);
        var interfaceDeclaration = _cSharpInterfaceGenerator.Generate(classDeclaration);
        var namespaceDeclaration = _cSharpNamespaceGenerator.Generate(namespaceName);
        namespaceDeclaration = namespaceDeclaration.AddMembers(interfaceDeclaration, classDeclaration);
        
        return SyntaxFactory.CompilationUnit()
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Threading.Tasks")),
                SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("Microsoft.JSInterop")))
            .AddMembers(namespaceDeclaration)
            .NormalizeWhitespace();
    }
}