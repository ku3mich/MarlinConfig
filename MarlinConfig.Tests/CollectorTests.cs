using Clang;
using Xunit;
using Xunit.Abstractions;
using XUnit.Antlr4;
using XUnit.Core;

namespace MarlinConfig.Tests
{
    public class CollectorTests : AntlrTest
    {
        public CollectorTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Theory]
        [FileContents("samples/simpleDefine.h")]
        public void T(string text)
        {
            var config = new Configuration();
            var collector = new Collector(config);
            var stream = ClangHelper.CreateTokenStream(text);
            var parser = new ClangParser(stream);
            parser.AddErrorListener(new SyntaxErrorThrower());
            parser.AddParseListener(collector);
            parser.file();

            Assert.Equal(3, config.Defines.Count);
        }
    }
}