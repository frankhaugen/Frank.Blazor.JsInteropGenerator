using Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;
using Frank.Blazor.JsInteropGenerator.Internals.Walkers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Blazor.JsInteropGenerator;

public class JsInteropGenerator : IJsInteropGenerator
{
	private readonly ICSharpCodeGenerator _cSharpCodeGenerator;
	private readonly IJsSyntaxWalker _jsSyntaxWalker;

	public JsInteropGenerator(ICSharpCodeGenerator cSharpCodeGenerator, IJsSyntaxWalker jsSyntaxWalker)
	{
		_cSharpCodeGenerator = cSharpCodeGenerator;
		_jsSyntaxWalker = jsSyntaxWalker;
	}

	public CompilationUnitSyntax Generate(string javascript, string className = "GeneratedInterop", string namespaceName = "YourNamespace")
	{
		var functionDefinitions = _jsSyntaxWalker.GetFunctionDefinitions(javascript);
		return _cSharpCodeGenerator.Generate(functionDefinitions, className, namespaceName);
	}

}