namespace Frank.Blazor.JsInteropGenerator.Internals.Js;

public readonly record struct JsFunctionDefinition(string Name, JsFunctionReturnType? ReturnType, JsFunctionArgument[]? Arguments = null);