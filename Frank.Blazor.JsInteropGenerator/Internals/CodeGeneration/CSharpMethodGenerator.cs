using Frank.Blazor.JsInteropGenerator.Internals.Js;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public class CSharpMethodGenerator : ICSharpMethodGenerator
{
    public MethodDeclarationSyntax Generate(JsFunctionDefinition functionDefinition)
    {
        return GenerateMethod(functionDefinition.Name);
    }
    
    private MethodDeclarationSyntax GenerateMethod(string methodName)
    {
        return SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("Task"),
                SyntaxFactory.Identifier(methodName + "Async"))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
            .WithParameterList(
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Parameter(SyntaxFactory.Identifier("args"))
                            .WithType(CreateArrayType())
                    )
                )
            )
            .WithBody(CreateMethodBody(methodName));
    }

    private ArrayTypeSyntax CreateArrayType()
    {
        return SyntaxFactory.ArrayType(
            SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ObjectKeyword)),
            SyntaxFactory.SingletonList(SyntaxFactory.ArrayRankSpecifier())
        );
    }

    private BlockSyntax CreateMethodBody(string methodName)
    {
        return SyntaxFactory.Block(
            SyntaxFactory.SingletonList<StatementSyntax>(
                SyntaxFactory.ExpressionStatement(
                    CreateInvocationExpression(methodName)
                )
            )
        );
    }

    private InvocationExpressionSyntax CreateInvocationExpression(string methodName)
    {
        return SyntaxFactory.InvocationExpression(
            SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.IdentifierName("_jsRuntime"),
                SyntaxFactory.IdentifierName("InvokeAsync")
            ),
            SyntaxFactory.ArgumentList(
                SyntaxFactory.SeparatedList<ArgumentSyntax>(
                    new SyntaxNodeOrToken[]
                    {
                        SyntaxFactory.Argument(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                SyntaxFactory.Literal(methodName)
                            )
                        ),
                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                        SyntaxFactory.Argument(
                            SyntaxFactory.IdentifierName("args")
                        )
                    }
                )
            )
        );
    }
}