using Frank.Blazor.JsInteropGenerator.Internals.CodeGeneration;
using Frank.Blazor.JsInteropGenerator.Internals.Walkers;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.Blazor.JsInteropGenerator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJsInteropGenerator(this IServiceCollection services)
    {
        services.AddSingleton<ICSharpCodeGenerator, CSharpCodeGenerator>();
        services.AddSingleton<ICSharpNamespaceGenerator, CSharpNamespaceGenerator>();
        services.AddSingleton<ICSharpClassGenerator, CSharpClassGenerator>();
        services.AddSingleton<ICSharpMethodGenerator, CSharpMethodGenerator>();
        services.AddSingleton<ICSharpInterfaceGenerator, CSharpInterfaceGenerator>();

        services.AddSingleton<IJsInteropGenerator, JsInteropGenerator>();
        services.AddSingleton<IJsSyntaxWalker, JsSyntaxWalker>();

        return services;
    }
}