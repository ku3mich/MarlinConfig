using Xunit;
using Xunit.Abstractions;
using XUnit.Antlr4;

namespace Clang.Tests
{
    public class FragmetsTests : AntlrTest
    {
        
        public FragmetsTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [FileContents("samples/Marlin/v1/Configuration.h")]
        [FileContents("samples/Marlin/v1/Configuration_adv.h")]
        [FileContents("samples/Marlin/v2/Configuration.h")]
        [FileContents("samples/Marlin/v2/Configuration_adv.h")]
        [FileContents("samples/ifndef.h")]
        [FileContents("samples/if.h")]
        [FileContents("samples/hello-world.c")]
        [FileContents("samples/undef.h")]
        [FileContents("samples/if-else.h")]
        public void Parse(string text)
        {
            var p = Prepare(text);
            p.file();
        }
    }
}