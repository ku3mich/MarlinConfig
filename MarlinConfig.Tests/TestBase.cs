using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace MarlinConfig.Tests;

#pragma warning disable CS9113 // Parameter is unread.
public abstract class TestBase(ITestOutputHelper helper) : IConfigureServices
#pragma warning restore CS9113 // Parameter is unread.
{
    [Mark(As.Injected)]
    [NotNull]
    public ITestLogger? Logger { get; set; }

    public abstract void ConfigureServices(IServiceCollection services);
}
