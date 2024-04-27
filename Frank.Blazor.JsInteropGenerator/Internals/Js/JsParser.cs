using Esprima;
using Esprima.Ast;

namespace Frank.Blazor.JsInteropGenerator.Internals.Js;

public class JsParser(JavaScriptParser parser) : IJsParser
{
    /// <inheritdoc />
    public Script ParseScript(string javascript) => parser.ParseScript(javascript);

    /// <inheritdoc />
    public Expression ParseExpression(string javascript) => parser.ParseExpression(javascript);

    /// <inheritdoc />
    public Module ParseModule(string javascript) => parser.ParseModule(javascript);

    /// <inheritdoc />
    public ScriptFlavor Identify(string source) => JsIdentifier.Identify(source);
}