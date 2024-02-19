using Antlr4.Runtime;
using MarlinConfig.Parser;
using Xunit.Extensions.Antlr4;

namespace MarlinConfig.Tests;

public class LexerFactory : ILexerFactory
{
    public Lexer Create(ICharStream stream) => new MarlinConfigLexer(stream);
}
