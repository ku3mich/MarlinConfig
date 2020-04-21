using Xunit;
using Xunit.Abstractions;

namespace Clang.Tests
{
    public class ClangTests : AntlrTest
    {
        public ClangTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Empty_File()
        {
            Prepare("").file();
        }

        [Fact]
        public void Define_identifier()
        {
            var parser = Prepare("#define a q");
            var f = parser.define();
            Assert.Equal("a", f.Identifier);
            Assert.Equal("q", f.Value);
        }

        [Fact]
        public void Define_Simple()
        {
            var parser = Prepare("#define a");
            var f = parser.define();
            Assert.Equal("a", f.Identifier);
            Assert.Null(f.Value);
        }

        [Fact]
        public void Define_number_int()
        {
            var parser = Prepare("#define a 1");
            var f = parser.define();
            Assert.Equal("a", f.Identifier);
            Assert.Equal("1", f.Value);
        }

        [Fact]
        public void Define_char()
        {
            var parser = Prepare("#define a 'a'");
            var f = parser.define();
            Assert.Equal("a", f.Identifier);
            Assert.Equal("'a'", f.Value);
        }

        [Fact]
        public void Define_string()
        {
            var parser = Prepare("#define a \"asd\"");
            var f = parser.define();
            Assert.Equal("a", f.Identifier);
            Assert.Equal("\"asd\"", f.Value);
        }

        [Fact]
        public void Define_number_float()
        {
            var parser = Prepare("#define a 1.");
            var f = parser.define();
            Assert.Equal("a", f.Identifier);
            Assert.Equal("1.", f.Value);
        }

        [Fact]
        public void Define_number_float2()
        {
            var parser = Prepare("#define a 1.1");
            var f = parser.define();
            Assert.Equal("a", f.Identifier);
            Assert.Equal("1.1", f.Value);
        }



        [Fact]
        public void Include()
        {
            var i = Prepare("#include <io/std.h>").include();
            Assert.False(i.isLocal);
            Assert.Equal("io/std.h", i.fname);
        }

        [Fact]
        public void Include_Quoted()
        {
            var i = Prepare("#include \"io/std.h\"").include();
            Assert.True(i.isLocal);
            Assert.Equal("io/std.h", i.fname);
        }


        [Fact]
        public void SingleQuotedString()
        {
            Prepare("'asd\\'fgh'");
        }

        [Fact]
        public void SingleLineComment()
        {
            var parser = Prepare("// oops");
            var c = parser.comment();

            Assert.Equal(" oops", c.body);
        }

        [Fact]
        public void ifdef()
        {
            var parser = Prepare("#ifdef a\n#define a\n#endif");
            var c = parser.ifdef();
        }

        [Fact]
        public void EmptySingleComment()
        {
            var parser = Prepare("//");
            var c = parser.comment();

            Assert.True(string.IsNullOrEmpty(c.body));
        }

        [Fact]
        public void CommentedSingleDefine()
        {
            var c = Prepare("// #define A").comment();
            Assert.Equal(" #define A", c.body);
        }

        [Fact]
        public void MultiLineComment()
        {
            var c = Prepare("/*1oops\ntwo*/").comment();
            Assert.Equal("1oops\ntwo", c.body);
        }

        [Fact]
        public void MultiLineComment2()
        {
            var c = Prepare("/*1oops\ntwo*/\n/** oops*/").file();

        }

        [Fact]
        public void DefineArray()
        {
            Prepare("#define SWITCHING_EXTRUDER_SERVO_ANGLES { 0, 90 } // Angles for E0, E1[, E2, E3]").file();
        }

        [Fact]
        public void DefineStringComment()
        {
            Prepare("#define STRING_CONFIG_H_AUTHOR \"(none, default config)\" // Who made the changes.").file();
        }

        [Fact]
        public void DefineExpressionParen()
        {
            Prepare("#define DELTA_RADIUS (DELTA_SMOOTH_ROD_OFFSET-(DELTA_EFFECTOR_OFFSET)-(DELTA_CARRIAGE_OFFSET))").file();
        }

        [Fact]
        public void DefineExpressionParen1()
        {
            Prepare("#define DELTA_RADIUS (1 - (1+1))").file();
        }
        [Fact]
        public void DefineFunc()
        {
        }

        // #define TMC_ADV() {  }
        [Fact]
        public void IfExpr()
        {
            var c = Prepare(@"
#if EXTRUDERS > 3
    #define SWITCHING_EXTRUDER_E23_SERVO_NR 1
  #endif").file();
        }
    }
}