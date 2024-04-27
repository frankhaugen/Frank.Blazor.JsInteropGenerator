using Esprima;
using Esprima.Ast;
using Frank.Blazor.JsInteropGenerator.Internals.Js;

namespace Frank.Blazor.JsInteropGenerator.Internals.Walkers;

public class JsSyntaxWalker : IJsSyntaxWalker
{
    public JsFunctionDefinition[] GetFunctionDefinitions(string javascript)
    {
        var parser = new JavaScriptParser();
        // var script = parser.ParseScript(javascript);
        var script = parser.ParseModule(javascript);
        
        var functionDefinitions = new List<JsFunctionDefinition>();
        
        foreach (var node in script.Body)
        {
            if (node is FunctionDeclaration functionDeclaration)
            {
                var name = functionDeclaration.Id?.Name;
                var returnObject = functionDeclaration.Body.Body.OfType<ReturnStatement>().FirstOrDefault()?.Argument?.Type switch
                {
                    Nodes.ObjectExpression => true,
                    _ => false
                };
                
                var returnType = returnObject ? new JsFunctionReturnType("object", JsType.Object) : (JsFunctionReturnType?)null;
                
                var arguments = new List<JsFunctionArgument>();

                foreach (var param in functionDeclaration.Params)
                {
                    if (param is Identifier identifier)
                    {
                        arguments.Add(new JsFunctionArgument(identifier.Name, "object"));
                    }
                }
                
                functionDefinitions.Add(new JsFunctionDefinition(name, returnType, arguments.ToArray()));
            }
        }
        
        return functionDefinitions.ToArray();
    }
}