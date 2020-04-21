using Xunit.Abstractions;

namespace XUnit.Antlr4
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