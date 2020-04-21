using Clang;
using Xunit.Abstractions;

namespace XUnit.Antlr4
{
    public class AntlrTest : TestBase
    {
        protected readonly AntlrTestHelper AntlrHelper;

        public AntlrTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            AntlrHelper = new AntlrTestHelper(outputHelper);
        }

        protected ClangParser Prepare(string s) => AntlrHelper.CreateParser(AntlrHelper.CreateStream(s));
    }
}