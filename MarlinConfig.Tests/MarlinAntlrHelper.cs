using Antlr4.Runtime;
using MarlinConfig.Parser;
using Xunit.Extensions.Antlr4;

namespace MarlinConfig.Tests;

public class MarlinAntlrHelper(ILexerFactory lexerFactory, IParserFactory parserFactory, DebugListener debugListener, IVocabulary vocabulary, ITestLogger logger)
    : Antlr4Helper<MarlinConfigParser>(lexerFactory, parserFactory, debugListener, vocabulary, logger)
{
}
