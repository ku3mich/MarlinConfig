global using Xunit;
global using Marks.Annotations;
global using Xunit.Extensions.DependencyInjection;
global using ITestLogger = Xunit.Extensions.DependencyInjection.ITestLogger;

[assembly: TestFramework("Xunit.Extensions.DependencyInjection.DependencyInjectionTestFramework", "Xunit.Extensions.DependencyInjection")]
