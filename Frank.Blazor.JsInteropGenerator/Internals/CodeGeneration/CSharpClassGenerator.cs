using Frank.Blazor.JsInteropGenerator.Internals.Js;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public class CSharpClassGenerator : ICSharpClassGenerator
{
    private readonly ICSharpMethodGenerator _methodGenerator;

    public CSharpClassGenerator(ICSharpMethodGenerator methodGenerator)
    {
        _methodGenerator = methodGenerator;
    }

    public ClassDeclarationSyntax Generate(IEnumerable<JsFunctionDefinition> functionDefinitions, string className = "GeneratedInterop")
    {
        var methods = functionDefinitions.Select(_methodGenerator.Generate).ToArray();
        
        var field = GenerateJsRuntimeField();
        
        var constructor = GenerateConstructor();

        return SyntaxFactory.ClassDeclaration(className)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(field)
                .AddMembers(constructor)
                .AddMembers(methods.Cast<MemberDeclarationSyntax>().ToArray())
            ;
    }
    
    private FieldDeclarationSyntax GenerateJsRuntimeField()
    {
        return SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("IJSRuntime"))
                    .AddVariables(SyntaxFactory.VariableDeclarator("_jsRuntime")))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));
    }
    
    private ConstructorDeclarationSyntax GenerateConstructor()
    {
        return SyntaxFactory.ConstructorDeclaration("GeneratedInterop")
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(
                SyntaxFactory.Parameter(SyntaxFactory.Identifier("jsRuntime"))
                    .WithType(SyntaxFactory.ParseTypeName("IJSRuntime")))
            .WithBody(
                SyntaxFactory.Block(
                    SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName("_jsRuntime"),
                            SyntaxFactory.IdentifierName("jsRuntime")
                        )
                    )
                )
            );
    }
}