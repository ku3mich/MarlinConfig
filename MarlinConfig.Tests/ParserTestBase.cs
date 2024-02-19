using MarlinConfig.Parser;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Extensions.Antlr4;

namespace MarlinConfig.Tests;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class ParserTestBase(ITestOutputHelper helper) : TestBase(helper)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
{
    [Mark(As.Injected)]
    public MarlinAntlrHelper Helper { get; set; }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILexerFactory, LexerFactory>();
        services.AddSingleton<IParserFactory, ParserFactory>();
        services.AddTransient<DebugListener>();
        services.AddTransient<MarlinAntlrHelper>();
        services.AddSingleton(s => MarlinConfigLexer.DefaultVocabulary);
    }
}
