﻿using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;

public class CSharpNamespaceGenerator : ICSharpNamespaceGenerator
{
    public NamespaceDeclarationSyntax Generate(string namespaceName)
    {
        return SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(namespaceName));
    }
}