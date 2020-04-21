using Xunit.Abstractions;

namespace Clang.Tests
{
    public class TestBase
    {
        protected readonly ITestOutputHelper OutputHelper;

        public TestBase(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }
    }
}