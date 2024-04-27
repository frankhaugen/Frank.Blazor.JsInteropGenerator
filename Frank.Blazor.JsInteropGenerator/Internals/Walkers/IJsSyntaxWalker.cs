using Frank.Blazor.JsInteropGenerator.Internals.Js;

namespace Frank.Blazor.JsInteropGenerator.Internals.Walkers;

public interface IJsSyntaxWalker
{
    public JsFunctionDefinition[] GetFunctionDefinitions(string javascript);
}