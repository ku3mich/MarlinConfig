using Antlr4.Runtime;
using System.Linq;
using Xunit.Abstractions;

namespace Clang.Tests
{
    public class AntlrTestHelper
    {
        private readonly ITestOutputHelper OutputHelper;

        public AntlrTestHelper(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        public ITokenStream CreateStream(string s)
        {
            OutputHelper.WriteLine(s);
            var stream = new AntlrInputStream(s);
            var lexer = new ClangLexer(stream);
            var q = new CommonTokenStream(lexer);
            q.Fill();
            OutputHelper.WriteLine(
                string.Join(" -> ",
                    q.GetTokens()
                    .Select(s => new { Line = s.Line, Pos = s.Column, Token = lexer.Vocabulary.GetDisplayName(s.Type) })
                    .Select(s => s.Token == "EOL" ? $"EOL@{s.Line}\n" : s.Token)));

            return q;

        }

        public ClangParser CreateParser(ITokenStream s)
        {
            var parser = new ClangParser(s);
            var l = new DebugListener(OutputHelper);
            parser.AddParseListener(l);
            parser.AddErrorListener(new ErrorThrower());
            return parser;
        }
    }
}