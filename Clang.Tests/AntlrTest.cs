using Clang;
using Xunit.Abstractions;

namespace Clang.Tests
{
    /*
    public class Collector : ClangParserBaseListener
    {
        public Collector()
        {

        }

        public override void ExitDefine([NotNull] ClangParser.DefineContext context)
        {
            
        }
    }
    */

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