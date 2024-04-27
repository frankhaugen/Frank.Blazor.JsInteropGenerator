using System.Text;
using Frank.Blazor.JsInteropGenerator.Internals.Js;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Frank.Blazor.JsInteropGenerator.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _outputHelper;

    public UnitTest1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public async Task TestGeneration5()
    {
        // var javascript = @"
        // function testFunction() {
        //     return 'Hello World';
        // }
        // ";
        var path = "https://cdn.jsdelivr.net/npm/mermaid@10.9.0/dist/mermaid.min.js";
        var download = await new HttpClient().GetByteArrayAsync(path);
        var javascript = Encoding.UTF8.GetString(download);

        var result = JsIdentifier.Identify(javascript);
        
        _outputHelper.WriteLine(result.ToString());
    }

    [Fact]
    public void TestGeneration()
    {
        var generator = GetGenerator();
        
        var javascript = @"
        function testFunction() {
            return 'Hello World';
        }
        ";
        
        var result = generator.Generate(javascript);
        
        _outputHelper.WriteLine(result.NormalizeWhitespace().ToFullString());
    }

    [Fact]
    public async Task TestGenerationAsync()
    {
        var generator = GetGenerator();
        var path = "https://cdn.jsdelivr.net/npm/mermaid@10.9.0/dist/mermaid.min.js";
        var download =await new HttpClient().GetByteArrayAsync(path);
        var javascript = Encoding.UTF8.GetString(download);
        
        var result = generator.Generate(javascript);
        
        _outputHelper.WriteLine(result.NormalizeWhitespace().ToFullString());
    }
    
    
    private IJsInteropGenerator GetGenerator()
    {
        var services = new ServiceCollection();
        services.AddJsInteropGenerator();
        var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions() {ValidateOnBuild = true, ValidateScopes = true});
        return serviceProvider.GetRequiredService<IJsInteropGenerator>();
    }
}