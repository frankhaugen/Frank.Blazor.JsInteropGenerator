using Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;
using Frank.Blazor.JsInteropGenerator.Internals.Walkers;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.Blazor.JsInteropGenerator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJsInteropGenerator(this IServiceCollection services)
    {
        services.AddSingleton<IJsSyntaxWalker, JsSyntaxWalker>();
        services.AddSingleton<ICSharpMethodGenerator, CSharpMethodGenerator>();
        services.AddSingleton<ICSharpClassGenerator, CSharpClassGenerator>();
        services.AddSingleton<ICSharpNamespaceGenerator, CSharpNamespaceGenerator>();
        services.AddSingleton<ICSharpCodeGenerator, CSharpCodeGenerator>();
        services.AddSingleton<IJsInteropGenerator, JsInteropGenerator>();

        return services;
    }
}