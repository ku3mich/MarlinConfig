using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Include;

namespace MarlinConfig.Tests;

public class ReaderTests(ITestOutputHelper helper) : TestBase(helper)
{
    [Mark(As.Injected)]
    public ConfigurationReader Reader { get; set; }

    [Test]
    [ContentsOfFile("samples/Configuration.h")]
    [ContentsOfFile("samples/Configuration_adv.h")]
    public void Configuration(ContentsOfFile sample)
    {
        var config = Reader.Read(sample);
        var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true});
        Logger.Write(json);
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ConfigurationReader>();
    }
}
