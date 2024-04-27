using Esprima.Ast;

namespace Frank.Blazor.JsInteropGenerator.Internals.Js;

public interface IJsParser
{
    public Script ParseScript(string javascript);
    public Expression ParseExpression(string javascript);
    public Module ParseModule(string javascript);
    
    public ScriptFlavor Identify(string source);
}