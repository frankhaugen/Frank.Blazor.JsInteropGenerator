using System.Globalization;
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
        return SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("Task<object?>"),
                SyntaxFactory.Identifier(PrepareMethodName(methodName)))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
            .WithParameterList(CreateParameterList())
            .WithBody(
                CreateMethodBody(
                    methodName
                )
            );
    }

    private ParameterListSyntax CreateParameterList()
    {
        return SyntaxFactory.ParameterList(
            SyntaxFactory.SingletonSeparatedList(
                CreateNullableObjectParamsParameter()
            ));
    }

    private ParameterSyntax CreateNullableObjectParamsParameter()
    {
        return SyntaxFactory.Parameter(
                SyntaxFactory.Identifier("args")
            )
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.ParamsKeyword)
            )
            .WithType(
                SyntaxFactory.NullableType(
                    SyntaxFactory.ArrayType(
                                SyntaxFactory.PredefinedType(
                                    SyntaxFactory.Token(
                                        SyntaxKind.ObjectKeyword
                                    )
                                ))
                        
                    .AddRankSpecifiers(
                        SyntaxFactory.ArrayRankSpecifier(
                            SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
                                SyntaxFactory.OmittedArraySizeExpression())
                        )
                    )
                )
            )
            // .WithDefault(
            //     SyntaxFactory.EqualsValueClause(
            //         SyntaxFactory.LiteralExpression(
            //             SyntaxKind.NullLiteralExpression
            //         )
            //     )
            // )
            ;
    }

    private string PrepareMethodName(string methodName)
    {
        return CapitalizeFirstLetter(methodName) + "Async";
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
                SyntaxFactory.ReturnStatement(
                    CreateInvocationExpression(methodName)
                )
            )
        );
    }

    private string CapitalizeFirstLetter(string methodName)
    {
        return methodName[0].ToString(CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture) + methodName[1..];
    }

    private AwaitExpressionSyntax CreateInvocationExpression(string methodName)
    {
        var exp = SyntaxFactory.InvocationExpression(
            SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.IdentifierName("_jsRuntime"),
                SyntaxFactory.GenericName(
                    SyntaxFactory.Identifier("InvokeAsync")
                ).WithTypeArgumentList(
                    SyntaxFactory.TypeArgumentList(
                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                            SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ObjectKeyword))
                        )
                    )
                )
            )
        ).WithArgumentList(
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

        return SyntaxFactory.AwaitExpression(exp);
    }
}