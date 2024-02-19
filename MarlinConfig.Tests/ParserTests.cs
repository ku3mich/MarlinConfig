using Xunit.Abstractions;
using Xunit.Include;

namespace MarlinConfig.Tests;

public class ParserTests(ITestOutputHelper helper) : ParserTestBase(helper)
{
    [Test]
    [InlineData("\r")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    [InlineData("\n\r")]

    public void Eol(string sample) => Helper.CreateParser(sample).eol();

    [Test]
    [InlineData("\"\"")]
    [InlineData("\"a\"")]
    [InlineData("\" a\\\" \"")]
    [InlineData("\" a' \"")]
    public void DqString(string sample) => Helper.CreateParser(sample).dq_string();

    [Test]
    [InlineData("''")]
    [InlineData("'a'")]
    [InlineData("'a\\''")]
    [InlineData("'a\"'")]
    public void SqString(string sample) => Helper.CreateParser(sample).sq_string();


    [Test]
    [InlineData("#include <asd>")]
    [InlineData("#include <asd/f.h>")]
    [InlineData("#pragma once")]
    [InlineData("#define A")]
    [InlineData("#define A B")]
    [InlineData("#define CONFIGURATION_ADV_H_VERSION 02010201\r\n")]
    public void Directive(string sample)
    {
        Helper.OutputStreamTokens(sample);
        Helper.CreateParser(sample).simple_directive();
    }

    [Test]
    [ContentsOfFile("samples/long-define.h")]
    public void Define(ContentsOfFile sample)
    {
        Helper.OutputStreamTokens(sample);
        var v = Helper.CreateParser(sample).simple_directive().directive_define();
        Assert.Equal("MMU2_LOAD_TO_NOZZLE_SEQUENCE", v.Symbol);
        var l = "\\\n{  7.2, 1145 }";
        Assert.Equal(l, v.Value);
    }

    [Test]
    [InlineData("{ }")]
    [InlineData("{/*asd*/}")]
    [InlineData("{\n#define A\n}")]
    [InlineData("{\n}")]
    [InlineData("{{}}")]
    public void Blocks(string sample)
    {
        Helper.OutputStreamTokens(sample);
        Helper.CreateParser(sample).block();
    }

    [Test]
    [ContentsOfFile("samples/Configuration.h")]
    [ContentsOfFile("samples/Configuration_adv.h")]
    [ContentsOfFile("samples/header-comment.h")]
    [ContentsOfFile("samples/single-line-comments.h")]
    [ContentsOfFile("samples/motherboard.h")]
    [ContentsOfFile("samples/header-comment.h")]
    [ContentsOfFile("samples/conditional-define.h")]
    [ContentsOfFile("samples/conditional-expr.h")]
    [ContentsOfFile("samples/conditional-complex.h")]
    public void Configuration(ContentsOfFile sample)
    {
        Helper.OutputStreamTokens(sample);
        Helper.CreateParser(sample).file();
    }

    [Test]
    [InlineData("/**/")]
    [InlineData("/* asd */")]
    [InlineData("/* asd *//* two */")]
    [InlineData("/* asd *//* two */")]
    [InlineData("/* asd \n* */")]
    public void Comments(string sample)
    {
        Helper.OutputStreamTokens(sample);
        var result = Helper.CreateParser(sample).multi_comment();
    }

    [Test]
    [InlineData("#ifndef A\n#endif")]
    [InlineData("#ifndef A\n//comment\n#endif")]
    [InlineData("#ifndef A\n#define A\n#endif")]
    [InlineData("#ifndef A\n#define A B\n#endif")]
    [InlineData("#if A\n#endif")]
    public void Conditional(string sample)
    {
        Helper.OutputStreamTokens(sample);
        var result = Helper.CreateParser(sample).conditional();
    }

    [Test]
    [InlineData("// #define A B")]
    [InlineData("//#define A B")]
    [InlineData("/*#define A B*/")]
    public void Commented(string sample)
    {
        Helper.OutputStreamTokens(sample);
        var result = Helper.CreateParser(sample).file();
    }
}
