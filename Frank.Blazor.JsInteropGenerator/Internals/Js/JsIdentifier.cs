using Esprima;
using Esprima.Ast;

namespace Frank.Blazor.JsInteropGenerator.Internals.Js;

public static class JsIdentifier
{
    public static ScriptFlavor Identify(string source)
    {
        // Define the parsing options
        var scriptOptions = new ParserOptions { Tolerant = false };
        var moduleOptions = new ParserOptions { Tolerant = false };
        var expressionOptions = new ParserOptions { Tolerant = false };

        // Parse as script
        var scriptParser = new JavaScriptParser(scriptOptions);
        var script = scriptParser.ParseScript(source, strict: true);
        
        var moduleParser = new JavaScriptParser(moduleOptions);
        var module = moduleParser.ParseModule(source);
        
        var expressionParser = new JavaScriptParser(expressionOptions);
        var expression = expressionParser.ParseExpression(source);
        
        if (script.Body.Count > 0 && script.Body[0] is ExpressionStatement)
            return ScriptFlavor.Expression;
        
        if (module.Body.Count > 0 && (module.Body[0] is ImportDeclaration || module.Body[0] is ExportDeclaration))
            return ScriptFlavor.Module;
        
        return ScriptFlavor.Script;
    } 
}