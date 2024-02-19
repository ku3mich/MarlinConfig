using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace MarlinConfig.Tests;

public class ConfigTestBase(ITestOutputHelper helper) : TestBase(helper)
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ConfigurationReader>();
    }
}
