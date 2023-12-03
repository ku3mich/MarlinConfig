using Xunit;
using Xunit.Abstractions;
using Xunit.Include;
using XUnit.Antlr4;

namespace Clang.Tests
{
    public class FragmetsTests : AntlrTest
    {

        public FragmetsTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [ContentsOfFile("samples/Marlin/v2/Configuration.h")]
        [ContentsOfFile("samples/Marlin/v2/Configuration_adv.h")]
        [ContentsOfFile("samples/ifndef.h")]
        [ContentsOfFile("samples/if.h")]
        [ContentsOfFile("samples/hello-world.c")]
        [ContentsOfFile("samples/undef.h")]
        [ContentsOfFile("samples/if-else.h")]
        public void Parse(ContentsOfFile file)
        {
            var p = Prepare(file);
            p.file();
        }
    }
}