using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public class CSharpInterfaceGenerator : ICSharpInterfaceGenerator
{
    public InterfaceDeclarationSyntax Generate(ClassDeclarationSyntax classDeclaration) =>
        SyntaxFactory.InterfaceDeclaration("I" + classDeclaration.Identifier.Text)
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword))).WithMembers(SyntaxFactory.List(classDeclaration.Members
                .Where(member =>
                    member is MethodDeclarationSyntax ||
                    member is PropertyDeclarationSyntax ||
                    member is EventDeclarationSyntax ||
                    member is IndexerDeclarationSyntax)
                .Select(AsInterfaceMember)));

    private static MemberDeclarationSyntax AsInterfaceMember(MemberDeclarationSyntax member) =>
        member switch
        {
            MethodDeclarationSyntax method => method.WithModifiers(SyntaxFactory.TokenList()).WithBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
            PropertyDeclarationSyntax property => property.WithModifiers(SyntaxFactory.TokenList()).WithAccessorList(SyntaxFactory.AccessorList(SyntaxFactory.List(property.AccessorList?.Accessors.Select(accessor => accessor.WithBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))) ?? Array.Empty<AccessorDeclarationSyntax>()))),
            EventDeclarationSyntax eventDeclaration => eventDeclaration.WithModifiers(SyntaxFactory.TokenList()).WithAccessorList(SyntaxFactory.AccessorList(SyntaxFactory.List(eventDeclaration.AccessorList?.Accessors.Select(accessor => accessor.WithBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))) ?? Array.Empty<AccessorDeclarationSyntax>()))),
            IndexerDeclarationSyntax indexer => indexer.WithModifiers(SyntaxFactory.TokenList()).WithAccessorList(SyntaxFactory.AccessorList(SyntaxFactory.List(indexer.AccessorList?.Accessors.Select(accessor => accessor.WithBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))) ?? Array.Empty<AccessorDeclarationSyntax>()))),
            _ => member
        };
}