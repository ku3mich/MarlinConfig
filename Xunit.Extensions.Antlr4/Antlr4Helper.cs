using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Xunit.Extensions.DependencyInjection;

namespace Xunit.Extensions.Antlr4
{
    public class Antlr4Helper<T>(
        ILexerFactory lexerFactory,
        IParserFactory parserFactory,
        DebugListener debugListener,
        IVocabulary vocabulary,
        ITestLogger logger) where T : Parser
    {
        public CommonTokenStream CreateStream(string text) =>
            new CommonTokenStream(lexerFactory.Create(new AntlrInputStream(text)));

        public T CreateParser(ITokenStream s)
        {
            var parser = (T)parserFactory.Create(s);

            // parser.Interpreter.PredictionMode = PredictionMode.Ll;
            /*
            parser.Interpreter.PredictionMode = PredictionMode.Ll;
            parser.Interpreter.PredictionMode = PredictionMode.Sll;
            */

            parser.AddParseListener(debugListener);
            parser.AddErrorListener(new SyntaxErrorThrower());
            return parser;
        }

        public T CreateParser(string s) => CreateParser(CreateStream(s));

        public void OutputStreamTokens(string s)
        {
            var stream = CreateStream(s);
            stream.Fill();

            var tokens = stream.GetTokens()
                .Select(s => new
                {
                    s.Line,
                    Pos = s.Column,
                    Token = vocabulary.GetDisplayName(s.Type)
                });

            string LF(string lf) => lf == "LF" ? "\n" : "";

            var tokenStream = string.Join(" -> ", tokens.Select(s => $"{s.Token}{LF(s.Token)}"));
            logger.Write($"{tokenStream}\n");
        }
    }
}