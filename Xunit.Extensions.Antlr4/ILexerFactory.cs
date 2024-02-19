using Antlr4.Runtime;

namespace Xunit.Extensions.Antlr4;

public interface ILexerFactory
{
    Lexer Create(ICharStream stream);
}
